using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WPF.Core
{
	public class RegexValidationRule : ValidationRule
	{
		public string RegexPattern { get; set; }
		public string ErrorMessage { get; set; }

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (Regex.IsMatch(value.ToString(), RegexPattern))
			{
				return ValidationResult.ValidResult; 
			}

			return new ValidationResult(false, ErrorMessage);
		}
	}
}
