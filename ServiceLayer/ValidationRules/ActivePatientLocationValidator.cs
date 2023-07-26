using FluentValidation;
using MedicineScheduler.ServiceLayer.DTO;

namespace MedicineScheduler.ServiceLayer.ValidationRules;
public class ActivePatientLocationValidator : AbstractValidator<IActivePatientLocationDTO>
{
  public ActivePatientLocationValidator()
  {
    RuleFor(a => a.PatientName)
      .NotEmpty().WithMessage("Required")
      .MaximumLength(25).WithMessage("Must not exceed 25 characters");

    RuleFor(a => a.LocationName)
      .NotEmpty().WithMessage("Required")
      .MaximumLength(25).WithMessage("Must not exceed 25 characters");
  }
}
