using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            settingsMenu();
        }
        // Кнопка назад
        private void BackBtn_Click(object sender, EventArgs e)
        {
            mainMenu();
        }

        // Главное меню
        private void mainMenu()
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
        // Настройки
        private void settingsMenu()
        {
            Controls.Clear(); // Очистка элементов

            // Создание текстов
            Label settings_Lbl = new Label();
            Label soundsOnOff_Lbl = new Label();
            Label musicOnOff_Lbl = new Label();
            Label soundVolume_Lbl = new Label();
            Label musicVolume_Lbl = new Label();
            Label soundVolumePercent_Lbl = new Label();
            Label musicVolumePercent_Lbl = new Label();
            Label inputDevice_Lbl = new Label();

            // Создание кнопок
            Button soundsOnOff_Btn= new Button();
            Button musicOnOff_Btn = new Button();
            Button soundMinusVolume_Btn = new Button();
            Button soundPlusVolume_Btn = new Button();
            Button musicMinusVolume_Btn = new Button();
            Button musicPlusVolume_Btn = new Button();
            Button inputDevice_Btn = new Button();
            Button captions_Btn = new Button();
            Button backBtn = new Button();

            // Настройки
            settings_Lbl.Location = new Point(55, 187);
            settings_Lbl.Size = new Size(350, 60);
            settings_Lbl.Font = new Font("Segoe Script", 28);
            settings_Lbl.Text = "Настройки";
            Controls.Add(settings_Lbl);
            // Включить-выключить звуки
            soundsOnOff_Lbl.Location = new Point(133, 300);
            soundsOnOff_Lbl.Size = new Size(350, 60);
            soundsOnOff_Lbl.Font = new Font("Segoe Script", 18);
            soundsOnOff_Lbl.Text = "Выключить звуки";
            Controls.Add(soundsOnOff_Lbl);
            // Включить-выключить музыку
            musicOnOff_Lbl.Location = new Point(133, 368);
            musicOnOff_Lbl.Size = new Size(350, 60);
            musicOnOff_Lbl.Font = new Font("Segoe Script", 18);
            musicOnOff_Lbl.Text = "Выключить музыку";
            Controls.Add(musicOnOff_Lbl);
            // Громкость звука
            soundVolume_Lbl.Location = new Point(133, 436);
            soundVolume_Lbl.Size = new Size(350, 60);
            soundVolume_Lbl.Font = new Font("Segoe Script", 18);
            soundVolume_Lbl.Text = "Громкость звуков";
            Controls.Add(soundVolume_Lbl);
            // Громкость музыки
            musicVolume_Lbl.Location = new Point(133, 504);
            musicVolume_Lbl.Size = new Size(350, 60);
            musicVolume_Lbl.Font = new Font("Segoe Script", 18);
            musicVolume_Lbl.Text = "Громкость музыки";
            Controls.Add(musicVolume_Lbl);
            // Громкость звука проценты
            soundVolumePercent_Lbl.Location = new Point(495, 434);
            soundVolumePercent_Lbl.Size = new Size(99, 38);
            soundVolumePercent_Lbl.Font = new Font("Segoe Script", 18);
            soundVolumePercent_Lbl.Text = "100 %";
            Controls.Add(soundVolumePercent_Lbl);
            // Громкость музыки проценты
            musicVolumePercent_Lbl.Location = new Point(495, 504);
            musicVolumePercent_Lbl.Size = new Size(99, 38);
            musicVolumePercent_Lbl.Font = new Font("Segoe Script", 18);
            musicVolumePercent_Lbl.Text = "100 %";
            Controls.Add(musicVolumePercent_Lbl);
            // Устройство ввода
            inputDevice_Lbl.Location = new Point(133, 571);
            inputDevice_Lbl.Size = new Size(254, 38);
            inputDevice_Lbl.Font = new Font("Segoe Script", 18);
            inputDevice_Lbl.Text = "Устройство ввода";
            Controls.Add(inputDevice_Lbl);

            // Кнопка назад
            backBtn.Location = new Point(65, 942);
            backBtn.Size = new Size(135, 38);
            backBtn.Text = "Назад";
            backBtn.Visible = true;
            backBtn.Click += BackBtn_Click;
            Controls.Add(backBtn);
        }
    }
}
