using MainUser.Views.Reminder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MainUser.Views.Medicine
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedicineEntry : ContentPage
    {
        public MedicineEntry()
        {
            InitializeComponent();
        }

        private void TxtStrengthUnit_Focused(object sender, FocusEventArgs e)
        {
            Navigation.PushModalAsync(new StrengthUnitPage());
        }

        private void TxtDoseTime_Focused(object sender, FocusEventArgs e)
        {
            Navigation.PushModalAsync(new DoseTimePage());
        }

        private void ButtonSaveMed_Clicked(object sender, EventArgs e)
        {

        }
    }
}