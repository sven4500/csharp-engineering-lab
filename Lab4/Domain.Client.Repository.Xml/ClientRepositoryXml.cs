using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Client.Repository.Interface;

namespace Domain.Client.Repository.Xml
{
    public class ClientRepositoryXml : ClientRepositoryInterface
    {
        private readonly string fileName;

        private List<DomainClient> clients = new List<DomainClient>();

        public ClientRepositoryXml(string fileName)
        {
            this.fileName = fileName;
            ReadFile();
        }

        private void ReadFile()
        { 
            // Читаем файл. Заполняем список.
            Add(new DomainClient(0, "Иванов", "+79158635922", 15));
            Add(new DomainClient(1, "Петров", "+79645135584", 5));
            Add(new DomainClient(2, "Мария", "+79662514873", 7));
        }

        public DomainClient FindById(uint id)
        {
            return clients.Find(o => o.Id == id);
        }

        public DomainClient FindByName(string name)
        {
            return clients.Find(o => o.Name == name);
        }

        public DomainClient FindByPhone(string phone)
        {
            return clients.Find(o => o.Phone == phone);
        }

        private bool Contains(DomainClient client)
        {
            return FindById(client.Id) != null;
        }

        public void Add(DomainClient client)
        {
            if (Contains(client) == false)
                clients.Add(client);    
        }

        public void Remove(DomainClient client)
        {
            if (Contains(client) == true)
                clients.Remove(FindById(client.Id));
        }

        public int Count()
        {
            return clients.Count;
        }

        public void Update()
        { 
            // Записываем изменения в файл.
        }
    }
}
