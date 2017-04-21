using System.Collections.Generic;

namespace SearchAlgorithmsLib {
    public interface ISearcheble<T> {
        /// <summary>
        /// Gets the initial stateItem.
        /// </summary>
        /// <returns></returns>
        State<T> GetInitialState();

        /// <summary>
        /// Gets the final stateItem.
        /// </summary>
        /// <returns></returns>
        State<T> GetFinalState();

        /// <summary>
        /// Gets the list states.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns></returns>
        List<State<T>> GetListStates(State<T> n);
    }
}