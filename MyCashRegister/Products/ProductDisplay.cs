using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCashRegister.Products
{
    public class ProductDisplay
    {
        //private List<Product> products;
        private IProductRepository _productRepository;
        private int _receiptNumber;
        private ProductFileManager _productFileManager;

        public ProductDisplay(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void DisplayProducts()
        {
            Console.Clear();
            Console.WriteLine("--------------------------");
            Console.WriteLine("- Tillgängliga Produkter -");
            Console.WriteLine("--------------------------");
            var products = _productRepository.GetAll();

            foreach (var product in products)
            {
                //Console.WriteLine($"| PLU: {product.PLU}| {product.Name}| Pris: {product.Price} kr |");
                Console.WriteLine(
                    $"| PLU: {product.PLU.ToString().PadRight(5)}| {product.Name.PadRight(10)} |");

            }
            Console.WriteLine("--------------------------");
        }
    }
}
