using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Edge<T> {
        /// <summary>
        /// The v1
        /// </summary>
        private State<T> v1;

        /// <summary>
        /// The v2
        /// </summary>
        private State<T> v2;

        /// <summary>
        /// Initializes a new instance of the <see cref="Edge{T}"/> class.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        public Edge(State<T> v1, State<T> v2) {
            this.v1 = v1;
            this.v2 = v2;
        }

        /// <summary>
        /// Gets the v1.
        /// </summary>
        /// <returns></returns>
        public State<T> GetV1() {
            return this.v1;
        }

        /// <summary>
        /// Gets the v2.
        /// </summary>
        /// <returns></returns>
        public State<T> GetV2() {
            return this.v2;
        }

        /// <summary>
        /// Equalses the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public bool Equals(Edge<T> obj) {
            return this.v1.Equals(obj.GetV1()) && this.v2.Equals(obj.GetV2());
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() {
            return (v1.GetHashCode().ToString() + v2.GetHashCode().ToString()).GetHashCode();
        }
    }
}