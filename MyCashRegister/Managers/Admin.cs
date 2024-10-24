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

        public void AddProduct()
        {
            Product product = new Product();
            product.Add();

            SaveProductToFile("../../../Files/products.txt", product);
        }
        public void SaveProductToFile(string filePath, Product product)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine($"{product.PLU};{product.Name.ToUpper()};{product.Price}:{product.PriceType}");
                }
                Console.WriteLine($"Produkten {product.Name} sparades till {filePath}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid sparande av produkten: {ex.Message}");
            }
            //_fileManager.SaveToFile(filePath, product);
            //    Console.WriteLine();
        }

        //private List<Product> products = new List<Product>();
        //private List<Campaign> campaigns = new List<Campaign>();

        //public void SaveCampaignToFile(string filePath, Campaign editedCampaign)
        //{
        //    try
        //    {
        //        List<string> lines = File.ReadAllLines(filePath).ToList();
        //        for (int i = 0; i < lines.Count; i++)
        //        {
        //            string[] parts = lines[i].Split(';');
        //            if (parts[0].ToLower() == editedCampaign.Name.ToLower())
        //            {
        //                lines[i] = $"{editedCampaign.Name};{editedCampaign.Discount};{editedCampaign.StartDate};{editedCampaign.EndDate}";
        //                break;
        //            }
        //        }
        //        File.WriteAllLines(filePath, lines);
        //        Console.WriteLine($"Kampanjen {editedCampaign.Name} har uppdaterats.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Något gick fel vid uppdateringen av kampanjen.{ex.Message}");
        //    }
        //}

        public void ManageItem(IManageable item)
        {
            Console.Write("Vad vill du göra? ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    item.Add();
                    break;

                case "2":
                    item.Edit();
                    break;

                case "3":
                    item.Remove();
                    break;

                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;

            }
        }

        //public void AddProduct()
        //{
        //    Console.WriteLine("\n~~ Lägg till produkt ~~");
        //    Console.Write("Ange produktens ID: ");
        //    int productID = int.Parse(Console.ReadLine());

        //    Console.Write("Ange produktens namn: ");
        //    string name = Console.ReadLine();

        //    Console.Write("Ange produktens pris: ");
        //    decimal price = decimal.Parse(Console.ReadLine());

        //    Product newProduct = new Product(productID, name, price);
        //    products.Add(newProduct);

        //    Product.Add()
        //   //Console.WriteLine($"Produkt {name} har lagts till.");
        //Console.WriteLine("Tryck ENTER för att återgå till menyn.");
        //    Console.ReadLine();

        //    SaveProductToFile("../../../Files/products.txt", newProduct);
        //}
        //public void RemoveProduct()
        //{
        //    Console.WriteLine("\n~~ Ta bort produkt ~~");
        //    Console.Write("Ange produktens namn: ");
        //    string name = Console.ReadLine();

        //    Product productToRemove = products.Find(p => p.Name.ToLower() == name.ToLower());
        //    if (productToRemove != null)
        //    {
        //        products.Remove(productToRemove);
        //        Console.WriteLine($"Produkten {name} har tagits bort.");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Produkten {name} finns inte.");
        //    }
        //}
        //public void AddCampaign()
        //{
        //    Console.WriteLine("\n~~ Lägg till kampanj ~~");
        //    Console.Write("Ange kampanjens namn: ");
        //    string name = Console.ReadLine();

        //    Console.Write("Ange kampanjrabatt i kronor: ");
        //    decimal discount = decimal.Parse(Console.ReadLine());

        //    DateOnly startDate;
        //    DateOnly endDate;

        //    while (true)
        //    {
        //        Console.Write("Ange datum för kampanjstart (YYYY-MM-DD): ");
        //        string startInput = Console.ReadLine();

        //        if (DateOnly.TryParse(startInput, out startDate))
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Ogiltigt datum, ange ett datum enligt YYYY-MM-DD");
        //        }
        //    }

        //    while (true)
        //    {
        //        Console.Write("Ange ett datum för kampanjslut: ");
        //        string endInput = Console.ReadLine();

        //        if (DateOnly.TryParse(endInput, out endDate))
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Ogiltigt datum, ange ett datum enligt YYYY-MM-DD");
        //        }
        //    }
        //    Campaign newCampaign = new Campaign(name, discount, startDate, endDate);
        //    campaigns.Add(newCampaign);
        //    Console.WriteLine($"Kampanjen {name} har lagts till.");

        //    SaveCampaignToFile("../../../Files/campaigns.txt", newCampaign);

        //    Console.WriteLine("Tryck ENTER för att återgå till menyn.");
        //    Console.ReadLine();
        //}
        //public void RemoveCampaign()
        //{
        //    Console.WriteLine("\n~~ Ta bort kampanj ~~");
        //    Console.Write("Ange kampanjens namn: ");
        //    string name = Console.ReadLine();

        //    Campaign campaignToRemove = campaigns.Find(c => c.Name.ToLower() == name.ToLower());
        //    if (campaignToRemove != null)
        //    {
        //        campaigns.Remove(campaignToRemove);
        //        Console.WriteLine($"Kampanjen {name} har tagits bort.");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Kampanjen {name} finns inte.");
        //    }
        //}

        //public void EditCampaign()
        //{
        //    Console.WriteLine("\nTillgängliga kampanjer:");
        //    foreach (var campaign in campaigns)
        //    {
        //        Console.WriteLine($"Namn: {campaign.Name}, Rabatt: {campaign.Discount}, Startdatum: {campaign.StartDate}, Slutdatum: {campaign.EndDate}");
        //    }

        //    Console.Write("\nAnge namnet på kampanjen du vill redigera: ");
        //    string campaignName = Console.ReadLine();

        //    Campaign campaignToEdit = campaigns.Find(c => c.Name.ToLower() == campaignName.ToLower());

        //    if (campaignToEdit != null)
        //    {
        //        Console.Write("Ange ett nytt kampanjnamn: ");
        //        campaignToEdit.Name = Console.ReadLine();

        //        Console.Write("Ange ny rabatt i kronor: ");
        //        campaignToEdit.Discount = decimal.Parse(Console.ReadLine());

        //        SaveCampaignToFile("../../../File/campaigns.txt", campaignToEdit);
        //        Console.WriteLine("Kampanjen har uppdaterats!");
        //    }

        //    else
        //    {
        //        Console.WriteLine($"Kampanjen {campaignName} hittades inte.");
        //    }
        //}

        //public void SaveProductToFile(string filePath, Product product)
        //{
        //    try
        //    {
        //        using (StreamWriter writer = new StreamWriter(filePath, true))
        //        {
        //            foreach (var pr in products)
        //            {
        //                writer.WriteLine($"{product.PLU};{product.Name.ToUpper()};{product.Price}");
        //            }
        //        }
        //        Console.WriteLine($"Produkter sparades till {filePath}");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Fel vid sparande av produkter: {ex.Message}");
        //    }
        //}

    }
}

