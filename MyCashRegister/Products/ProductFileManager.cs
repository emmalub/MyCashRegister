using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCashRegister.Managers;

namespace MyCashRegister.Products
{
    public class ProductFileManager : IFileManager<Product>
    {
        private string _filePath;

        public ProductFileManager(string filePath)
        {
            _filePath = filePath;
        }

        public void SaveToFile(string filePath, List<Product> products)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, false))
                {
                    foreach (Product product in products)
                    {
                        sw.WriteLine($"{product.PLU};{product.Name};{product.Price};{product.PriceType}");
                    }
                }

                Console.WriteLine($"Produkten uppdaterats i filen.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Något gick fel vid sparandet av produkten: {ex.Message}");
            }
        }
        public List<Product> ReadProductsFromFile()
        {
            Console.WriteLine($"Kollar fil: {_filePath}");

            if (!File.Exists(_filePath))
            {
                Console.WriteLine($"Filen {_filePath} kunde inte läsas");
                return new List<Product>();
            }

            List<Product> products = new List<Product>();

            try
            {
                using (StreamReader reader = new StreamReader(_filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(';');
                        if (parts.Length != 4)
                        {
                            Console.WriteLine($"Fel format på raden: {line}. Skippas.");
                            continue;
                        }

                        if (!int.TryParse(parts[0], out int plu) ||
                            !decimal.TryParse(parts[2], out decimal price) ||
                            string.IsNullOrWhiteSpace(parts[3]))
                        {
                            Console.WriteLine($"Felaktigt nummerformat i raden {line}. Skippas.");
                            continue;
                        }

                        string name = parts[1];
                        string priceType = parts[3];

                        if (!Enum.TryParse<PriceType>(priceType, true, out PriceType parsedPriceType))
                        {
                            Console.WriteLine($"Ogiltigt prisformat i raden: {line}. Skippas.");
                            continue;
                        }

                        //PriceType parsedPriceType = (PriceType)Enum.Parse(typeof(PriceType), priceType, true);
                        products.Add(new Product(plu, name, price, parsedPriceType));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid inläsning av produkter: {ex.Message}");
            }
            return products;




            //List<Product> products = new List<Product>();

            //foreach (var line in lines)
            //{
            //    var parts = line.Split(';');
            //    if (parts.Length != 4)
            //    {
            //        Console.WriteLine($"Fel format på raden: {line}. Skippas.");
            //        continue;
            //    }
            //    if (!int.TryParse(parts[0], out int plu) ||
            //            !decimal.TryParse(parts[2], out decimal price) ||
            //            string.IsNullOrEmpty(parts[3]))
            //            {
            //                Console.WriteLine($"Felaktigt nummerformat i rad {line}. Skippas");
            //                continue;
            //            }

            //        //int plu = int.Parse(parts[0]);
            //        string name = parts[1];
            //    //decimal price = decimal.Parse(parts[2]);
            //    //PriceType priceType = Enum.Parse<PriceType>(parts[3]);
            //    string priceType = parts[3];

            //    PriceType parsedPriceType = (PriceType)Enum.Parse(typeof(PriceType), priceType, true);

            //    products.Add(new Product(plu, name, price, parsedPriceType));
            //        //{
            //        //    //PLU = plu,
            //        //    //Name = name,
            //        //    //Price = price,
            //        //    //PriceType = priceType
            //        //});

            //    //}
            //}
            //return products;
        }

        public List<Product> LoadFromFile(string filePath)
        {
            return ReadProductsFromFile();
        }
        //    List<Product> products = new List<Product>();
        //    //List<Product> products = ReadProductsFromFile(File.ReadAllLines(_filePath));
        //    //return products;

        //    //if (!File.Exists(filePath))
        //    //{
        //    //    Console.WriteLine($"Filen {filePath} kunde inte läsas");
        //    //    return products;
        //    //}

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
        //                !Enum.TryParse<PriceType>(parts[3], true, out PriceType priceType))
        //            {
        //                Console.WriteLine($"Felaktigt nummerformat i raden {line}. Skippas.");
        //                continue;
        //            }

        //            string name = parts[1];
        //            products.Add(new Product(plu, name, price, priceType));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Fel vid inläsning av produkter: {ex.Message}");
        //    }
        //    return products;
        //}

        //public void UpdateProductInFile(string filePath, Product editedProduct)

        //{
        //}
    }
}
