using System;
using System.Drawing;
using System.IO;
using System.Reflection.Emit;
using System.Threading;
using System.Windows.Forms;

namespace Bleat_Buddy
{
    internal class Gameplay : UserControl
    {
        public PictureBox goatSprite;
        public System.Windows.Forms.Timer fallTimer;
        private System.Windows.Forms.Timer nearnessTimer;
        private System.Windows.Forms.Label hpLabel;
        public Goat goat = new Goat();
        Fire fire = new Fire();
        public PictureBox a = CreatePlatform(0, 991, 750, 150);
        public PictureBox b = CreatePlatform(900, 991, 1920, 150);
        //public PictureBox c = CreatePlatform(800, 900, 150, 25);
        public PictureBox bathroom_Floar = CreatePlatform(0, 900, 1920, 150);
        int messCount = 1;
        private static string projectRoot = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\"));
        private bool isNearBath = false;

        private bool isBathAnimating = false;
        private System.Windows.Forms.Timer bathAnimationTimer;
        private int bathAnimationFrame = 0;
        private PictureBox currentBath;
        private Image bath1, bath2, bath3;
        private StatsUI statsUI;

        public Gameplay()
        {
            InitializeFallTimer();
            InitializeNearnessTimer();

            statsUI = new StatsUI(goat);
            statsUI.SetPosition(new Point(10, 10));

            goat.SetGameplayReference(this);
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

            fallTimer.Start();
            nearnessTimer.Start();
            goat.StartEnergyTimer();

            Controls.Clear();
            fallTimer.Start();

            Fire fire = new Fire();
            fire.Dock = DockStyle.Fill;
            fire.SetGameplayReference(this);
            fire.SetGoatReference(this.goat);
            Controls.Add(fire);

            fire.FireScreen();
            goatSprite = goat.CreateGoat(new Point(90, 870), goat.level);
            Controls.Add(goatSprite);
            goatSprite.BringToFront();

            Controls.Add(statsUI);
            statsUI.BringToFront();

            fallTimer.Start();

            //Bathroom();
        }

