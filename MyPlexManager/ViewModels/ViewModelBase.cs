using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using MyPlexManager.Interfaces;

namespace MyPlexManager.ViewModels;


public class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo, IValidatable
{
	//https://stackoverflow.com/questions/41484994/c-sharp-uwp-async-binding

	public event PropertyChangedEventHandler? PropertyChanged;
	public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

	readonly Dictionary<string, List<ValidationResult>> _errors = new Dictionary<string, List<ValidationResult>>();

	protected bool SetProperty<T>(ref T originalValue, T newValue, [CallerMemberName] string propertyName = null!)
	{
		if (!EqualityComparer<T>.Default.Equals(originalValue, newValue))
		{
			originalValue = newValue;
			OnPropertyChanged(propertyName, newValue!);

			return true;
		}

		return false;
	}

	public bool HasErrors
	{
		get
		{
			return _errors.Any();
		}
	}

	private void OnPropertyChanged(string propertyName, object value)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		Validate(propertyName, value);
	}

	protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}


	private void AddErrors(string? propertyName, IEnumerable<ValidationResult> results)
	{
		if (!_errors.TryGetValue(propertyName!, out var errors))
		{
			errors = new List<ValidationResult>();
			_errors.Add(propertyName!, errors);
		}

		errors.AddRange(results);
		ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
	}

	public IEnumerable GetErrors(string? propertyName)
	{
		return _errors[propertyName!];
	}

	private void ClearErrors(string? propertyName)
	{
		if (_errors.TryGetValue(propertyName!, out var errors))
		{
			errors.Clear();
			ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
		}
	}

	public void Validate(string memberName, object value)
	{
		ClearErrors(memberName);
		List<ValidationResult> results = new List<ValidationResult>();
		bool result = Validator.TryValidateProperty(
			value,
			new ValidationContext(this, null, null)
			{
				MemberName = memberName
			},
			results
			);

		if (!result)
		{
			AddErrors(memberName, results);
		}
	}
}
