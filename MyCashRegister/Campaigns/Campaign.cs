using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCashRegister.Managers;

namespace MyCashRegister.Campaigns
{
    public class Campaign : IManageable
    {
        public string CampaignID { get; set; }
        public string Name { get; set; }
        public decimal Discount { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public IDiscountType DiscountType { get; set; }

        //private List<Campaign> _campaigns = new List<Campaign>(); //används inte?


        public Campaign(string campaignID, string name, decimal discount, DateOnly startDate, DateOnly endDate, IDiscountType discountType)
        {
            CampaignID = campaignID;
            Name = name;
            Discount = discount;
            StartDate = startDate;
            EndDate = endDate;
            DiscountType = discountType;
        }

        public Campaign()
        { }

        public void Add()
        {
            //Håller på med den här jäveln
            ////////////////////////////////////
            //string name = GetCampaignName();
            //decimal discountValue = GetCampaignType();
            //DateOnly startDate = GetCampaignStartDate();
            //DateOnly endDate = GetCampaignEndDate();

            //string campaignID = CampaignFileManager.GenerateCampaignId(
            //    "../../../Files/campaigns.txt");
            Campaign newCampaign = new Campaign(
                campaignID, name, discountValue, startDate, endDate);

            CampaignFileManager.AddCampaign(newCampaign,
                "../../../Files/campaigns.txt");

            Console.WriteLine"Tryck ENTER för att återgå till menyn.");
            Console.ReadLine();


            //Console.WriteLine("\n~~ Lägg till kampanj ~~");
            //Console.Write("Ange kampanjens namn: ");
            //string name = Console.ReadLine();

            //IDiscountType discountType = null;
            //decimal discountValue = 0;

            //Console.WriteLine("Ange typ av rabatt: ");
            //Console.WriteLine("1. Procentuell rabatt");
            //Console.WriteLine("2. Fast beloppsrabatt");
            //string choice = Console.ReadLine();

            //if (choice == "1")
            //{
            //    Console.Write("Ange en procentuell rabatt: ");
            //    discountValue = decimal.Parse(Console.ReadLine());
            //    discountType = new PercentageDiscount(discountValue);
            //}
            //else if (choice == "2")
            //{
            //    Console.Write("Ange rabattbelopp i kronor: ");
            //    discountValue = decimal.Parse(Console.ReadLine());
            //    DiscountType = new FixedAmountDiscount(discountValue);
            //}
            //else
            //{
            //    Console.WriteLine("Ogiltigt val");
            //    return;
            //}

            //DateOnly startDate;
            //DateOnly endDate;

            //while (true)
            //{
            //    Console.Write("Ange datum för kampanjstart (YYYY-MM-DD): ");
            //    string startInput = Console.ReadLine();
            //    if (DateOnly.TryParse(startInput, out startDate))
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Ogiltigt datum, ange ett datum enligt YYYY-MM-DD");
            //    }
            //}

            //while (true)
            //{
            //    Console.Write("Ange ett datum för kampanjslut: ");
            //    string endInput = Console.ReadLine();

            //    if (DateOnly.TryParse(endInput, out endDate))
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Ogiltigt datum, ange ett datum enligt YYYY-MM-DD");
            //    }
            //}

            string campaignID = GenerateNewCampaignID("../../../Files/campaigns.txt");
            Campaign newCampaign = new Campaign(campaignID, name, discountValue, startDate, endDate, discountType);
            _campaigns.Add(newCampaign);

            CampaignFileManager campaignFileManager = new CampaignFileManager();
            campaignFileManager.SaveToFile("../../../Files/campaigns.txt", newCampaign);

            Console.WriteLine($"Kampanjen {name} har lagts till.");
            Console.WriteLine("Tryck ENTER för att återgå till menyn.");
            Console.ReadLine();
        }
        public void Remove()
        {
            Console.WriteLine("\n~~ Ta bort kampanj ~~");
            Console.Write("Ange kampanjens namn: ");
            string name = Console.ReadLine();

            if (Guid.TryParse(name, out Guid campaignId))
            {
                Campaign campaignToRemove = _campaigns.Find(c => c.CampaignID == CampaignID);
                if (campaignToRemove != null)
                {
                    _campaigns.Remove(campaignToRemove);

                    List<string> lines = File.ReadAllLines("../../../Files/campaigns.txt").ToList();
                    lines = lines.Where(line => !line.StartsWith(campaignToRemove.CampaignID.ToString())).ToList();
                    File.WriteAllLines("../../../Files/campaigns.txt", lines);

                    Console.WriteLine($"Kampanjen med ID {campaignId} har tagits bort.");
                }
                else
                {
                    Console.WriteLine($"Kampanjen {campaignId} finns inte.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt ID");
            }
        }
        public void Edit()
        {
            Console.WriteLine("\nTillgängliga kampanjer:");
            foreach (var campaign in _campaigns)
            {
                Console.WriteLine($"ID: {campaign.CampaignID}, Namn: {campaign.Name}, Rabatt: {campaign.Discount}, Startdatum: {campaign.StartDate}, Slutdatum: {campaign.EndDate}");
            }

            Console.Write("\nAnge namnet på kampanjen du vill redigera: ");
            string idInput = Console.ReadLine();

            Campaign campaignToEdit = _campaigns.Find(c => c.CampaignID.Equals(idInput, StringComparison.OrdinalIgnoreCase));

            if (campaignToEdit != null)
            {
                Console.Write("Ange ett nytt kampanjnamn: ");
                campaignToEdit.Name = Console.ReadLine();

                Console.Write("Ange ny rabatt i kronor: ");
                campaignToEdit.Discount = decimal.Parse(Console.ReadLine());

                List<string> lines = File.ReadAllLines("../../../Files/campaigns.txt").ToList();
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].StartsWith(campaignToEdit.CampaignID))
                    {
                        lines[i] = $"{campaignToEdit.CampaignID};{campaignToEdit.Name};{campaignToEdit.Discount};{campaignToEdit.StartDate};{campaignToEdit.EndDate}";
                        break;
                    }
                }
                File.WriteAllLines("../../../Files/campaigns.txt", lines);
                Console.WriteLine("Kampanjen har uppdaterats!");
            }

            else
            {
                Console.WriteLine($"Kampanjen {idInput} hittades inte.");
            }
        }

        //public string GenerateNewCampaignID(string filePath)
        //{
        //    List<string> lines = File.ReadAllLines(filePath).ToList();

        //    if (lines.Count == 0)
        //    {
        //        return "01";
        //    }
        //    int maxID = 0;
        //    foreach (string line in lines)
        //    {
        //        string[] parts = line.Split(';');
        //        if (int.TryParse(parts[0], out int currentId))
        //        {
        //            if (currentId > maxID)
        //            {
        //                maxID = currentId;
        //            }
        //        }
        //    }
        //    return (maxID + 1).ToString("D2");
        //}
        // En metod för att hämta det aktuella priset baserat på kampanj
    }
}
