using Firebase.Auth;
using Plugin.Media.Abstractions;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;
using System.IO;
using MainUser.Views.Reminder;

namespace MainUser.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        UserTypeRepository userTypeRepository = new UserTypeRepository();

        string status, caretakerEmail, caretakerName;
        MediaFile file;
        public ProfilePage()
        {
            bool haskey = Preferences.ContainsKey("token");
            if (haskey)
            {
                InitializeComponent();
                OnAppearing();
            }
        }
        protected override async void OnAppearing()
        {
            var profile = await userTypeRepository.GetByEmail();
            TxtID.Text = profile.ID;
            TxtFullName.Text = profile.fullName;
            TxtEmail.Text = profile.email;
            pickerUserType.SelectedItem = profile.userType;
            pickerGender.SelectedItem = profile.gender;
            TxtBirthDate.Date = profile.birthdate;
            status = profile.status;
            caretakerEmail = profile.caretakerEmail;
            caretakerName = profile.caretakerName;
            if (profile.profileImage != null)
            {
                profileImage.Source = profile.profileImage;
            }
        }

        private async void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void LogoutTap_Tapped(object sender, EventArgs e)
        {
            //await Navigation.PopToRootAsync();
            Preferences.Clear();
            await Navigation.PushModalAsync(new LoginPage());
        }

        private async void btmRequestCaretaker_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RequestCaretakerPage());
        }

        private async void cameraIcon_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                /*file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                //{
                //    Directory = "Test",
                //    SaveToAlbum = true,
                //    CompressionQuality = 75,
                //    CustomPhotoSize = 50,
                //    PhotoSize = PhotoSize.MaxWidthHeight,
                //    MaxWidthHeight = 2000,
                //    DefaultCamera = CameraDevice.Rear
                //});

                //if (file == null)
                //{
                //    return;
                //}
                //medicineImage.Source = ImageSource.FromStream(() =>
                //{
                //    return file.GetStream();

                });*/

                var cameraMediaOptions = new StoreCameraMediaOptions
                {
                    DefaultCamera = CameraDevice.Rear,

                    // Set the value to true if you want to save the photo to your public storage.
                    SaveToAlbum = true,

                    // Give the name of the folder you want to save to
                    Directory = "MyAppName",

                    // Give a photo name of your choice,
                    // or set it to null if you want to use the default naming convention
                    Name = null,

                    // Set the compression quality
                    // 0 = Maximum compression but worse quality
                    // 100 = Minimum compression but best quality
                    CompressionQuality = 100,

                    CustomPhotoSize = 100,

                };
                file = await CrossMedia.Current.TakePhotoAsync(cameraMediaOptions);
                if (file == null) return;
                profileImage.Source = ImageSource.FromStream(() => file.GetStream());

                /*var result = await MediaPicker.CapturePhotoAsync();
                //if (result != null)
                //{
                //    var stream = await result.OpenReadAsync();
                //    medicineImage.Source = ImageSource.FromStream(() => stream);
                }*/
            }
            catch (Exception ex)
            {

            }
        }

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            string fullName = TxtFullName.Text;
            string email = TxtEmail.Text;
            string userType = pickerUserType.SelectedItem as String;
            string gender = pickerGender.SelectedItem as String;
            DateTime birthdate = TxtBirthDate.Date;

            UserTypeModel userTypeModel = new UserTypeModel();
            userTypeModel.ID = TxtID.Text;
            userTypeModel.fullName = fullName;
            userTypeModel.email = email;
            userTypeModel.gender = gender;
            userTypeModel.birthdate = birthdate;
            userTypeModel.userType = userType;
            userTypeModel.status = status;
            userTypeModel.caretakerEmail = caretakerEmail;
            userTypeModel.caretakerName = caretakerName;
            userTypeModel.email = Preferences.Get("userEmail", "default");

            if (file != null)
            {
                string imageProfile = await userTypeRepository.upload(file.GetStream(), Path.GetFileName(file.Path));
                userTypeModel.profileImage = imageProfile;
            }

            bool isUpdated = await userTypeRepository.Update(userTypeModel);

            if (isUpdated)
            {
                await DisplayAlert("Information", "Profile succussful edit", "Ok");
                //await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Update failed", "Ok");
                return;
            }

        }
    }
}