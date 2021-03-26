using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Item.Repository.Interface;

namespace Domain.Item.Repository.Xml
{
    public class ItemRepositoryXml : ItemRepositoryInterface
    {
        private readonly string fileName;
        private List<DomainItem> items = new List<DomainItem>();

        public ItemRepositoryXml(string fileName)
        {
            this.fileName = fileName;
            ReadFile();
        }

        private void ReadFile()
        {
            // Здесь открываем Xml файл с возможными товарами. Для демнострации
            // задаём некоторые товары руками.
            Add(new DomainItem(0, "Капучино", 105.00m));
            Add(new DomainItem(1, "Латте", 115.00m));
            Add(new DomainItem(2, "Американо", 95.00m));
            Add(new DomainItem(3, "Эспрессо", 70.00m));
        }

        public DomainItem FindByName(string name)
        {
            return items.Find(o => o.Name == name);
        }

        public DomainItem FindById(uint id)
        {
            return items.Find(o => o.Id == id);
        }

        private bool Contains(DomainItem item)
        {
            return FindById(item.Id) != null;
        }

        public void Add(DomainItem item)
        {
            if (Contains(item) == false)
                items.Add(item);
        }

        public void Remove(DomainItem item)
        {
            if (Contains(item) == true)
                items.Remove(FindById(item.Id));
        }

        public void Update()
        { 
            // Пишем в файл элементы items.
        }

        public int Count()
        {
            return items.Count;
        }

    }
}
