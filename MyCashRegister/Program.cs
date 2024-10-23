using MyCashRegister.Menus;
using System.Security.Cryptography.X509Certificates;

namespace MyCashRegister
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenu menu = new MainMenu();
            menu.Display();
        }
    }
}
