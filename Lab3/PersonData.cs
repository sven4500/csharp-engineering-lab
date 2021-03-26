using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; // INotifyPropertyChanged

namespace Lab3
{
    public class PersonData: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PersonData()
        {
            // Явно задаём значения так как при добавлении новой записи табица
            // не добавляет нулевую строку в поля.
            Name = "";
            ContactNumber = "";
            PersonalContactNumber = "";
            EmailAddress = "";
            SkypeAddress = "";
            Comment = "";
        }

        private DateTime dateOfBirth;
        public DateTime DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                // https://stackoverflow.com/questions/42455878/how-to-update-datagrid-using-mvvm
                dateOfBirth = value;
                OnPropertyChanged("IsBirthdaySoon");
            }
        }

        public bool IsBirthdaySoon
        {
            get
            {
                var mothSpan = 12 * (DateOfBirth.Month < DateTime.Now.Month ? 1 : 0) + DateOfBirth.Month - DateTime.Now.Month;
                if (mothSpan == 0 || mothSpan == 1)
                    if (mothSpan * 30 + DateOfBirth.Day - DateTime.Now.Day < 14)
                        return true;
                return false;
            }
        }

        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string PersonalContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string SkypeAddress { get; set; }
        public string Comment { get; set; }
    }
}
