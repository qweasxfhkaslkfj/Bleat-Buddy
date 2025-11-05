using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Bleat_Buddy
{
    internal class Gameplay : UserControl
    {
        private PictureBox goatSprite;
        private System.Windows.Forms.Timer fallTimer;
        private System.Windows.Forms.Timer nearnessTimer;
        Goat goat = new Goat();
        Fire fire = new Fire();
        public PictureBox a = CreatePlatform(0, 991, 750, 150);
        public PictureBox b = CreatePlatform(900, 991, 1920, 150);
        int messCount = 1;

        public Gameplay()
        {
            InitializeFallTimer();
            InitializeNearnessTimer();
        }

        public void FirstScreen()
        {
            Controls.Add(a);
            Controls.Add(b);
            PictureBox fireBox = fire.CreateFire(450, 900);
            Controls.Add(fireBox);
            fireBox.SendToBack();

            goatSprite = goat.CreateGoat(new Point(90, 900));
            Controls.Add(goatSprite);
            goatSprite.BringToFront();
            a.SendToBack();
            b.SendToBack();

            fallTimer.Start();
            nearnessTimer.Start();

            //Controls.Clear();
            //fallTimer.Start();

            //Fire fire = new Fire();
            //fire.Dock = DockStyle.Fill;
            //fire.SetGameplayReference(this);

            //Controls.Add(fire);
            //fire.FireScreen();

            //goatSprite = goat.CreateGoat(new Point(90, 900));
            //Controls.Add(goatSprite);
            //goatSprite.BringToFront();

            //fallTimer.Start();
        }

        // Таймер для падения
        private void InitializeFallTimer()
        {
            fallTimer = new System.Windows.Forms.Timer();
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
            if (!IsOnPlatform(goatSprite, a) && !IsOnPlatform(goatSprite, b))
            {
                goatSprite.Top += 10;
                Thread.Sleep(5);
            }
            if (goatSprite.Top >= 1080 && messCount == 1)
            {
                messCount = 0;
                End();
            }
        }
        // Проверка нахождения на платформе
        private bool IsOnPlatform(PictureBox goat, PictureBox platform)
        {
            return goat.Bottom >= platform.Top &&
                   goat.Bottom <= platform.Bottom &&
                   goat.Right > platform.Left &&
                   goat.Left < platform.Right;
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

        // Меню паузы
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