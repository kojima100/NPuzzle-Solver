using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Puzzle
{
    public struct iVec2 : IComparable<iVec2>, IEquatable<iVec2>
    {
        public int x, y;

        public iVec2(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public int CompareTo(iVec2 other)
        {
            int C = 0;

            if((y > other.y) || (y == other.y && x > other.x))
            {
                C = 1;
            }
            else if(!this.Equals(other))
            {
                C = -1;
            }

            return C;
        }

        public bool Equals(iVec2 other)
        {
            return x == other.x && y == other.y;
        }
    }

    public struct Tile
    {
        public int Number;
        
        bool IsEmpty
        {
            get
            {
                return Number == 0;
            }
        }

        public Tile(int N)
        {
            Number = N;
        }
    }

    public struct PlacedTile
    {
        public iVec2 Coord;
        public Tile T;

        public PlacedTile(iVec2 _Coords, Tile _t)
        {
            Coord = _Coords;
            T = _t;
        }
    }

    public enum MOVE
    {
        UP = 0,
        DOWN,
        LEFT,
        RIGHT,
    }

    public abstract class NPuzzle : ICloneable, IEquatable<NPuzzle>
    {
        public abstract int Width
        {
            get;
        }

        protected abstract Tile[,] RawTiles
        {
            get;
        }

        private List<MOVE> m_PreviousMoves = new List<MOVE>();
        public List<MOVE> PreviousMoves
        {
            get
            {
                return m_PreviousMoves;
            }
        }

        public NPuzzle()
        {

        }

        public NPuzzle(NPuzzle other)
        {
            this.m_PreviousMoves = other.m_PreviousMoves.ToList();
        }

        protected void FillRawTiles(int Length)
        {
            List<int> AvalibleNumbers = new List<int>();
            for (int i = 0; i != Length; ++i)
            {
                AvalibleNumbers.Add(i);
            }

            Random R = new Random();
            for (int y = 0; y != Width; ++y)
            {
                for (int x = 0; x != Width; ++x)
                {
                    int Num = AvalibleNumbers[R.Next(AvalibleNumbers.Count)];
                    RawTiles[x, y] = new Tile(Num);

                    AvalibleNumbers.Remove(Num);
                }
            }
        }

        public virtual iVec2 EmptyTile()
        {
            iVec2 E = new iVec2();
            foreach(PlacedTile T in Tiles)
            {
                if(T.T.Number == 0)
                {
                    E = T.Coord;
                    break;
                }
            }
            return E;
        }

        public IEnumerable<MOVE> AvalibleMoves
        {
            get
            {
                iVec2 EmptyTileCoord = EmptyTile();
                if (EmptyTileCoord.x != 0)
                {
                    yield return MOVE.RIGHT;
                }

                if (EmptyTileCoord.x != Width - 1)
                {
                    yield return MOVE.LEFT;
                }

                if (EmptyTileCoord.y != 0)
                {
                    yield return MOVE.DOWN;
                }

                if (EmptyTileCoord.y != Width - 1)
                {
                    yield return MOVE.UP;
                }
            }
        }

        public bool IsSolved
        {
            get
            {
                bool Solved = true;
                foreach(PlacedTile T in Tiles)
                {
                    if(T.T.Number != T.Coord.x + (T.Coord.y * Width))
                    {
                        Solved = false;
                        break;
                    }
                }
                return Solved;
            }
        }

        protected bool IsSolvable()
        {
            List<PlacedTile> NonEmptyTiles = Tiles.Where(T => T.T.Number != 0).ToList();

            int Inversions = 0;
            for(int i = 0; i != NonEmptyTiles.Count; ++i)
            {
                for(int j = i + 1; j < NonEmptyTiles.Count; ++j)
                {
                    if(NonEmptyTiles[j].T.Number > NonEmptyTiles[i].T.Number)
                    {
                        ++Inversions;
                    }
                }
            }

            System.Diagnostics.Debug.WriteLineIf(Inversions % 2 == 1, "Not Solvable");

            return Inversions % 2 != 1;
        }

        public IEnumerable<PlacedTile> Tiles
        {
            get
            {
                for(int y = 0; y != Width; ++y)
                {
                    for(int x = 0; x != Width; ++x)
                    {
                        yield return new PlacedTile(new iVec2(x, y), RawTiles[x, y]);
                    }
                }
            }
        }

        protected void SwitchTiles(iVec2 TC1, iVec2 TC2)
        {
            Tile T1 = RawTiles[TC1.x, TC1.y];

            RawTiles[TC1.x, TC1.y] = RawTiles[TC2.x, TC2.y];
            RawTiles[TC2.x, TC2.y] = T1;
        }

        public NPuzzle Move(MOVE Move)
        {
            NPuzzle PuzzleCopy = (NPuzzle)this.Clone();
            PuzzleCopy.m_PreviousMoves.Add(Move);

            iVec2 Empty = EmptyTile();

#if DEBUG
            System.Diagnostics.Debug.WriteLineIf(!this.AvalibleMoves.Contains(Move), "UnValid Move Maid");
#endif

            switch (Move)
            {
                case MOVE.DOWN:
                    PuzzleCopy.SwitchTiles(Empty, new iVec2(Empty.x, Empty.y - 1));
                    break;
                case MOVE.LEFT:
                    PuzzleCopy.SwitchTiles(Empty, new iVec2(Empty.x + 1, Empty.y));
                    break;
                case MOVE.RIGHT:
                    PuzzleCopy.SwitchTiles(Empty, new iVec2(Empty.x - 1, Empty.y));
                    break;
                case MOVE.UP:
                    PuzzleCopy.SwitchTiles(Empty, new iVec2(Empty.x, Empty.y + 1));
                    break;
            }

            return PuzzleCopy;
        }

        public NPuzzle UnMove()
        {
            NPuzzle PuzzleCopy = (NPuzzle)this.Clone();

            if (PuzzleCopy.m_PreviousMoves.Count > 0)
            {
                MOVE Move = PuzzleCopy.m_PreviousMoves[PuzzleCopy.m_PreviousMoves.Count - 1];

                PuzzleCopy.m_PreviousMoves.RemoveAt(m_PreviousMoves.Count - 1);

                iVec2 Empty = PuzzleCopy.EmptyTile();

                switch (Move)
                {
                    case MOVE.UP:
                        PuzzleCopy.SwitchTiles(Empty, new iVec2(Empty.x, Empty.y - 1));
                        break;
                    case MOVE.RIGHT:
                        PuzzleCopy.SwitchTiles(Empty, new iVec2(Empty.x + 1, Empty.y));
                        break;
                    case MOVE.LEFT:
                        PuzzleCopy.SwitchTiles(Empty, new iVec2(Empty.x - 1, Empty.y));
                        break;
                    case MOVE.DOWN:
                        PuzzleCopy.SwitchTiles(Empty, new iVec2(Empty.x, Empty.y + 1));
                        break;
                }
            }

            return PuzzleCopy;
        }

        public override string ToString()
        {
            StringBuilder PuzzleString = new StringBuilder();

            for(int y = 0; y != Width; ++y)
            {
                for(int x = 0; x != Width; ++x)
                {
                    if (RawTiles[x, y].Number != 0)
                    {
                        PuzzleString.Append("¦" + RawTiles[x, y].Number.ToString());
                    }
                    else
                    {
                        PuzzleString.Append("¦  ");
                    }
                }
                PuzzleString.Append("¦" + Environment.NewLine);
            }
            return PuzzleString.ToString();
        }

        public abstract object Clone();

        public bool Equals(NPuzzle other)
        {
            bool IsEqual = false;

            if(this.Width == other.Width)
            {
                IsEqual = true;
                for(int y = 0; y != this.Width && IsEqual; ++y)
                {
                    for(int x = 0; x != this.Width && IsEqual; ++x)
                    {
                        if(this.RawTiles[x, y].Number != other.RawTiles[x, y].Number)
                        {
                            IsEqual = false;
                        }
                    }
                }
            }

            return IsEqual;
        }
    }

    public class Puzzle_8 : NPuzzle
    {
        private Tile[,] m_Tiles = new Tile[3, 3];

        public override int Width
        {
            get
            {
                return 3;
            }
        }

        protected override Tile[,] RawTiles
        {
            get
            {
                return m_Tiles;
            }
        }

        public override object Clone()
        {
            return new Puzzle_8(this);
        }

        public Puzzle_8()
        {
            do
            {
                FillRawTiles(9);
            }
            while (!IsSolvable());
        }

        public Puzzle_8(Puzzle_8 Other) : base(Other)
        {
            m_Tiles = (Tile[,])Other.m_Tiles.Clone();
        }
    }

    public class Puzzle_15 : NPuzzle
    {
        private Tile[,] m_Tiles = new Tile[9, 9];

        public override int Width
        {
            get
            {
                return 4;
            }
        }

        protected override Tile[,] RawTiles
        {
            get
            {
                return m_Tiles;
            }
        }

        public override object Clone()
        {
            return new Puzzle_15(this);
        }

        public Puzzle_15()
        {
            do
            {
                FillRawTiles(16);
            }
            while (!IsSolvable());
        }

        public Puzzle_15(Puzzle_15 Other) : base(Other)
        {
            m_Tiles = (Tile[,])Other.m_Tiles.Clone();
        }

        public override string ToString()
        {
            StringBuilder PuzzleString = new StringBuilder();

            for (int y = 0; y != Width; ++y)
            {
                for (int x = 0; x != Width; ++x)
                {
                    if (RawTiles[x, y].Number != 0)
                    {
                        PuzzleString.AppendFormat("¦{0,0:D2}", RawTiles[x, y].Number);
                    }
                    else
                    {
                        PuzzleString.Append("¦    ");
                    }
                }
                PuzzleString.Append("¦" + Environment.NewLine);
            }
            return PuzzleString.ToString();
        }
    }
}
