using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCashRegister.Products
{
    public class ProductRepositary : IProductRepository
    {
        private readonly string _filePath;
        public ProductRepositary(string filePath)
        {
            _filePath = filePath;
        }
        public List<Product> GetAll()
        {
            var products = new List<Product>();
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 4)
                {
                    int plu = int.Parse(parts[0]);
                    string name = parts[1];
                    decimal price = decimal.Parse(parts[2]);
                    PriceType priceType = Enum.Parse<PriceType>(parts[3]);

                    products.Add(new Product
                    {
                        PLU = plu,
                        Name = name,
                        Price = price,
                        PriceType = priceType
                    });

                }
            }
            return products;
        }
    }
}
