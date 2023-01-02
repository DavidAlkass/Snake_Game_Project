using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snakegame
{
    public partial class Form1 : Form
    {

        private List<Square> Snake = new List<Square>();
        private Foods food;

        int maxWidth;
        int maxHeight;

        int score;
        
        Random rand = new Random();

        bool MoveLeft, MoveRight, MoveDown, MoveUp;



        public Form1()
        {
            InitializeComponent();
            food = new smallFood();
            new Settings();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left && Settings.directions != "right")
            {
                MoveLeft = true;
            }
            if (e.KeyCode == Keys.Right && Settings.directions != "left")
            {
                MoveRight = true;
            }
            if (e.KeyCode == Keys.Up && Settings.directions != "down")
            {
                MoveUp = true;
            }
            if (e.KeyCode == Keys.Down && Settings.directions != "up")
            {
                MoveDown = true;
            }



        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                MoveLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                MoveRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                MoveUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                MoveDown = false;
            }
        }

        private void StartGame(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            // Setting directions

            if (MoveLeft)
            {
                Settings.directions = "left";
            }
            if (MoveRight)
            {
                Settings.directions = "right";
            }
            if (MoveUp)
            {
                Settings.directions = "up";
            }
            if (MoveDown)
            {
                Settings.directions = "down";
            }
            // end of directions

            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {

                    switch (Settings.directions)
                    {
                        case "left":
                            Snake[i].X--;
                            break;
                        case "right":
                            Snake[i].X++;
                            break;
                        case "up":
                            Snake[i].Y--;
                            break;
                        case "down":
                            Snake[i].Y++;
                            break;

                    }

                    if (Snake[i].X < 0)
                    {
                        Snake[i].X = maxWidth;
                    }
                    if (Snake[i].X > maxWidth)
                    {
                        Snake[i].X = 0;
                    }
                    if (Snake[i].Y < 0)
                    {
                        Snake[i].Y = maxHeight;
                    }
                    if (Snake[i].Y > maxHeight)
                    {
                        Snake[i].Y = 0;
                    }

                    if (Snake[i].X == food.X && Snake[i].Y == food.Y)
                    {
                        EatFood();
                    }

                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            GameOver();
                        }
                    }
                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }




            }

            gameFrame.Invalidate();



        }

        private void UpdatePictureFrame(object sender, PaintEventArgs e)
        {

            Graphics canvas = e.Graphics;

            Brush snakeColour;

            for (int i = 0; i  < Snake.Count; i++) 
            { 
                if (i == 0)
                {
                    snakeColour = Brushes.Black;
                }
                else 
                {
                    snakeColour = Brushes.Green;
                }

                canvas.FillRectangle(snakeColour, new Rectangle
                    (
                    Snake[i].X * Settings.Width,
                    Snake[i].Y * Settings.Height,
                    Settings.Width, Settings.Height
                    ));
            }

            Color color = Color.FromName(food.Colour);
            SolidBrush brush = new SolidBrush(color);
            canvas.FillEllipse(brush, new Rectangle
            (
            food.X * Settings.Width,
            food.Y * Settings.Height,
            Settings.Width, Settings.Height
            ));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void RestartGame()
        {
            maxWidth = gameFrame.Width / Settings.Width - 1;
            maxHeight = gameFrame.Height / Settings.Height - 1;

            Snake.Clear();

            StartButton.Enabled = false;
            score = 0;
            Scorebar.Text = "Score: " + score;

            Square head = new Square { X = 10, Y = 5 };
            Snake.Add(head); // Adding head of the snake to list

            for (int i = 0; i < 10; i++) 
            {
                Square body = new Square();
                Snake.Add(body);

            }

            food = new smallFood { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight)};

            GameTimer.Start();
        }

        private void EatFood()
        {
            if (food is smallFood)
            {
                score += 1;

                Square body = new Square
                {
                    X = Snake[Snake.Count - 1].X,
                    Y = Snake[Snake.Count - 1].Y
                };

                Snake.Add(body);
            }
            else if (food is bigFood)
            {
                score += 5;

                Square body = new Square
                {
                    X = Snake[Snake.Count - 1].X,
                    Y = Snake[Snake.Count - 1].Y
                };

                Snake.Add(body);

                Square body2 = new Square
                {
                    X = Snake[Snake.Count - 1].X,
                    Y = Snake[Snake.Count - 1].Y
                };

                Snake.Add(body2);
            }
            Scorebar.Text = "Score: " + score;
 

            if (rand.Next(2) == 1)
            {
                food = new smallFood { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };
            }
            else
            {
                food = new bigFood { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };
            }


        }

        private void GameOver()
        {
            GameTimer.Stop();
            Label gameover = new Label();
            gameover.Text = "GAME OVER";
            gameover.Font = new Font("Ariel", 20, FontStyle.Bold);
            gameover.AutoSize = false;
            gameover.Height = 100;
            gameover.Width = gameFrame.Width;
            gameover.TextAlign = ContentAlignment.MiddleCenter;
            gameFrame.Controls.Add(gameover);
            StartButton.Enabled = true;


        }
    }
}
