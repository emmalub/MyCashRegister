using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCashRegister.Campaigns;
using MyCashRegister.Managers;

namespace MyCashRegister.Products
{
    public class ProductFileManager : IFileManager<Product>
    {

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
                public List<Product> LoadFromFile(string filePath)
                {
                    List<Product> products = new List<Product>();

                    if (!File.Exists(filePath))
                    {
                        Console.WriteLine($"Filen {filePath} kunde inte läsas");
                        return products;
                    }

                    try
                    {
                        string[] lines = File.ReadAllLines(filePath);
                        foreach (var line in lines)
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

                            PriceType parsedPriceType = (PriceType)Enum.Parse(typeof(PriceType), priceType, true);

                            products.Add(new Product(plu, name, price, parsedPriceType));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Fel vid inläsning av produkter: {ex.Message}");
                    }
                    return products;
                }

                //public void UpdateProductInFile(string filePath, Product editedProduct)

                //{
                //}
            }
        }
