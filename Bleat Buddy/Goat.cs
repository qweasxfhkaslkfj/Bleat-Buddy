using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Bleat_Buddy 
{
    internal class Goat : UserControl
    {
        Button goat = new Button();
        int speed;
        // Текстуры козла
        static string projectRoot = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\"));
        private Image goatRight_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "textureRight.png"));
        private Image goatLeft_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "textureLeft.png"));
        // Блеяние
        private string audioPath = Path.Combine(projectRoot, "resurse", "bleat.wav");
        // Прыжок
        private Timer jumpTimer;
        private int jumpHeight = 100;
        private int jumpSpeed = 5;
        private int currentJumpStep = 0;
        private bool isJumping = false;
        private int startY;
        private int jumpCount = 0;
        private bool doubleJump = false;

        // Конструктор класса
        public Goat()
        {
            this.speed = 10;
            jumpTimer = new Timer();
            jumpTimer.Interval = 25;
            jumpTimer.Tick += JumpAnimation;
        }
        // Создание кнопки-козла
        public Button CreateGoat(Point location)
        {
            goat= new Button();
            goat.Size = new Size(90, 90);
            goat.Location = location;
            goat.BackColor = this.BackColor;
            goat.BackgroundImage = goatRight_Texture;
            goat.BackgroundImageLayout = ImageLayout.Stretch;
            goat.FlatStyle = FlatStyle.Flat;
            goat.FlatAppearance.BorderSize = 0;
            return goat;
        }

        // ToDo: добавить анимации
        // Обработка движения козла
        public void Goat_Movemant(Button goatBtn, KeyEventArgs e)
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
                        goat.FlatStyle = FlatStyle.Flat;
                        goat.FlatAppearance.BorderSize = 0;
                        break;
                    }
                    break;
                case Keys.D:
                    if (leftBound + 90 < Screen.PrimaryScreen.Bounds.Width)
                    {
                        goatBtn.Left += speed;
                        goat.BackgroundImage = goatRight_Texture;
                        goat.BackgroundImageLayout = ImageLayout.Stretch;
                        goat.FlatStyle = FlatStyle.Flat;
                        goat.FlatAppearance.BorderSize = 0;
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
        // ToDo: добавить движение влево-вправо во время прыжка
        // Прыжок
        private void Jump()
        {
            // Первый прыжок
            if (!isJumping)
            {
                startY = goat.Location.Y;
                currentJumpStep = 0;
                isJumping = true;
                jumpCount = 1;
                doubleJump = true;
                jumpTimer.Start();
            }
            // Второй прыжок
            else if (doubleJump && jumpCount == 1)
            {
                startY = goat.Location.Y;
                currentJumpStep = 0;
                jumpCount = 2;
                doubleJump = false;
            }
        }
        // Плавный прыжок
        private void JumpAnimation(object sender, EventArgs e)
        {
            float progress = (float)currentJumpStep / jumpHeight;
            float p = 4 * progress * (1 - progress);

            int newY = startY - (int)(p * jumpHeight);
            goat.Location = new Point(goat.Location.X, newY);

            currentJumpStep += jumpSpeed;

            if (currentJumpStep > jumpHeight)
            {
                jumpTimer.Stop();
                isJumping = false;
                jumpCount = 0;
                doubleJump = false;
                goat.Location = new Point(goat.Location.X, startY);
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
