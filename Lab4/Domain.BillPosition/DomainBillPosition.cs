using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Item;

namespace Domain.BillPosition
{
    public class DomainBillPosition
    {
        private readonly uint id;
        public uint Id { get { return id; } }

        private readonly DomainItem item;
        public DomainItem Item { get { return item; } }

        public uint Count { get; private set; }

        public decimal Total
        {
            get
            {
                return item.Price * Count;
            }
        }

        public DomainBillPosition(uint id, DomainItem item, uint count = 1)
        {
            this.id = id;
            this.item = item;
            Count = count;
        }

        public void Add(uint count)
        {
            Count += count;
        }

        public void Remove(uint count)
        {
            Count -= count;
        }

        public void Update(DomainBillPosition other)
        {
            if (this.id == other.id)
                this.Count = other.Count;
        }
    }
}
