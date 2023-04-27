using Firebase.Auth;
using Firebase.Database;
using Firebase.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MainUser
{
    public class UserTypeRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://alzheimerapp-4f12d-default-rtdb.firebaseio.com");
        FirebaseStorage firebaseStorage = new FirebaseStorage("alzheimerapp-4f12d.appspot.com");

        public async Task<bool> Save(UserTypeModel userTypeModel)
        {
            var data = await firebaseClient.Child(nameof(UserTypeModel)).PostAsync(JsonConvert.SerializeObject(userTypeModel));

            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<UserTypeModel> GetByEmail()
        {
            var UserEmail = Preferences.Get("userEmail", "");
            var allUser = await GetAllUserList();
            await firebaseClient
              .Child(nameof(UserTypeModel))
              .OnceAsync<UserTypeModel>();
            return allUser.Where(a => a.email == UserEmail).FirstOrDefault();
        }

        public async Task<List<UserTypeModel>> GetAllUserList()
        {
            return (await firebaseClient.Child(nameof(UserTypeModel)).OnceAsync<UserTypeModel>()).Select(item => new UserTypeModel
            {
                email = item.Object.email,
                userType = item.Object.userType,
                fullName = item.Object.fullName,
                status = item.Object.status,
                caretakerEmail = item.Object.caretakerEmail,
                caretakerName = item.Object.caretakerName,
                gender = item.Object.gender,
                birthdate = item.Object.birthdate,
                profileImage = item.Object.profileImage,
                ID = item.Key
            }).ToList();
        }

        public async Task<List<UserTypeModel>> GetPatientList()
        {
            var UserEmail = Preferences.Get("userEmail", "");
            return (await firebaseClient.Child(nameof(UserTypeModel)).OnceAsync<UserTypeModel>()).Select(item => new UserTypeModel
            {
                email = item.Object.email,
                userType = item.Object.userType,
                fullName = item.Object.fullName,
                status = item.Object.status,
                caretakerEmail = item.Object.caretakerEmail,
                caretakerName = item.Object.caretakerName,
                gender = item.Object.gender,
                birthdate = item.Object.birthdate,
                profileImage = item.Object.profileImage,
                ID = item.Key
            }).Where(a => a.userType == "Normal User/patient").
            Where(a => a.status != "Approved").ToList();
        }

        public async Task<List<UserTypeModel>> GetPatientApproveList()
        {
            var UserEmail = Preferences.Get("userEmail", "");
            return (await firebaseClient.Child(nameof(UserTypeModel)).OnceAsync<UserTypeModel>()).Select(item => new UserTypeModel
            {
                email = item.Object.email,
                userType = item.Object.userType,
                fullName = item.Object.fullName,
                status = item.Object.status,
                caretakerEmail = item.Object.caretakerEmail,
                caretakerName = item.Object.caretakerName,
                gender = item.Object.gender,
                birthdate = item.Object.birthdate,
                profileImage = item.Object.profileImage,
                ID = item.Key
            }).Where(a => a.userType == "Normal User/patient")
            .Where(a => a.caretakerEmail == UserEmail)
            .Where(a => a.status == "Approved").ToList();
        }

        public async Task<List<UserTypeModel>> GetPatientPendingList()
        {
            var UserEmail = Preferences.Get("userEmail", "");
            return (await firebaseClient.Child(nameof(UserTypeModel)).OnceAsync<UserTypeModel>()).Select(item => new UserTypeModel
            {
                email = item.Object.email,
                userType = item.Object.userType,
                fullName = item.Object.fullName,
                status = item.Object.status,
                caretakerEmail = item.Object.caretakerEmail,
                caretakerName = item.Object.caretakerName,
                gender = item.Object.gender,
                birthdate = item.Object.birthdate,
                profileImage = item.Object.profileImage,
                ID = item.Key
            }).Where(a => a.userType == "Normal User/patient")
            .Where(a => a.email == UserEmail)
            .Where(a => a.status == "Request").ToList();
        }

        public async Task<List<UserTypeModel>> GetUserList(string fullname)
        {
            var allUser = await GetPatientList();
            await firebaseClient
              .Child(nameof(UserTypeModel))
              .OnceAsync<UserTypeModel>();
            return allUser.Where(c => c.fullName.ToLower().Contains(fullname.ToLower())).ToList();
        }
        public async Task<bool> UpdateStatusPatient(UserTypeModel userTypeModel)
        {
            await firebaseClient.Child(nameof(UserTypeModel) + "/" + userTypeModel.ID).PutAsync(JsonConvert.SerializeObject(userTypeModel));
            return true;
        }

        public async Task<UserTypeModel> GetPatientyId(string id)
        {
            return (await firebaseClient.Child(nameof(UserTypeModel) + "/" + id).OnceSingleAsync<UserTypeModel>());
        }

        public async Task<bool> UpdatePatient(UserTypeModel userTypeModel)
        {
            await firebaseClient.Child(nameof(UserTypeModel) + "/" + userTypeModel.ID).PutAsync(JsonConvert.SerializeObject(userTypeModel));
            return true;
        }

        public async Task<bool> Update(UserTypeModel userTypeModel)
        {
            await firebaseClient.Child(nameof(UserTypeModel) + "/" + userTypeModel.ID).PutAsync(JsonConvert.SerializeObject(userTypeModel));
            return true;
        }

        public async Task<string> upload(Stream img, string filename)
        {
            var image = await firebaseStorage.Child("Images").Child(filename).PutAsync(img);
            return image;
        }
    }
}
