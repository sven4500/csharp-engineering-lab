using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Client
{
    public class DomainClient
    {
        private readonly uint id;
        public uint Id { get { return id; } }

        public string Name { get; private set; }

        public string Phone { get; private set; }

        public uint Visits { get; private set; }

        public DomainClient(uint id, string name, string phone, uint visits)
        {
            this.id = id;
            Name = name;
            Phone = phone;
            Visits = visits;
        }

        public bool BankingRequest(decimal total)
        { 
            // Запрос в банк на снятие.
            if (true)
                Visits++;
            return true;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangePhone(string phone)
        {
            Phone = phone;
        }

        public bool HappyVisit()
        {
            // Бесплатный кофе каждое 10 посещение.
            return Visits % 10 == 0;
        }
    }
}
