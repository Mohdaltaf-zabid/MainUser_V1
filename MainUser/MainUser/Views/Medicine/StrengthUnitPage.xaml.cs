using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MainUser.Views.Medicine
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StrengthUnitPage : ContentPage
    {
        public StrengthUnitPage()
        {
            InitializeComponent();
            Preferences.Set("strength", "");
            Preferences.Set("priority", "");
        }

        private async void ButtonSetStrengthUnit_Clicked(object sender, EventArgs e)
        {
            string strength = TxtStrength.Text;
            string unit = pickerUnit.SelectedItem as String;

            if (string.IsNullOrEmpty(strength))
            {
                await DisplayAlert("Warning", "Please enter Strength", "Cancel");
                return;
            }
            if (pickerUnit.SelectedIndex == -1)
            {
                await DisplayAlert("Warning", "Please enter Priority", "Cancel");
                return;
            }

            Preferences.Set("strength", strength);
            Preferences.Set("priority", unit);
            await Navigation.PopModalAsync();
        }
    }
}