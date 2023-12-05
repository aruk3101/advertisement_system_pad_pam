using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.ViewModels
{
    public partial class ValidatableViewModel : ObservableObject
    {
        [ObservableProperty]
        protected Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        protected void ResetErrors()
        {
            foreach(var error in Errors)
            {
                error.Value.Clear();
            }
            NotifyErrorsChanged();
        }

        protected void UpdateErorrs(List<ValidationResult> violations)
        {
            foreach (ValidationResult violation in violations)
            {
                var fieldName = violation.MemberNames.First() + "Entry";
                Errors[fieldName].Add(violation.ErrorMessage);
            }
            NotifyErrorsChanged();
        }

        protected void NotifyErrorsChanged()
        {
            var copy = Errors;
            Errors = null;
            Errors = copy;
        }
    }
}
