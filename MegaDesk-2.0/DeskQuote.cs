﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaDesk_4_ColeCannon
{
    class DeskQuote
    {
        private const string RUSH_FILE_NAME = @"../../assets/rushOrderPrices.txt";
        public Desk desk { get; set; }
        public string name { get; set; }
        public DateTime quoteDate { get; set; }
        public int priceQuote { get; set; }


        public static int getPrice(int height, int width, string material, string rush, int drawer)
        {
            int price = 200;
            price += surfaceAreaPrice(height, width);
            price += getMaterialPrice(material);
            price += getRushPrice(rush, height, width);
            price += getDrawerPrice(drawer);

            return price;
        }

        public static int calculateSurfaceArea(int height, int width)
        {
            return height * width;
        }

        public static int surfaceAreaPrice(int height, int width)
        {
            int surfaceArea = calculateSurfaceArea(height, width);

            if (surfaceArea - 1000 > 0)
                return surfaceArea - 1000;
            else
                return 0;
        }

        public static int getMaterialPrice(string material)
        {
     
            string[] names = Enum.GetNames(typeof(Materials));
            for (int i = 0; i < names.Length; i++)
            {
                if (material == names[i])
                {
                    int[] value = (int[])Enum.GetValues(typeof(Materials));
                    return value[i];
                }
            }


            return 0;
        }

        public static int getRushPrice(string rush, int height, int width)
        {
            int days;

            if (rush != "None")
            {
                days = Int32.Parse(rush.Split(null)[0]);
            }
            else
            {
                days = 0;
            }


            int[,] rushCost = {
              { 60, 70, 80 },
              { 40, 50, 60 },
              { 30, 35, 40 }
             };

            try
            {
             
              if (File.Exists(RUSH_FILE_NAME))
              {
                using (StreamReader reader = new StreamReader(RUSH_FILE_NAME))
                {
                  for (int i=0; i<3; i++)
                  {
                    for (int j=0; j<3; j++)
                    {
                      string rushField = reader.ReadLine();
                      Int32.TryParse(rushField, out rushCost[i, j]);
                    }                    
                  }
                } 
              }
            } catch (Exception ex)
            {
              Console.WriteLine("Error loading rush quote prices " + ex);
            }

            int index;
            int area = calculateSurfaceArea(height, width);

            if (area > 2000)
            {
                index = 2;
            }
            else if (area > 1000)
            {
                index = 1;
            }
            else
            {
                index = 0;
            }

            switch (days)
            {
                case 3:
                    return rushCost[0, index];
                case 5:
                    return rushCost[1, index]; ;
                case 7:
                    return rushCost[2, index]; ;
                default:
                    return 0;
            }

        }

        public static int getDrawerPrice(int numOfDrawers)
        {
            return numOfDrawers * 50;
        }
    }
}
