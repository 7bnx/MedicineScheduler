using FluentValidation;
using MedicineScheduler.ServiceLayer.DTO;
using System.Text.RegularExpressions;

namespace MedicineScheduler.ServiceLayer.ValidationRules;

public partial class PrescriptionDTOValidator : AbstractValidator<IPrescriptionDTO>
{
  public PrescriptionDTOValidator()
  {

    RuleFor(a => a.PatientId)
     .NotEmpty().WithMessage("Required")
     .NotEqual(0);

    RuleFor(a => a.MedicineTitle)
     .NotEmpty().WithMessage("Required")
     .MaximumLength(25).WithMessage("Must not exceed 25 characters");

    RuleFor(a => a.DosageFormType)
      .IsInEnum().WithMessage("Not valid value");

    RuleFor(a => a.DosageFormUnit)
      .IsInEnum().WithMessage("Not valid value");

    RuleFor(a => a.DosageFormValue)
      .Must(d => ValidateDouble().IsMatch($"{d}")).WithMessage("Allow 2 decimal places")
      .GreaterThan(0).WithMessage("Should be greater than 0")
      .Must(n => n <= 10000).WithMessage("Must no exceed 10000");

    RuleFor(a => a.StartDate)
      .Must((a, d) => a.EndDate >= d).WithMessage("Should be less than End Date");

    RuleFor(a => a.PrescriptionDate)
      .Must((a, d) => a.EndDate >= d).WithMessage("Should be less than End Date");

    RuleFor(a => a.AmountMedication)
      .Must(d => ValidateDouble().IsMatch($"{d}")).WithMessage("Allow 2 decimal places")
      .GreaterThan(0).WithMessage("Should be greater than 0")
      .Must(n => n <= 100).WithMessage("Must no exceed 100");


    RuleFor(a => a.MedicationsPerDay)
      .GreaterThan(0).WithMessage("Should be greater than 0")
      .Must(n => n <= 100).WithMessage("Must no exceed 100");

  }

  [GeneratedRegex(@"^(?:\d+|\d*\,\d{1,2})$")]
  private static partial Regex ValidateDouble();
}
