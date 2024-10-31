using MyCashRegister.Menus;
using MyCashRegister.Products;
using System.Security.Cryptography.X509Certificates;

namespace MyCashRegister
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "../../../Files/products.txt";
            ProductFileManager productFileManager = new ProductFileManager(filePath);
            List<Product> products = productFileManager.ReadProductsFromFile();

            MainMenu menu = new MainMenu();

            Console.SetCursorPosition(35, 10); //nytt
            menu.Display();
            Console.SetCursorPosition(35, 10); //nytt
        }
    }
}
