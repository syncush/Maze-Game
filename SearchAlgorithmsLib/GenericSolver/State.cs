using System;
using System.Collections.Generic;
using System.Xml;

namespace SearchAlgorithmsLib {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="State{T}" />
    public class State<T> : IComparable<State<T>> {
        /// <summary>
        /// The pool of states.
        /// </summary>
        private static Dictionary<int, State<T>> pool = new Dictionary<int, State<T>>();

        /// <summary>
        /// Gets or sets the came from - father of the current stateItem.
        /// </summary>
        /// <value>
        /// The father of the current stateItem.
        /// </value>
        public State<T> CameFrom { get; set; }

        /// <summary>
        /// Gets or sets the Cost of the stateItem.
        /// </summary>
        /// <value>
        /// The Cost.
        /// </value>
        public double Cost { get; set; }

        /// <summary>
        /// Gets or sets the acutally stateItem (generic).
        /// </summary>
        /// <value>
        /// The stateItem.
        /// </value>
        public T stateItem { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="State{T}"/> class.
        /// this Ctor is private because we dont want to create a new instance
        /// of stateItem that we've already created (it is in the pool).
        /// </summary>
        /// <param name="state">The stateItem.</param>
        private State(T state) {
            this.stateItem = state;
            this.Cost = 0.0;
            this.CameFrom = null;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() {

            return stateItem.GetHashCode();
        }

        /// <summary>
        /// Equalses the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public bool Equals(State<T> obj) {
            return this.stateItem.Equals(obj.stateItem);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="other" /> in the sort order.  Zero This instance occurs in the same position in the sort order as <paramref name="other" />. Greater than zero This instance follows <paramref name="other" /> in the sort order.
        /// </returns>
        public int CompareTo(State<T> other) {
            return (int) (other.Cost - Cost);
        }

        /// <summary>
        /// class of the stateItem pool.
        /// </summary>
        /// <seealso cref="State{T}" />
        public static class StatePool {
            /// <summary>
            /// Gets the stateItem.
            /// </summary>
            /// <param name="state">The stateItem.</param>
            /// <returns> new stateItem if it  is not in pool, or the stateItem if it is in the pool </returns>
            public static State<T> GetState(T state) {
                // yhe hash code of the stateItem.
                int code = state.ToString().GetHashCode();
                // if the stateItem is already in the pool - return it from pull.
                if (pool.ContainsKey(code)) {
                    return pool[code];
                }
                // if it is not in the pool create new instance of the stateItem.
                else {
                    State<T> s = new State<T>(state);
                    pool.Add(state.ToString().GetHashCode(), s);
                    return s;
                }
            }
            /// <summary>
            /// Clears the pool.
            /// </summary>
            /// <returns></returns>
            public static void Clear() {
                pool.Clear();
            }
        }
    }
}