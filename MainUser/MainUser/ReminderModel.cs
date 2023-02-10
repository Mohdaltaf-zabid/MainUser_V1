using System;
using System.Collections.Generic;
using System.Text;

namespace MainUser
{
    public class ReminderModel
    {
        public string ID { get; set; }
        public string title { get; set; }
        public string notes { get; set; }
        public string priority { get; set; }
        public string repeat { get; set; }
        public string setDate { get; set; }
        public string setTime { get; set; }
    }
}
