using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace Bleat_Buddy 
{
    internal class Goat : UserControl
    {
        // ToDo: Вывести базовые показатели и возможность их изменения

        PictureBox goat;
        int speed;
        // Текстуры козла
        private static string projectRoot = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\"));
        private Image goatRight_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "textureRight.png"));
        private Image goatLeft_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "textureLeft.png"));
        // Блеяние
        private string audioPath = Path.Combine(projectRoot, "resurse", "bleat.wav");
        // Прыжок
        //private Timer jumpTimer;
        // Базовые показатели 
        public int energyPoint = 50;
        public int healthPoint = 2;
        public int dirtPoint = 0;
        public int medCount = 0;
        public int crystalsCount = 1;
        public int level = 2;
        public bool isSick = false;


        // Конструктор класса
        public Goat()
        {
            this.speed = 10;
        }
        // Создание козла
        public PictureBox CreateGoat(Point location)
        {
            goat = new PictureBox();
            goat.Size = new Size(90, 90);
            goat.Location = location;
            goat.BackgroundImage = goatRight_Texture;
            goat.BackgroundImageLayout = ImageLayout.Stretch;
            return goat;
        }

        // ToDo: добавить анимации
        // Обработка движения козла
        public void Goat_Movemant(PictureBox goatBtn, KeyEventArgs e)
        {
            int leftBound = goatBtn.Left;

            switch (e.KeyCode)
            {
                case Keys.A:
                    if (leftBound > 0)
                    {
                        goatBtn.Left -= speed;
                        goat.BackgroundImage = goatLeft_Texture;
                        goat.BackgroundImageLayout = ImageLayout.Stretch;
                        break;
                    }
                    break;
                case Keys.D:
                    if (leftBound + 90 < Screen.PrimaryScreen.Bounds.Width)
                    {
                        goatBtn.Left += speed;
                        goat.BackgroundImage = goatRight_Texture;
                        goat.BackgroundImageLayout = ImageLayout.Stretch;
                        break;
                    }
                    break;
                case Keys.Space:
                    Jump();
                    break;
                case Keys.W:
                    Butting();
                    break;
                case Keys.Q:
                    Bleating();
                    break;
            }

        }

        // ToDo:
        //       + Добавить двойной прыжок
        //       + добавить движение влево-вправо во время прыжка
        // Прыжок
        private void Jump()
        {
            int originalY = goat.Location.Y;
            int jumpSteps = 20;

            for (int i = 0; i < jumpSteps; i++)
            {
                goat.Top -= 5;
                Thread.Sleep(5);
            }

            for (int i = 0; i < jumpSteps; i++)
            {
                goat.Top += 5;
                Thread.Sleep(5);
            }
        }

        // Бадание
        private void Butting()
        {
            goat.Location = new Point(goat.Location.X + 30, goat.Location.Y);
        }
        // Блеяние
        private void Bleating()
        {
            if (File.Exists(audioPath))
            {
                var soundPlayer = new SoundPlayer(audioPath);
                try
                {
                    soundPlayer.Play();
                }
                finally
                {
                    soundPlayer.Dispose();
                }
            }
        }
    }
}
