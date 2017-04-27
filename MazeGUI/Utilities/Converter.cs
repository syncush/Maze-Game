﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGUI.DataSources;
using MazeLib;

namespace MazeGUI.Utilities {
    class Converter {
        public static  int AlgoToInt(Algorithm algo) {
            switch (algo) {
                case Algorithm.BFS: {
                    return 1;
                }
                    break;
                case Algorithm.DFS: {
                    return 0;
                }
                    break;
            }
            return -1;
        }

        public  static Position FromDirectionToNewPosition(Position pos, Direction direct) {
            switch (direct) {
                case Direction.Up: {
                    return new Position(pos.Row - 1, pos.Col);

                }
                    break;
                case Direction.Down: {
                    return new Position(pos.Row + 1, pos.Col);
                }
                    break;
                case Direction.Right: {
                    return new Position(pos.Row, pos.Col + 1);

                }
                    break;
                case Direction.Left: {
                    return new Position(pos.Row, pos.Col - 1);

                }
                    break;
                case Direction.Unknown: {
                    return pos;
                }
                    break;
            }
            return pos;
        }
    

        public static Direction StringToDirection(string str) {
            Direction parseDirection;
            switch (str) {
                case "Up":
                case "up": {
                    parseDirection = Direction.Up;
                }
                    break;
                case "Down":
                case "down": {
                    parseDirection = Direction.Down;
                }
                    break;
                case "Left":
                case "left": {
                    parseDirection = Direction.Left;
                }
                    break;
                case "Right":
                case "right": {
                    parseDirection = Direction.Right;
                }
                    break;
                default: {
                    parseDirection = Direction.Unknown;
                }
                    break;
            }
            return parseDirection;
        }

        public static Position CharToPosition(Position prev,char num) {
            Direction direct;
            switch (num)
            {
                
                case '0':
                    {
                        direct = Direction.Left;
                    }
                    break;
                
                case '1':
                    {
                        direct = Direction.Right;
                    }
                    break;
                
                case '2':
                    {
                        direct = Direction.Up;
                    }
                    break;
                
                case '3':
                    {
                        direct = Direction.Down;
                    }
                    break;
                default:
                    {
                        direct = Direction.Unknown;
                    }
                    break;
            }
            return Converter.FromDirectionToNewPosition(prev, direct);

        }
    }
}