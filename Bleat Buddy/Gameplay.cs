using System.Drawing;
using System.Windows.Forms;

namespace Bleat_Buddy
{
    internal class Gameplay : UserControl
    {
        Goat goat = new Goat();
        private Button goatBtn;

        public void FirstScreen()
        {
            goatBtn = goat.CreateGoat(new Point(90, 900));
            Controls.Add(goatBtn);
        }
        // Меню паузы
        private void PauseMenu()
        {
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
