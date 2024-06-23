using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Battleships
{
    public class OurShip : Ship
    {
        //METHOD FOR PUTTING OUR SHIPS (BUTTON IN FORM PROBABLY

        private static System.Windows.Forms.Timer timer;
        private static System.Windows.Forms.Timer timer2;
        public static bool hit { get; set; }
        public static int hited { get; set; }

        public static int prevX { get; set; }
        public static int prevY { get; set; }

        public static bool Turn { get; set; }

        public static HashSet<String> setovi { get; set; }
        public OurShip() : base(){
            Turn = true;
            setovi = new HashSet<String>();
            hit = false;
            hited = 4;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += ticked;
            timer2 = new System.Windows.Forms.Timer();
            timer2.Interval = 1000;
            timer2.Tick += ticking;
        }

        public static void newValues()
        {
            Random random = new Random();
            int x = random.Next(10);
            int y = random.Next(10);
            int randomhit_y = 0;
            int randomhit_x = 0;
            if (hit)
            {
                randomhit_x = random.Next(3);
                randomhit_y = random.Next(3);
                randomhit_x--;
                randomhit_y--;
                if (randomhit_x == 0 && randomhit_y == 0)
                {
                    randomhit_x = -1;
                    randomhit_y = -1;
                }

            }
            prevX = x + randomhit_x;
            prevY = y + randomhit_y;
            if (prevX < 0)
            {
                prevX = 0;
            }
            else if (prevX > 9)
            {
                prevX = 9;
            }
            if (prevY < 0)
            {
                prevY = 0;
            }
            else if (prevY > 9)
            {
                prevY = 9;
            }
        }

        

        public static void hits()
        {
            if (Ship.OurHits != 14)
            {
                Form1.lblTurn.Text = "ENEMY TURN";
                Turn = false;
                newValues();
                string value = $"{prevX} {prevY}";
                while (setovi.Contains(value))
                {
                    newValues();
                    value = $"{prevX} {prevY}";
                };
                setovi.Add(value);
                hit = false;
                if (Form1.Our.matrix[prevX][prevY].state%10 == 1)
                {
                    Ship.OurHits++;
                    hit = true;
                }
                else
                {
                    hit = false;
                }
                Form1.Our.checkHit(prevX, prevY);
                timer.Start();
            }
        }

        private static void ticked(object sender, EventArgs e)
        {
            Form1.Our.panel.Invalidate();
            timer.Stop();
            timer2.Start();
        }
        private void ticking(object sender, EventArgs e)
        {
            Form1.Our.panel.Invalidate();
            if (hit)
            {
                hits();
            }
            timer2.Stop();
            Turn= true;
            Form1.lblTurn.Text = "YOUR TURN";
        }

        public override void Check(object sender, MouseEventArgs e)
        {
            return;
        }

        public override void HoverCheck(object sender, MouseEventArgs e)
        {
            return;
        }
    }
}

