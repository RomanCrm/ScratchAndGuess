using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScratchAndGuess
{
    enum BrushSize
    {
        Width = 20,
        Height = 20
    }

    enum Locations
    {
        Half = 2,
        Quart = 4,
        Y = 25
    }

    public partial class Form1 : Form
    {
        Picture picture = new Picture();
        Move move = new Move();
        Coin coin = new Coin();

        Region pictureLayer;
        Region scratchLayer;

        TextureBrush textureBrush;
        SolidBrush solidBrush;

        Region mouse;

        List<Point> points = new List<Point>();

        char[] letters;
        int countClick = 0;

        public Form1()
        {
            DoubleBuffered = true;
            InitializeComponent();

            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            label5.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;

            picture.RandNewPict(ref textureBrush, ref pictureLayer, ref picture, ClientSize);

            CreateScratchLayer();

            NewWordHelp();
            label5.Text = letters.Length.ToString();

            Paint += Form1_Paint;
        }

        private void CreateScratchLayer()
        {
            solidBrush = new SolidBrush(Color.Gray);
            Rectangle rectangleScratch = new Rectangle(ClientSize.Width / (int)Locations.Quart, 40,
                                                       picture.Images[0].Width,
                                                       picture.Images[0].Height);
            scratchLayer = new Region(rectangleScratch);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.FillRegion(textureBrush, pictureLayer);
            g.FillRegion(solidBrush, scratchLayer);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            RectangleF region = scratchLayer.GetBounds(g);

            if (e.Button == MouseButtons.Left)
            {
                if (e.X >= region.X && e.Y >= region.Y)
                {
                    if (e.X <= region.X + region.Width && e.Y <= region.Y + region.Height)
                    {
                        Point point = new Point(e.X, e.Y);
                        points.Add(point);
                        GraphicsPath ellipse = new GraphicsPath();
                        Rectangle rect = new Rectangle(e.X - (int)BrushSize.Width / 2,
                                                       e.Y - (int)BrushSize.Height / 2,
                                                       (int)BrushSize.Width,
                                                       (int)BrushSize.Height);
                        ellipse.AddEllipse(rect);
                        mouse = new Region(ellipse);
                        scratchLayer.Exclude(mouse);
                        g.FillRegion(new SolidBrush(Color.Orange), mouse);
                    }
                }
            }
            else
            {
                Invalidate();
            }
        }

        private void TextChangedTextBox(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
            foreach (Image image in picture.Images)
            {
                if (textBox1.Text == image.Tag.ToString())
                {
                    picture.RandNewPict(ref textureBrush, ref pictureLayer, ref picture, ClientSize);
                    CreateScratchLayer();
                    textBox1.Text = String.Empty;
                    label1.Text = coin.AddCoin(label1.Text);
                    NewWordHelp();

                    label5.Text = letters.Length.ToString();

                    move.CountPict = int.Parse(label3.Text);
                    label3.Text = (move.Moves + move.CountPict).ToString();

                    countClick = 0;
                    Invalidate();
                }
            }
        }

        private void NewWordHelp()
        {
            letters = picture.Images[picture.IdxPict].Tag.ToString().ToCharArray();
        }

        private void HelpClicked(object sender, EventArgs e)
        {
            if (int.Parse(label1.Text) != 0)
            {
                countClick++;
                if (countClick - 1 >= 0 && countClick - 1 <= letters.Length)
                {
                    textBox1.Text += letters[countClick - 1];
                    label1.Text = coin.MinusCoin(label1.Text);
                }
            }
            else
            {
                MessageBox.Show("Недостаточно монет!");
            }
        }

    }
}
