using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Dungeon_Crawler
{
    public partial class MainMenu : Form
    {
        List<Label> buttonlist = new List<Label>();
        int labelindex = 0;

            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            

        public MainMenu()
        {
            InitializeComponent();
            this.Size = new Size(800, 600);

            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "";
            // Enter the executable to run, including the complete path
            start.FileName = "D:\\Spiele\\stone_soup-tiles-0.11\\crawl.exe";
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;

            label2.Text = "> " + label2.Text;
            buttonlist.Add(label2);
            buttonlist.Add(label6);
            buttonlist.Add(label4);
            buttonlist.Add(label3);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Program.startgame = true;
            this.Close();
           
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void MainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (labelindex == 0)
                {
                    Program.startgame = true;
                    this.Close();
                }
                if (labelindex == 1)
                {
                    using (Process proc = Process.Start(start))
                    {
                        proc.WaitForExit();
                    }
                }
                if (labelindex == 2)
                {
                    this.Visible = false;
                    using (Options o = new Options())
                        o.ShowDialog();
                    this.Visible = true;
                }
                if (labelindex == 3)
                {
                    this.Close();
                }

                
            }

            if (e.KeyData == Keys.N )
            {
                Program.startgame = true;
                this.Close();
            }
            if (e.KeyData == Keys.Escape || e.KeyData == Keys.E)
            {
                this.Close();
            }
            if (e.KeyData == Keys.O)
            {
                this.Visible = false;
                using (Options o = new Options())
                    o.ShowDialog();
                this.Visible = true;
            }
            if (e.KeyData == Keys.Up)
            {
                if (labelindex > 0)
                {
                    
                    ChangeLabelIndex("+");
                }
            }
            if (e.KeyData == Keys.Down)
            {
                if (labelindex != buttonlist.Count - 1)
                {
                    
                    ChangeLabelIndex("-");
                }
            }
            

        }

        private void ChangeLabelIndex(string zeichen)
        {
            buttonlist[labelindex].Text = buttonlist[labelindex].Text.Substring(2, buttonlist[labelindex].Text.Length - 2);

            if (zeichen == "-")
                labelindex++;
            else
                labelindex--;
                
            
                buttonlist[labelindex].Text = "> " + buttonlist[labelindex].Text;
            

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            using (Options o = new Options())
                o.ShowDialog();
            this.Visible = true;
        }
    }
}
