using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dungeon_Crawler
{
    public partial class Options : Form
    {
        public static int GameSizeWidth=1024;
        public static int GameSizeHeight=600;
        private int gameSizeIndex = 2;
        List<Size> gameSizes = new List<Size>();

        List<Label> buttonlist = new List<Label>();
        int labelindex = 0;

        public Options()
        {
            InitializeComponent();
            this.Size = new Size(800, 600);

            this.label2.Text = "> " + label2.Text;

            buttonlist.Add(label2);
            buttonlist.Add(label3);
            buttonlist.Add(label6);
            buttonlist.Add(label7);
            buttonlist.Add(label4);

            gameSizes.Add(new Size(640, 480));
            gameSizes.Add(new Size(800, 600));
            gameSizes.Add(new Size(1024, 600));
            gameSizes.Add(new Size(1024, 800));
            gameSizes.Add(new Size(1280, 800));
            gameSizes.Add(new Size(1280, 1024));
            gameSizes.Add(new Size(1600, 900));
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
            Program.w = gameSizes[gameSizeIndex].Width;
            Program.h = gameSizes[gameSizeIndex].Height;
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

            if (gameSizeIndex + 1 < gameSizes.Count)
            {
                label8.Text = gameSizes[gameSizeIndex + 1].Width + "x" + gameSizes[gameSizeIndex + 1].Height;
                gameSizeIndex++;
            }
            else
            {
                label8.Text = gameSizes[0].Width + "x" + gameSizes[0].Height;
                gameSizeIndex = 0;
            }
        }

        private void Options_KeyDown(object sender, KeyEventArgs e)
        {
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

            if (e.KeyData == Keys.Enter)
            {
                if (labelindex == 2)
                {
                    if (gameSizeIndex + 1 < gameSizes.Count)
                    {
                        label8.Text = gameSizes[gameSizeIndex + 1].Width + "x" + gameSizes[gameSizeIndex + 1].Height;
                        gameSizeIndex++;
                    }
                    else
                    {
                        label8.Text = gameSizes[0].Width + "x" + gameSizes[0].Height;
                        gameSizeIndex = 0;
                    }
                }
                if (labelindex == 3)
                {
                    
                }
                if (labelindex == 4)
                {
                    Program.w = gameSizes[gameSizeIndex].Width;
                    Program.h = gameSizes[gameSizeIndex].Height;
                    this.Close();
                }
            }
        }
    }
}
