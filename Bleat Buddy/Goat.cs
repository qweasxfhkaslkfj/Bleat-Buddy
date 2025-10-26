using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Bleat_Buddy 
{
    internal class Goat : UserControl
    {
        private static string gameDirectory = Directory.GetCurrentDirectory();
        // Текстуры козла
        static string projectRoot = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\"));
        private Image goatRight_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "textureRight.png"));
        private Image goatLeft_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "textureLeft.png"));
        Button goat = new Button();
        private int level;
        int speed;
        // Словарь возраст - текстура козла
        private Dictionary<string, string> goatTypes = new Dictionary<string, string>
        {
            { "little", "littleGoat.png"},
            { "teenager", "teenagerGoat.png"},
            { "adult", "adultGoat.png"},
            { "elderly", "elderlyGoat.png"}
        };

        // Конструктор класса
        public Goat()
        {
            this.speed = 10;
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
            }
        }
    }
}
