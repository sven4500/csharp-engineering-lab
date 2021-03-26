using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    class StaffRecord
    {
        public uint Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string ContactNumber { get; set; }
        public string Position { get; set; }
        public DateTime EmploymentDate { get; set; }

        public StaffRecord()
        {
            FirstName = "";
            MiddleName = "";
            LastName = "";
            ContactNumber = "";
            Position = "";
        }

    }
}
