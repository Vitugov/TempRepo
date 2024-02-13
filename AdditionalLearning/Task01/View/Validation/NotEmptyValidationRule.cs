using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Task01.View.Validation
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || (value is string str && !string.IsNullOrWhiteSpace(str)))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Значение не может быть пустым");
            }
        }
    }
}
