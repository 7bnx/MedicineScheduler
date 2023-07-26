using MedicineScheduler.Common;
using MedicineScheduler.ServiceLayer.DTO;
using MedicineScheduler.ServiceLayer.Services;
using System;
using System.Collections.ObjectModel;

namespace MedicineScheduler.WPFUI.Stores;

public class PrescriptionStore
{
  public event Action? OnPrescriptionsChanged;
  public event Action<PrescriptionDTO>? OnAddedPrescription;
  public event Action<PrescriptionDTO>? OnUpdatedPrescription;
  public event Action<PrescriptionDTO>? OnRemovePrescription;

  private readonly IPrescriptionService _prescriptionService;
  private readonly ActivePatientLocationStore _activePatientLocationStore;
  private readonly MedicineStore _medicineStore;
  private readonly DosageFormStore _dosageFormStore;

  public ReadOnlyObservableCollection<PrescriptionDTO> Prescriptions
  {
    get
    {
      if (_readOnlyPrescriptions is not null) return _readOnlyPrescriptions;
      _prescriptions ??= new(_prescriptions ??= new(_prescriptionService.GetByPatientId(_activePatientLocationStore.CurrentActive.PatientId).Value!));
      return _readOnlyPrescriptions = new(_prescriptions);
    }
  }
  private ObservableCollection<PrescriptionDTO> _prescriptions = null!;
  private ReadOnlyObservableCollection<PrescriptionDTO> _readOnlyPrescriptions = null!;


  public PrescriptionDTO CurrentSelected { get; set; } = null!;

  public PrescriptionStore
  (
    IPrescriptionService prescriptionService,
    ActivePatientLocationStore activePatientLocationStore,
    MedicineStore medicineStore,
    DosageFormStore dosageFormStore
  )
  {
    _prescriptionService = prescriptionService;
    _activePatientLocationStore = activePatientLocationStore;
    _medicineStore = medicineStore;
    _dosageFormStore = dosageFormStore;
    _activePatientLocationStore.OnCurrentActiveChanged += CurrentActiveChanged;
  }

  public OperationResult<IPrescriptionDTO> Remove(PrescriptionDTO prescription)
  {
    var operationResult = _prescriptionService.Remove(prescription);
    if (operationResult.IsOk)
    {
      _prescriptions.Remove(prescription);
      OnRemovePrescription?.Invoke(prescription);
    }
    return operationResult;
  }

  public OperationResult<PrescriptionDTO> Update(IPrescriptionDTO prescription)
  {
    return AddUpdate(prescription, (prescription) =>
    {
      var operationResult = _prescriptionService.Update(prescription);
      if (operationResult.IsOk && operationResult.Value is not null)
      {
        var index = _prescriptions.IndexOf(CurrentSelected);
        if (index != -1)
          _prescriptions[index] = operationResult.Value;
        OnUpdatedPrescription?.Invoke(operationResult.Value);

      }
      return operationResult;
    });
  }

  private void CurrentActiveChanged()
  {
    _prescriptions = new(_prescriptionService.GetByPatientId(_activePatientLocationStore.CurrentActive.PatientId).Value!);
    _readOnlyPrescriptions = new(_prescriptions);
    OnPrescriptionsChanged?.Invoke();
  }

  public OperationResult<PrescriptionDTO> Add(IPrescriptionDTO prescription)
  {
    return AddUpdate(prescription, (prescription) =>
    {
      var operationResult = _prescriptionService.Add(prescription);
      if (operationResult.IsOk && operationResult.Value is not null)
      {
        OnAddedPrescription?.Invoke(operationResult.Value);
        _prescriptions.Add(operationResult.Value);
      }
      return operationResult;
    });
  }

  private OperationResult<PrescriptionDTO> AddUpdate(IPrescriptionDTO prescription, Func<IPrescriptionDTO, OperationResult<PrescriptionDTO>> operation)
  {
    var medResult = _medicineStore.Add(prescription.MedicineTitle);
    if (!medResult.IsOk || medResult.Value is null)
      return new(medResult);

    var dosageFormResult = _dosageFormStore.Add(prescription.DosageFormType, prescription.DosageFormUnit, prescription.DosageFormValue);
    if (!dosageFormResult.IsOk || dosageFormResult.Value is null)
      return new(dosageFormResult);

    prescription.MedicineId = medResult.Value.MedicineId;
    prescription.DosageFormId = dosageFormResult.Value.DosageFormId;

    return operation(prescription);
  }
}
