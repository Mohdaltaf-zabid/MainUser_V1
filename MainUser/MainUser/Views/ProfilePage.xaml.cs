using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MainUser.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
			InitializeComponent ();
            //UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);
            // See the UserRecord reference doc for the contents of userRecord.
            //Console.WriteLine($"Successfully fetched user data: {userRecord.Uid}");
        }
	}
}