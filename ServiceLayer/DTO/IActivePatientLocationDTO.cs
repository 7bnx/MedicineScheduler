namespace MedicineScheduler.ServiceLayer.DTO;
public interface IActivePatientLocationDTO
{
  int PatientId { get; set; }
  int LocationId { get; set; }
  string PatientName { get; set; }
  string LocationName { get; set; }
}
