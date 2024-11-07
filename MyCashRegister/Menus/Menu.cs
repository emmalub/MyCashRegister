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

        public abstract void Display();
    }
}
    

