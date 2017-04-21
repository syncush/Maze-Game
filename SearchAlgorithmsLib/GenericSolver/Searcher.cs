using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.ISearcher{T}" />
    public abstract class Searcher<T> : ISearcher<T>
    {
        /// <summary>
        /// The evaluated nodes
        /// </summary>
        protected int evaluatedNodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="Searcher{T}"/> class.
        /// </summary>
        public Searcher()
        {
            evaluatedNodes = 0;
        }

        /// <summary>
        /// Backs the trace.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public Solution<T> BackTrace(State<T> s)
        {
            Stack<T> path = new Stack<T>();
            double sum = s.Cost;
            do
            {
                path.Push(s.stateItem);
                s = s.CameFrom;
            } while (s != null);
            return new Solution<T>(path, evaluatedNodes, sum);
        }

        /// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        /// <summary>
        /// Searches the specified searcher.
        /// </summary>
        /// <param name="searcher">The searcher.</param>
        /// <returns></returns>
        public abstract Solution<T> Search(ISearcheble<T> searcher);
    }
}
