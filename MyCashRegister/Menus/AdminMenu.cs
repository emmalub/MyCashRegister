﻿using MyCashRegister.Managers;
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
            var validator = new InputValidator();

            bool running = true;
            while (running)
            {
                PrintHeader("Adminvy");
                Console.WriteLine($@"| 1. Lägg till produkt                    |
| 2. Ta bort produkt                      |
| 3. Ändra produkt                        |
| 0. Tillbaka                             |
+-----------------------------------------+
");
                string input = GetUserInput();

                Product product = new Product();

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

                    case "0":
                        running = false;
                        MainMenu mainMenu = new MainMenu();
                        mainMenu.Display();
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



