using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bleat_Buddy
{
    internal class Goat
    {
        private const string bleat = ""; // ToDo: Написать строку звука
        private int level;
        double speed;
        Button goat;

        public Goat(Button goatBtn, int level)
        {
            this.goat = goatBtn;
            this.level = level;
            this.speed = 100;
        }
    }
}
