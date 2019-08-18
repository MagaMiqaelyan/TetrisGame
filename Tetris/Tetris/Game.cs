using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Tetris
{
    /// <summary>
    /// Extended DataGridView 
    /// DoubleBuffered. Restricts user selection of cells.
    /// </summary>
    internal class Game : DataGridView
    {
        // Constants used with Keypresses
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_LBUTTONDBLCLK = 0x203;
        private const int WM_KEYDOWN = 0x100;
        private const int VK_LEFT = 0x25;
        private const int VK_RIGHT = 0x27;
        private const int VK_DOWN = 0x28;
        private const int VK_UP = 0x26;

        // Custom events
        public delegate void IncrementScoreEventHandler(int newPoints);
        public event IncrementScoreEventHandler IncrementScore;

        public delegate void ShapeChangedEventHandler(Point[] shapePoints, string shapeColor);
        public event ShapeChangedEventHandler ShapeChanged;

        private int rowCounter = 0;

        // Set up timer handlers
        public Game()
        {
            this.DoubleBuffered = true;
            tmr.Tick += TmrTick;
            flashtmr.Tick += FlashTmrTick;
        }

        /// <summary>
        /// OnRowPrePaint
        /// Avoid DGV cell focussing
        /// </summary>
        protected override void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)
        {
            var p = (int)e.PaintParts;
            p -= (int)DataGridViewPaintParts.Focus;
            e.PaintParts = (DataGridViewPaintParts)p;
            base.OnRowPrePaint(e);
        }

        /// <summary>
        /// WndProc
        /// Avoid DGV focussing, and catch Keypresses
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDBLCLK || m.Msg == WM_LBUTTONDOWN)
            {
                return;
            }
            else if (m.Msg == WM_KEYDOWN)
            {
                if (m.WParam.ToInt32() == VK_LEFT)
                {
                    MoveLeft();
                }
                else if (m.WParam.ToInt32() == VK_RIGHT)
                {
                    MoveRight();
                }
                else if (m.WParam.ToInt32() == VK_DOWN)
                {
                    MoveDown();
                }
                else if (m.WParam.ToInt32() == VK_UP)
                {
                    RotateShape();
                }
                return;
            }
            base.WndProc(ref m);
        }

        // Timers used in game play
        private Timer tmr = new Timer { Interval = 500 };
        private Timer flashtmr = new Timer { Interval = 125 };

        // Variables used with flashtmr tick event
        private int flashCounter = 1;
        private int flashRow;
        private bool missATick = false;

        // Variables holding cell colors and shapes
        private string[][] gameGrid;
        private Shape currentShape;
        private List<Shape> listShapes = new List<Shape>();

        private Random r = new Random();
        private int moveCounter = 0;

        // Clears the game board and score and initiates a new game
        public void NewGame()
        {
            tmr.Stop();
            tmr.Interval = 500;
            listShapes.Clear();
            moveCounter = 0;

            gameGrid = new string[30][];
            for (int x = 1; x <= 30; x++)
            {
                string[] row = new string[20];
                gameGrid[x - 1] = (string[])row.Clone();
            }
            NewShape();
            currentShape = listShapes[0];
            ShapeChanged?.Invoke(currentShape.CurrentPoints, currentShape.ShapeColor); // ShapeChanged != NULL
            rowCounter = 0;
            tmr.Start();
            flashtmr.Start();
        }

        // Creates a new falling shape
        private void NewShape()
        {
            string[] sc = { "Black", "White", "RoyalBlue", "DarkBlue" };
            Shape ns = new Shape(r.Next(0, 7), sc[r.Next(0, 4)]);
            listShapes.Add(ns);
            ns.TouchDown += CurrentShapeTouchDown;
            HasChanged(gameGrid, false, -1);
        }

        // Responds to LEFT arrow button key down
        public void MoveLeft()
        {
            if (currentShape == null)
            {
                return;
            }
            gameGrid = currentShape.MoveLeft(gameGrid);
            HasChanged(gameGrid, false, -1);
        }

        // Responds to RIGHT arrow button key down
        public void MoveRight()
        {
            if (currentShape == null)
            {
                return;
            }
            gameGrid = currentShape.MoveRight(gameGrid);
            HasChanged(gameGrid, false, -1);
        }

        // Responds to DOWN arrow button key down
        public void MoveDown()
        {
            do
            {
                for (int i = 0; i < listShapes.Count; i++)
                {
                    if (i > listShapes.Count - 1)
                    {
                        continue;
                    }
                    gameGrid = listShapes[i].MoveDown(gameGrid);
                    HasChanged(gameGrid, false, -1);
                }
                break;
            }
            while (true);
            moveCounter += 1;
        }

        // Responds to UP arrow button key down
        public void RotateShape()
        {
            if (currentShape == null)
            {
                return;
            }
            gameGrid = currentShape.RotateShape(gameGrid);
            HasChanged(gameGrid, false, -1);
            ShapeChanged?.Invoke(currentShape.CurrentPoints, currentShape.ShapeColor); // ShapeChanged != null
        }

        // On tick, all shapes move down one row
        private void TmrTick(object sender, EventArgs e)
        {
            if (missATick)
            {
                return;
            }
            if (moveCounter >= 27)
            {
                moveCounter = 0;
                NewShape();
                if (listShapes.Count == 1)
                {
                    currentShape = listShapes[0];
                    ShapeChanged?.Invoke(currentShape.CurrentPoints, currentShape.ShapeColor);
                }
            }
            MoveDown();
        }

        // Responds to shape touchdown
        private void CurrentShapeTouchDown(Shape sender)
        {
            if (sender.CurrentPoints.Any(p => p.Y < 0))
            {
                tmr.Stop();
            }

            currentShape.TouchDown -= CurrentShapeTouchDown;
            listShapes.Remove(sender);

            if (listShapes.Count < 1)
            {
                currentShape = null;
                moveCounter = 27;
            }
            else
            {
                currentShape = listShapes[0];
                ShapeChanged?.Invoke(currentShape.CurrentPoints, currentShape.ShapeColor); // ShapeChanged != NULL
            }
        }

        // Clears full rows as they occur
        private void FlashTmrTick(object sender, EventArgs e)
        {
            switch (flashCounter)
            {
                case 1:
                    flashRow = FindFullRow();
                    if (flashRow > -1)
                    {
                        flashCounter = 2;
                        HasChanged(gameGrid, true, flashRow);
                    }
                    break;
                case 2:
                    flashCounter = 3;
                    HasChanged(gameGrid, false, -1);
                    break;
                case 3:
                    flashCounter = 4;
                    HasChanged(gameGrid, true, flashRow);
                    break;
                case 4:
                    var newGrid = new List<string[]>(gameGrid);
                    if (listShapes.Count == 0)
                    {
                        return;
                    }
                    foreach (var p in listShapes.Last().CurrentPoints)
                    {
                        if (p.Y > -1)
                        {
                            newGrid[p.Y][p.X] = "";
                        }
                    }
                    var newRow = new string[20];
                    newGrid.RemoveAt(flashRow);
                    newGrid.Insert(0, newRow);
                    missATick = true;
                    gameGrid = newGrid.ToArray();
                    flashCounter = 1;
                    MoveDown();
                    HasChanged(gameGrid, false, -1);
                    missATick = false;
                    rowCounter += 1;
                    if (rowCounter % 10 == 0)
                    {
                        tmr.Interval -= 40;
                        IncrementScore?.Invoke(((int)((1000 - tmr.Interval) * 0.35))); // IncrementStore != NULL
                    }
                    else if (rowCounter % 5 == 0)
                    {
                        tmr.Interval -= 20;
                        IncrementScore?.Invoke(((int)((1000 - tmr.Interval) * 0.25))); // IncrementStore != NULL
                    }
                    else
                    {
                        IncrementScore?.Invoke(((int)((1000 - tmr.Interval) * 0.05))); // IncrementStore != NULL
                    }
                    break;
            }
        }

        // Finds full rows in DGV
        private int FindFullRow()
        {
            for (int i = 29; i >= 0; i--)
            {
                if (gameGrid[i].All(s => !string.IsNullOrEmpty(s)))
                {
                    return i;
                }
            }
            return -1;
        }

        // Renders the colors in the DGV
        private void HasChanged(string[][] grid, bool flash, int flashRow)
        {
            Dictionary<string, Color> colors = new Dictionary<string, Color>()
            {
               {"Black", Color.Black},
               {"White", Color.White},
               {"RoyalBlue", Color.RoyalBlue},
               {"DarkBlue", Color.DarkBlue}
            };

            Dictionary<string, Color> flashColors = new Dictionary<string, Color>()
            {
               {"Black", Color.FromArgb(0,0,0)},
               {"White", Color.FromArgb(255, 255, 255)},
               {"RoyalBlue", Color.FromArgb(65,105,225)},
               {"DarkBlue", Color.FromArgb(0,0,139)}
            };

            for (int y = 0; y <= 29; y++)
            {
                for (int x = 0; x <= 19; x++)
                {
                    if (string.IsNullOrEmpty(grid[y][x]))
                    {
                        this.Rows[y].Cells[x].Style.BackColor = Color.SlateGray;
                    }
                    else
                    {
                        if (!flash || (flash && !(flashRow == y)))
                        {
                            this.Rows[y].Cells[x].Style.BackColor = colors[grid[y][x]];
                        }
                        else
                        {
                            this.Rows[y].Cells[x].Style.BackColor = flashColors[grid[y][x]];
                        }
                    }
                }
            }
        }
    }
}
