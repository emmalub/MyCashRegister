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
        private ProductFileManager _productFileManager;
        private int _receiptNumber;


        public ProductDisplay(ProductFileManager productFileManager)
        {
            _productFileManager = productFileManager;
        }

        public void DisplayProducts()
        {
            List<Product> products = _productFileManager.ReadProductsFromFile();

            Console.Clear();
            Console.WriteLine("--------------------------");
            Console.WriteLine("- Tillgängliga Produkter -");
            Console.WriteLine("--------------------------");

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
