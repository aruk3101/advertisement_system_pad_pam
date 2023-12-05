using CommunityToolkit.Mvvm.ComponentModel;
using Projekt.Models.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        private AuthUtilities authUtilities;
        public DashboardViewModel(AuthUtilities authUtilities)
        {
            this.authUtilities = authUtilities;
        }
    }
}
