using Firebase.Database;
using Firebase.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public async Task<string> upload(Stream img, string filename)
        {
            var image = await firebaseStorage.Child("Images").Child(filename).PutAsync(img);
            return image;
        }

        public async Task<List<MedicineModel>> GetMedicineByDate(string email, DateTime date)
        {
            return (await firebaseClient.Child(nameof(MedicineModel)).OnceAsync<MedicineModel>()).Select(item => new MedicineModel
            {
                email = item.Object.email,
                med_Day = item.Object.med_Day,
                med_EndDate = item.Object.med_EndDate,
                med_Frequency = item.Object.med_Frequency,
                med_Name = item.Object.med_Name,
                med_StartDate = item.Object.med_StartDate,
                med_Strength = item.Object.med_Strength,
                med_Unit = item.Object.med_Unit,
                med_status = item.Object.med_status,
                med_remark = item.Object.med_remark,
                med_statusTime = item.Object.med_statusTime,
                Image = item.Object.Image,
                ID = item.Key
            }).Where(a => a.email == email)
            .Where(a => a.med_Day == date).ToList();
        }

        public async Task<MedicineModel> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(MedicineModel) + "/" + id).OnceSingleAsync<MedicineModel>());
        }

        public async Task<bool> Update(MedicineModel medicineModel)
        {
            await firebaseClient.Child(nameof(MedicineModel) + "/" + medicineModel.ID).PutAsync(JsonConvert.SerializeObject(medicineModel));
            return true;
        }
        public async Task<bool> Delete(string ID)
        {
            await firebaseClient.Child(nameof(MedicineModel) + "/" + ID).DeleteAsync();
            return true;
        }
    }
}
