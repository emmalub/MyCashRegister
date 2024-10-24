using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
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

            Console.Write("Ange produktens ID: ");
            string productIDInput = (Console.ReadLine());
            if (!InputValidator.Instance.ValidateInt(productIDInput, out int productID))
            {
                Console.WriteLine("Ogiltigt produktID. Ange ett heltal.");
                return;
            }

            Console.Write("Ange produktens namn: ");
            string nameInput = (Console.ReadLine());
            if (!InputValidator.Instance.NonEmptyString(nameInput, out string name))
            {
                Console.WriteLine("Namnet får inte vara tomt");
                return;
            }

            Console.Write("Ange produktens pris: ");
            string priceInput = (Console.ReadLine());
            if (InputValidator.Instance.ValidateDecimal(priceInput, out decimal price))
            {
                Console.WriteLine("Priset måste vara ett positivt tal.");
                return;
            }

            PriceType priceType;
            Console.Write("Ange om priset gäller per kilo eller styck (KG för Kilo och ST för styck): ");
            string inputPriceType = Console.ReadLine().ToUpper();

            if (inputPriceType == "KG")
            {
                priceType = PriceType.PerKilo;
            }
            else if (inputPriceType == "ST")
            {
                priceType = PriceType.PerPiece;
            }
            else
            {
                Console.WriteLine("Ogiltigt alternativ, ange KG (för kilo) eller ST (för styck)");
                return;
            }

            Product newProduct = new Product(productID, name, price, priceType);
            Products.Add(newProduct);

            Console.WriteLine($"Produkt {name} har lagts till.");
            Console.WriteLine("Tryck ENTER för att återgå till menyn.");
            Console.ReadLine();

            Admin admin = new Admin();
            admin.SaveProductToFile("../../../Files/products.txt", newProduct);
        }
        public void Remove()
        {
            Console.WriteLine("\n~~ Ta bort produkt ~~");
            Console.Write("Ange produktens namn: ");
            string name = Console.ReadLine();

            Product productToRemove = Products.Find(p => p.Name.ToLower() == name.ToLower());
            if (productToRemove != null)
            {
                Products.Remove(productToRemove);
                Console.WriteLine($"Produkten {name} har tagits bort.");
            }
            else
            {
                Console.WriteLine($"Produkten {name} finns inte.");
            }
        }
        public void Edit()
        {
            //ProductFileManager fileManager = new ProductFileManager();

            ProductFileManager productFileManager = new ProductFileManager("../../../Files/products.txt");    
            
            List<Product> products = productFileManager.ReadProductsFromFile();
            
            ProductDisplay display = new ProductDisplay(productFileManager);
            display.DisplayProducts();

            Console.WriteLine("\n~~ Redigera produkt ~~");
            Console.Write("Ange prodfuktens ID (PLU) för den produkt du vill redigera: ");
            int productID = int.Parse(Console.ReadLine());

            Product productToEdit = Products.Find(p => p.PLU == productID);

            if (productToEdit != null)
            {
                Console.WriteLine($"Redigerar produkt: {productToEdit.Name}");

                Console.Write("Ange ett nytt namn eller lämna tomt för att behålla nuvarande namn: ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName))
                {
                    productToEdit.Name = newName;
                }

                Console.Write("Ange ett nytt pris eller lämna tomt för att behålla tidigare pris: ");
                string newPriceInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(newPriceInput) 
                    && decimal.TryParse(newPriceInput, out decimal newPrice))
                {
                    productToEdit.Price = newPrice;
                }

                Console.WriteLine("Ändra typ av pris, ange S för styckpris och K för kilopris: ");
                string priceTypeInput = Console.ReadLine().ToUpper();
                if (priceTypeInput == "S")
                {
                    productToEdit.PriceType = PriceType.PerKilo;
                }
                else if (priceTypeInput == "K")
                {
                    productToEdit.PriceType = PriceType.PerKilo;
                }
                // tog bort 24/10 efter att ha lagt till product repository längst upp i metoden
                //fileManager.SaveToFile("../../../Files/products.txt", Products);
                Console.WriteLine("Produkten har uppdaterats.");
            }
            else
            {
                Console.WriteLine("Ingen produkt med detta ID hittades.");
            }
        }


        //public static List<Product> LoadProducts(string filePath)
        //{
        //    List<Product> products = new List<Product>();

        //    if (!File.Exists(filePath))
        //    {
        //        Console.WriteLine($"Filen {filePath} kunde inte läsas");
        //        return products;
        //    }

        //    try
        //    {
        //        string[] lines = File.ReadAllLines(filePath);
        //        foreach (var line in lines)
        //        {
        //            var parts = line.Split(';');
        //            if (parts.Length != 4)
        //            {
        //                Console.WriteLine($"Fel format på raden: {line}. Skippas.");
        //                continue;
        //            }

        //            if (!int.TryParse(parts[0], out int plu) || 
        //                !decimal.TryParse(parts[2], out decimal price) || 
        //                string.IsNullOrWhiteSpace(parts[3]))
        //            {
        //                Console.WriteLine($"Felaktigt nummerformat i raden {line}. Skippas.");
        //                continue;
        //            }
        //            //int plu = int.Parse(parts[0]);
        //            //decimal price = decimal.Parse(parts[2]);

        //            string name = parts[1];
        //            string priceType = parts[3];

        //            PriceType parsedPriceType = (PriceType)Enum.Parse(typeof(PriceType), priceType, true);

        //            products.Add(new Product(plu, name, price, parsedPriceType));
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Fel vid inläsning av produkter: {ex.Message}");
        //    }
        //    return products;
        //}

        //public decimal CalculatePrice(decimal quantity)
        //{
        //    if (PriceType == PriceType.PerKilo)
        //    {
        //        return Price * quantity;
        //    }
        //    else
        //    {
        //        return Price * (int)quantity;
        //    }
        //}

        //public void DisplayProducts()
        //{
        //    Console.WriteLine("--- Tillgängliga Produkter ---");
        //    foreach (var product in _productLookUp.Values)
        //    {
        //        Console.WriteLine($"| PLU: {PLU}| Namn: {Name}| Pris: {Price} kr |");
        //    }
        //    Console.WriteLine("______________________________");
        //}

    }
}

