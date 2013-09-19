using Blinds.com.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blinds.com.Data
{
    public class BlindData
    {
        public static IQueryable<Blind> GetBlinds(int numberOfBlinds)
        {
            Random rnd = new Random();
            var blinds = new List<Blind>();
            for (int i = 0; i < numberOfBlinds; i++)
            {
                var blind = new Blind();
                blind.Name = "blind" + i.ToString();

                if (i % 3 == 0)
                    blind.Category = "Blackout";
                else if (i % 3 == 1)
                    blind.Category = "HoneyComb";
                else
                    blind.Category = "Wood";

                //get random price                
                int price = rnd.Next(30, 100);
                blind.Price = (decimal)price;

                blinds.Add(blind);
            }

            return blinds.AsQueryable();
        }
    }
}