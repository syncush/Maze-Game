using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib {
    /// <summary>
    /// class for dfs algo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.Searcher{T}" />
    public class DFS<T> : Searcher<T> {
        HashSet<State<T>> greyList;
        HashSet<State<T>> blackList;
        Stack<State<T>> stack;

        /// <summary>
        /// Initializes a new instance of the <see cref="DFS{T}"/> class.
        /// </summary>
        public DFS() {
            // the three colors that can be in dfs algo
            greyList = new HashSet<State<T>>();
            blackList = new HashSet<State<T>>();
            stack = new Stack<State<T>>();
        }

        /// <summary>
        /// Searches the specified searcher.
        /// </summary>
        /// <param name="searcher">The searcher.</param>
        /// <returns></returns>
        public override Solution<T> Search(ISearcheble<T> searcher) {
            State<T>.StatePool.Clear();
            State<T> goal = searcher.GetFinalState();
            stack.Push(searcher.GetInitialState());
            while (stack.Count > 0) {
                State<T> temp = stack.Pop();
                evaluatedNodes++;
                greyList.Add(temp);
                // if the current node is the goal node.
                if (temp.Equals(goal)) {
                    return BackTrace(temp);
                }
                List<State<T>> succerssors = searcher.GetListStates(temp);
                // for each neighbor of the current node:
                foreach (State<T> s in succerssors) {
                    if (!blackList.Contains(s) && !greyList.Contains(s)) {
                        s.CameFrom = temp;
                        stack.Push(s);
                    }
                }
                blackList.Add(temp);
            }
            // threre is no solution!
            return null;
        }
    }
}