using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Item
{
    public class DomainItem
    {
        private readonly uint id;
        public uint Id { get { return id; } }

        private readonly string name;
        public string Name { get { return name; } }

        private readonly decimal price = 0.0m;
        public decimal Price { get { return price; } }

        public DomainItem(uint id, string name, decimal price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }
    }
}
