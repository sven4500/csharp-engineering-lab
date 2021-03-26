using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Item.Repository.Interface
{
    public interface ItemRepositoryInterface
    {
        DomainItem FindById(uint id);

        DomainItem FindByName(string name);

        void Add(DomainItem item);

        void Remove(DomainItem item);

        int Count();

        void Update();

    }
}
