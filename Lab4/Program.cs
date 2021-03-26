using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Client;
using Domain.Client.Repository.Xml;
using Domain.Item;
using Domain.Item.Repository.Xml;
using Domain.Cart;

namespace Lab4
{
    // https://habr.com/ru/post/334126/
    // https://slides.silverfire.me/2017/eatdog-ddd/#/3
    // https://habr.com/ru/post/427739/
    class Program
    {
        static void Main(string[] args)
        {
            ItemRepositoryXml itemRepo = new ItemRepositoryXml("./MyCoffeeItems.xml");
            ClientRepositoryXml clientRepo = new ClientRepositoryXml("./MyClients.xml");
            DomainClient client = clientRepo.FindByName("Петров");
            DomainCart cart = new DomainCart(0, client);
            cart.Add(itemRepo.FindByName("Капучино"), 2);
            cart.Add(itemRepo.FindByName("Латте"), 1);
            cart.Bill.Issue();
            return;
        }
    }
}
