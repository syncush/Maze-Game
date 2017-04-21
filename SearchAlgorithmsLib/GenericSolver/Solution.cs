using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib {
    /// <summary>
    /// a generic solution for graph search algorithm
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Solution<T> {
        /// <summary>
        /// Gets or sets the chippest path.
        /// </summary>
        /// <value>
        /// list of positions that represent the chippest path.
        /// </value>
        public List<T> ChippestPath { get; set; }
        /// <summary>
        /// Gets or sets the path Cost
        /// </summary>
        /// <value>
        /// the price of the path.
        /// </value>
        public double PathCost { get; set; }
        /// <summary>
        /// Gets or sets number of evaluated nodes.
        /// </summary>
        /// <value>
        /// number of evaluated nodes.
        /// </value>
        public int EvaluatedNodes { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Solution{T}"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="evaluatedNodes">The evaluated nodes.</param>
        /// <param name="pathCost">The path Cost.</param>
        public Solution(Stack<T> path, int evaluatedNodes, double pathCost) {
            ChippestPath = path.ToList();
            this.PathCost = pathCost;
            this.EvaluatedNodes = evaluatedNodes;
        }
    }
}