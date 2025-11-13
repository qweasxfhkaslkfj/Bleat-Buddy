using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Bleat_Buddy
{
    internal class Fire : UserControl
    {
        private static string projectRoot = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\"));
        private string littleGoatFeed = Path.Combine(projectRoot, "resurse", "little-goat-Feed");
        private Image fire_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "fire.png"));
        private Gameplay gameplay;
        Button firstBtn, secondBtn, thirdBtn, fourthBtn, fifthBtn, exitBtn;
        Goat goat = new Goat();

        public PictureBox CreateFire(int x, int y)
        {
            PictureBox fire = new PictureBox();
            fire.Location = new Point(x, y);
            fire.Size = new Size(90, 90);
            fire.Visible = true;
            fire.BackgroundImage = fire_Texture;
            fire.BackgroundImageLayout = ImageLayout.Stretch;
            return fire;
        }
        public void FireScreen()
        {
            // Пол
            PictureBox floor = new PictureBox();
            floor.Location = new Point(0, 991);
            floor.Size = new Size(1920, 150);
            floor.Visible = true;
            floor.BackColor = Color.Brown;
            Controls.Add(floor);

            // ВРЕМЕННЫЕ КНОПКИ
            Button sleepBtn = new Button();
            Button feedBtn = new Button();
            Button medBtn = new Button();
            Button cleanBtn = new Button();
            Button clothesBtn = new Button();
            // Кнопка Сна
            sleepBtn.Location = new Point(494, 53);
            sleepBtn.Size = new Size(190, 80);
            sleepBtn.Text = "СОН";
            sleepBtn.Visible = true;
            sleepBtn.Click += SleepBtn_Click;
            Controls.Add(sleepBtn);
            // Кнопка покупки еды
            feedBtn.Location = new Point(699, 53);
            feedBtn.Size = new Size(190, 80);
            feedBtn.Text = "КОРМИТЬ";
            feedBtn.Visible = true;
            feedBtn.Click += FeedBtn_Click;
            Controls.Add(feedBtn);
            // Кнопка покупки лекарства
            medBtn.Location = new Point(903, 53);
            medBtn.Size = new Size(190, 80);
            medBtn.Text = "КУПИТЬ ЛЕКАРСТВА";
            medBtn.Visible = true;
            medBtn.Click += MedBtn_Click;
            Controls.Add(medBtn);
            // Кнопка купания
            cleanBtn.Location = new Point(1109, 53);
            cleanBtn.Size = new Size(190, 80);
            cleanBtn.Text = "КУПАНИЕ";
            cleanBtn.Visible = true;
            cleanBtn.Click += CleanBtn_Click;
            Controls.Add(cleanBtn);
            // Кнопка обновления гардероба
            clothesBtn.Location = new Point(1315, 53);
            clothesBtn.Size = new Size(190, 80);
            clothesBtn.Text = "ГАРДЕРОБ";
            clothesBtn.Visible = true;
            clothesBtn.Click += ClothesBtn_Click;
            Controls.Add(clothesBtn);
        }
        public void SetGameplayReference(Gameplay game)
        {
            gameplay = game;
        }

        public void IsNear(int goatX, int goatY, int fireX, int fireY)
        {
            FireScreen();
        }

        // ВРЕМЕННЫЙ ИНТЕРФЕЙС сна
        private void SleepBtn_Click(object sender, EventArgs e)
        {
            if (goat.energyPoint < 70)
            {
                MessageBox.Show("Козлик поспал!");
                goat.energyPoint = 100;
            }
            else
            {
                MessageBox.Show("Козлик ещё не очень устал, он не хочет спать");
            }
        }
        // ВРЕМЕННАЯ РЕАЛИЗАЦИЯ кормления
        private void FeedBtn_Click(object sender, EventArgs e)
        {
            if (goat.healthPoint < 3)
            {
                if (goat.crystalsCount < 1)
                {
                    MessageBox.Show("У вас не хватает осколков кристалла");
                }
                else
                {
                    DialogResult result = MessageBox.Show("Вы хотите восстановить здоровье за 1 осколок кристалла?", "Еда", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        int x = goat.Location.X;
                        int y = goat.Location.Y;
                        for (int i = 1; i < 23; i++)
                        {
                            PictureBox feed = new PictureBox();
                            feed.Size = new Size(122, 138);
                            feed.Location = new Point(x, y);
                            feed.BackgroundImage = Image.FromFile(Path.Combine(littleGoatFeed, $"feed{i}.png"));
                            feed.BackgroundImageLayout = ImageLayout.Stretch;
                            feed.BringToFront();
                            Controls.Add(feed);

                            Refresh();
                            Thread.Sleep(100);

                            Controls.Remove(feed);
                            feed.Dispose();
                        }
                        goat.healthPoint = 3;
                        goat.crystalsCount--;

                        gameplay.UpdateHealthDisplay();
                    }
                }
            }
            else
            {
                MessageBox.Show("Козлик полностью здоров!");
            }

        }
        // ВРЕМЕННЫЙ ИНТЕРФЕЙС покупки лекарств
        private void MedBtn_Click(object sender, EventArgs e)
        {
            if (goat.medCount < 3)
            {
                if (goat.crystalsCount < 1)
                {
                    MessageBox.Show("У вас не хватает осколков кристалла");
                }
                else
                {
                    DialogResult result = MessageBox.Show("Вы хотите купить лекарства за 1 осколок кристалла?", "Лекарства", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        goat.medCount = 3;
                        goat.crystalsCount--;
                        MessageBox.Show("У вас теперь 3 лекарства!");
                    }
                }
            }
            else
            {
                MessageBox.Show("У вас достаточно лекарственных бутылочек!");
            }
        }
        // ВРЕМЕННЫЙ ИНТЕРФЕЙС мытья
        private void CleanBtn_Click(object sender, EventArgs e)
        {
            if (goat.dirtPoint > 1)
            {
                DialogResult result = MessageBox.Show("Вы хотите помыть козлика?", "Мытьё", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    goat.dirtPoint = 0;
                    MessageBox.Show("Теперь козлик чистый!");
                }
            }
            else
            {
                MessageBox.Show("Козлик и так достаточно чистый!");
            }
        }
        // ВРЕМЕННЫЙ ИНТЕРФЕЙС гардероба
        private void ClothesBtn_Click(object sender, EventArgs e)
        {
            if (gameplay != null)
            {
                gameplay.HideGoat();
            }

            Controls.Clear();
            int level = goat.level;
            PictureBox goatSuit = new PictureBox();
            goatSuit.Location = new Point(699, 319);
            goatSuit.Size = new Size(597, 508);
            goatSuit.Visible = true;
            goatSuit.BackColor = Color.Green;
            Controls.Add(goatSuit);
            GoatSuitsBtns();

            switch (level)
            {
                case 1:
                    firstBtn.Visible = true;
                    break;
                case 2:
                    firstBtn.Visible = true;
                    secondBtn.Visible = true;
                    break;
                case 3:
                    firstBtn.Visible = true;
                    secondBtn.Visible = true;
                    thirdBtn.Visible = true;
                    break;
                case 4:
                    firstBtn.Visible = true;
                    secondBtn.Visible = true;
                    thirdBtn.Visible = true;
                    fourthBtn.Visible = true;
                    break;
                case 5:
                    firstBtn.Visible = true;
                    secondBtn.Visible = true;
                    thirdBtn.Visible = true;
                    fourthBtn.Visible = true;
                    fifthBtn.Visible = true;
                    break;
                default:
                    firstBtn.Visible = true;
                    secondBtn.Visible = true;
                    thirdBtn.Visible = true;
                    fourthBtn.Visible = true;
                    fifthBtn.Visible = true;
                    break;
            }

            exitBtn.Visible = true;
        }

        private void GoatSuitsBtns()
        {
            firstBtn = new Button();
            secondBtn = new Button();
            thirdBtn = new Button();
            fourthBtn = new Button();
            fifthBtn = new Button();
            exitBtn = new Button();

            // ПЕРВЫЙ костюм
            firstBtn.Location = new Point(494, 53);
            firstBtn.Size = new Size(190, 80);
            firstBtn.Text = "ПЕРВЫЙ";
            firstBtn.Visible = false;
            firstBtn.Click += FirstBtn_Click;
            Controls.Add(firstBtn);
            // ВТОРОЙ костюм
            secondBtn.Location = new Point(699, 53);
            secondBtn.Size = new Size(190, 80);
            secondBtn.Text = "ВТОРОЙ";
            secondBtn.Visible = false;
            secondBtn.Click += SecondBtn_Click;
            Controls.Add(secondBtn);
            // ТРЕТИЙ костюм
            thirdBtn.Location = new Point(903, 53);
            thirdBtn.Size = new Size(190, 80);
            thirdBtn.Text = "ТРЕТИЙ";
            thirdBtn.Visible = false;
            thirdBtn.Click += ThirdBtn_Click;
            Controls.Add(thirdBtn);
            // ЧЕТВЁРТЫЙ костюм
            fourthBtn.Location = new Point(1109, 53);
            fourthBtn.Size = new Size(190, 80);
            fourthBtn.Text = "ЧЕТВЁРТЫЙ";
            fourthBtn.Visible = false;
            fourthBtn.Click += FourthBtn_Click;
            Controls.Add(fourthBtn);
            // ПЯТЫЙ костюм
            fifthBtn.Location = new Point(1315, 53);
            fifthBtn.Size = new Size(190, 80);
            fifthBtn.Text = "ПЯТЫЙ";
            fifthBtn.Visible = false;
            fifthBtn.Click += FifthBtn_Click;
            Controls.Add(fifthBtn);

            // Кнопка выхода
            exitBtn.Location = new Point(12, 12);
            exitBtn.Size = new Size(51, 63);
            exitBtn.Text = "ВЫХОД";
            exitBtn.Visible = true;
            exitBtn.Click += ExitBtn_Click;
            Controls.Add(exitBtn);
        }
        private void FirstBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выбран первый костюм");
        }
        private void SecondBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выбран второй костюм");
        }
        private void ThirdBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выбран тертий костюм");
        }
        private void FourthBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выбран четвёртый костюм");
        }
        private void FifthBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выбран пятый костюм");
        }
        private void ExitBtn_Click(object sender, EventArgs e)
        {
            if (gameplay != null)
            {
                gameplay.ShowGoat();
                gameplay.UpdateHealthDisplay();
            }

            Controls.Clear();
            FireScreen();
        }
    }
}
