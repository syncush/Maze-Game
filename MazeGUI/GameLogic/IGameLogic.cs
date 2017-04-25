using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace MazeGUI.GameLogic
{
    interface IGameLogic {
        Boolean IsLegitMove(Position playerPosition, Direction moveTo);
        Position Move(Position playerPosition, Direction moveToDirection);
    }
}
