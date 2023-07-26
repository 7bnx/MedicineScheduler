using MedicineScheduler.ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using MedicineScheduler.WPFUI.Stores;
using MedicineScheduler.ServiceLayer.ValidationRules;
using MedicineScheduler.Common.Enums;

namespace MedicineScheduler.WPFUI.ViewModel;

public abstract class PrescriptionBaseViewModel
  : ViewModelBaseWithValidation<IPrescriptionDTO>, IPrescriptionDTO
{
  private readonly MedicineStore _medicineStore;
  private DosageType _dosageFormType;
  private DateTime _startDate = DateTime.Now;
  private double _remainStorageQuantity;
  private int _remainStorageDays;
  private int _quantityServingsPerDay;
  private double _quantityPerServing;
  private DateTime _prescriptionDate = DateTime.Now;
  private string? _note;
  private string _medicineTitle = null!;
  private DateTime _endDate = DateTime.Now;
  private double _dosageFormValue;
  private DosageUnit _dosageFormUnit;

  public abstract string Title { get; }

  protected PrescriptionBaseViewModel
  (
    PrescriptionDTOValidator validator, 
    MedicineStore medicineStore
  ) : base(validator)
  {
    _medicineStore = medicineStore;
  }
  public abstract ICommand SubmitPrescriptionCommand { get; init; }
  public abstract ICommand CancelCommand { get; init; }
  public IEnumerable<MedicineDTO> Medicines => _medicineStore.Medicines;
  public DosageType DosageFormType
  {
    get => _dosageFormType;
    set
    {
      _dosageFormType = value;
      OnPropertyChanged();
      ValidateProperty();
    }
  }
  public DosageUnit DosageFormUnit
  {
    get => _dosageFormUnit;
    set
    {
      _dosageFormUnit = value;
      OnPropertyChanged();
      ValidateProperty();
    }
  }
  public double DosageFormValue
  {
    get => _dosageFormValue;
    set
    {
      _dosageFormValue = value;
      OnPropertyChanged();
      ValidateProperty();
    }
  }
  public DateTime EndDate
  {
    get => _endDate;
    set
    {
      _endDate = value;
      OnPropertyChanged();
      ValidateProperty();
    }
  }
  public string MedicineTitle
  {
    get => _medicineTitle;
    set
    {
      _medicineTitle = value;
      OnPropertyChanged();
      ValidateProperty();
    }
  }

  public int MedicineId { get; set; }
  public int DosageFormId { get; set; }
  public string? Note
  {
    get => _note;
    set
    {
      _note = value;
      OnPropertyChanged();
      ValidateProperty();
    }
  }

  public int PrescriptionId { get; set; }
  public int PatientId { get; set; }
  public DateTime PrescriptionDate
  {
    get => _prescriptionDate;
    set
    {
      _prescriptionDate = value;
      OnPropertyChanged();
      ValidateProperty();
    }
  }
  public double AmountMedication
  {
    get => _quantityPerServing;
    set
    {
      _quantityPerServing = value;
      OnPropertyChanged();
      ValidateProperty();
    }
  }
  public int MedicationsPerDay
  {
    get => _quantityServingsPerDay;
    set
    {
      _quantityServingsPerDay = value;
      OnPropertyChanged();
      ValidateProperty();
    }
  }
  public int RemainStorageDays
  {
    get => _remainStorageDays;
    set
    {
      _remainStorageDays = value;
      OnPropertyChanged();
      ValidateProperty();
    }
  }
  public double RemainStorageQuantity
  {
    get => _remainStorageQuantity;
    set
    {
      _remainStorageQuantity = value;
      OnPropertyChanged();
      ValidateProperty();
    }
  }
  public DateTime StartDate
  {
    get => _startDate;
    set
    {
      _startDate = value;
      OnPropertyChanged();
      ValidateProperty();
    }
  }

  public override void Dispose()
  {
    base.Dispose();
  }
}
