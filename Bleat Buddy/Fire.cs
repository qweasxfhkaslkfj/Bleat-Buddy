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
        // Козёл ест
        private string lvl_1_goatFeed = Path.Combine(projectRoot, "resurse", "feed", "lvl_1_goatFeed");
        private string lvl_2_goatFeed = Path.Combine(projectRoot, "resurse", "feed", "lvl_2_goatFeed");
        private string lvl_3_goatFeed = Path.Combine(projectRoot, "resurse", "feed", "lvl_3_goatFeed");
        private string lvl_4_goatFeed = Path.Combine(projectRoot, "resurse", "feed", "lvl_4_goatFeed");
        // Другие переменные
        private Image box_Texture = Image.FromFile(Path.Combine(projectRoot, "resurse", "box.png"));
        // Ванная
        private Image bathroom_Background = Image.FromFile(Path.Combine(projectRoot, "resurse", "backgrounds", "bathroom.png"));
        private string bath = Path.Combine(projectRoot, "resurse", "washing");

        private Gameplay gameplay;
        Button firstBtn, secondBtn, thirdBtn, fourthBtn, fifthBtn, exitBtn;
        Goat goat;

        public PictureBox CreateFire(int x, int y)
        {
            PictureBox fire = new PictureBox();
            fire.Location = new Point(x, y);
            fire.Size = new Size(105, 60);
            fire.Visible = true;
            fire.BackgroundImage = box_Texture;
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
            if (goat != null && goat.energyPoint < 70)
            {
                // Запускаем анимацию сна
                StartSleepAnimation();
            }
            else
            {
                MessageBox.Show("Козлик ещё не очень устал, он не хочет спать");
            }
        }

        // Анимация сна
        private void StartSleepAnimation()
        {
            // Создаем PictureBox для анимации
            PictureBox sleepAnimation = new PictureBox();
            sleepAnimation.Size = new Size(10, 10); // Начинаем с очень маленького размера
            sleepAnimation.Location = new Point(
                (this.Width - sleepAnimation.Width) / 2,
                (this.Height - sleepAnimation.Height) / 2
            );
            sleepAnimation.BackgroundImageLayout = ImageLayout.Stretch;

            // Загружаем первую текстуру
            string firstTexturePath = Path.Combine(projectRoot, "resurse", "1dot.png");
            if (File.Exists(firstTexturePath))
            {
                sleepAnimation.BackgroundImage = Image.FromFile(firstTexturePath);
            }
            else
            {
                sleepAnimation.BackColor = Color.Black; // Fallback цвет
            }

            Controls.Add(sleepAnimation);
            sleepAnimation.BringToFront();

            // Запускаем анимацию в отдельном потоке
            Thread animationThread = new Thread(() => SleepAnimation(sleepAnimation));
            animationThread.Start();
        }

        private void SleepAnimation(PictureBox sleepBox)
        {
            int steps = 25;
            int maxWidth = this.Width;
            int maxHeight = this.Height;

            // Фаза увеличения
            for (int i = 1; i <= steps; i++)
            {
                // Обновляем UI в основном потоке
                this.Invoke(new Action(() =>
                {
                    // Вычисляем новый размер
                    int newWidth = 10 + (maxWidth - 10) * i / steps;
                    int newHeight = 10 + (maxHeight - 10) * i / steps;

                    // Обновляем размер и позицию
                    sleepBox.Size = new Size(newWidth, newHeight);
                    sleepBox.Location = new Point(
                        (this.Width - newWidth) / 2,
                        (this.Height - newHeight) / 2
                    );

                    // Обновляем текстуру (если есть разные текстуры для разных шагов)
                    string texturePath = Path.Combine(projectRoot, "resurse", $"{Math.Min(i, 5)}dot.png");
                    if (File.Exists(texturePath))
                    {
                        sleepBox.BackgroundImage = Image.FromFile(texturePath);
                    }

                    this.Refresh();
                }));

                Thread.Sleep(50); // Задержка между шагами
            }

            // Ждем немного в полностью развернутом состоянии
            Thread.Sleep(500);

            // Фаза уменьшения
            for (int i = steps; i >= 0; i--)
            {
                this.Invoke(new Action(() =>
                {
                    if (i > 0)
                    {
                        // Вычисляем новый размер
                        int newWidth = 10 + (maxWidth - 10) * i / steps;
                        int newHeight = 10 + (maxHeight - 10) * i / steps;

                        // Обновляем размер и позицию
                        sleepBox.Size = new Size(newWidth, newHeight);
                        sleepBox.Location = new Point(
                            (this.Width - newWidth) / 2,
                            (this.Height - newHeight) / 2
                        );

                        // Обновляем текстуру
                        string texturePath = Path.Combine(projectRoot, "resurse", $"{Math.Min(i, 5)}dot.png");
                        if (File.Exists(texturePath))
                        {
                            sleepBox.BackgroundImage = Image.FromFile(texturePath);
                        }
                    }
                    else
                    {
                        // Удаляем PictureBox
                        Controls.Remove(sleepBox);
                        sleepBox.Dispose();

                        // Восстанавливаем энергию козлика
                        goat.energyPoint = 100;
                        if (gameplay != null)
                            gameplay.UpdateStatsDisplay();

                        MessageBox.Show("Козлик поспал!");
                    }

                    this.Refresh();
                }));

                Thread.Sleep(50); // Задержка между шагами
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
                        int x = gameplay.goat.Location.X;
                        int y = goat.Location.Y;

                        switch (goat.level)
                        {
                            case 1:
                                FeedAnimation(lvl_1_goatFeed, goat.level);
                                break;
                            case 2:
                                FeedAnimation(lvl_2_goatFeed, goat.level);
                                break;
                            case 3:
                                FeedAnimation(lvl_3_goatFeed, goat.level);
                                break;
                            case 4:
                                FeedAnimation(lvl_4_goatFeed, goat.level);
                                break;
                        }

                        goat.healthPoint = 3;
                        goat.crystalsCount--;

                        gameplay.UpdateStatsDisplay();
                    }
                }
            }
            else
            {
                MessageBox.Show("Козлик полностью здоров!");
            }

        }
        // Анимация кормления
        // ToDo:
        //          + ЗАТРЯСТИ ДИЗАЙНЕРОВ, ЧТОБЫ ОНИ НОРМАЛЬНЫЕ КАДРЫ СДЕЛАЛИ
        private void FeedAnimation(string folder, int level)
        {
            gameplay.HideGoat();

            int frameCount = AnimFrameCount(folder);
            PictureBox goatFeed = new PictureBox();
            switch (level)
            {
                case 1:
                    goatFeed.Size = new Size(92, 104);
                    break;
                case 2:
                    goatFeed.Size = new Size(130, 140);
                    break;
                case 3:
                    goatFeed.Size = new Size(150, 170);
                    break;
                case 4:
                    goatFeed.Size = new Size(104, 136);
                    break;
            }

            goatFeed.Location = gameplay.goatSprite.Location;
            goatFeed.BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(goatFeed);
            goatFeed.BringToFront();

            for (int i = 1; i <= frameCount; i++)
            {
                string imagePath = Path.Combine(folder, $"feed{i}.png");
                if (File.Exists(imagePath))
                {
                    goatFeed.BackgroundImage = Image.FromFile(imagePath);
                    this.Refresh();
                    Thread.Sleep(100);
                }
            }
            Controls.Remove(goatFeed);
            goatFeed.Dispose();
            gameplay.ShowGoat();
        }
        private int AnimFrameCount(string folder)
        {
            return Directory.GetFiles(folder).Length;
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
                        gameplay.UpdateStatsDisplay();
                    }
                }
            }
            else
            {
                MessageBox.Show("У вас достаточно лекарственных бутылочек!");
            }
        }
        // Интерфейс мытья

        private void CleanBtn_Click(object sender, EventArgs e)
        {
            /*if (goat.dirty)
            {
                DialogResult result = MessageBox.Show("Вы хотите помыть козлика?", "Мытьё", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    goat.dirty = false;
                    MessageBox.Show("Теперь козлик чистый!");
                }
            }
            else
            {
                MessageBox.Show("Козлик и так достаточно чистый!");
            }*/
        }

        public PictureBox CreateBath(int x, int y)
        {
            PictureBox bath = new PictureBox();
            bath.Location = new Point(x, y);
            bath.Size = new Size(300, 300);
            bath.Visible = true;

            string bathImagePath = Path.Combine(projectRoot, "resurse", "washing", "bath1.png");
            if (File.Exists(bathImagePath))
            {
                bath.BackgroundImage = Image.FromFile(bathImagePath);
            }
            else
            {
                bath.BackColor = Color.Blue;
            }

            bath.BackgroundImageLayout = ImageLayout.Stretch;
            return bath;
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
        public void SetGoatReference(Goat goatReference)
        {
            this.goat = goatReference;
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