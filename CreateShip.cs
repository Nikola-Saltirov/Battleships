using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleships
{
    public class CreateShip : Ship
    {
        public int SetCounter { get; set; }
        public int adder1;
        public int adder2;
        public int sadder1;
        public int sadder2;
        public Dictionary<String,int> types { get; set; }
        public CreateShip() {
            SetCounter = 14;
            adder1 = 1;
            adder2 = -1;
            sadder1 = 1;
            sadder2 = 1;
            types = new Dictionary<String,int>();
            types.Add("TWO", 0);
            types.Add("THREE", 0);
            types.Add("SQUARE", 0);
        }

        public bool CheckNeighboorTwo(int i,int j)
        {
            return matrix[i][j].state == 0;
        }

        public int twocheck(int i,int j)
        {
            int adder = 1;
            if (Form1.rbHorizontal.Checked)
            {
                //j
                if (j == 9)
                {
                    adder = -1;
                }
                //check neighboors
            }
            else
            {
                //i
                if (i == 9)
                {
                    adder = -1;
                }
            }
            return adder;
        }
        public void threecheck(int i, int j)
        {
            if (Form1.rbHorizontal.Checked)
            {
                //j
                if (j == 9)
                {
                    adder1 = -1;
                    adder2 = -2;
                }else if (j == 0)
                {
                    adder1 = 1;
                    adder2 = 2;
                }else
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
                }else if (i == 0)
                {
                    adder1 = 1;
                    adder2 = 2;
                }else
                {
                    adder1 = -1;
                    adder2 = 1;
                }
            }
        }
        public void checksquare(int i,int j)
        {
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
        }
        public override void Check(object sender, MouseEventArgs e)
        {
            int x = e.X; int y = e.Y;
            int i = y / Cell.cellSize;
            int j = x / Cell.cellSize;
            int adder = 1;
            if (Form1.radioButton1.Checked)
            {
                adder=twocheck(i,j);
            }
            if (Form1.radioButton2.Checked)
            {
                threecheck(i, j);
            }
            if (Form1.radioButton3.Checked)
            {
                checksquare(i, j);
            }

            //TWO
            if (Form1.radioButton1.Checked && matrix[i][j].state==0 && types["TWO"]<2)
            {
                if (Form1.rbHorizontal.Checked)
                {
                    if (matrix[i][j].state!=0|| matrix[i][j+adder].state != 0)
                    {
                        MessageBox.Show("INVALID PLACEMENT");
                        return;
                    }
                    else
                    {
                        matrix[i][j].state = 21;
                        matrix[i][j + adder].state = 21;
                    }
                }
                else
                {
                    if (matrix[i][j].state != 0 || matrix[i + adder][j].state != 0)
                    {
                        MessageBox.Show("INVALID PLACEMENT");
                        return;
                    }
                    else
                    {
                        matrix[i][j].state = 21;
                        matrix[i + adder][j].state = 21;
                    }
                    
                }
                    SetCounter -= 2;
                types["TWO"]++;
                Form1.lblShipsLeft.Text = $"Ships with size 2 Left: {2 - types["TWO"]}";
            }
            else if (Form1.radioButton1.Checked && matrix[i][j].state == 21 && Form1.rbHorizontal.Checked)
            {
                
                if ((matrix[i][j].state == 21 && matrix[i][j + adder].state == 21))
                {
                    matrix[i][j].state = 0;
                    matrix[i][j + adder].state = 0;
                    SetCounter += 2;
                    types["TWO"]--;
                    Form1.lblShipsLeft.Text = $"Ships with size 2 Left: {2 - types["TWO"]}";
                }else if((matrix[i][j].state == 21 && matrix[i][j - adder].state == 21))
                {
                    matrix[i][j].state = 0;
                    matrix[i][j - adder].state = 0;
                    SetCounter += 2;
                    types["TWO"]--;
                    Form1.lblShipsLeft.Text = $"Ships with size 2 Left: {2 - types["TWO"]}";
                }
            }else if(Form1.radioButton1.Checked && matrix[i][j].state == 21 && Form1.rbVertical.Checked)
            {
                if ((matrix[i][j].state == 21 && matrix[i + adder][j].state == 21))
                {
                    matrix[i][j].state = 0;
                    matrix[i + adder][j].state = 0;
                    SetCounter += 2;
                    types["TWO"]--;
                    Form1.lblShipsLeft.Text = $"Ships with size 2 Left: {2 - types["TWO"]}";
                }else if((matrix[i][j].state == 21 && matrix[i - adder][j].state == 21))
                {
                    matrix[i][j].state = 0;
                    matrix[i - adder][j].state = 0;
                    SetCounter += 2;
                    types["TWO"]--;
                    Form1.lblShipsLeft.Text = $"Ships with size 2 Left: {2 - types["TWO"]}";
                }
            }

            //THREE


            if (Form1.radioButton2.Checked&&matrix[i][j].state == 0 && types["THREE"] < 2)
            {
                if (Form1.rbHorizontal.Checked)
                {
                    if(matrix[i][j].state != 0|| matrix[i][j + adder1].state != 0|| matrix[i][j + adder2].state != 0)
                    {
                        MessageBox.Show("INVALID PLACEMENT");
                        return;
                    }
                    else
                    {
                        matrix[i][j].state = 31;
                        matrix[i][j + adder1].state = 31;
                        matrix[i][j + adder2].state = 31;
                    }
                }
                else
                {
                    if (matrix[i][j].state != 0 || matrix[i + adder1][j].state != 0 || matrix[i + adder2][j].state != 0)
                    {
                        MessageBox.Show("INVALID PLACEMENT");
                        return;
                    }
                    else
                    {
                        matrix[i][j].state = 31;
                        matrix[i + adder1][j].state = 31;
                        matrix[i + adder2][j].state = 31;
                    }
                }
                SetCounter -= 3;
                types["THREE"]++;
                Form1.lblShips1.Text = $"Ships with size 3 Left: {2 - types["THREE"]}";

            }
            else if (Form1.radioButton2.Checked && matrix[i][j].state == 31 && Form1.rbHorizontal.Checked)
            {
                //center
                if ((matrix[i][j].state == 31 && matrix[i][j + adder1].state == 31) && matrix[i][j + adder2].state == 31)
                {
                    matrix[i][j].state = 0;
                    matrix[i][j + adder1].state = 0;
                    matrix[i][j + adder2].state = 0;
                    SetCounter += 3;
                    types["THREE"]--;
                    Form1.lblShips1.Text = $"Ships with size 3 Left: {2 - types["THREE"]}";
                }
                //right
                else if (matrix[i][j].state == 31 && matrix[i][j-1].state == 31 && matrix[i][j-2].state == 31)
                {
                    matrix[i][j].state = 0;
                    matrix[i][j-1].state = 0;
                    matrix[i][j-2].state = 0;
                    SetCounter += 3;
                    types["THREE"]--;
                    Form1.lblShips1.Text = $"Ships with size 3 Left: {2 - types["THREE"]}";
                }
                //left
                else if (matrix[i][j].state == 31 && matrix[i][j + 1].state == 31 && matrix[i][j + 2].state == 31)
                {
                    matrix[i][j].state = 0;
                    matrix[i][j + 2].state = 0;
                    matrix[i][j + 1].state = 0;
                    SetCounter += 3;
                    types["THREE"]--;
                    Form1.lblShips1.Text = $"Ships with size 3 Left: {2 - types["THREE"]}";
                }
            }
            else if (Form1.radioButton2.Checked && matrix[i][j].state == 31 && Form1.rbVertical.Checked)
            {
                //center
                if ((matrix[i][j].state == 31 && matrix[i + adder1][j].state == 31) && matrix[i + adder2][j].state == 31)
                {
                    matrix[i][j].state = 0;
                    matrix[i + adder2][j].state = 0;
                    matrix[i + adder1][j].state = 0;
                    SetCounter += 3;
                    types["THREE"]--;
                    Form1.lblShips1.Text = $"Ships with size 3 Left: {2 - types["THREE"]}";
                }
                //right
                else if (matrix[i][j].state == 31 && matrix[i - 1][j].state == 31 && matrix[i - 2][j].state == 31)
                {
                    matrix[i][j].state = 0;
                    matrix[i - 1][j].state = 0;
                    matrix[i - 2][j].state = 0;
                    SetCounter += 3;
                    types["THREE"]--;
                    Form1.lblShips1.Text = $"Ships with size 3 Left: {2 - types["THREE"]}";
                }
                //left
                else if (matrix[i][j].state == 31 && matrix[i + 1][j].state == 31 && matrix[i + 2][j].state == 31)
                {
                    matrix[i][j].state = 0;
                    matrix[i + 2][j].state = 0;
                    matrix[i + 1][j].state = 0;
                    SetCounter += 3;
                    types["THREE"]--;
                    Form1.lblShips1.Text = $"Ships with size 3 Left: {2 - types["THREE"]}";
                }
            }

            //square
            if (Form1.radioButton3.Checked && matrix[i][j].state == 0 && types["SQUARE"] < 1)
            {
                if(matrix[i][j].state == 41 || matrix[i][j + sadder2].state == 41 || matrix[i + sadder1][j + sadder2].state == 41 || matrix[i + sadder1][j].state == 41)
                {
                    MessageBox.Show("INVALID PLACEMENT");
                    return;
                }
                else
                {
                    matrix[i][j].state = 41;
                    matrix[i][j + sadder2].state = 41;
                    matrix[i + sadder1][j + sadder2].state = 41;
                    matrix[i + sadder1][j].state = 41;
                    SetCounter -= 4;
                    types["SQUARE"]++;
                    Form1.lblShips2.Text = $"Ships with size 4 Left: {1 - types["SQUARE"]}";
                }
            }else if(Form1.radioButton3.Checked && matrix[i][j].state == 41)
            {
                //topleft
                if ((matrix[i][j].state == 41 && matrix[i][j + sadder2].state == 41) && matrix[i+sadder1][j].state == 41 && matrix[i + sadder1][j+sadder2].state == 41)
                {
                    matrix[i][j].state = 0;
                    matrix[i+sadder1][j + sadder2].state = 0;
                    matrix[i][j + sadder2].state = 0;
                    matrix[i + sadder1][j].state = 0;
                    SetCounter += 4;
                    types["SQUARE"]--;
                    Form1.lblShips2.Text = $"Ships with size 4 Left: {1 - types["SQUARE"]}";
                }
                //bottomleft -1 +1
                else if ((matrix[i][j].state == 41 && matrix[i-1][j].state == 41) && matrix[i][j+1].state == 41 && matrix[i-1][j+1].state == 41)
                {
                    matrix[i][j].state = 0;
                    matrix[i-1][j+1].state = 0;
                    matrix[i][j+1].state = 0;
                    matrix[i-1][j].state = 0;
                    SetCounter += 4;
                    types["SQUARE"]--;
                    Form1.lblShips2.Text = $"Ships with size 4 Left: {1 - types["SQUARE"]}";
                }
                //topright +1 -1
                else if ((matrix[i][j].state == 41 && matrix[i][j-1].state == 41) && matrix[i+1][j].state == 41 && matrix[i+1][j-1].state == 41)
                {
                    matrix[i][j].state = 0;
                    matrix[i+1][j-1].state = 0;
                    matrix[i][j-1].state = 0;
                    matrix[i+1][j].state = 0;
                    SetCounter += 4;
                    types["SQUARE"]--;
                    Form1.lblShips2.Text = $"Ships with size 4 Left: {1 - types["SQUARE"]}";
                }
                //bottomright -1 -1
                else if ((matrix[i][j].state == 41 && matrix[i][j-1].state == 41) && matrix[i-1][j].state == 41 && matrix[i - 1][j-1].state == 41)
                {
                    matrix[i][j].state = 0;
                    matrix[i - 1][j - 1].state = 0;
                    matrix[i][j - 1].state = 0;
                    matrix[i - 1][j].state = 0;
                    SetCounter += 4;
                    types["SQUARE"]--;
                    Form1.lblShips2.Text = $"Ships with size 4 Left: {1 - types["SQUARE"]}";
                }
            }
            panel.Invalidate();
            if (SetCounter == 0)
            {
                MessageBox.Show("No more ships left");
            }
        }

        public override void HoverCheck(object sender, MouseEventArgs e)
        {
            int x = e.X; int y = e.Y;
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
