using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Entities
{
    public class ValidatableEntity
    {
        public List<ValidationResult> validate()
        {
            var context = new ValidationContext(this, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true);
            return results;
        }

        public List<ValidationResult> validate(IEnumerable<PropertyInfo> propeties, ValidationContext context)
        {
            var results = new List<ValidationResult>();
            foreach (var property in propeties)
            {
                var propertyValue = property.GetValue(context.ObjectInstance);
                var validationContextForProperty = new ValidationContext(context.ObjectInstance)
                {
                    MemberName = property.Name
                };

                Validator.TryValidateProperty(propertyValue, validationContextForProperty, results);
            }
            return results;
        }
    }
}
