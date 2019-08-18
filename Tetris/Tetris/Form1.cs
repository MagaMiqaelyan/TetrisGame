using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Tetris
{
    public partial class FormTetris : Form
    {
        private int score = 0;
        // Sets up handlers
        public FormTetris()
        {
            InitializeComponent();
            this.Shown += Form_Shown;
           // this.Paint += Form_Paint;
            this.buttonNewGame.Click += buttonNewGame_Click;
            this.game.IncrementScore += game_IncrementScore;
            this.game.ShapeChanged += game_ShapeChanged;
        }

        private void FormTetris_Load(object sender, EventArgs e)
        {
            for (int x = 1; x <= 20; x++)
            {
                game.Columns.Add(new DataGridViewTextBoxColumn());
                game.Columns[x - 1].Width = 15;
                if (x < 7)
                {
                    nextShapePreview.Columns.Add(new DataGridViewTextBoxColumn());
                    nextShapePreview.Columns[x - 1].Width = 15;
                }
            }
            nextShapePreview.Rows.Add(6);
            game.Rows.Add(30);
            for (int x = 1; x <= 30; x++)
            {
                game.Rows[x - 1].Height = 15;
                if (x < 7)
                {
                    nextShapePreview.Rows[x - 1].Height = 15;
                }
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // this.TransparencyKey = Color.White;
        }

        // Removes focus from DGVs
        private void Form_Shown(object sender, EventArgs e)
        {
            nextShapePreview.CurrentCell = null;
            nextShapePreview.ShowCellToolTips = false;
            game.CurrentCell = null;
            game.ShowCellToolTips = false;
        }

        // Renders game form
        //private void Form_Paint(object sender, PaintEventArgs e)
        //{
        //    Point[] points = { game.Location, new Point(game.Left, game.Bottom), new Point(game.Right, game.Bottom), new Point(game.Right, game.Top) };
        //    Pen silverPen = new Pen(Color.Black, 2);
        //    e.Graphics.DrawLines(silverPen, points);
        //    e.Graphics.DrawLine(silverPen, game.Right + 2, game.Bottom - 212, game.Right + 2, game.Bottom + 1); //left
        //    e.Graphics.DrawLine(silverPen, game.Right + 1, game.Bottom - 213, game.Right + 126, game.Bottom - 213); //top
        //    e.Graphics.DrawLine(silverPen, game.Right + 2, game.Bottom, game.Right + 125, game.Bottom); //bottom
        //    e.Graphics.DrawLine(silverPen, game.Right + 125, game.Bottom - 212, game.Right + 125, game.Bottom + 1); //right
        //    var xPosition = game.Right + 4;
        //    var yPosition = game.Bottom - 16;
        //    int[] cellLines = { 1, 2, 9, 10, 13, 14 };
        //    for (int y = 1; y <= 14; y++)
        //    {
        //        if (cellLines.Contains(y))
        //        {
        //            for (int x = 0; x <= 7; x++)
        //            {
        //                e.Graphics.FillRectangle(Brushes.Silver, new Rectangle(xPosition + (x * 15), yPosition, 14, 14));
        //            }
        //        }
        //        else
        //        {
        //            e.Graphics.FillRectangle(Brushes.Silver, new Rectangle(xPosition, yPosition, 14, 14));
        //            e.Graphics.FillRectangle(Brushes.Silver, new Rectangle(xPosition + (7 * 15), yPosition, 14, 14));
        //        }
        //        yPosition -= 15;
        //    }
        //}

        // Initiates new game
        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            score = 0;
            lblScore.Text = score.ToString("000000");
            game.NewGame();
        }

        private void nextShapePreview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           // this.TransparencyKey = Color.White;
        }

        // Increases score and updates score display
        private void game_IncrementScore(int newPoints)
        {
            score += newPoints;
            lblScore.Text = score.ToString("000000");
        }

        // Updates shape preview
        private void game_ShapeChanged(Point[] shapePoints, string shapeColor)
        {
            Point[] pts = (Point[])shapePoints.Clone();
            for (int y = 0; y <= 5; y++)
            {
                for (int x = 0; x <= 5; x++)
                {
                    nextShapePreview.Rows[y].Cells[x].Style.BackColor = Color.SlateGray;
                }
            }
            int minX = pts.Min(p => p.X);
            int minY = pts.Min(p => p.Y);
            for (int x = 0; x <= pts.GetUpperBound(0); x++)
            {
                pts[x].Offset(-minX, -minY);
            }
            int w = pts.Max(p => p.X) - pts.Min(p => p.X);
            int h = pts.Max(p => p.Y) - pts.Min(p => p.Y);
            int offSetX = Convert.ToInt32(Math.Floor((6 - w) / 2.0));
            int offSetY = Convert.ToInt32(Math.Floor((6 - h) / 2.0));
            Dictionary<string, Color> colors = new Dictionary<string, Color>()
            {
                {"Black", Color.Black},
                {"White", Color.White},
                {"RoyalBlue", Color.RoyalBlue},
                {"DarkBlue", Color.DarkBlue}
            };
            for (int x = 0; x <= pts.GetUpperBound(0); x++)
            {
                pts[x].Offset(offSetX, offSetY);
                nextShapePreview.Rows[pts[x].Y].Cells[pts[x].X].Style.BackColor = colors[shapeColor];
            }
            game.Focus();
            game.CurrentCell = null;
        }
    }
}
