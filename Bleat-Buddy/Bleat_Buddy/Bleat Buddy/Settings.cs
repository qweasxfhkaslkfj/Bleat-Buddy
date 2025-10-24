using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bleat_Buddy
{
    internal class Settings : UserControl
    {
        private int soundVolume = 100;
        private int musicVolume = 100;
        private bool sound = true;
        private bool music = true;
        private string inputDevice = "Клавиатура";
        Label soundVolumePercent_Lbl = new Label();
        Label musicVolumePercent_Lbl = new Label();
        Button soundMinusVolume_Btn = new Button();
        Button soundPlusVolume_Btn = new Button();
        Button musicMinusVolume_Btn = new Button();
        Button musicPlusVolume_Btn = new Button();
        public Form1 mainForm { get; set; }

        // Меню настроек
        public void settingsMenu()
        {
            Controls.Clear(); // Очистка элементов

            settingLabels();
            settingsButtons();
        }
        // Создание текста и кнопок
        private void settingLabels()
        {
            // Создание текстов
            Label settings_Lbl = new Label();
            Label soundsOnOff_Lbl = new Label();
            Label musicOnOff_Lbl = new Label();
            Label soundVolume_Lbl = new Label();
            Label musicVolume_Lbl = new Label();
            Label inputDevice_Lbl = new Label();

            // Настройки
            settings_Lbl.Location = new Point(55, 187);
            settings_Lbl.Size = new Size(266, 60);
            settings_Lbl.Font = new Font("Segoe Script", 28);
            settings_Lbl.Text = "Настройки";
            Controls.Add(settings_Lbl);
            // Включить-выключить звуки
            soundsOnOff_Lbl.Location = new Point(133, 300);
            soundsOnOff_Lbl.Size = new Size(262, 38);
            soundsOnOff_Lbl.Font = new Font("Segoe Script", 18);
            soundsOnOff_Lbl.Text = "Звуки";
            Controls.Add(soundsOnOff_Lbl);
            // Включить-выключить музыку
            musicOnOff_Lbl.Location = new Point(133, 368);
            musicOnOff_Lbl.Size = new Size(292, 38);
            musicOnOff_Lbl.Font = new Font("Segoe Script", 18);
            musicOnOff_Lbl.Text = "Музыка";
            Controls.Add(musicOnOff_Lbl);
            // Громкость звука
            soundVolume_Lbl.Location = new Point(133, 436);
            soundVolume_Lbl.Size = new Size(259, 38);
            soundVolume_Lbl.Font = new Font("Segoe Script", 18);
            soundVolume_Lbl.Text = "Громкость звуков";
            Controls.Add(soundVolume_Lbl);
            // Громкость музыки
            musicVolume_Lbl.Location = new Point(133, 504);
            musicVolume_Lbl.Size = new Size(283, 38);
            musicVolume_Lbl.Font = new Font("Segoe Script", 18);
            musicVolume_Lbl.Text = "Громкость музыки";
            Controls.Add(musicVolume_Lbl);
            // Громкость звука проценты
            soundVolumePercent_Lbl.Location = new Point(495, 434);
            soundVolumePercent_Lbl.Size = new Size(99, 38);
            soundVolumePercent_Lbl.Font = new Font("Segoe Script", 18);
            soundVolumePercent_Lbl.Text = $"{soundVolume} %";
            Controls.Add(soundVolumePercent_Lbl);
            // Громкость музыки проценты
            musicVolumePercent_Lbl.Location = new Point(495, 504);
            musicVolumePercent_Lbl.Size = new Size(99, 38);
            musicVolumePercent_Lbl.Font = new Font("Segoe Script", 18);
            musicVolumePercent_Lbl.Text = $"{musicVolume} %";
            Controls.Add(musicVolumePercent_Lbl);
            // Устройство ввода
            inputDevice_Lbl.Location = new Point(133, 571);
            inputDevice_Lbl.Size = new Size(254, 38);
            inputDevice_Lbl.Font = new Font("Segoe Script", 18);
            inputDevice_Lbl.Text = "Устройство ввода";
            Controls.Add(inputDevice_Lbl);
        }
        private void settingsButtons()
        {
            // Создание кнопок
            Button soundsOnOff_Btn = new Button();
            Button musicOnOff_Btn = new Button();
            Button inputDevice_Btn = new Button();
            Button captions_Btn = new Button();
            Button backBtn = new Button();

            // Включить-выключить звуки
            soundsOnOff_Btn.Location = new Point(464, 302);
            soundsOnOff_Btn.Size = new Size(48, 38);
            soundsOnOff_Btn.Text = "On";
            soundsOnOff_Btn.Visible = true;
            soundsOnOff_Btn.Click += SoundsOnOff_Btn_Click;
            Controls.Add(soundsOnOff_Btn);
            // Включить-выключить Музыку
            musicOnOff_Btn.Location = new Point(464, 370);
            musicOnOff_Btn.Size = new Size(48, 38);
            musicOnOff_Btn.Text = "On";
            musicOnOff_Btn.Visible = true;
            musicOnOff_Btn.Click += MusicOnOff_Btn_Click;
            Controls.Add(musicOnOff_Btn);
            // Минус громкость звуков
            soundMinusVolume_Btn.Location = new Point(464, 436);
            soundMinusVolume_Btn.Size = new Size(25, 36);
            soundMinusVolume_Btn.Text = "-";
            soundMinusVolume_Btn.Visible = true;
            soundMinusVolume_Btn.Click += SoundMinusVolume_Btn_Click;
            Controls.Add(soundMinusVolume_Btn);
            // Плюс громкость звуков
            soundPlusVolume_Btn.Location = new Point(600, 437);
            soundPlusVolume_Btn.Size = new Size(25, 36);
            soundPlusVolume_Btn.Text = "+";
            soundPlusVolume_Btn.Visible = true;
            soundPlusVolume_Btn.Click += SoundPlusVolume_Btn_Click;
            Controls.Add(soundPlusVolume_Btn);
            // Минус громкость музыки
            musicMinusVolume_Btn.Location = new Point(464, 504);
            musicMinusVolume_Btn.Size = new Size(25, 36);
            musicMinusVolume_Btn.Text = "-";
            musicMinusVolume_Btn.Visible = true;
            musicMinusVolume_Btn.Click += MusicMinusVolume_Btn_Click;
            Controls.Add(musicMinusVolume_Btn);
            // Плюс громкость звуков
            musicPlusVolume_Btn.Location = new Point(600, 503);
            musicPlusVolume_Btn.Size = new Size(25, 36);
            musicPlusVolume_Btn.Text = "+";
            musicPlusVolume_Btn.Visible = true;
            musicPlusVolume_Btn.Click += MusicPlusVolume_Btn_Click;
            Controls.Add(musicPlusVolume_Btn);
            // Устройство ввода
            inputDevice_Btn.Location = new Point(464, 571);
            inputDevice_Btn.Size = new Size(161, 38);
            inputDevice_Btn.Text = "Клавиатура";
            inputDevice_Btn.Visible = true;
            inputDevice_Btn.Click += InputDevice_Btn_Click;
            Controls.Add(inputDevice_Btn);
            // Титры
            captions_Btn.Location = new Point(140, 634);
            captions_Btn.Size = new Size(276, 38);
            captions_Btn.Text = "Титры";
            captions_Btn.Visible = true;
            captions_Btn.Click += Captions_Btn_Click;
            Controls.Add(captions_Btn);
            // Кнопка назад
            backBtn.Location = new Point(65, 942);
            backBtn.Size = new Size(135, 38);
            backBtn.Text = "Назад";
            backBtn.Visible = true;
            backBtn.Click += BackBtn_Click;
            Controls.Add(backBtn);
        }


        // Кнопка Вкл/Выкл звука
        private void SoundsOnOff_Btn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (sound)
            {
                button.Text = "Off";
                sound = false;
                soundMinusVolume_Btn.Enabled = false;
                soundPlusVolume_Btn.Enabled = false;
            }
            else
            {
                button.Text = "On";
                sound = true;
                soundMinusVolume_Btn.Enabled = true;
                soundPlusVolume_Btn.Enabled = true;
            }
        }
        // Кнопка Вкл/Выкл музыки
        private void MusicOnOff_Btn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (music)
            {
                button.Text = "Off";
                music = false;
                musicMinusVolume_Btn.Enabled = false;
                musicPlusVolume_Btn.Enabled = false;
            }
            else
            {
                button.Text = "On";
                music = true;
                musicMinusVolume_Btn.Enabled = true;
                musicPlusVolume_Btn.Enabled = true;
            }
        }
        // Кнопка минус звука
        private void SoundMinusVolume_Btn_Click(object sender, EventArgs e)
        {
            if (soundVolume != 0)
            {
                soundVolume -= 5;
                soundVolumePercent_Lbl.Text = $"{soundVolume} %";
            }
        }
        // Кнопка плюс звука
        private void SoundPlusVolume_Btn_Click(object sender, EventArgs e)
        {
            if (soundVolume != 100)
            {
                soundVolume += 5;
                soundVolumePercent_Lbl.Text = $"{soundVolume} %";
            }
        }
        // Кнопка минус музыки
        private void MusicMinusVolume_Btn_Click(object sender, EventArgs e)
        {
            if (musicVolume != 0)
            {
                musicVolume -= 5;
                musicVolumePercent_Lbl.Text = $"{musicVolume} %";
            }
        }
        // Кнопка плюс музыки
        private void MusicPlusVolume_Btn_Click(object sender, EventArgs e)
        {
            if (musicVolume != 100)
            {
                musicVolume += 5;
                musicVolumePercent_Lbl.Text = $"{musicVolume} %";
            }
        }
        // Кнопка устройство ввода
        private void InputDevice_Btn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (inputDevice == "Клавиатура")
            {
                inputDevice = "Геймпад";
                button.Text = inputDevice;
            }
            else
            {
                inputDevice = "Клавиатура";
                button.Text = inputDevice;
            }
        }
        // Титры
        private void Captions_Btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Титры будут...");
        }
        // Кнопка назад
        private void BackBtn_Click(object sender, EventArgs e)
        {
            mainForm.mainMenu();
        }
    }
}
