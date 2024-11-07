using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;
using MyCashRegister.Managers;

namespace MyCashRegister.Products
{
    public class Product : IManageable
    {
        public int PLU { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public PriceType PriceType { get; set; }


        private List<Product> Products { get; set; } = new List<Product>();

        public Product(int plu, string name, decimal price, PriceType priceType)
        {
            PLU = plu;
            Name = name;
            Price = price;
            PriceType = priceType;
        }
        public Product()
        {
            string filePath = "../../../Files/products.txt";
            ProductFileManager fileManager = new ProductFileManager(filePath);
            Products = fileManager.LoadFromFile(filePath);
            Name = "unknown";
        }

        public void Add()
        {
            Console.WriteLine("\n~~ Lägg till produkt ~~");

            int productID = 0;
            bool isUniqueID = false;

            while (!isUniqueID)
            {
                Console.Write("Ange produktens ID: ");
                string productIDInput = Console.ReadLine();

                if (!InputValidator.Instance.ValidateInt(productIDInput, out productID))
                {
                    Console.WriteLine("Ogiltigt produktID, ange ett giltigt heltal.");
                    continue;
                }
                if (Products.Any(p => p.PLU == productID))
                {
                    Console.WriteLine($"En produkt med samma ID {productID} finns redan. Välj ett annat.");
                }
                else
                {
                    isUniqueID = true;
                }

            }

            string name;
            while (true)
            {
                Console.Write("Ange produktens namn: ");
                string nameInput = Console.ReadLine();
                if (InputValidator.Instance.NonEmptyString(nameInput, out name))
                {
                    break;
                }
                Console.WriteLine("Namnet får inte vara tomt.");
            }

            decimal price;
            while (true)
            {
                Console.Write("Ange produktens pris: ");
                string priceInput = Console.ReadLine();
                if (InputValidator.Instance.ValidateDecimal(priceInput, out price))
                {
                    break;
                }
                Console.WriteLine("Priset måste vara ett decimaltal, ex 10,00");
            }

            PriceType priceType;
            while (true)
            {
                Console.Write("Ange om priset gäller per kilo eller styck (KG för kilo och ST för styck): ");
                string inputPriceType = Console.ReadLine().ToUpper();

                if (inputPriceType == "KG")
                {
                    priceType = PriceType.PerKilo;
                    break;
                }
                else if (inputPriceType == "ST")
                {
                    priceType = PriceType.PerPiece;
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltigt alternativ, ange KG (för kilo) eller ST (för styck).");
                }
            }

            Product newProduct = new Product(productID, name, price, priceType);
            Products.Add(newProduct);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nProdukt {name} har lagts till.");
            Console.ResetColor();
            Console.WriteLine("Tryck ENTER för att återgå till menyn.");
            Console.ReadLine();

            Admin admin = new Admin();
            admin.SaveProductToFile("../../../Files/products.txt", newProduct);

        }

        public void Remove()
        {
            ProductFileManager productFileManager = new ProductFileManager("../../../Files/products.txt");
            List<Product> products = productFileManager.ReadProductsFromFile();

            ProductDisplay display = new ProductDisplay(productFileManager);
            display.DisplayProducts();

            Console.WriteLine("\n~~ Ta bort produkt ~~");

            while (true)
            {
                Console.Write("Ange produktens namn: (eller skriv AVBRYT för att gå tillbaka) ");
                string name = Console.ReadLine().ToUpper().Trim();

                if (name == "AVBRYT")
                {
                    Console.WriteLine("Åtgärden avbröts.");
                    break;
                }

                var productToRemove = Products.Find(p => p.Name.Equals(name));

                if (productToRemove != null)
                {
                    Products.Remove(productToRemove);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nProdukten {name} har tagits bort.");
                    Console.ResetColor();
                    productFileManager.SaveToFile("../../../Files/products.txt", Products);
                    break;
                }
                else
                {
                    Console.WriteLine($"Produkten {name} finns inte. Försök igen");
                }
            }
            Console.WriteLine("Tryck ENTER för att återgå till menyn.");
            Console.ReadLine();
        }
        public void Edit()
        {
            ProductFileManager productFileManager = new ProductFileManager("../../../Files/products.txt");

            Products = productFileManager.ReadProductsFromFile();

            var display = new ProductDisplay(productFileManager);
            display.DisplayProducts();

            Console.WriteLine("\n~~ Redigera produkt ~~");

            while (true)
            {
                Console.Write("Ange produktens ID (PLU) för den produkt du vill redigera: ");
                //int productID = int.Parse(Console.ReadLine());
                if (!int.TryParse(Console.ReadLine(), out var productID))
                {
                    Console.WriteLine("Felaktigt ID-format. Försök igen. ");
                    continue;
                }

                var productToEdit = Products.Find(p => p.PLU == productID);

                if (productToEdit != null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\nRedigerar produkt: {productToEdit.Name}");
                    Console.ResetColor();

                    Console.Write("Ange ett nytt namn eller lämna tomt för att behålla nuvarande namn: ");
                    string newName = Console.ReadLine().ToUpper();
                    if (!string.IsNullOrEmpty(newName))
                    {
                        productToEdit.Name = newName;
                    }

                    Console.Write("Ange ett nytt pris eller lämna tomt för att behålla tidigare pris (Ange ett decimaltal ex 10.00): ");
                    string newPriceInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newPriceInput)
                        && decimal.TryParse(newPriceInput, out decimal newPrice))
                    {
                        productToEdit.Price = newPrice;
                    }

                    Console.Write("Ändra typ av pris, ange ST för styckpris och KG för kilopris: ");
                    string priceTypeInput = Console.ReadLine().ToUpper();
                    if (priceTypeInput == "ST")
                    {
                        productToEdit.PriceType = PriceType.PerKilo;
                    }
                    else if (priceTypeInput == "KG")
                    {
                        productToEdit.PriceType = PriceType.PerKilo;
                    }

                    productFileManager.SaveToFile("../../../Files/products.txt", Products);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Produkten har uppdaterats.");
                    Console.ResetColor();
                    break;
                }
                else
                {
                    Console.WriteLine("Ingen produkt med detta ID hittades.");
                }
            }
            Console.WriteLine("Tryck ENTER för att återgå till menyn");
            Console.ReadLine();
        }
    }
}

