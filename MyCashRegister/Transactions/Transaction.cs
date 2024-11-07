using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using MyCashRegister.Products;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyCashRegister.Transactions
{
    public class Transaction
    {

        public int ReceiptNumber { get; private set; }
        public decimal TotalAmount { get; set; }
        private List<Product> products;
        private List<(Product Product, decimal Quantity)> 
            soldProducts = new List<(Product, decimal)>();

        public Transaction(int receiptNumber)
        {
            ReceiptNumber = GetNextReceiptNumber();
            TotalAmount = 0;
        }
        public Transaction()
        {
            products = new List<Product>();
        }

        private int GetNextReceiptNumber()
        {
            string path = "../../../Receipts(receipt_number.txt";

            if (!File.Exists(path))
            {
                File.WriteAllText(path, "1");
            }
            int receiptNumber = int.Parse(File.ReadAllText(path));
            File.WriteAllText(path, (receiptNumber + 1).ToString());
            return receiptNumber;
        }

        public void AddProduct(Product product, decimal quantity)
        {
            soldProducts.Add((product, quantity));
            TotalAmount += product.Price * quantity;

            Console.Clear();
            CurrentCart();
        }
      
        public void PrintReciept()
        {
            string date = DateTime.Now.ToString("yyyyMMdd");
            string fileName = $"../../../Receipts/RECEIPT_{date}.txt;";

            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                sw.WriteLine(@"
                             _     
               ___ _ __ ___ ( )___ 
              / _ \ '_ ` _ \|// __|
             |  __/ | | | | | \__ \
              \___|_| |_| |_| |___/
                         _ _  _  _ 
                        (_(_)(_)|_)
                                |    
");
                sw.WriteLine("--------------------------------------------------");
                sw.WriteLine($"Kvitto #{ReceiptNumber}");
                sw.WriteLine($"Datum: {DateTime.Now}");
                sw.WriteLine(@"
                                             SEK
--------------------------------------------------");
                foreach (var item in soldProducts)
                {
                    string productName = item.Product.Name
                        .PadRight(30);
                    string totalPrice = (item.Product.Price * item.Quantity)
                        .ToString("F2").PadLeft(10);

                    string quantityInfo;

                    if (item.Product.PriceType == PriceType.PerKilo)
                    {
                        quantityInfo = $"{item.Quantity:F2} kg x {item.Product.Price:F2} kr/kg"
                            .PadRight(40);
                        
                    }
                    else
                    {
                        quantityInfo = $"{item.Quantity:F0} st x {item.Product.Price:F2} kr/st"
                            .PadRight(40);
                        
                    }
                    sw.WriteLine($"{productName} {totalPrice.PadLeft(19)}");
                    sw.WriteLine(quantityInfo);
                }
                sw.WriteLine("                                           -------");
                sw.WriteLine($"Att betala: {TotalAmount.ToString("F2")
                    .PadLeft(38)}");
                sw.WriteLine("--------------------------------------------------");
                sw.WriteLine();
                sw.Close();
            }
            Console.SetCursorPosition(0, 20);
            Console.WriteLine($"Kvitto har skrivits till {fileName}");
        }
        public void CurrentCart()
        {
            ProductFileManager productFileManager = new ProductFileManager("../../../Files/products.txt");
            List<Product> products = productFileManager.ReadProductsFromFile();

            ProductDisplay display = new ProductDisplay(productFileManager);
            display.DisplayProducts();

            Console.SetCursorPosition(35, 2);
            Console.WriteLine("----------------------------------------------------------------");
            Console.SetCursorPosition(35, 3);
            Console.WriteLine("PRODUKT                  Mängd                        Pris");

            Console.SetCursorPosition(35, 4);
            Console.WriteLine("----------------------------------------------------------------");


            decimal totalPrice = 0;
            int unitPricePadding = 7;
            int totalAmountPadding = 10;
            int quantityPadding = 18;
            int maxProductNameLength = 9;

            int currentRow = 5;

            foreach (var item in soldProducts)
            {
                string productName = item.Product.Name.Length > maxProductNameLength
                    ? item.Product.Name.Substring(0, maxProductNameLength - 3) + "..."
                    : item.Product.Name;

                decimal productTotalPrice = item.Quantity * item.Product.Price;

                string quantity = item.Product.PriceType == PriceType.PerKilo
                    ? $"{item.Quantity:F1} kg" 
                    : $"{item.Quantity:F1} st" ;


                Console.SetCursorPosition(35, currentRow);
                Console.WriteLine($"| {productName
                    .PadRight(maxProductNameLength)} " +
                    $"{quantity
                    .PadLeft(quantityPadding)} x " +
                    $"{item.Product.Price.ToString("F2")
                    .PadLeft(unitPricePadding)} " +
                    $"{(item.Product.PriceType == PriceType.PerKilo ? "kr/kg" : "kr/st")
                    .PadRight(6)} " +
                    $"{productTotalPrice.ToString("F2")
                    .PadLeft(totalAmountPadding + 1)} kr |");
                

                totalPrice += productTotalPrice;
                currentRow++;
            }

            Console.SetCursorPosition(35, currentRow);
            Console.WriteLine("----------------------------------------------------------------");

            currentRow++;
            Console.SetCursorPosition(35, currentRow);
            Console.WriteLine($"Summa: {totalPrice.ToString("F2")
                .PadLeft(52)} kr");

            currentRow++;
            Console.SetCursorPosition(35, currentRow);
            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
