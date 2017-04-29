using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace MazeGUI.GameLogic {
    /// <summary>
    /// Interface describes game logic.
    /// </summary>
    interface IGameLogic {
        /// <summary>
        /// Determines whether [is legit move] [the specified player position].
        /// </summary>
        /// <param name="playerPosition">The player position.</param>
        /// <param name="moveTo">The move to.</param>
        /// <returns>
        ///   <c>true</c> if [is legit move] [the specified player position]; otherwise, <c>false</c>.
        /// </returns>
        Boolean IsLegitMove(Position playerPosition, Direction moveTo);
        /// <summary>
        /// Moves the specified player position.
        /// </summary>
        /// <param name="playerPosition">The player position.</param>
        /// <param name="moveToDirection">The move to direction.</param>
        /// <returns>The new position after the movement.</returns>
        Position Move(Position playerPosition, Direction moveToDirection);
    }
}