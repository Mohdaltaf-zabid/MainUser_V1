using Firebase.Database;
using Firebase.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MainUser
{
    public class MedicineRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://alzheimerapp-4f12d-default-rtdb.firebaseio.com");
        FirebaseStorage firebaseStorage = new FirebaseStorage("alzheimerapp-4f12d.appspot.com");

        public async Task<bool> Save(MedicineModel medicineModel)
        {
            var data = await firebaseClient.Child(nameof(MedicineModel)).PostAsync(JsonConvert.SerializeObject(medicineModel));

            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }
        public async Task<string>upload(Stream img, string filename)
        {
            var image = await firebaseStorage.Child("Images").Child(filename).PutAsync(img);
            return image;
        }
    }
}
