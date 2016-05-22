using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Puzzle
{
    public static class NPuzzleSolver
    {
        class NPuzzleNode : IComparable<NPuzzleNode>, IEquatable<NPuzzleNode>
        {
            public NPuzzleNode Parent
            {
                get; private set;
            }

            public NPuzzle State
            {
                get; private set;
            }

            public float G
            {
                get
                {
                    NPuzzleNode No = this;
                    float g = 0.0f;
                    while(No.Parent != null)
                    {
                        No = No.Parent;
                        g += 1.0f;
                    }
                    return g;
                }
            }

            public float H
            {
                get; private set;
            }

            public float F
            {
                get
                {
                    return G + H;
                }
            }

            public NPuzzleNode(NPuzzleNode _Parent, NPuzzle _State)
            {
                Parent = _Parent;
                State = _State;
             
                H = EvaluateState(State);
            }

            public int CompareTo(NPuzzleNode other)
            {
                return F.CompareTo(other.F);
            }

            public IEnumerable<NPuzzleNode> Successors()
            {
                foreach(MOVE Move in State.AvalibleMoves)
                {
                    yield return new NPuzzleNode(this, State.Move(Move));
                }
            }

            public bool Equals(NPuzzleNode other)
            {
                return this.State.Equals(other.State);
            }
        }

        static float EvaluateState(NPuzzle Puzzle)
        {
            float f = 0.0f;

            foreach(PlacedTile T in Puzzle.Tiles)
            {
                iVec2 Target = new iVec2();
                Target.x = T.T.Number % Puzzle.Width;
                Target.y = T.T.Number / Puzzle.Width;


                f += Math.Abs(T.Coord.x - Target.x) + Math.Abs(T.Coord.y - Target.y);
            }

            return f;
        }

        public static IEnumerable<N_Puzzle.MOVE> Solve(NPuzzle Puzzle)
        {
            List<NPuzzleNode> Open = new List<NPuzzleNode>();
            List<NPuzzleNode> Closed = new List<NPuzzleNode>();

            NPuzzleNode Goal = null;

            Open.Add(new NPuzzleNode(null, Puzzle));

            while(Open.Count > 0)
            {
                NPuzzleNode Q = Open.Min();
                Open.Remove(Q);

                if(Q.State.IsSolved)
                {
                    Goal = Q;
                    break;
                }

                foreach(NPuzzleNode S in Q.Successors())
                {
                    if (!Closed.Contains(S))
                    {
                        NPuzzleNode AlreadyExploredOpen = Open.Find(N => N.Equals(S));
                        if (AlreadyExploredOpen != null)
                        {
                            if (AlreadyExploredOpen.F > S.F)
                            {
                                Open.Remove(AlreadyExploredOpen);
                                Open.Add(S);
                            }
                        }
                        else
                        {
                            Open.Add(S);
                        }
                    }
                }

                Closed.Add(Q);
            }

            return Goal.State.PreviousMoves;
        }
    }
}
