using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MainUser.Views.Reminder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReminderDetail : ContentPage
    {
        public ReminderDetail(ReminderModel reminder)
        {
            InitializeComponent();
            LabelTitle.Text = reminder.title;
            LabelNotes.Text = reminder.notes;
            LabelPriority.Text = reminder.priority;
            LabelRepeat.Text = reminder.repeat;
            //LabelSetDate.Text = reminder.setDate;
            //LabelSetTime.Text = reminder.setTime;
            LabelID.Text = reminder.ID;
        }
    }
}