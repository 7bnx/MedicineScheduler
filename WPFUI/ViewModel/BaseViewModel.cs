using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MedicineScheduler.WPFUI.ViewModel;

public class ViewModelBase : INotifyPropertyChanged, IDisposable
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual void Dispose() { }
}

public class ViewModelBaseWithValidation<T> : ViewModelBase, INotifyDataErrorInfo where T : class
{

    private readonly AbstractValidator<T> _validator;
    public ViewModelBaseWithValidation(AbstractValidator<T> validator)
      => _validator = validator;

    private readonly Dictionary<string, List<string>> _propertyErrors = new();

    public bool HasErrors => _propertyErrors.Any();

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        return _propertyErrors!.GetValueOrDefault(propertyName, new List<string>());
    }

    public void AddError(string propertyName, string errorMessage)
    {
        if (!_propertyErrors.ContainsKey(propertyName))
        {
            _propertyErrors.Add(propertyName, new List<string>());
        }

        _propertyErrors[propertyName].Add(errorMessage);
        OnErrorsChanged(propertyName);
    }

    public void ClearErrors()
    {
        _propertyErrors.Clear();
        OnErrorsChanged();
    }

    public void ClearErrors(string propertyName)
    {
        if (_propertyErrors.Remove(propertyName))
        {
            OnErrorsChanged(propertyName);
        }
    }

    private void OnErrorsChanged(string? propertyName = null)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    public void Validate()
    {
        ClearErrors();

        ValidationResult result = _validator.Validate((this as T)!);

        IEnumerable<ValidationFailure> errors = result.Errors.DistinctBy(e => e.PropertyName);

        foreach (ValidationFailure error in errors)
        {
            AddError(error.PropertyName, error.ErrorMessage);
        }
    }

    public void ValidateProperty([CallerMemberName] string propertyName = null!)
    {
        ClearErrors(propertyName);

        ValidationResult result = _validator.Validate((this as T)!, o => o.IncludeProperties(propertyName));

        if (result.Errors.Any())
        {
            string firstErrorMessage = result.Errors.First().ErrorMessage;

            AddError(propertyName, firstErrorMessage);
        }
    }
}
