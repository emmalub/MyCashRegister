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
                int cartColumnPosition = 35; //nya för att testa snygg varukorg brevid lista

                Console.WriteLine();
                Console.SetCursorPosition(cartColumnPosition, 1); //nya för att testa snygg varukorg brevid lista
                Console.ForegroundColor= ConsoleColor.Green;
                Console.Write("Ange PLU-nummer och antal/mängd för att lägga till vara: ");
                Console.ResetColor();

                string input = Console.ReadLine();

                if (input.ToLower() == "pay")
                {
                    transaction.PrintReciept();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Återgår till huvudmenyn..\n");
                    Console.ResetColor();
                    Thread.Sleep(3000);
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
                        //int cartColumnPosition = 50; //nya för att testa snygg varukorg brevid lista

                        transaction.AddProduct(product, quantity);
                        Console.SetCursorPosition(cartColumnPosition, 1); //nya för att testa snygg varukorg brevid lista
                        Console.WriteLine($"Lagt till {quantity} x {product.Name} - {product.Price} kr.");
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 20);
                        Console.WriteLine("Produkten med angivet PLU-nummer hittades inte.");
                    }
                }
                else
                {
                    Console.SetCursorPosition(0, 20);
                    Console.WriteLine("Ogiltig inmatning, ange ett giltigt PLU-nummer eller 'pay' för att betala.");
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
    //public void SaveProducts()
    //{
    //    _productFileManager.SaveToFile("../../../Files/products.txt", _products);
    //}
    }
}


//Transaction transaction = new Transaction(receiptNumber);
//transaction.AddProduct(product, quantity);
//    transaction.PrintReceipt();


