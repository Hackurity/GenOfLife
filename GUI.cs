using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GameOfLife
{

    public partial class GUI : Form
    {
        private Graphics graphics;
        private int resolution;
        private Logic Game;
        int x_, y_;
        public GUI()
        {
            InitializeComponent();
            Text = $"Game of life";
        }

        private void StartGame()
        {
            
            if (timer.Enabled)
                return;
            
            numericResolution.Enabled = false;
            numericDensity.Enabled = false;
            resolution = (int)numericResolution.Value;

            Game = new Logic
                (
                 rows: pictureBox1.Height / resolution,
                 cols: pictureBox1.Width / resolution,
                  (int) numericDensity.Value
                );
            Game.countGeneration = 0;
            Text = $"Generation{Game.countGeneration}";

            pictureBox1.MouseClick += OnPictureBoxClicked;

            void OnPictureBoxClicked(object sender, MouseEventArgs args)
{
                
                 x_ = args.Location.X;
                 y_ = args.Location.Y;
                textBox1.Text = x_.ToString()+" "+y_.ToString();
                
            }
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer.Enabled = true;
            timer.Start();

        }
        private void StopGame()
        {
            if (!timer.Enabled)
                return;
            timer.Stop();
            numericDensity.Enabled = true;
            numericResolution.Enabled = true;
        }
        private void DrowNextGeneration()
        {

            graphics.Clear(Color.Green);
            //var field = Game.GetCurrentGeneration();
            var population = Game.GetCurrentGeneration();
            for(int x = 0; x < population.GetLength(0); x++)
            {
                for(int y = 0; y < population.GetLength(1) ;y++)
                {
                    //textBox1.Text = population[x_, y_].alive.ToString();
                    //if(field[x,y].alive)
                    bool[] genomForColor = population[x, y].getGenom();
                    if (population[x, y].alive && (genomForColor[3] == true || genomForColor[4] == true))
                        graphics.FillRectangle(Brushes.Blue, x * resolution, y * resolution, resolution, resolution);
                    else if(population[x,y].alive)
                        graphics.FillRectangle(Brushes.Brown, x * resolution, y * resolution, resolution, resolution);

                }
            }



           
            Game.NextGeneration();
            pictureBox1.Refresh();
            Text = $"Generation{Game.countGeneration++}";
        }
            
            private void timer_Tick(object sender, EventArgs e)
            {
                //Game.NextGeneration();
                DrowNextGeneration();//когда тикает таймер вызываем метод DrowNextGeneration
            }

            private void buttonStart_Click(object sender, EventArgs e)
            {
                resolution = (int)numericResolution.Value;
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                graphics = Graphics.FromImage(pictureBox1.Image);
                


                StartGame();
                

            }

            private void buttonStop_Click(object sender, EventArgs e)
            {
                StopGame();
            }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
    }
