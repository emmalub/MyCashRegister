using MyCashRegister.Campaigns;
using MyCashRegister.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace MyCashRegister.Menus
{
    public class AdminMenu : Menu
    {
        public override void Display()
        {
            PrintHeader("Adminvy");
            Console.WriteLine($@"| 1. Lägg till produkt                    |
| 2. Ta bort produkt                      |
| 3. Ändra produkt                        |
| 4. Lägg till kampanj                    |
| 5. Ta bort kampanj                      |
| 6. Ändra kampanj                        |
| 0. Tillbaka                             |
+-----------------------------------------+
");
            string input = GetUserInput();

            Product product = new Product();
            Campaign campaign = new Campaign();

            switch (input)
            {
                case "1":
                    product.Add();
                    break;

                case "2":
                    product.Remove();
                    break;

                case "3":
                    product.Edit();
                    break;

                case "4":
                    campaign.Add();
                    break;

                case "5":
                    campaign.Remove();
                    break;

                case "6":
                    campaign.Edit();
                    break;

                case "0":
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.Display();
                    break;

                default:
                    InvalidInputMessage();
                    break;
            }
        }
    }
}
    

