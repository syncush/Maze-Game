using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SearchAlgorithmsLib {
    /// <summary>
    /// the weight function of the edeges.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class WeightForEdges<T> {


        enum DefaultCost
        {
             infinite = 999999999
        }

        /// <summary>
        /// map between edege's hash code to weight.
        /// </summary>
        Dictionary<int, double> edgesWeight;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightForEdges{T}"/> class.
        /// </summary>
        public WeightForEdges() {
            edgesWeight = new Dictionary<int, double>();
        }

        /// <summary>
        /// Gets the weight.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns></returns>
        public virtual double GetWeight(State<T> v1, State<T> v2) {
            try {
                return edgesWeight[(new Edge<T>(v1, v2)).GetHashCode()];
            }
            catch {
                return (double)DefaultCost.infinite;
            }
        }

        /// <summary>
        /// Initialzies this instance.
        /// </summary>
        public virtual void Initialzie() {
            // implement if we want a graph with weights different from 1.
        }
    }
}