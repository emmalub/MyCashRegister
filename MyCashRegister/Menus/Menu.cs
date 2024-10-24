using Microsoft.Win32;
using MyCashRegister.Managers;
using MyCashRegister.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCashRegister.Menus
{
    public abstract class Menu
    {
        protected void PrintHeader(string title)
        {
            Console.Clear();
            Console.WriteLine($@"+-----------------------------------------+
|          ~~~  Välkommen ~~~             |
|                                         |
+-----------------------------------------+");
        }

        protected string GetUserInput()
        {
            Console.Write("Gör ett val: ");
            return Console.ReadLine();
        }

        protected void InvalidInputMessage()
        {
            Console.WriteLine("Ogiltigt val, försök igen: a");
        }

        public abstract void Display();
    }
}
    //private CashRegister _cashRegister;

    //        public Menu(string productFilePath)
    //        {
    //            _cashRegister = new CashRegister();
    //        }

    //        public void DisplayMainMenu()
    //        {
    //            Console.Clear();
    //            Console.WriteLine($@"
    //+-----------------------------------------+
    //|          ~~~  Välkommen ~~~             |
    //|                                         |
    //+-----------------------------------------+
    //| 1. Ny kund                              |
    //| 2. Admin-vy                             |
    //| 0. Avsluta                              |
    //+-----------------------------------------+
    //");
    //            Console.Write("Gör ett val: ");
    //  string input = Console.ReadLine();

    //string productFilePath = "products.txt";
    //CashRegister register = new CashRegister();

    //    switch (input)
    //    {
    //        case "1":
    //            register.Start();
    //            break;
    //        case "2":
    //            DisplayAdminMenu();
    //            break;
    //        case "0":
    //            Console.WriteLine("Tack för idag!");
    //            break;

    //        default:
    //            Console.WriteLine("Ogiltigt val, försök igen.");
    //            break;
    //    }
    //}

    //        public void DisplayAdminMenu()
    //        {
    //            Console.Clear();
    //            Console.WriteLine($@"
    //+-----------------------------------------+
    //|           ~~~  Adminvy  ~~~             |
    //|                                         |
    //+-----------------------------------------+
    //| 1. Lägg till produkt                    |
    //| 2. Ta bort produkt                      |
    //| 3. Ändra produkt                        |
    //| 4. Lägg till kampanj                    |
    //| 5. Ta bort kampanj                      |
    //| 6. Ändra kampanj                        |
    //| 0. Tillbaka                             |
    //+-----------------------------------------+
    //");
    //            Console.Write("Gör ett val: ");
    //            string input = Console.ReadLine();

    //            Admin admin = new Admin();
    //            Product product = new Product();
    //            Campaign campaign = new Campaign();

    //            switch (input)
    //            {
    //                case "1":
    //                    product.Add();
    //                    break;

    //                case "2":
    //                    product.Remove();
    //                    break;

    //                case "3":
    //                    product.Edit();
    //                    break;

    //                case "4":
    //                    campaign.Add();
    //                    break;

    //                case "5":
    //                    campaign.Remove();
    //                    break;

    //                case "6":
    //                    campaign.Edit();
    //                    break;

    //                case "0":
    //                    DisplayMainMenu();
    //                    break;

    //                default:
    //                    Console.WriteLine("Ogiltigt val, försök igen.");
    //                    break;
    //            }
    //            DisplayAdminMenu();
    //        }


