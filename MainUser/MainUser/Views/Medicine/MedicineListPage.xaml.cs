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
    public partial class MedicineListPage : ContentPage
    {
        public MedicineListPage()
        {
            InitializeComponent();
        }

        private void AddMedicineButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}