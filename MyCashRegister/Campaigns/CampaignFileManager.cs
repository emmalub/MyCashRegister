using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyCashRegister.Managers;

namespace MyCashRegister.Campaigns
{
    public class CampaignFileManager : IFileManager<Campaign>
    {

        public void SaveToFile(string filePath, List<Campaign> campaigns)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    foreach (Campaign campaign in campaigns)
                    {
                        sw.WriteLine($"{campaign.CampaignID};{campaign.Name};{campaign.Discount};{campaign.StartDate};{campaign.EndDate};{campaign.DiscountType}");
                    }
                    }
                Console.WriteLine($"Kampanjen har lagts till!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Något gick fel vid sparandet av kampanjen: {ex.Message}");
            }
        }

        public List<Campaign> LoadFromFile(string filePath)
        {
            List<Campaign> campaigns = new List<Campaign>();

            foreach (var line in File.ReadLines(filePath))
            {
                var parts = line.Split(";");
                if (parts.Length == 6)
                {
                    var campaignID = parts[0];
                    var name = parts[1];
                    var discount = decimal.Parse(parts[2]);
                    var startDate = DateOnly.Parse(parts[3]);
                    var endDate = DateOnly.Parse(parts[4]);
                    var discountType = CreateDiscountType(parts[5], discount);

                    var campaign = new Campaign(campaignID, name, discount, startDate, endDate, discountType)
                    {
                        CampaignID = campaignID
                    };
                    campaigns.Add(campaign);
                }
            }
            return campaigns;
        }
        private IDiscountType CreateDiscountType(string typeName, decimal discountValue)
        {
            switch (typeName)
            {
                case nameof(PercentageDiscount):
                    return new PercentageDiscount(discountValue);


                case nameof(FixedAmountDiscount):
                    return new FixedAmountDiscount(discountValue);
                default:
                    throw new InvalidOperationException("Ogiltig rabattyp.");
            }
        }
        public void UpdateCampaignInFile(string filePath, Campaign editedCampaign)
        {
            List<Campaign> campaigns = LoadFromFile(filePath);

            for (int i = 0; i < campaigns.Count; i++)
            {
                if (campaigns[i].Name.Equals(editedCampaign.Name, StringComparison.OrdinalIgnoreCase))
                {
                    campaigns[i] = editedCampaign;
                    break;
                }
            }
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                foreach (Campaign campaign in campaigns)
                {
                    sw.WriteLine($"{campaign.Name};{campaign.Discount};{campaign.StartDate};{campaign.EndDate};{campaign.DiscountType}");
                }
            }
        }
        public static string GenerateNewCampaignID(string filePath)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();

            if (lines.Count == 0)
            {
                return "01";
            }
            int maxID = 0;
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                if (int.TryParse(parts[0], out int currentId))
                {
                    if (currentId > maxID)
                    {
                        maxID = currentId;
                    }
                }
            }
            return (maxID + 1).ToString("D2");
        }
    }
}
