using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class PersonData
    {
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

        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public string PersonalContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string SkypeAddress { get; set; }
        public string Comment { get; set; }
        public bool IsBirthdaySoon 
        {
            get
            {
                var mothSpan = 12 * (DateOfBirth.Month < DateTime.Now.Month ? 1 : 0) + DateOfBirth.Month - DateTime.Now.Month;
                if (mothSpan == 0 || mothSpan == 1)
                    if(mothSpan * 30 + DateOfBirth.Day - DateTime.Now.Day < 14)
                        return true;
                return false;
            } 
        }
    }
}
