using Microsoft.Win32;
using MyCashRegister.Managers;
using MyCashRegister.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCashRegister.Menus
{
    internal class MainMenu : Menu
    {
        public override void Display()
        {
            var validator = new InputValidator();

            bool running = true;

            while (running)
            {
                PrintHeader("~~ Välkommen ~~~");
                Console.WriteLine($@"| 1. Ny kund                              |
| 2. Admin-vy                             |
| 0. Avsluta                              |
+-----------------------------------------+
");
                string input = GetUserInput();


                switch (input)
                {
                    case "1":
                        CashRegister register = new CashRegister();
                        register.Start();
                        break;
                    case "2":
                        AdminMenu adminMenu = new AdminMenu();
                        adminMenu.Display();
                        break;
                    case "0":
                        Console.WriteLine("Tack för idag!");
                        running = false;
                        break;

                    default:
                        InputValidator.IsPoop(input);
                        InputValidator.InvalidInputMessage();
                        break;
                }
            }
        }
    }
}
