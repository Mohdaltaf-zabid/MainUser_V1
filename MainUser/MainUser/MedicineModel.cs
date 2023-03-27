using System;
using System.Collections.Generic;
using System.Text;

namespace MainUser
{
    public class MedicineModel
    {
        public string ID { get; set; }
        public string email { get; set; }
        public string med_Name { get; set; }
        public string med_Strength { get; set; }
        public string med_Unit { get; set; }
        public string med_Frequency { get; set; }
        public string med_TimesADay { get; set; }
        public string med_SetTime { get; set; }
        public DateTime med_StartDate { get; set; }
        public DateTime med_EndDate { get; set; }
        public string Image { get; set; }
    }
}
