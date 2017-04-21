using Academy.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib {
    /// <summary>
    /// a class that represent the bfs algorithm.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.SearcherWithPQ{T}" />
    public class BFS<T> : SearcherWithPQ<T> {
        /// <summary>
        /// The function of the weight in the edeges.
        /// </summary>
        private WeightForEdges<T> we;

        /// <summary>
        /// Initializes a new instance of the <see cref="BFS{T}"/> class.
        /// </summary>
        /// <param name="we">The we.</param>
        public BFS(WeightForEdges<T> we) {
            this.we = we;
        }

        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        public override Solution<T> Search(ISearcheble<T> searchable) {
            State<T>.StatePool.Clear();
            // add the first node to the open list.
            AddToOpenList(searchable.GetInitialState());
            // all the nodes that the algo finished to handle them.
            HashSet<State<T>> closed = new HashSet<State<T>>();
            State<T> goal = searchable.GetFinalState();
            // while the open list is not empty.
            while (OpenListSize > 0) {
                State<T> n = PopOpenList();
                closed.Add(n);
                // if the node that we handle is the goal stateItem.
                if (n.Equals(goal)) {
                    return BackTrace(n);
                }
                List<State<T>> succerssors = searchable.GetListStates(n);
                // for each nieghbor of current node
                foreach (State<T> s in succerssors) {
                    // if the node is bot in the open or close list.
                    if (!closed.Contains(s) && !OpenListContains(s)) {
                        s.CameFrom = n;
                        s.Cost = n.Cost + we.GetWeight(n, s);
                        AddToOpenList(s);
                    }
                    else {
                        // if the new Cost is lower then the current Cost.
                        if (n.Cost + we.GetWeight(n, s) < s.Cost) {
                            s.Cost = n.Cost + we.GetWeight(n, s);
                            if (!OpenListContains(s)) {
                                AddToOpenList(s);
                            }
                            else {
                                RemoveFromOpenList(s);
                                AddToOpenList(s);
                            }
                        }
                    }
                }
            }
            /*there is no solution*/
            return null;
        }
    }
}