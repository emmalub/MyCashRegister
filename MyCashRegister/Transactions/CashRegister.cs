using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MyCashRegister.Menus;
using MyCashRegister.Products;

namespace MyCashRegister.Transactions
{
    public class CashRegister
    {
        private List<Product> _products;
        private int _receiptNumber;
        private ProductFileManager _productFileManager;

        public CashRegister()
        {
            string filePath = "../../../Files/products.txt";
            ProductFileManager productFileManager = new ProductFileManager(filePath);
            _products = productFileManager.LoadFromFile(filePath);
            _receiptNumber = 1;
        }

        public void StartTransaction()
        {
            Transaction transaction = new Transaction(_receiptNumber);

            while (true)
            {
                int cartColumnPosition = 35;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(0, 18);
                Console.WriteLine("~Skriv PAY för att betala~");
                Console.ResetColor();

                Console.WriteLine();
                Console.SetCursorPosition(cartColumnPosition, 1);
                Console.ForegroundColor= ConsoleColor.Green;
                Console.Write("Ange PLU-nummer och antal/mängd för att lägga till vara: ");
                Console.ResetColor();

                string input = Console.ReadLine().Replace('.',',');

                if (input.ToLower() == "pay")
                {
                    transaction.PrintReciept();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Tryck ENTER för att återgå till huvudmenyn.");
                    Console.ResetColor();
                    Console.ReadLine();
                    MainMenu menu = new MainMenu();
                    menu.Display();
                    break;
                }

                var inputParts = input.Split(' ');
                if (inputParts.Length == 2 && int.TryParse(inputParts[0], out int plu)
                    && decimal.TryParse(inputParts[1], out decimal quantity))
                {
                    var product = _products.FirstOrDefault(p => p.PLU == plu);
                    if (product != null)
                    {
                        transaction.AddProduct(product, quantity);
                        Console.SetCursorPosition(cartColumnPosition, 1);
                        Console.WriteLine($"Lagt till {quantity} x {product.Name} - {product.Price} kr.");
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 20);
                        Console.WriteLine(
                            "Produkten med angivet PLU-nummer hittades inte.");
                    }
                }
                else
                {
                    Console.SetCursorPosition(0, 20);
                    Console.WriteLine(
                        "Ogiltig inmatning, ange ett giltigt PLU-nummer eller 'pay' för att betala.");
                }
            }
        }
        public void Start()
        {

            ProductFileManager productFileManager = new ProductFileManager("../../../Files/products.txt");
            List<Product> products = productFileManager.ReadProductsFromFile();

            ProductDisplay display = new ProductDisplay(productFileManager);

            display.DisplayProducts();
            StartTransaction();
        }
    }
}





