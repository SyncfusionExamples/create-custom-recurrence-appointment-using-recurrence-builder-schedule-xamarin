using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace ScheduleXamarin
{
    public class SchedulerViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Meeting> meetings;
        public event PropertyChangedEventHandler PropertyChanged;
        public SchedulerViewModel()
        {
            this.Meetings = new ObservableCollection<Meeting>();
            this.AddAppointments();

        }
        public ObservableCollection<Meeting> Meetings
        {
            get
            {
                return this.meetings;
            }
            set
            {
                this.meetings = value;
                this.RaiseOnPropertyChanged("Meetings");
            }
        }
        private void AddAppointments()
        {
            Meeting meeting = new Meeting();
            meeting.From = new DateTime(2017, 06, 11, 10, 0, 0);
            meeting.To = meeting.From.AddHours(2);
            meeting.EventName = "Client Meeting";
            meeting.Color = Color.Green;
            meeting.IsRecursive = true;
            Meetings.Add(meeting);

            RecurrenceProperties recurrenceProperties = new RecurrenceProperties();
            recurrenceProperties.RecurrenceType = RecurrenceType.Weekly;
            recurrenceProperties.RecurrenceRange = RecurrenceRange.Count;
            recurrenceProperties.Interval = 1;
            recurrenceProperties.WeekDays = WeekDays.Monday | WeekDays.Wednesday | WeekDays.Friday;
            recurrenceProperties.RecurrenceCount = 10;
            recurrenceProperties.RecurrenceRule = DependencyService.Get<IRecurrenceBuilder>().RRuleGenerator(recurrenceProperties, meeting.From, meeting.To);

            // Setting the recursive rule for an event
            meeting.RecurrenceRule = recurrenceProperties.RecurrenceRule;
        }
        private void RaiseOnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

