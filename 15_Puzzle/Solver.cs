using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Puzzle
{
    public interface IEvaluation
    {
        float Evaluate(NPuzzle Puzzle);
    }

    public class ManhattanDistanceEvaluator : IEvaluation
    {
        public float Evaluate(NPuzzle Puzzle)
        {
            float f = 0.0f;

            foreach (PlacedTile T in Puzzle.Tiles)
            {
                iVec2 Target = new iVec2();
                Target.x = T.T.Number % Puzzle.Width;
                Target.y = T.T.Number / Puzzle.Width;


                f += Math.Abs(T.Coord.x - Target.x) + Math.Abs(T.Coord.y - Target.y);
            }

            return f;
        }
    }

    public class FringeDistanceEvaluator : IEvaluation
    {
        public float Evaluate(NPuzzle Puzzle)
        {
            float f = 0.0f;

            foreach (PlacedTile T in Puzzle.Tiles)
            {
                if(IsFringe(T.T.Number))
                {
                    iVec2 Target = new iVec2();
                    Target.x = T.T.Number % Puzzle.Width;
                    Target.y = T.T.Number / Puzzle.Width;


                    f += Math.Abs(T.Coord.x - Target.x) + Math.Abs(T.Coord.y - Target.y);
                }
            }

                return f;
        }

        //shut up, I know it's lazy
        private readonly int[] Fringe = { 3, 7, 11, 15, 12, 13, 14 };
        private bool IsFringe(int Number)
        {
            return Fringe.Contains(Number);
        }
    }

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

            private IEvaluation m_Evaluator;
            public IEvaluation Evaluator
            {
                get
                {
                    return m_Evaluator;
                }

                private set
                {
                    m_Evaluator = value;
                    H = Evaluator.Evaluate(State);
                }
            }

            public float G
            {
                get
                {
                    return (float)State.PreviousMoves.Count;
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

            public NPuzzleNode(NPuzzleNode _Parent, NPuzzle _State, IEvaluation Eval)
            {
                Parent = _Parent;
                State = _State;
                Evaluator = Eval;
            }

            public int CompareTo(NPuzzleNode other)
            {
                return F.CompareTo(other.F);
            }

            public IEnumerable<NPuzzleNode> Successors()
            {
                foreach(MOVE Move in State.AvalibleMoves)
                {
                    yield return new NPuzzleNode(this, State.Move(Move), this.Evaluator);
                }
            }

            public bool Equals(NPuzzleNode other)
            {
                return this.State.Equals(other.State);
            }
        }

        public static IEnumerable<N_Puzzle.MOVE> Solve(NPuzzle Puzzle, IEvaluation Eval)
        {
            List<NPuzzleNode> Open = new List<NPuzzleNode>();
            List<NPuzzleNode> Closed = new List<NPuzzleNode>();

            NPuzzleNode Goal = null;

            Open.Add(new NPuzzleNode(null, Puzzle, Eval));

            while(Open.Count > 0)
            {
                NPuzzleNode Q = Open[0];
                Open.Remove(Q);

                if(Q.H == 0.0f)
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
                                Open.InsertSorted(S);
                            }
                        }
                        else
                        {
                            Open.InsertSorted(S);
                        }
                    }
                }

                Closed.Add(Q);
            }

            return Goal.State.PreviousMoves;
        }

        public static void InsertSorted<T>(this List<T> OrderedList, T t) where T : IComparable<T>
        {
            int i = 0;
            for(i = 0; i != OrderedList.Count; ++i)
            {
                if((OrderedList[i].CompareTo(t) == 1) || (i == OrderedList.Count - 1))
                {
                    break;
                }
            }
            OrderedList.Insert(i, t);
        }
    }
}
