using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Client;
using Domain.BillPosition;
using Domain.Item;

namespace Domain.Bill
{
    public class DomainBill
    {
        private readonly uint id;
        public uint Id { get { return id; } }

        private readonly DomainClient client;
        public DomainClient Client { get { return client; } }

        public decimal Total
        {
            get
            {
                decimal total = 0.0m;
                foreach (DomainBillPosition position in positions)
                    total += position.Total;
                return total;
            }
        }

        private List<DomainBillPosition> positions = new List<DomainBillPosition>();

        public DomainBill(uint id, DomainClient client)
        {
            this.id = id;
            this.client = client;
        }

        public void UpdatePosition(DomainItem item, uint count)
        {
            DomainBillPosition position = positions.Find(o => o.Item.Id == item.Id);
            if (position != null)
                position.Add(count);
            else
                positions.Add(new DomainBillPosition(0, item, count));
        }

        public void UpdatePosition(DomainBillPosition other)
        {
            DomainBillPosition position = positions.Find(o => o.Id == other.Id);
            if (position != null)
                position.Update(other);
            else
                positions.Add(other);
        }

        public void RemovePosition(DomainItem item, uint count)
        {
            DomainBillPosition position = positions.Find(o => o.Item.Id == item.Id);
            if (position != null)
                position.Remove(count);
        }

        public bool RemovePosition(DomainBillPosition other)
        {
            DomainBillPosition position = positions.Find(o => o.Id == other.Id);
            return (position != null) ? positions.Remove(position) : false;
        }

        public void Issue()
        {
            client.BankingRequest(Total);
        }

    }
}
