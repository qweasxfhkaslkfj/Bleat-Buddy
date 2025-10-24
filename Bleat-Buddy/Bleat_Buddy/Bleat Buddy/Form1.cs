using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bleat_Buddy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            mainMenu();
        }


        // Главное меню
        public void mainMenu()
        {
            Controls.Clear(); // Очистка прошлых элементов

            // Создание элементов визуала
            Button startBtn = new Button();
            Button settingsBtn = new Button();
            Button exitBtn = new Button();
            Label gameName = new Label();

            // Кнопка "Начать"
            startBtn.Location = new Point(80, 230);
            startBtn.Size = new Size(190, 60);
            startBtn.Text = "Начать";
            startBtn.Visible = true;
            startBtn.Click += StartBtn_Click;
            Controls.Add(startBtn);

            // Кнопка "Настройки"
            settingsBtn.Location = new Point(80, 330);
            settingsBtn.Size = new Size(190, 60);
            settingsBtn.Text = "Настройки";
            settingsBtn.Visible = true;
            settingsBtn.Click += SettingsBtn_Click;
            Controls.Add(settingsBtn);

            // Кнопка "Выход"
            exitBtn.Location = new Point(80, 430);
            exitBtn.Size = new Size(190, 60);
            exitBtn.Text = "Выход";
            exitBtn.Visible = true;
            exitBtn.Click += ExitBtn_Click;
            Controls.Add(exitBtn);

            // Название игры
            gameName.Location = new Point(70, 140);
            gameName.Size = new Size(350, 60);
            gameName.Font = new Font("Segoe Script", 28);
            gameName.Text = "Bleat Buddy";
            Controls.Add(gameName);
        }

        // Кнопка начать игру
        private void StartBtn_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        // Кнопка выхода из игры
        private void ExitBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите уйти?", "Выход", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        // Кнопка настроек
        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            Controls.Clear();

            Settings settings = new Settings();
            settings.mainForm = this;
            settings.Dock = DockStyle.Fill;
            Controls.Add(settings);
            settings.settingsMenu();
        }
    }
}
