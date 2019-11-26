using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class PersonData
    {
        /*public PersonData()
        {
            Name = "";
            ContactNumber = "";
            PersonalContactNumber = "";
            EmailAddress = "";
            SkypeAddress = "";
            Comment = "";
        }*/

        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public string PersonalContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string SkypeAddress { get; set; }
        public string Comment { get; set; }
    }
}
