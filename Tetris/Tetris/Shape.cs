using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace Tetris
{
    internal class Shape
    {
        public delegate void TouchDownEventHandler(Shape sender);
        public event TouchDownEventHandler TouchDown;

        /// <summary>
        /// API function and Constants used to detect Shift keydown
        /// </summary>
        [DllImport("User", EntryPoint = "GetAsyncKeyState", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern short GetAsyncKeyState(int vkey);
        private const int VK_LSHIFT = 0xA0;
        private const int VK_RSHIFT = 0xA1;

        private Dictionary<int, Point[][]> rotationOffsets = new Dictionary<int, Point[][]>();
        private readonly int xType;
        public string ShapeColor { get; } // Shape color accessor
        private int RotationIndex { get; set; } = 0; // Shape current rotation accessor
        public Point[] CurrentPoints { get; private set; }  // Shape locations accessor
        private readonly Point[][] shapeType =
        {
          new Point[]
          {
            new Point(10, -1),
            new Point(10, -2),
            new Point(10, -3),
            new Point(11, -2)
          },
          new Point[]
          {
            new Point(8, -1),
            new Point(8, -2),
            new Point(9, -2),
            new Point(10, -2)
          },
          new Point[]
          {
            new Point(8, -1),
            new Point(8, -2),
            new Point(9, -1),
            new Point(10, -1)
          },
          new Point[]
          {
            new Point(8, -2),
            new Point(9, -1),
            new Point(9, -2),
            new Point(10, -1)
          },
          new Point[]
          {
            new Point(8, -1),
            new Point(9, -1),
            new Point(9, -2),
            new Point(10, -2)
          },
          new Point[]
          {
            new Point(9, -1),
            new Point(9, -2),
            new Point(9, -3),
            new Point(9, -4)
          },
          new Point[]
          {
            new Point(9, -1),
            new Point(9, -2),
            new Point(10, -1),
            new Point(10, -2)
          }
        }; // 0 - tShape, 1 - lShape, 2 - rlShape, 3 - zShape, 4 - rzShape, 5 - line, 6 - square

        // Constructor
        public Shape(int xType, string shapeColor)
        {
            this.xType = xType;
            this.ShapeColor = shapeColor;
            this.CurrentPoints = shapeType[xType];

            rotationOffsets.Add(0, new[]
            {
              new Point[]
              {
                new Point(0, -1),
                new Point(1, 1),
                new Point(1, 1),
                new Point(1, 0)

              },
              new Point[]
              {
                new Point(0, 0),
                new Point(0, 0),
                new Point(0, 0),
                new Point(-1, -1)

              },
              new Point[]
              {
                new Point(0, 1),
                new Point(0, 0),
                new Point(0, 0),
                new Point(1, 2)
              },
              new Point[]
              {
                new Point(0, 0),
                new Point(-1, -1),
                new Point(-1, -1),
                new Point(-1, -1)
              }
            });

            rotationOffsets.Add(1, new[]
            {
              new Point[]
              {
                new Point(0, -2),
                new Point(1, -1),
                new Point(0, 0),
                new Point(-1, 1)
              },
              new Point[]
              {
                new Point(0, 2),
                new Point(0, 2),
                new Point(1, 1),
                new Point(1, -1)
              },
              new Point[]
              {
                new Point(0, 0),
                new Point(-1, -1),
                new Point(-2, -2),
                new Point(-1, 1)
              },
              new Point[]
              {
                new Point(0, 0),
                new Point(0, 0),
                new Point(1, 1),
                new Point(1, -1)
              }
           });

            rotationOffsets.Add(2, new[]
            {
              new Point[]
              {
                new Point(0, 0),
                new Point(0, 0),
                new Point(-1, -2),
                new Point(-1, -2)
              },
              new Point[]
              {
                new Point(0, -1),
                new Point(1, 0),
                new Point(2, 2),
                new Point(1, 1)
              },
              new Point[]
              {
                new Point(0, 1),
                new Point(0, 1),
                new Point(-1, -1),
                new Point(-1, -1)
              },
              new Point[]
              {
                new Point(0, 0),
                new Point(-1, -1),
                new Point(0, 1),
                new Point(1, 2)
              }
            });

            rotationOffsets.Add(3, new[]
            {
              new Point[]
              {
                new Point(0, 1),
                new Point(-1, -1),
                new Point(0, 0),
                new Point(-1, -2)
              },
              new Point[]
              {
                new Point(0, -1),
                new Point(1, 1),
                new Point(0, 0),
                new Point(1, 2)
              },
              new Point[]
              {
                new Point(0, 1),
                new Point(-1, -1),
                new Point(0, 0),
                new Point(-1, -2)
              },
              new Point[]
              {
                new Point(0, -1),
                new Point(1, 1),
                new Point(0, 0),
                new Point(1, 2)
              }
            });

            rotationOffsets.Add(4, new[]
            {
              new Point[]
              {
                new Point(0, -1),
                new Point(-1, -2),
                new Point(0, 1),
                new Point(-1, 0)
              },
              new Point[]
              {
                new Point(0, 1),
                new Point(1, 2),
                new Point(0, -1),
                new Point(1, 0)
              },
              new Point[]
              {
                new Point(0, -1),
                new Point(-1, -2),
                new Point(0, 1),
                new Point(-1, 0)
              },
              new Point[]
              {
                new Point(0, 1),
                new Point(1, 2),
                new Point(0, -1),
                new Point(1, 0)
              }
            });

            rotationOffsets.Add(5, new[]
            {
              new Point[]
              {
                new Point(0, 0),
                new Point(1, 1),
                new Point(2, 2),
                new Point(3, 3)
              },
              new Point[]
              {
                new Point(0, 0),
                new Point(-1, -1),
                new Point(-2, -2),
                new Point(-3, -3)
              },
              new Point[]
              {
                new Point(0, 0),
                new Point(1, 1),
                new Point(2, 2),
                new Point(3, 3)
              },
              new Point[]
              {
                new Point(0, 0),
                new Point(-1, -1),
                new Point(-2, -2),
                new Point(-3, -3)
              }
            });
        }

        // Moves shape down
        public string[][] MoveDown(string[][] grid)
        {
            foreach (var p in CurrentPoints)
            {
                if (p.Y >= 0)
                {
                    grid[p.Y][p.X] = "";
                }
            }
            var pts = CurrentPoints;
            if (CanMoveBelow(CurrentPoints, grid))
            {
                for (int i = 0; i < CurrentPoints.Count(); i++)
                {
                    CurrentPoints[i].Y += 1;
                    var p = CurrentPoints[i];
                    if (p.Y >= 0)
                    {
                        grid[p.Y][p.X] = ShapeColor;
                    }
                }
            }
            else
            {
                foreach (var p in pts)
                {
                    if (p.Y >= 0)
                    {
                        grid[p.Y][p.X] = ShapeColor;
                    }
                }
            }
            return grid;
        }

        // Moves shape left
        public string[][] MoveLeft(string[][] grid)
        {
            foreach (var p in CurrentPoints)
            {
                if (p.Y >= 0)
                {
                    grid[p.Y][p.X] = "";
                }
            }
            var pts = CurrentPoints;
            if (CanMoveLeft(CurrentPoints, grid))
            {
                for (int i = 0; i < CurrentPoints.Count(); i++)
                {
                    if (CurrentPoints[i].X > 0)
                    {
                        CurrentPoints[i].X -= 1;
                        var p = CurrentPoints[i];
                        grid[p.Y][p.X] = ShapeColor;
                    }
                }
            }
            else
            {
                foreach (var p in pts)
                {
                    if (p.Y >= 0)
                    {
                        grid[p.Y][p.X] = ShapeColor;
                    }
                }
            }
            return grid;
        }

        // Moves shape right
        public string[][] MoveRight(string[][] grid)
        {
            foreach (var p in CurrentPoints)
            {
                if (p.Y >= 0)
                {
                    grid[p.Y][p.X] = "";
                }
            }
            var pts = CurrentPoints;
            if (CanMoveRight(CurrentPoints, grid))
            {
                for (int x = 0; x < CurrentPoints.Count(); x++)
                {
                    if (CurrentPoints[x].X < 19)
                    {
                        CurrentPoints[x].X += 1;
                        Point p = CurrentPoints[x];
                        grid[p.Y][p.X] = ShapeColor;
                    }
                }
            }
            else
            {
                foreach (Point p in pts)
                {
                    if (p.Y >= 0)
                    {
                        grid[p.Y][p.X] = ShapeColor;
                    }
                }
            }
            return grid;
        }

        // Rotates shape
        public string[][] RotateShape(string[][] grid)
        {
            if (xType == 6)
            {
                return grid;
            }

            var shifting = GetAsyncKeyState(VK_LSHIFT) < 0 || GetAsyncKeyState(VK_RSHIFT) < 0;

            foreach (var p in CurrentPoints)
            {
                if (p.Y >= 0)
                {
                    grid[p.Y][p.X] = "";
                }
            }
            if (RotationIndex == 4)
            {
                RotationIndex = 0;
            }

            Point[] pts = (Point[])CurrentPoints.Clone();

            if (!shifting)
            {
                for (int i = 0; i < pts.Count(); i++)
                {
                    pts[i].Offset(rotationOffsets[xType][RotationIndex][i]);
                }
                if (ShapeIsClear(pts, grid))
                {
                    CurrentPoints = pts;
                    RotationIndex += 1;
                }
            }
            else
            {
                if (RotationIndex == 0)
                {
                    RotationIndex = 3;
                }
                else
                {
                    RotationIndex -= 1;
                }
                for (int x = 0; x < pts.Count(); x++)
                {
                    pts[x].Offset(negatePoint(rotationOffsets[xType][RotationIndex][x]));
                }
                if (ShapeIsClear(pts, grid))
                {
                    CurrentPoints = pts;
                }
            }
            foreach (var p in CurrentPoints)
            {
                if (p.Y >= 0)
                {
                    grid[p.Y][p.X] = ShapeColor;
                }
            }
            return grid;
        }

        // Checks if shape can move to the left
        private bool CanMoveLeft(Point[] pts, string[][] grid)
        {
            if (pts.Any(p => p.Y == -1))
            {
                return false;
            }
            foreach (var p in pts)
            {
                if (p.X - 1 < 0)
                {
                    return false;
                }
                if (p.Y >= 0 && (p.X > 0 && p.X < 19))
                {
                    if (!string.IsNullOrEmpty(grid[p.Y][p.X - 1]))
                    {
                        return false;
                    }
                }
                else if (p.X < 0 || p.X > 19)
                {
                    return false;
                }
            }
            return true;
        }

        // Checks if shape can move to the right
        private bool CanMoveRight(Point[] pts, string[][] grid)
        {
            if (pts.Any(p => p.Y == -1))
            {
                return false;
            }
            foreach (var p in pts)
            {
                if (p.X + 1 > 19)
                {
                    return false;
                }
                if (p.Y >= 0 && (p.X > 0 && p.X < 19))
                {
                    if (!string.IsNullOrEmpty(grid[p.Y][p.X + 1]))
                    {
                        return false;
                    }
                }
                else if (p.X < 0 || p.X > 19)
                {
                    return false;
                }
            }
            return true;
        }

        // Checks if shape can move lower
        private bool CanMoveBelow(Point[] pts, string[][] grid)
        {
            foreach (var p in pts)
            {
                if (p.Y + 1 > 29)
                {
                    if (TouchDown != null)
                    {
                        TouchDown(this);
                        return false;
                    }
                }
                if (p.Y >= 0)
                {
                    if (!string.IsNullOrEmpty(grid[p.Y + 1][p.X]))
                    {
                        if (TouchDown != null)
                        {
                            TouchDown(this);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        // Checks if shape can rotate
        private bool ShapeIsClear(Point[] pts, string[][] grid)
        {
            foreach (var p in pts)
            {
                if (p.Y >= 0)
                {
                    if (p.X < 0 || p.X > 19)
                    {
                        return false;
                    }
                    if (!string.IsNullOrEmpty(grid[p.Y][p.X]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Negates a point
        private Point negatePoint(Point p)
        {
            return new Point(-p.X, -p.Y);
        }
    }
}
