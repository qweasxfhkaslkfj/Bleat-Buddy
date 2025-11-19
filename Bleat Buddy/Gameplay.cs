using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bleat_Buddy
{
    internal class Gameplay : UserControl
    {
        public PictureBox goatSprite;
        private Timer fallTimer;
        private Timer nearnessTimer;
        private Label hpLabel;
        public Goat goat = new Goat();
        Fire fire = new Fire();
        public PictureBox a = CreatePlatform(0, 991, 750, 150);
        public PictureBox b = CreatePlatform(900, 991, 1920, 150);
        public PictureBox c = CreatePlatform(800, 900, 150, 25);
        int messCount = 1;

        public Gameplay()
        {
            InitializeFallTimer();
            InitializeNearnessTimer();
        }

        public void FirstScreen()
        {
            //Controls.Add(a);
            //Controls.Add(b);
            //Controls.Add(c);
            //PictureBox fireBox = fire.CreateFire(450, 931);
            //Controls.Add(fireBox);
            //fireBox.SendToBack();

            //goatSprite = goat.CreateGoat(new Point(90, 870), goat.level);
            //Controls.Add(goatSprite);
            //goatSprite.BringToFront();
            //a.SendToBack();
            //b.SendToBack();
            //c.SendToBack();

            //fallTimer.Start();
            //nearnessTimer.Start();

            Controls.Clear();
            fallTimer.Start();

            Fire fire = new Fire();
            fire.Dock = DockStyle.Fill;
            fire.SetGameplayReference(this);

            Controls.Add(fire);
            fire.FireScreen();

            goatSprite = goat.CreateGoat(new Point(90, 870), goat.level);
            Controls.Add(goatSprite);
            goatSprite.BringToFront();

            CreateUserBar();

            fallTimer.Start();
        }

        // Таймер для падения
        private void InitializeFallTimer()
        {
            fallTimer = new Timer();
            fallTimer.Interval = 50;
            fallTimer.Tick += FallTimer_Tick;
        }
        private void FallTimer_Tick(object sender, System.EventArgs e)
        {
            Falling();
        }

        // Проверка нахождения рядом с костром
        private void InitializeNearnessTimer()
        {
            nearnessTimer = new System.Windows.Forms.Timer();
            nearnessTimer.Interval = 500;
            nearnessTimer.Tick += NearnessTimer_Tick;
        }
        private void NearnessTimer_Tick(object sender, EventArgs e)
        {
            int fireX = 450;
            int fireY = 900;

            int goatX = goatSprite.Left;
            int goatY = goatSprite.Top;

            int distance = (int)Math.Sqrt(Math.Pow(goatX - fireX, 2) + Math.Pow(goatY - fireY, 2));

            if (distance < 100)
            {
                fire.IsNear(goatX, goatY, fireX, fireY);
            }

        }

        // Падение
        public void Falling()
        {
            goat.UpdatePhysics();

            CheckPlatformCollisions();

            if (goatSprite.Top >= 1080 && messCount == 1)
            {
                messCount = 0;
                End();
            }
        }

        private void CheckPlatformCollisions()
        {
            bool landed = false;

            PictureBox[] platforms = { a, b, c };

            foreach (var platform in platforms)
            {
                if (IsOnPlatform(goatSprite, platform))
                {
                    if (goat.VerticalVelocity > 0 &&
                        goatSprite.Bottom <= platform.Top + 15)
                    {
                        goatSprite.Top = platform.Top - goatSprite.Height;
                        goat.Land();
                        landed = true;
                        break;
                    }
                }
            }

            if (!landed && goatSprite.Top < 1080 - goatSprite.Height)
            {
                goat.isOnGround = false;
            }

            if (goatSprite.Bottom >= 1080 && goat.VerticalVelocity > 0)
            {
                goatSprite.Top = 1080 - goatSprite.Height;
                goat.Land();
            }
        }

        // Проверка нахождения на платформе
        private bool IsOnPlatform(PictureBox goat, PictureBox platform)
        {
            bool isAbove = goat.Bottom >= platform.Top - 10 &&
                          goat.Bottom <= platform.Top + 15;
            bool isHorizontallyAligned = goat.Right > platform.Left + 10 &&
                                        goat.Left < platform.Right - 10;

            return isAbove && isHorizontallyAligned;
        }

        // Создание платформ
        private static PictureBox CreatePlatform(int x, int y, int w, int h)
        {
            PictureBox platform = new PictureBox();
            platform.Location = new Point(x, y);
            platform.Size = new Size(w, h);
            platform.Visible = true;
            platform.BackColor = Color.Green;
            return platform;
        }

        // ВРЕМЕННОЕ меню паузы
        private void PauseMenu()
        {
            fallTimer.Stop();

            Label pauseLbl = new Label();
            Button continueBtn = new Button();
            Button settingsBtn = new Button();
            Button exitBtn = new Button();
            BackColor = Color.Black;

            // Пауза
            pauseLbl.Location = new Point(890, 390);
            pauseLbl.Size = new Size(148, 60);
            pauseLbl.Font = new Font("Segoe Script", 28);
            pauseLbl.Text = "Пауза";
            Controls.Add(pauseLbl);

            // Продолжить
            continueBtn.Location = new Point(540, 500);
            continueBtn.Size = new Size(250, 80);
            continueBtn.Text = "Продолжить";
            continueBtn.Visible = true;
            Controls.Add(continueBtn);

            // Настройки
            settingsBtn.Location = new Point(835, 500);
            settingsBtn.Size = new Size(250, 80);
            settingsBtn.Text = "Настройки";
            settingsBtn.Visible = true;
            Controls.Add(settingsBtn);

            // Выход
            exitBtn.Location = new Point(1130, 500);
            exitBtn.Size = new Size(250, 80);
            exitBtn.Text = "Выйти";
            exitBtn.Visible = true;
            Controls.Add(exitBtn);
        }
        // ВРЕМЕННОЕ меню смерти
        private void End()
        {
            DialogResult result = MessageBox.Show("Вы упали", "Конец", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                messCount = 1;
                Controls.Clear();
                Dock = DockStyle.Fill;
                FirstScreen();
            }
            else if (result == DialogResult.Cancel)
            {
                messCount = 1;
                Form1 mainForm = this.Parent as Form1;
                fallTimer.Stop();
                Controls.Clear();

                if (mainForm != null)
                {
                    mainForm.mainMenu();
                }
            }
        }
        // ВРЕМЕННОЕ меню интерфейса

        // Метод для создания UserBar
        private void CreateUserBar()
        {
            if (hpLabel != null)
            {
                Controls.Remove(hpLabel);
                hpLabel.Dispose();
            }

            hpLabel = new Label();
            hpLabel.Location = new Point(70, 140);
            hpLabel.Size = new Size(350, 60);
            hpLabel.Font = new Font("Segoe Script", 28);
            hpLabel.ForeColor = Color.Black;
            hpLabel.BackColor = Color.Transparent;
            hpLabel.Text = $"Здоровье: {goat.healthPoint}";

            Controls.Add(hpLabel);
            hpLabel.BringToFront();
        }

        // Метод для обновления HP
        public void UpdateHealthDisplay()
        {
            if (hpLabel != null && !hpLabel.IsDisposed)
            {
                hpLabel.Text = $"Здоровье: {goat.healthPoint}";
            }
        }

        // Показать/спрятать козла
        public void HideGoat()
        {
            if (goatSprite != null)
            {
                goatSprite.Visible = false;
            }
        }
        public void ShowGoat()
        {
            if (goatSprite != null)
            {
                goatSprite.Visible = true;
            }
        }

        // Нажатие на кнопки
        public void KeyPress(KeyEventArgs e)
        {
            goat.Goat_Movemant(goatSprite, e);
        }

        public void EscapeButton(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Controls.Clear();
                    PauseMenu();
                    break;
            }
        }
    }
}
