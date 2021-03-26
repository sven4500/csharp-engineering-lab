using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Client;

namespace Domain.Client.Repository.Interface
{
    public interface ClientRepositoryInterface
    {
        DomainClient FindById(uint id);

        DomainClient FindByName(string name);

        DomainClient FindByPhone(string phone);

        void Add(DomainClient client);

        void Remove(DomainClient client);

        int Count();

        void Update();

    }
}
