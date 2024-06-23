using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    public class Cell
    {
        public int x { get; set; }
        public int y { get; set; }
        public int state { get; set; }
        public static int cellSize { get; set; } = 40;

        public bool hovered { get; set; }

        //0-nema brod, nema hit
        //1-ima brod, nema hit
        //2-ima brod, ima hit
        //3-nema brod, ima hit

        public Cell(int x, int y)
        {
            hovered = false;
            this.x = x;
            this.y = y;
            this.state = 0;
        }

        public void Draw(Graphics g)
        {
            Brush brush =new SolidBrush(Color.FromArgb(hovered ? 50 : 100, Color.LightYellow));
            if (state%10==1)
            {
                brush = new SolidBrush(Color.Orange);

            }
            else if (state == 2)
            {
                brush = new SolidBrush(Color.Red);
                
            }
            else if(state == 3)
            {
                brush = new SolidBrush(Color.Silver);
            }
            
            g.FillRectangle(brush,x,y,cellSize,cellSize);
            Pen pen = Pens.Black;
            g.DrawRectangle(pen, x, y, cellSize, cellSize);
            brush.Dispose();
        }


    }
}
