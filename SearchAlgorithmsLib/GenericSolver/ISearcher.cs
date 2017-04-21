using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib {
    public interface ISearcher<T> {
        /// <summary>
        /// Searches the specified searcher.
        /// </summary>
        /// <param name="searcher">The searcher.</param>
        /// <returns></returns>
        Solution<T> Search(ISearcheble<T> searcher);

        /// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <returns></returns>
        int GetNumberOfNodesEvaluated();

        /// <summary>
        /// Backs the trace.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        Solution<T> BackTrace(State<T> s);
    }
}