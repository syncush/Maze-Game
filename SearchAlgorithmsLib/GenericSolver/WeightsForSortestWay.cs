using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// if we want the shortest path, we put all the weights on edeges to be 1.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.WeightForEdges{T}" />
    public class WeightsForShortestWay<T>: WeightForEdges<T>
    {
        /// <summary>
        /// Gets the weight.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns>The weight between two states</returns>
        public override double GetWeight(State<T> v1, State<T> v2)
        {
            return 1;
        }
    }
}
