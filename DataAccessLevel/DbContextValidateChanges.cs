using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace MedicineScheduler.DataAccessLayer;

public static class DbContextValidateChanges
{
  public static ImmutableList<ValidationResult> ExecuteValidation(this DbContext context)
  {
    var result = new List<ValidationResult>();
    foreach (var entry in context.ChangeTracker.Entries().Where(e =>
            (e.State == EntityState.Added) ||
            (e.State == EntityState.Modified)))
    {
      var entity = entry.Entity;
      var valProvider = new ValidationDbContextServiceProvider(context);
      var valContext = new ValidationContext(entity, valProvider, null);
      var entityErrors = new List<ValidationResult>();
      if (!Validator.TryValidateObject(entity, valContext, entityErrors, true))
        result.AddRange(entityErrors);
    }
    return result.ToImmutableList();

  }
}
