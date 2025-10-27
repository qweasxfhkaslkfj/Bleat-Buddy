using System.Drawing;
using System.Windows.Forms;

namespace Bleat_Buddy
{
    internal class Gameplay : UserControl
    {
        Goat goat = new Goat();
        private Button goatBtn;
        private Timer fallTimer;
        public Button a = CreatePlatform(0, 991, 750, 150);
        public Button b = CreatePlatform(900, 991, 1920, 150);

        public Gameplay()
        {
            InitializeFallTimer();
        }

        public void FirstScreen()
        {
            Controls.Add(a);
            Controls.Add(b);
            goatBtn = goat.CreateGoat(new Point(90, 900));
            Controls.Add(goatBtn);
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

        // Падение
        public void Falling()
        {
            if (!IsOnPlatform(goatBtn, a) && !IsOnPlatform(goatBtn, b))
            {
                goatBtn.Top += 10;
            }
            if (goatBtn.Top == 1080)
            {
                End();
            }
        }
        // Проверка нахождения на платформе
        private bool IsOnPlatform(Button goat, Button platform)
        {
            return goat.Bottom >= platform.Top &&
                   goat.Bottom <= platform.Bottom &&
                   goat.Right > platform.Left &&
                   goat.Left < platform.Right;
        }
        // Создание платформ
        private static Button CreatePlatform(int x, int y, int w, int h)
        {
            Button platform = new Button();
            platform.Location = new Point(x, y);
            platform.Size = new Size(w, h);
            platform.Visible = true;
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
                Controls.Clear();
                Dock = DockStyle.Fill;
                FirstScreen();
            }
            else if (result == DialogResult.Cancel)
            {
                Form1 mainForm = this.Parent as Form1;
                fallTimer.Stop();
                Controls.Clear();

                if (mainForm != null)
                {
                    mainForm.mainMenu();
                }
            }
        }

        // Нажатие на кнопки
        public void KeyPress(KeyEventArgs e)
        {
            goat.Goat_Movemant(goatBtn, e);
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