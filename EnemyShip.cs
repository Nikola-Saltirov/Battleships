using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleships
{
    public class EnemyShip : Ship
    {
        //METHOD FOR ATTACKING THE ENEMY;

        public int hits { get; set; }
        public EnemyShip() : base(){ 
            hits = 0;
            Random random = new Random();
            int cnt2=0;
            int cnt3=0;
            int cnt4=0;
            while(cnt2 < 2||cnt3<2||cnt4<1)
            {
                int x=random.Next(10);
                int y=random.Next(10);
                int z=random.Next(2);
                if (cnt2 < 2)
                {
                    if (generateShip2(x, y, z))
                    {
                        cnt2++;
                    }
                }else if (cnt3 < 2)
                {
                    if (generateShip3(x, y, z))
                    {
                        cnt3++;
                    }
                }
                else
                {
                    if (generateShip4(x, y))
                    {
                        cnt4++;
                    }
                }
                
            }
        }

        public bool generateShip4(int i, int j)
        {
            int sadder1 = 1;
            int sadder2 = 1;
            if (i == 9)
            {
                sadder1 = -1;
            }
            else
            {
                sadder1 = 1;
            }
            if (j == 9)
            {
                sadder2 = -1;
            }
            else
            {
                sadder2 = 1;
            }
            if (matrix[i][j].state == 0)
            {
                if (matrix[i][j].state != 0 || matrix[i][j + sadder2].state != 0 || matrix[i + sadder1][j + sadder2].state != 0 || matrix[i + sadder1][j].state != 0)
                {
                    return false;
                }
                else
                {
                    matrix[i][j].state = 4;
                    matrix[i][j + sadder2].state = 4;
                    matrix[i + sadder1][j + sadder2].state = 4;
                    matrix[i + sadder1][j].state = 4;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public bool generateShip3(int i, int j, int side)
        {
            int adder1 = 1;
            int adder2 = -1;
            if (side==0)
            {
                //j
                if (j == 9)
                {
                    adder1 = -1;
                    adder2 = -2;
                }
                else if (j == 0)
                {
                    adder1 = 1;
                    adder2 = 2;
                }
                else
                {
                    adder1 = -1;
                    adder2 = 1;
                }
                //check neighboors
            }
            else
            {
                //i
                if (i == 9)
                {
                    adder1 = -1;
                    adder2 = -2;
                }
                else if (i == 0)
                {
                    adder1 = 1;
                    adder2 = 2;
                }
                else
                {
                    adder1 = -1;
                    adder2 = 1;
                }
            }
            if (matrix[i][j].state == 0)
            {
                if (side==0)
                {
                    if (matrix[i][j].state != 0 || matrix[i][j + adder1].state != 0 || matrix[i][j + adder2].state != 0)
                    {
                        return false;
                    }
                    else
                    {
                        matrix[i][j].state = 4;
                        matrix[i][j + adder1].state = 4;
                        matrix[i][j + adder2].state = 4;
                        return true;
                    }
                }
                else
                {
                    if (matrix[i][j].state != 0 || matrix[i + adder1][j].state != 0 || matrix[i + adder2][j].state != 0)
                    {
                        return false;
                    }
                    else
                    {
                        matrix[i][j].state = 4;
                        matrix[i + adder1][j].state = 4;
                        matrix[i + adder2][j].state = 4;
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        public bool generateShip2(int i ,int j,int side)
        {
            int adder = 1;
            if (side==0)
            {
                if (j == 9)
                {
                    adder = -1;
                }
            }
            else
            {
                if (i == 9)
                {
                    adder = -1;
                }
            }
            if (matrix[i][j].state == 0)
            {
                if (side==0)
                {
                    if (matrix[i][j].state != 0 || matrix[i][j + adder].state != 0)
                    {
                        return false;
                    }
                    else
                    {
                        matrix[i][j].state = 4;
                        matrix[i][j + adder].state = 4;
                        return true;
                    }
                }
                else
                {
                    if (matrix[i][j].state != 0 || matrix[i + adder][j].state != 0)
                    {
                        return false;
                    }
                    else
                    {
                        matrix[i][j].state = 4;
                        matrix[i + adder][j].state = 4;
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        
        public override void Check(object sender, MouseEventArgs e)
        {
            if (EnemyHits != 14 && OurShip.Turn)
            {
                //implementiranje na poseben check za da se napagja
                int x = e.X; int y = e.Y;
                int i = y / Cell.cellSize;
                int j = x / Cell.cellSize;
                bool hit = false;
                if (matrix[i][j].state == 0 || matrix[i][j].state == 1 || matrix[i][j].state == 4)
                {
                    if (matrix[i][j].state == 4)
                    {
                        hit = true;
                    }
                    checkHit(i, j);
                    if (matrix[i][j].state == 2)
                    {
                        hits++;
                        EnemyHits++;
                        Form1.UpdateStatusStrip(1, 0);
                    }
                    else if (matrix[i][j].state == 3)
                    {
                        Form1.UpdateStatusStrip(0, 1);
                    }
                    panel.Invalidate();
                    if (EnemyHits == 14)
                    {
                        MessageBox.Show("CONGRATULATIONS YOU HAVE BEATEN CHATGPT");
                        return;
                    }
                    if (!hit)
                    {
                        OurShip.hits();
                    }
                    if(OurHits == 14)
                    {
                        MessageBox.Show("LOL BETTER LUCK NEXT TIME");
                    }
                }
            }
        }

        public override void HoverCheck(object sender, MouseEventArgs e)
        {
            int x = e.X; int y=e.Y;
            int i = y / Cell.cellSize;
            int j = x / Cell.cellSize;
            
            if (!matrix[i][j].hovered)
            {
                for (int i2 = 0; i2 < 10; i2++)
                {
                    for (int j2 = 0; j2 < 10; j2++)
                    {
                        matrix[i2][j2].hovered = false;
                    }
                }
                matrix[i][j].hovered = true;
                panel.Invalidate();
            }
        }
    }
}
