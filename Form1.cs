using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Battleships
{
    public partial class Form1 : Form
    {

        public Ship Enemy { get; set; }
        public static Ship Our { get; set; }
        public CreateShip Create { get; set; }
        public bool btn { get; set; }
        public static StatusStrip statusStrip;
        public static ToolStripStatusLabel toolStripStatusLabel;
        public static int SetCounter { get; set; }
        public static int hits { get; set; }
        public static int misses { get; set; }

        public System.Windows.Forms.Label lblENEMY { get; set; }
        public System.Windows.Forms.Label lblOUR { get; set; }
        public System.Windows.Forms.Label lblCreate { get; set; }
        public static System.Windows.Forms.Label lblShips1 { get; set; }
        public static System.Windows.Forms.Label lblShips2 { get; set; }
        
        public static System.Windows.Forms.Label lblShipsLeft { get; set; }
        public static System.Windows.Forms.Label lblTurn { get; set; }

        public System.Windows.Forms.Button AddShip { get; set; }
        public System.Windows.Forms.Button New_Game { get; set; }
        public System.Windows.Forms.Button btnSet { get; set; }
        public static RadioButton radioButton1 { get; set; }
        public static RadioButton radioButton2 { get; set; }
        public static RadioButton radioButton3 { get; set; }
        public static GroupBox groupBox { get; set; }
        public static RadioButton rbVertical { get; set; }
        public static RadioButton rbHorizontal { get; set; }
        public static GroupBox gbSides { get; set; }

        public Form1()
        {
            InitializeComponent();
            init();
        }
        public static void UpdateStatusStrip(int hit, int miss)
        {
            hits += hit;
            misses += miss;
            toolStripStatusLabel.Text = $"HITS: {hits}  MISSES: {misses}";
        }

        private void init()
        {
            btn = false;
            SetCounter = 20;
            this.DoubleBuffered = true;
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            statusStrip.Items.Add(toolStripStatusLabel);
            this.Controls.Add(statusStrip);
            hits = 0;
            misses = 0;
            initLabels();
            radioBtns();
            
        }

        private void radioBtns() {
            groupBox = new GroupBox();
            groupBox.Text = "Options";
            groupBox.Location = new System.Drawing.Point(700, 200);
            groupBox.Size = new System.Drawing.Size(200, 100);

            radioButton1 = new RadioButton();
            radioButton1.Text = "Two";
            radioButton1.Location = new System.Drawing.Point(6, 19);

            radioButton2 = new RadioButton();
            radioButton2.Text = "Three";
            radioButton2.Location = new System.Drawing.Point(6, 42);
            
            radioButton3 = new RadioButton();
            radioButton3.Text = "Square";
            radioButton3.Location = new System.Drawing.Point(6, 65);
            radioButton3.CheckedChanged += checkChange;

            groupBox.Controls.Add(radioButton1);
            groupBox.Controls.Add(radioButton2);
            groupBox.Controls.Add(radioButton3);

            gbSides = new GroupBox();
            gbSides.Text = "Sides";
            gbSides.Location = new System.Drawing.Point(700, 300);
            gbSides.Size = new System.Drawing.Size(200, 100);

            rbVertical = new RadioButton();
            rbVertical.Text = "Vertical";
            rbVertical.Location = new System.Drawing.Point(6, 42);

            rbHorizontal = new RadioButton();
            rbHorizontal.Text = "Horizontal";
            rbHorizontal.Location = new System.Drawing.Point(6, 19);

            gbSides.Controls.Add(rbVertical);
            gbSides.Controls.Add(rbHorizontal);

        }
        public void checkChange(object sender, EventArgs e)
        {
            if(radioButton3.Checked) { 
                rbHorizontal.Enabled= false; 
                rbVertical.Enabled= false;
            }
            else
            {
                rbHorizontal.Enabled = true;
                rbVertical.Enabled = true;
            }
        }
        private void initLabels()
        {
            lblENEMY = new System.Windows.Forms.Label
            {
                Text = "ENEMY",
                Location = new Point(58, 40),
                Size = new Size(58, 16),
                ForeColor = Color.Black
            };
            lblOUR = new System.Windows.Forms.Label
            {
                Text = "!ENEMY",
                Location = new Point(550, 40),
                Size = new Size(58, 16),
                ForeColor = Color.Black
            };
            lblCreate = new System.Windows.Forms.Label
            {
                Text = "CREATE",
                Location = new Point(58, 40),
                Size = new Size(58, 16),
                ForeColor = Color.Black
            };
            lblShipsLeft = new System.Windows.Forms.Label
            {
                Text = $"Ships with size 2 Left: 2",
                Location = new Point(700, 100),
                Size = new Size(150, 16),
                ForeColor = Color.Black
            };
            lblTurn=new System.Windows.Forms.Label
            {
                Text = "YOUR TURN",
            Location = new Point(88 + 150, 40),
            Size = new Size(150, 20),
            ForeColor = Color.Black
            };
            lblShips1 = new System.Windows.Forms.Label
            {
                Text = "Ships with size 3 Left: 2",
                Location = new Point(700, 116),
                Size = new Size(200, 16),
                ForeColor = Color.Black
            };
            lblShips2 = new System.Windows.Forms.Label
            {
                Text = "Ships with size 4 Left: 1",
                Location = new Point(700, 132),
                Size = new Size(200, 16),
                ForeColor = Color.Black
            };
            newShips();
            AddShip = new System.Windows.Forms.Button();
            AddShip.Location = new Point(700,60);
            AddShip.Text= "START";
            AddShip.Click += AddShipClick;

            New_Game = new System.Windows.Forms.Button();
            New_Game.Size = new Size(100, 50);
            New_Game.Location = new Point((lblENEMY.Location.X+(lblOUR.Location.X+Our.panel.Size.Width))-50, 100);
            New_Game.Text = "NEW GAME";
            New_Game.Click += newGame;

            this.btnSet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.btnSet.Size = new Size(200, 70);
            this.btnSet.Location = new System.Drawing.Point(this.Size.Width / 2 - (btnSet.Size.Width / 2), this.Size.Height / 2 - btnSet.Size.Height);
            this.btnSet.Name = "btnSet";
            this.btnSet.TabIndex = 5;
            this.btnSet.Text = "START!";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSetClick);
            this.Controls.Add(btnSet);
        }
        public void newShips()
        {
            Enemy = new EnemyShip();
            Our = new OurShip();
            Create = new CreateShip();
            Ship.EnemyHits = 0;
            Ship.OurHits = 0;
            Create.InitializeMatrixPanel(lblCreate.Location.X, 60);
            Enemy.InitializeMatrixPanel(lblENEMY.Location.X, 60);
            Our.InitializeMatrixPanel(lblOUR.Location.X, 60);
        }

        private void newGame(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.Controls.Add(btnSet);
            newShips();
        }

        private void AddShipClick(object sender, EventArgs e)
        {
            if (Create.SetCounter != 0)
            {
                MessageBox.Show("Must use all of the ships");
                return;
            }
            Our.matrix = Create.matrix;
            this.Controls.Clear();
            this.Controls.Add(lblENEMY);
            this.Controls.Add(lblOUR);
            this.btn = true;
            this.Controls.Add(Enemy.panel);
            this.Controls.Add(Our.panel);
            this.Controls.Add(statusStrip);
            this.Controls.Add(New_Game);
            this.Controls.Add(lblTurn);

        }

        private void btnSetClick(object sender, EventArgs e)
        {
            this.Controls.Clear();
            radioButton1.Checked = true;
            rbHorizontal.Checked = true;
            this.Controls.Add(lblCreate);
            this.Controls.Add(Create.panel);
            this.Controls.Add(AddShip);
            this.Controls.Add(lblShipsLeft);
            this.Controls.Add(lblShips2);
            this.Controls.Add(lblShips1);
            this.Controls.Add(groupBox);
            this.Controls.Add(gbSides);
        }
    }
}
