using MyCashRegister.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCashRegister.Campaigns
{
    public class CampaignInputHandler
    {
        public string GetCampaignName()
        {
            while (true)
            {
                Console.WriteLine("Ange kampanjens namn: ");
                string name = Console.ReadLine();

                try
                {
                    return InputValidator.NonEmptyString(name);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public decimal GetCampaignDiscountValue()
        {
            decimal discountValue = 0;
            InputValidator validator = InputValidator.Instance;

            Console.Write("Ange rabatt: ");
            string input = Console.ReadLine();

            if (validator.ValidateDecimal(input, out discountValue))
            {
                return discountValue;
            }
            else 
            {
                validator.InvalidInputMessage();
                return GetCampaignDiscountValue();
        }

        public DateOnly GetCampaignStartDate()
        {
            while (true)
            {
                Console.Write("Ange datum för kampanjstart (YYYY-MM-DD): ");
                string startInput = Console.ReadLine();

                if (DateOnly.TryParse(startInput, out DateOnly startDate))
                {
                    return startDate;
                }
                else
                {
                    Console.WriteLine("Ogiltigt datum, ange ett datum enligt YYYY-MM-DD");
                }
            }
        }
        public DateOnly GetCampaignEndDate()
        {
            while (true)
            {
                Console.Write("Ange ett datum för kampanjslut: ");
                string endInput = Console.ReadLine();

                if (DateOnly.TryParse(endInput, out DateOnly endDate))
                {
                    return endDate;
                }
                else
                {
                    Console.WriteLine("Ogiltigt datum, ange ett datum enligt YYYY-MM-DD");
                }
            }
        }
        public IDiscountType GetCampaignType()
        {
            //IDiscountType discountType = null;
            decimal discountValue = 0;
            InputValidator validator = InputValidator.Instance;

            while (true)
            {
                Console.WriteLine("Ange typ av rabatt: ");
                Console.WriteLine("1. Procentuell rabatt");
                Console.WriteLine("2. Fast beloppsrabatt");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    //Console.Write("Ange en procentuell rabatt: ");
                    //string input = Console.ReadLine();

                    //if (validator.ValidateDecimal(input, out discountValue))
                    //{
                        return new PercentageDiscount(discountValue);
                    //}
                    //else
                    //{
                    //    validator.InvalidInputMessage();
                    //}
                }
                else if (choice == "2")
                {
                    //Console.Write("Ange rabattbelopp i kronor: ");
                    //string input = Console.ReadLine();

                    //if (validator.ValidateDecimal(input, out discountValue))
                    //{
                        return new FixedAmountDiscount(discountValue);
                    //}
                    //else
                    //{
                    //    validator.InvalidInputMessage();
                    //}
                }
                else
                {
                    validator.InvalidInputMessage();
                }
            }
        }
        public Campaign CreateCampaign()
        {
            string name = GetCampaignName();
            decimal discountValue = GetCampaignDiscountValue();
            DateOnly startDate = GetCampaignStartDate();
            DateOnly endDate = GetCampaignEndDate();
            IDiscountType discountType = GetCampaignType();


            string campaignID = CampaignFileManager.GenerateNewCampaignID("../../../Files/campaigns.txt");

            return new Campaign(campaignID, name, discountValue, startDate, endDate, discountType);
        }
    }
}
