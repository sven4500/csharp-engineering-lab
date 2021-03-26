using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Item;
using Domain.Client;
using Domain.Bill;

namespace Domain.Cart
{
    public class DomainCart
    {
        private readonly uint id;
        public uint Id { get { return id; } }

        private readonly DomainClient client;
        public DomainClient Client { get { return client; } }

        private readonly DomainBill bill;
        public DomainBill Bill { get { return bill; } }

        public DomainCart(uint id, DomainClient client)
        {
            this.id = id;
            this.client = client;
            this.bill = new DomainBill(0, client);
        }

        public void Add(DomainItem item, uint count = 1)
        {
            bill.UpdatePosition(item, count);
        }

        public void Remove(DomainItem item, uint count = 1)
        {
            bill.UpdatePosition(item, count);
        }
    }
}