        public void Bathroom()
        {
            Image bathroom_Background = Image.FromFile(Path.Combine(projectRoot, "resurse", "backgrounds", "bathroom.png"));

            Controls.Clear();

            PictureBox bathroom = new PictureBox();
            bathroom.Dock = DockStyle.Fill;
            bathroom.BackgroundImage = bathroom_Background;
            bathroom.BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(bathroom);

            PictureBox bath = fire.CreateBath(960, 520);
            bath.Tag = "bath";
            Controls.Add(bath);
            bath.BringToFront();

            goatSprite = goat.CreateGoat(new Point(85, 600), goat.level);
            Controls.Add(goatSprite);
            goatSprite.BringToFront();

            bathroom_Floar = CreatePlatform(0, 850, 1920, 50);
            Controls.Add(bathroom_Floar);
            bathroom_Floar.SendToBack();

            fallTimer.Start();
            nearnessTimer.Start();
        }
        private void CheckBathNearness()
        {
            PictureBox bath = null;
            foreach (Control control in Controls)
            {
                if (control is PictureBox && control.Tag?.ToString() == "bath")
                {
                    bath = (PictureBox)control;
                    break;
                }
            }

            if (bath != null)
            {
                int bathCenterX = bath.Left + bath.Width / 2;
                int bathCenterY = bath.Top + bath.Height / 2;

                int goatCenterX = goatSprite.Left + goatSprite.Width / 2;
                int goatCenterY = goatSprite.Top + goatSprite.Height / 2;

                int distance = (int)Math.Sqrt(Math.Pow(goatCenterX - bathCenterX, 2) + Math.Pow(goatCenterY - bathCenterY, 2));

                isNearBath = distance < 150;
            }
            else
            {
                isNearBath = false;
            }
        }
        public void InteractWithBath()
        {
            if (isNearBath && !isBathAnimating)
            {
                currentBath = null;
                foreach (Control control in Controls)
                {
                    if (control is PictureBox && control.Tag?.ToString() == "bath")
                    {
                        currentBath = (PictureBox)control;
                        break;
                    }
                }

                if (currentBath != null)
                {
                    if (bath1 == null || bath2 == null || bath3 == null)
                    {
                        string bathImagePath1 = Path.Combine(projectRoot, "resurse", "washing", "bath1.png");
                        string bathImagePath2 = Path.Combine(projectRoot, "resurse", "washing", "bath2.png");
                        string bathImagePath3 = Path.Combine(projectRoot, "resurse", "washing", "bath3.png");

                        if (!File.Exists(bathImagePath1) || !File.Exists(bathImagePath2) || !File.Exists(bathImagePath3))
                        {
                            MessageBox.Show("Файлы анимации ванны не найдены!");
                            return;
                        }

                        try
                        {
                            bath1 = Image.FromFile(bathImagePath1);
                            bath2 = Image.FromFile(bathImagePath2);
                            bath3 = Image.FromFile(bathImagePath3);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка загрузки изображений: {ex.Message}");
                            return;
                        }
                    }

                    isBathAnimating = true;
                    bathAnimationFrame = 0;

                    bathAnimationTimer = new System.Windows.Forms.Timer();
                    bathAnimationTimer.Interval = 100;
                    bathAnimationTimer.Tick += BathAnimationTimer_Tick;
                    bathAnimationTimer.Start();
                }
            }
        }
        private void BathAnimationTimer_Tick(object sender, EventArgs e)
        {
            if (currentBath == null || bath1 == null || bath2 == null || bath3 == null)
            {
                StopBathAnimation();
                return;
            }

            try
            {
                if (bathAnimationFrame < 12)
                {
                    if (bathAnimationFrame % 2 == 0)
                    {
                        currentBath.BackgroundImage = bath2;
                    }
                    else
                    {
                        currentBath.BackgroundImage = bath3;
                    }

                    bathAnimationFrame++;
                }
                else
                {
                    currentBath.BackgroundImage = bath1;
                    StopBathAnimation();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка анимации: {ex.Message}");
                StopBathAnimation();
            }
        }
        private void StopBathAnimation()
        {
            if (bathAnimationTimer != null)
            {
                bathAnimationTimer.Stop();
                bathAnimationTimer.Dispose();
                bathAnimationTimer = null;
            }
            isBathAnimating = false;
            bathAnimationFrame = 0;

            goat.dirty = false;
            UpdateStatsDisplay();
            goat.Refresh();
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
            goat.UpdatePhysics();
            CheckPlatformCollisions();
            CheckBathNearness();
            CheckDeathConditions();
        }


        private void CheckPlatformCollisions()
        {
            bool landed = false;

            PictureBox[] platforms = { a, b };
            //PictureBox[] platforms = { bathroom_Floar };

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

            //if (goatSprite.Bottom >= 1080 && goat.VerticalVelocity > 0)
            //{
            //    goatSprite.Top = 1080 - goatSprite.Height;
            //    goat.Land();
            //}
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

            System.Windows.Forms.Label pauseLbl = new System.Windows.Forms.Label();
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
        // Метод смерти
        public void Death()
        {
            fallTimer.Stop();
            nearnessTimer.Stop();
            goat.StopEnergyTimer();

            StopBathAnimation();

            // Запускаем анимацию смерти
            StartDeathAnimation();
        }

        // Анимация смерти 1
        private void StartDeathAnimation()
        {
            PictureBox deathOverlay = new PictureBox();
            deathOverlay.Size = new Size(10, 10);
            deathOverlay.Location = new Point(
                (this.Width - deathOverlay.Width) / 2,
                (this.Height - deathOverlay.Height) / 2
            );
            deathOverlay.BackColor = Color.Black;
            deathOverlay.Visible = true;

            Controls.Add(deathOverlay);
            deathOverlay.BringToFront();

            System.Threading.Thread animationThread = new System.Threading.Thread(() => DeathAnimation(deathOverlay));
            animationThread.Start();
        }
        // Анимация смерти 2
        private void DeathAnimation(PictureBox deathBox)
        {
            int steps = 50;
            int maxWidth = this.Width;
            int maxHeight = this.Height;

            for (int i = 1; i <= steps; i++)
            {
                this.Invoke(new Action(() =>
                {
                    int newWidth = 10 + (maxWidth - 10) * i / steps;
                    int newHeight = 10 + (maxHeight - 10) * i / steps;

                    deathBox.Size = new Size(newWidth, newHeight);
                    deathBox.Location = new Point(
                        (this.Width - newWidth) / 2,
                        (this.Height - newHeight) / 2
                    );

                    this.Refresh();
                }));

                System.Threading.Thread.Sleep(30);
            }

            System.Threading.Thread.Sleep(500);

            this.Invoke(new Action(() =>
            {
                HideGoat();

                PictureBox deathScreen = new PictureBox();
                deathScreen.Size = new Size(this.Width, this.Height);
                deathScreen.Location = new Point(0, 0);
                deathScreen.BackgroundImageLayout = ImageLayout.Stretch;
                Controls.Add(deathScreen);
                deathScreen.BringToFront();

                for (int i = 1; i <= 10; i++)
                {
                    string imagePath = Path.Combine(projectRoot, "resurse", "death", $"ded_dead{i}.png");
                    if (File.Exists(imagePath))
                    {
                        deathScreen.BackgroundImage = Image.FromFile(imagePath);
                        this.Refresh();
                        Thread.Sleep(150);
                    }
                }

                Controls.Remove(deathScreen);
                deathScreen.Dispose();

                DialogResult result = MessageBox.Show("Козлик умер! Возврат в главное меню.", "Смерть",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                Controls.Remove(deathBox);
                deathBox.Dispose();

                messCount = 1;
                Form1 mainForm = this.Parent as Form1;
                Controls.Clear();

                if (mainForm != null)
                {
                    mainForm.mainMenu();
                }
            }));
        }
        // Проверка условий смерти
        private void CheckDeathConditions()
        {
            if (goatSprite.Top >= 1080)
            {
                if (goat.healthPoint == 1 || goat.isSick)
                {
                    Death();
                    return;
                }
                else if (goat.healthPoint > 1)
                {
                    goat.healthPoint--;
                    goatSprite.Location = new Point(90, 870);

                    UpdateStatsDisplay();
                }
            }
        }

        // ВРЕМЕННОЕ меню интерфейса
        public void UpdateStatsDisplay()
        {
            if (statsUI != null)
                statsUI.UpdateStats();
        }
        // Метод для создания UserBar
        private void CreateUserBar()
        {
            if (hpLabel != null)
            {
                Controls.Remove(hpLabel);
                hpLabel.Dispose();
            }

            hpLabel = new System.Windows.Forms.Label();
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

            if (e.KeyCode == Keys.E)
            {
                InteractWithBath();
            }
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopBathAnimation();

                bath1?.Dispose();
                bath2?.Dispose();
                bath3?.Dispose();

                bath1 = null;
                bath2 = null;
                bath3 = null;
            }
            base.Dispose(disposing);
        }
    }
}
