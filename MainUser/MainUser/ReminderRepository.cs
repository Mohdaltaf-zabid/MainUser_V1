using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MainUser
{
    public class ReminderRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://alzheimerapp-4f12d-default-rtdb.firebaseio.com");

        public async Task<bool> Save(ReminderModel reminder)
        {
            var data = await firebaseClient.Child(nameof(ReminderModel)).PostAsync(JsonConvert.SerializeObject(reminder));

            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<List<ReminderModel>> GetAll()
        {
            var UserEmail = Preferences.Get("userEmail", "");
            return (await firebaseClient.Child(nameof(ReminderModel)).OnceAsync<ReminderModel>()).Select(item => new ReminderModel
            {
                title = item.Object.title,
                notes = item.Object.notes,
                priority = item.Object.priority,
                repeat = item.Object.repeat,
                setDate = item.Object.setDate,
                setTime = item.Object.setTime,
                email = item.Object.email,
                ID = item.Key
            }).Where(a => a.email == UserEmail).ToList();
        }

        public async Task<ReminderModel> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(ReminderModel) + "/" + id).OnceSingleAsync<ReminderModel>());
        }

        public async Task<bool> Update(ReminderModel reminder)
        {
            await firebaseClient.Child(nameof(ReminderModel) + "/" + reminder.ID).PutAsync(JsonConvert.SerializeObject(reminder));
            return true;
        }

        public async Task<bool>Delete(string ID)
        {
            await firebaseClient.Child(nameof(ReminderModel)+ "/" + ID).DeleteAsync();
            return true;
        }
    }
}
