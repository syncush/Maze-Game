using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLib {
    /// <summary>
    /// Represents a tupple
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <typeparam name="U"></typeparam>
    class Tuple<T, V, U> {
        private T firstValue;
        private V secondValue;
        private U thirdValue;

        /// <summary>
        /// Gets or sets the first item.
        /// </summary>
        /// <value>
        /// The first item.
        /// </value>
        public T Item1 {
            get { return firstValue; }
            set { firstValue = value; }
        }

        /// <summary>
        /// Gets or sets the second item.
        /// </summary>
        /// <value>
        /// The second item.
        /// </value>
        public V Item2 {
            get { return this.secondValue; }
            set { this.secondValue = value; }

        }

        /// <summary>
        /// Gets or sets the third item.
        /// </summary>
        /// <value>
        /// The third item.
        /// </value>
        public U Item3 {
            get { return this.thirdValue; }
            set { this.thirdValue = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tuple{T, V, U}"/> class.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="value3">The value3.</param>
        public Tuple(T value1, V value2, U value3) {
            this.firstValue = value1;
            this.secondValue = value2;
            this.thirdValue = value3;
        }
    }
}