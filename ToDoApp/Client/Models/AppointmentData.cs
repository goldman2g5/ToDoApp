using System.ComponentModel;

namespace ToDoApp.Client.Models
{
    public class AppointmentData : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string subject { get; set; }
        public string Subject
        {
            get { return subject; }
            set
            {
                this.subject = value;
                NotifyPropertyChanged("Subject");
            }
        }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public bool IsAllDay { get; set; }
        public string RecurrenceRule { get; set; }
        public string RecurrenceException { get; set; }
        public Nullable<int> RecurrenceID { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
