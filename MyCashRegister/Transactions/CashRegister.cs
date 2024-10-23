using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MyCashRegister.Products;

namespace MyCashRegister.Transactions
{
    public class CashRegister
    {
        private List<Product> products;
        private int _receiptNumber;
        private ProductFileManager _productFileManager;


        //public CashRegister(string productFilePath)
        public CashRegister()
        {
            var productFileManager = new ProductFileManager();
            products = productFileManager.LoadFromFile("../../../Files/products.txt");
            _receiptNumber = 1;
        }

        public void StartTransaction()
        {
            Transaction transaction = new Transaction(_receiptNumber);

            while (true)
            {
                Console.WriteLine();
                Console.Write("Ange PLU-nummer och antal/mängd för att lägga till vara: ");
                string input = Console.ReadLine();

                if (input.ToLower() == "pay")
                {
                    transaction.PrintReciept();
                    break;
                }

                var inputParts = input.Split(' ');
                if (inputParts.Length == 2 && int.TryParse(inputParts[0], out int plu)
                    && decimal.TryParse(inputParts[1], out decimal quantity))
                {
                    var product = products.FirstOrDefault(p => p.PLU == plu);
                    if (product != null)
                    {
                        transaction.AddProduct(product, quantity);
                        Console.WriteLine($"Lagt till {quantity} x {product.Name} - {product.Price} kr.");
                    }
                    else
                    {
                        Console.WriteLine("Produkten med angivet PLU-nummer hittades inte.");
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning, ange ett giltigt PLU-nummer eller 'pay' för att betala.");
                }
            }
        }
public void Start()
{
    ProductDisplay display = new ProductDisplay(products);
    display.DisplayProducts();
    StartTransaction();
}
    }
}


//Transaction transaction = new Transaction(receiptNumber);
//transaction.AddProduct(product, quantity);
//    transaction.PrintReceipt();


