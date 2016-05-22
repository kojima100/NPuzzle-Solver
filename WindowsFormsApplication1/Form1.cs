using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public N_Puzzle.NPuzzle MainPuzzle;

        public List<N_Puzzle.MOVE> Solution = null;

        public Form1()
        {
            InitializeComponent();
       }

        System.Drawing.Bitmap m_Background;
        System.Drawing.Bitmap m_Foreground;

        private delegate void DrawBackgroundDelegate(N_Puzzle.NPuzzle Puzzle, int X, int Y, int Size);
        public void DrawBackground(N_Puzzle.NPuzzle Puzzle, int BoardX, int BoardY, int SquareSize)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DrawBackgroundDelegate(DrawBackground), Puzzle, BoardX, BoardY, SquareSize);
            }
            else
            {
                m_Background = new Bitmap(BoardX, BoardY);
                m_Foreground = new Bitmap(BoardX, BoardY);

                Graphics BackgroundGraphics = Graphics.FromImage(m_Background);

                SolidBrush BlackBrush = new SolidBrush(Color.Black);
                SolidBrush WhiteBrush = new SolidBrush(Color.White);

                SolidBrush CurrentBrush;

                for (int y = 0; y != Puzzle.Width; ++y)
                {
                    for (int x = 0; x != Puzzle.Width; ++x)
                    {
                        if (y % 2 == 0)
                        {
                            CurrentBrush = x % 2 != 0 ? BlackBrush : WhiteBrush;
                        }
                        else
                        {
                            CurrentBrush = x % 2 != 0 ? WhiteBrush : BlackBrush;
                        }

                        BackgroundGraphics.FillRectangle(CurrentBrush, (float)(x * SquareSize), (float)(y * SquareSize), SquareSize, SquareSize);
                    }
                }

                BlackBrush.Dispose();
                WhiteBrush.Dispose();
                BackgroundGraphics.Dispose();
            }
        }

        private delegate void DrawForegroundDelegate(N_Puzzle.NPuzzle Puzzle, int X, int Y, int Size);
        public void DrawForeground(N_Puzzle.NPuzzle Puzzle, int TopX, int TopY, int SquareSize)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DrawForegroundDelegate(DrawForeground), Puzzle, TopX, TopY, SquareSize);
            }
            else
            {
                Graphics ForegroundGraphics = Graphics.FromImage(m_Foreground);
                ForegroundGraphics.DrawImage(m_Background, new Point(TopX, TopY));
                if (Puzzle != null)
                {
                    foreach(N_Puzzle.PlacedTile Tiles in Puzzle.Tiles)
                    {
                        Point P = new Point();
                        P.X = ((Tiles.Coord.x - 1) * SquareSize) + TopX;
                        P.Y = ((Tiles.Coord.y - 1) * SquareSize) + TopY;

                        ForegroundGraphics.DrawString(Tiles.T.Number.ToString(), Font, Brushes.Blue, P);
                    }
                }
                ForegroundGraphics.Dispose();
            }
        }

        private delegate void PaintDelegate(object sender, PaintEventArgs e);
        private void Frm_Main_Paint(object sender, PaintEventArgs e)
        {
            //if (this.InvokeRequired)
            //{
            //    this.Invoke(new PaintDelegate(Frm_Main_Paint), sender, e);
            //}
            //else
            //{
            //    BlitGraphics.Graphics.Clear(this.BackColor);
            //    BlitGraphics.Graphics.DrawImage(m_Foreground, 1, 1);
            //    BlitGraphics.Render();
            //}
        }

        int CurrentIndex = 0;

        private void Btn_Gen_Click(object sender, EventArgs e)
        {
            Solution = null;
            Lst_Moves.DataSource = null;
            Lbl_Solved.Text = "";
            Lbl_CurrentMove.Text = "";

            if (Rad_8Puzzle.Checked)
            {
                MainPuzzle = new N_Puzzle.Puzzle_8();
            }
            else
            {
                MainPuzzle = new N_Puzzle.Puzzle_15();
            }

            Lbl_Puzzle.Text = MainPuzzle.ToString();

            Btn_Solve.Enabled = true;
            Btn_MoveBack.Enabled = false;
            Btn_MoveForward.Enabled = false;
        }

        private void Solve()
        {
            N_Puzzle.IEvaluation Eval = new N_Puzzle.ManhattanDistanceEvaluator();
            Solution = N_Puzzle.NPuzzleSolver.Solve(MainPuzzle, Eval).ToList();

            OnSolved();
        }

        private delegate void OnSolvedDelegate();
        private void OnSolved()
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new OnSolvedDelegate(OnSolved));
            }
            else
            {
                MessageBox.Show("SOLVED!!!");

                Lbl_Solved.Text = "Solved in " + Solution.Count + " moves.";
                Lbl_CurrentMove.Text = "Move 1 of " + Solution.Count;

                Lst_Moves.DataSource = Solution;
                Lst_Moves.SelectedIndex = 0;

                Btn_Gen.Enabled = true;
                Btn_Solve.Enabled = false;
                Btn_MoveBack.Enabled = true;
                Btn_MoveForward.Enabled = true;
                CurrentIndex = 0;

                Cursor.Current = Cursors.Default;
            }
        }

        private System.Threading.Thread SolveThread;

        private void Btn_Solve_Click(object sender, EventArgs e)
        {
            Btn_Gen.Enabled = false;
            Btn_Solve.Enabled = false;

            SolveThread = new System.Threading.Thread(new System.Threading.ThreadStart(Solve));
            SolveThread.Priority = System.Threading.ThreadPriority.Highest;

            SolveThread.Start();

            Cursor.Current = Cursors.WaitCursor;
        }

        private void Btn_MoveForward_Click(object sender, EventArgs e)
        {
            if (Solution != null && CurrentIndex < Solution.Count)
            {
                MainPuzzle = MainPuzzle.Move(Solution[CurrentIndex]);
                Lbl_Puzzle.Text = MainPuzzle.ToString();
                Lst_Moves.SelectedIndex = CurrentIndex;
                Lbl_CurrentMove.Text = "Move " + (CurrentIndex + 1) + " of " + Solution.Count;
                ++CurrentIndex;
            }
        }

        private void Btn_MoveBack_Click(object sender, EventArgs e)
        {
            if (Solution != null && CurrentIndex > 0)
            {
                MainPuzzle = MainPuzzle.UnMove();
                Lbl_Puzzle.Text = MainPuzzle.ToString();
                Lst_Moves.SelectedIndex = --CurrentIndex;
                Lbl_CurrentMove.Text = "Move " + (CurrentIndex + 1) + " of " + Solution.Count;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(SolveThread.IsAlive)
            {
                SolveThread.Abort();
            }
        }
    }
}
