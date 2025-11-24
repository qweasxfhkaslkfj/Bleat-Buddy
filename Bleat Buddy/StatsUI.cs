using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bleat_Buddy
{
    internal class StatsUI : UserControl
    {
        private Goat goat;

        // Элементы интерфейса
        private Panel statsPanel;
        private Label healthLabel;
        private Label energyLabel;
        private Label levelLabel;
        private Label crystalsLabel;
        private Label medicineLabel;
        private Label statusLabel;

        public StatsUI(Goat goatReference)
        {
            this.goat = goatReference;
            InitializeComponent();
            UpdateStats();
        }

        private void InitializeComponent()
        {
            // Основная панель
            statsPanel = new Panel();
            statsPanel.Size = new Size(200, 180);
            statsPanel.BackColor = Color.LightGoldenrodYellow;
            statsPanel.BorderStyle = BorderStyle.FixedSingle;
            statsPanel.Padding = new Padding(5);

            // Заголовок
            Label titleLabel = new Label();
            titleLabel.Text = "Статы";
            titleLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            titleLabel.Location = new Point(10, 5);
            titleLabel.Size = new Size(180, 20);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Здоровье
            healthLabel = CreateStatLabel(30);
            healthLabel.Text = "Здоровье: ";

            // Энергия
            energyLabel = CreateStatLabel(55);
            energyLabel.Text = "Энергия: ";

            // Уровень
            levelLabel = CreateStatLabel(80);
            levelLabel.Text = "Уровень: ";

            // Кристаллы
            crystalsLabel = CreateStatLabel(105);
            crystalsLabel.Text = "Кристаллы: ";

            // Лекарства
            medicineLabel = CreateStatLabel(130);
            medicineLabel.Text = "Лекарства: ";

            // Статус
            statusLabel = CreateStatLabel(155);
            statusLabel.Text = "Статус: ";

            // Добавляем элементы на панель
            statsPanel.Controls.Add(titleLabel);
            statsPanel.Controls.Add(healthLabel);
            statsPanel.Controls.Add(energyLabel);
            statsPanel.Controls.Add(levelLabel);
            statsPanel.Controls.Add(crystalsLabel);
            statsPanel.Controls.Add(medicineLabel);
            statsPanel.Controls.Add(statusLabel);

            // Добавляем панель на контрол
            this.Controls.Add(statsPanel);
            this.Size = statsPanel.Size;
        }

        private Label CreateStatLabel(int top)
        {
            Label label = new Label();
            label.Location = new Point(10, top);
            label.Size = new Size(180, 20);
            label.Font = new Font("Arial", 9);
            return label;
        }

        public void UpdateStats()
        {
            if (goat == null) return;

            // Обновляем все показатели
            healthLabel.Text = $"Здоровье: {goat.healthPoint}";
            energyLabel.Text = $"Энергия: {goat.energyPoint}";
            levelLabel.Text = $"Уровень: {goat.level}";
            crystalsLabel.Text = $"Кристаллы: {goat.crystalsCount}";
            medicineLabel.Text = $"Лекарства: {goat.medCount}";

            // Определяем статус
            string status = "Здоров";
            if (goat.isSick) status = "Болен";
            if (goat.dirty) status += ", Грязный";

            statusLabel.Text = $"Статус: {status}";

            // Меняем цвет здоровья в зависимости от значения
            if (goat.healthPoint <= 1)
                healthLabel.ForeColor = Color.Red;
            else if (goat.healthPoint <= 2)
                healthLabel.ForeColor = Color.Orange;
            else
                healthLabel.ForeColor = Color.Green;

            // Меняем цвет энергии
            if (goat.energyPoint <= 20)
                energyLabel.ForeColor = Color.Red;
            else if (goat.energyPoint <= 40)
                energyLabel.ForeColor = Color.Orange;
            else
                energyLabel.ForeColor = Color.Green;
        }

        // Метод для изменения положения интерфейса
        public void SetPosition(Point location)
        {
            this.Location = location;
        }

        // Метод для скрытия/показа интерфейса
        public void ToggleVisibility()
        {
            this.Visible = !this.Visible;
        }
    }

}
