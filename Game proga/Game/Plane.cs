using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    class Plane
    {
        public static readonly string[] Planes = new string[]
        {
            @"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\самолёт.png",
            @"C:\Users\НОРД\github\Game\Game proga\Game\Sprites\самолёт2.png"
        };
        public readonly int Speed = 10;
        public readonly Image Look;
        public readonly string Current;

        public Plane()
        {
            var random = new Random().Next(0, 2);
            Look = Image.FromFile(Planes[random]);
            Current = Planes[random];
        }

        public static void DropTheBomb()
        {

        }
    }
}
