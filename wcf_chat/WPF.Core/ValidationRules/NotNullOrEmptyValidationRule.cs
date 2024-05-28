using System.Globalization;
using System.Windows.Controls;

namespace WPF.Core
{
	public class NotNullOrEmptyValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (value is null || string.IsNullOrWhiteSpace(value.ToString()))
			{
				return new ValidationResult(false, "Field can't be empty"); 
			}

			return ValidationResult.ValidResult;
		}
	}
}