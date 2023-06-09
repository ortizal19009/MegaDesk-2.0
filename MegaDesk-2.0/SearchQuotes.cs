﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaDesk_4_ColeCannon
{
    public partial class SearchDisplay : Form
    {
        public SearchDisplay()
        {
            InitializeComponent();
            SearchColumnDisplay.Columns.Add("Name", 125, HorizontalAlignment.Center);
            SearchColumnDisplay.Columns.Add("Date", 90, HorizontalAlignment.Center);
            SearchColumnDisplay.Columns.Add("Height", 50, HorizontalAlignment.Center);
            SearchColumnDisplay.Columns.Add("Width", 40, HorizontalAlignment.Center);
            SearchColumnDisplay.Columns.Add("Materials", 90, HorizontalAlignment.Center);
            SearchColumnDisplay.Columns.Add("Rush Days", 70, HorizontalAlignment.Center);
            SearchColumnDisplay.Columns.Add("Drawers", 70, HorizontalAlignment.Center);
            SearchColumnDisplay.Columns.Add("Total", 70, HorizontalAlignment.Center);
        }

        private void SearchQuotes_Load(object sender, EventArgs e)
        {

            List<string> materialList = new List<string>();

            materialList.Add("Select");

            foreach (var name in Enum.GetNames(typeof(Materials)))
            {
                materialList.Add(name);
            }

            SearchMaterialCombo.DataSource = materialList;         
        }

        private void ViewQuotesBackBtn_Click(object sender, EventArgs e)
        {
            var mainMenu = (MainMenu)Tag;
            mainMenu.Show();
            Close();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            SearchColumnDisplay.Items.Clear();

            try
            {
                List<DeskQuote> deskQuotes = JsonConvert.DeserializeObject<List<DeskQuote>>(File.ReadAllText(@"../../assets/quotes.json")) ?? new List<DeskQuote>();
                foreach(DeskQuote deskQuote in deskQuotes) 
                {
                    if (deskQuote.desk.material.Equals(SearchMaterialCombo.Text)) 
                    {
                        SearchColumnDisplay.Items.Add(new ListViewItem(new[] { deskQuote.name, deskQuote.quoteDate.ToString("dd-MMM-yyyy"), deskQuote.desk.height.ToString() + "\"", deskQuote.desk.width.ToString() + "\"", deskQuote.desk.material, deskQuote.desk.rush, deskQuote.desk.drawers.ToString(), "$" + deskQuote.priceQuote }));
                    }
                }
            }
            catch (Exception j)
            {
                Console.WriteLine(j.ToString());
            }

        }

        private void SearchMaterialCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SearchMaterialChange(object sender, EventArgs e)
        {

        }

        private void SearchTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
