using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCashRegister.Products;

namespace MyCashRegister.Managers
{
    public class Admin
    {
        private string filePath = "../../../Files/products.txt";

        public void SaveProductToFile(string filePath, Product product)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine($"{product.PLU};{product.Name
                        .ToUpper()};{product.Price};{product.PriceType}");
                }
                Console.WriteLine(
                    $"Produkten {product.Name} sparades till {filePath}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Fel vid sparande av produkten: {ex.Message}");
            }
        }
    }
}

