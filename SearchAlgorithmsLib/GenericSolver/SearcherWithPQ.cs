using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academy.Collections.Generic;


namespace SearchAlgorithmsLib {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.Searcher{T}" />
    public abstract class SearcherWithPQ<T> : Searcher<T> {
        /// <summary>
        /// The open list
        /// </summary>
        private PriorityQueue<State<T>> openList;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearcherWithPQ{T}"/> class.
        /// </summary>
        public SearcherWithPQ() {
            openList = new PriorityQueue<State<T>>();
        }

        /// <summary>
        /// Adds to open list.
        /// </summary>
        /// <param name="elem">The elem.</param>
        public void AddToOpenList(State<T> elem) {
            openList.Enqueue(elem);
        }

        /// <summary>
        /// Opens the list contains.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public bool OpenListContains(State<T> s) {
            return openList.Contains(s);
        }

        /// <summary>
        /// Pops the open list.
        /// </summary>
        /// <returns></returns>
        protected State<T> PopOpenList() {
            evaluatedNodes++;
            return openList.Dequeue();
        }

        /// <summary>
        /// Gets the size of the open list.
        /// </summary>
        /// <value>
        /// The size of the open list.
        /// </value>
        public int OpenListSize {
            get { return openList.Count; }
        }

        /// <summary>
        /// Removes one element from open list.
        /// </summary>
        /// <param name="toDelete">The element that we want to delete.</param>
        /// <returns></returns>
        public bool RemoveFromOpenList(State<T> toDelete) {
            bool success = false;
            // list that will save all the elements that we pop from stack. 
            LinkedList<State<T>> l = new LinkedList<State<T>>();
            // size of open list
            int n = openList.Count;
            State<T> temp;
            // run on all the PQ and find the element we want to delete.
            for (int i = 0; i < n; i++) {
                temp = openList.Dequeue();
                if (temp.Equals(toDelete)) {
                    success = true;
                    break;
                }
                l.AddLast(temp);
            }
            // put back all the elements from the list to the PQ
            foreach (State<T> s in l) {
                openList.Enqueue(s);
            }
            return success;
        }
    }
}