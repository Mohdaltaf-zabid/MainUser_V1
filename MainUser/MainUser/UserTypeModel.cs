using System;
using System.Collections.Generic;
using System.Text;

namespace MainUser
{
    public class UserTypeModel
    {
        public string ID { get; set; }
        public string email { get; set; }
        public string fullName { get; set; }
        public string userType { get; set; }
        public string status { get; set; }
        public string caretakerEmail { get; set; }
        public string caretakerName { get; set; }
        public string gender { get; set; }
        public DateTime birthdate { get; set; }
        public string profileImage { get; set; }
    }
}
