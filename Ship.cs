using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleships
{
    public abstract class Ship
    {

        public BufferedPanel panel { get; set; }
        public int size { get; set; }

        public List<List<Cell>> matrix;

        public int x1 { get; set; }
        public int x2 { get; set; }
        public int y1 { get; set; }
        public int y2 { get; set; }

        public int cellSize { get; set; }

        public static int OurHits { get; set; } = 0;
        public static int EnemyHits { get; set; } = 0;



        //0-nema brod, nema hit
        //1-ima brod, nema hit
        //2-ima brod, ima hit
        //3-nema brod, ima hit
        //4-ima brod, nema hit i e invis
        //21
        //31
        //41

        public Ship() {
            
            size = 400;
            matrix = new List<List<Cell>>();

            cellSize = (this.size / 10);

            Cell.cellSize = cellSize;

            for (int i = 0; i < 10; i++)
            {
                matrix.Add(new List<Cell>());
                for (int j = 0; j < 10; j++)
                {
                    int x = j * cellSize;
                    int y = i * cellSize;
                    matrix[i].Add(new Cell(x, y));
                }
            }
            
        }

        public void addShip(int x,int y)
        {
            matrix[x][y].state = 1;
        }
        public void checkHit(int x,int y)
        {
            if (matrix[x][y].state == 4 || matrix[x][y].state%10 == 1)
            {
                matrix[x][y].state = 2;
            }
            else if (matrix[x][y].state == 0)
            {
                matrix[x][y].state = 3;
            }

        }
        public void InitializeMatrixPanel(int w, int h)
        {
            this.x1 = w;
            this.y1 = h;
            this.x2 = x1 + size;
            this.y2 = y1 + size;
            // Create the Panel

            panel = new BufferedPanel(new Size(size, size),new Point(w, h),Color.LightBlue);
            
            // Attach the Paint event handler
            panel.Paint += PaintMatrix;
            panel.Paint += PaintBorder;
            panel.MouseClick += Check;
            panel.MouseMove += Hover;
        }

        private void Hover(object sender, MouseEventArgs e)
        {
            HoverCheck(sender, e);
        }

        private void PaintBorder(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = Pens.Black;
            int width = panel.ClientSize.Width;
            int height = panel.ClientSize.Height;

            // Draw the right border
            g.DrawLine(pen, width - 1, 0, width - 1, height);
            // Draw the bottom border
            g.DrawLine(pen, 0, height - 1, width, height - 1);
        }
        public void PaintMatrix(object sender, PaintEventArgs e)
        {
            Font font = new Font("Arial", 12);
            Brush brush = Brushes.Black;
            Pen pen = Pens.Black;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int x = j * Cell.cellSize;
                    int y = i * Cell.cellSize;
                    matrix[i][j].Draw(e.Graphics);
                }
            }
            brush.Dispose();
            font.Dispose();
        }


        public abstract void Check(object sender, MouseEventArgs e);
        public abstract void HoverCheck(object sender, MouseEventArgs e);
    }
}
