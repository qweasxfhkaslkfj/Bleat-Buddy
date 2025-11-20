using System;
using System.Drawing;
using System.IO;
using System.Media;
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
        private Image lvl_1_goatRight_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "goats", "lvl_1_goatRight_Texture.png"));
        private Image lvl_1_goatLeft_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "goats", "lvl_1_goatLeft_Texture.png"));
        private Image lvl_2_goatRight_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "goats", "lvl_2_goatRight_Texture.png"));
        private Image lvl_2_goatLeft_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "goats", "lvl_2_goatLeft_Texture.png"));
        private Image lvl_3_goatRight_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "goats", "lvl_3_goatRight_Texture.png"));
        private Image lvl_3_goatLeft_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "goats", "lvl_3_goatLeft_Texture.png"));
        private Image lvl_4_goatRight_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "goats", "lvl_4_goatRight_Texture.png"));
        private Image lvl_4_goatLeft_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "goats", "lvl_4_goatLeft_Texture.png"));
        // Грязный козёл
        private Image lvl_1_dirtyGoatLeft_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "dirtyGoats", "lvl_1_dirtyGoatLeft_Texture.png"));
        private Image lvl_2_dirtyGoatLeft_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "dirtyGoats", "lvl_2_dirtyGoatLeft_Texture.png"));
        private Image lvl_3_dirtyGoatLeft_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "dirtyGoats", "lvl_3_dirtyGoatLeft_Texture.png"));
        private Image lvl_4_dirtyGoatLeft_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "dirtyGoats", "lvl_4_dirtyGoatLeft_Texture.png"));
        // Анимации козла
        // Козёл ходит
        private string lvl_1_goatWalkingRight = Path.Combine(projectRoot, "resurse", "walkingRight", "lvl_1_goatWalking");

        // Блеяние
        private string audioPath = Path.Combine(projectRoot, "resurse", "bleat.wav");
        // Прыжок
        private float verticalVelocity = 0;
        private float gravity = 1.5f;
        private float jumpForce = -18f;
        private bool isJumping = false;
        public bool isOnGround = false;
        private int jumpCount = 0;
        private const int maxJumps = 2;
        // Базовые показатели 
        public int energyPoint = 50;
        public int healthPoint = 2;
        public bool dirty = false;
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
        public PictureBox CreateGoat(Point location, int level)
        {
            goat = new PictureBox();

            switch (level)
            {
                case 1:
                    goat.Size = new Size(68, 68);
                    goat.BackgroundImage = lvl_1_goatRight_Texture;
                    break;
                case 2:
                    goat.Size = new Size(92, 100);
                    goat.BackgroundImage = lvl_2_goatRight_Texture;
                    break;
                case 3:
                    goat.Size = new Size(92, 120);
                    goat.BackgroundImage = lvl_3_goatRight_Texture;
                    break;
                case 4:
                    goat.Size = new Size(100, 120);
                    goat.BackgroundImage = lvl_4_goatRight_Texture;
                    break;
            }
            ;
            goat.Location = location;
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
                        switch (level)
                        {
                            case 1:
                                if (dirty)
                                {
                                    goat.BackgroundImage = lvl_1_dirtyGoatLeft_Texture;
                                }
                                else
                                {
                                    goat.BackgroundImage = lvl_1_goatLeft_Texture;
                                }
                                break;
                            case 2:
                                if (dirty)
                                {
                                    goat.BackgroundImage = lvl_2_dirtyGoatLeft_Texture;
                                }
                                else
                                {
                                    goat.BackgroundImage = lvl_2_goatLeft_Texture;
                                }
                                break;
                            case 3:
                                if (dirty)
                                {
                                    goat.BackgroundImage = lvl_3_dirtyGoatLeft_Texture;
                                }
                                else
                                {
                                    goat.BackgroundImage = lvl_3_goatLeft_Texture;
                                }
                                break;
                            case 4:
                                if (dirty)
                                {
                                    goat.BackgroundImage = lvl_4_dirtyGoatLeft_Texture;
                                }
                                else
                                {
                                    goat.BackgroundImage = lvl_4_goatLeft_Texture;
                                }
                                break;
                        }
                        ;
                        goat.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    break;
                case Keys.D:
                    if (leftBound + 90 < Screen.PrimaryScreen.Bounds.Width)
                    {
                        goatBtn.Left += speed;
                        switch (level)
                        {
                            case 1:
                                if (dirty)
                                {
                                    goat.BackgroundImage = lvl_1_dirtyGoatLeft_Texture;
                                }
                                else
                                {
                                    goat.BackgroundImage = lvl_1_goatRight_Texture;
                                }
                                break;
                            case 2:
                                if (dirty)
                                {
                                    goat.BackgroundImage = lvl_2_dirtyGoatLeft_Texture;
                                }
                                else
                                {
                                    goat.BackgroundImage = lvl_2_goatRight_Texture;
                                }
                                break;
                            case 3:
                                if (dirty)
                                {
                                    goat.BackgroundImage = lvl_3_dirtyGoatLeft_Texture;
                                }
                                else
                                {
                                    goat.BackgroundImage = lvl_3_goatRight_Texture;
                                }
                                break;
                            case 4:
                                if (dirty)
                                {
                                    goat.BackgroundImage = lvl_4_dirtyGoatLeft_Texture;
                                }
                                else
                                {
                                    goat.BackgroundImage = lvl_4_goatRight_Texture;
                                }
                                break;
                        };
                        goat.BackgroundImageLayout = ImageLayout.Stretch;
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
                case Keys.E:
                    break;
            }
        }

        // Прыжок
        private void Jump()
        {
            if (jumpCount < maxJumps)
            {
                verticalVelocity = jumpForce;
                isJumping = true;
                isOnGround = false;
                jumpCount++;

                if (jumpCount == 2)
                {
                    verticalVelocity = jumpForce * 0.7f;
                }
            }
        }
        // Физика прыжка
        public void UpdatePhysics()
        {
            verticalVelocity += gravity;

            if (goat != null)
            {
                goat.Top += (int)verticalVelocity;
            }
        }

        // Сброс состояния прыжка приземлении
        public void Land()
        {
            isJumping = false;
            isOnGround = true;
            jumpCount = 0;
            verticalVelocity = 0;
        }
        // Геттеры для проверки состояния
        public bool IsOnGround => isOnGround;
        public bool IsJumping => isJumping;
        public float VerticalVelocity => verticalVelocity;

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
