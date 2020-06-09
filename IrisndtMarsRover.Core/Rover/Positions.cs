using System;
using System.Collections.Generic;

namespace IrisndtMarsRover.Core
{
    public enum Directions
    {
        N = 1,//North
        S = 2,//South
        E = 3,//East
        W = 4//West
    };

    public class RoverPoints
    {
        public RoverPoints(int x, int y)
        {
            XPos = x;
            YPos = y;
        }
        public float XPos { get; set; }
        public float YPos { get; set; }
    }

    public interface IPosition
    {
        void StartMoving(List<int> maxPoints, string moves);
    }

    public class Position : IPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Directions Direction { get; set; }
        public List<RoverPoints> FlowPath { get; set; }

        public Position()
        {
            X = Y = 0;
            Direction = Directions.N;
            FlowPath = new List<RoverPoints>();
        }

        private void Rotate90Left()
        {
            switch (this.Direction)
            {
                case Directions.N:
                    this.Direction = Directions.W;
                    break;
                case Directions.S:
                    this.Direction = Directions.E;
                    break;
                case Directions.E:
                    this.Direction = Directions.N;
                    break;
                case Directions.W:
                    this.Direction = Directions.S;
                    break;
                default:
                    break;
            }
        }

        private void Rotate90Right()
        {
            switch (this.Direction)
            {
                case Directions.N:
                    this.Direction = Directions.E;
                    break;
                case Directions.S:
                    this.Direction = Directions.W;
                    break;
                case Directions.E:
                    this.Direction = Directions.S;
                    break;
                case Directions.W:
                    this.Direction = Directions.N;
                    break;
                default:
                    break;
            }
        }

        private void MoveInSameDirection()
        {
            switch (this.Direction)
            {
                case Directions.N:
                    this.Y += 1;
                    FlowPath.Add(new RoverPoints(X, Y));
                    break;
                case Directions.S:
                    this.Y -= 1;
                    FlowPath.Add(new RoverPoints(X, Y));
                    break;
                case Directions.E:
                    this.X += 1;
                    FlowPath.Add(new RoverPoints(X, Y));
                    break; 
                case Directions.W:
                    this.X -= 1;
                    FlowPath.Add(new RoverPoints(X, Y));
                    break;
                default:
                    break;
            }
        }

        public void StartMoving(List<int> maxPoints, string moves)
        {
            foreach (var move in moves)
            {
                switch (move)
                {
                    case 'M':
                        this.MoveInSameDirection();
                        break;
                    case 'L':
                        this.Rotate90Left();
                        break;
                    case 'R':
                        this.Rotate90Right();
                        break;
                    default:
                        Console.WriteLine($"Invalid Character {move}");
                        break;
                }

                if (this.X < 0 || this.X > maxPoints[0] || this.Y < 0 || this.Y > maxPoints[1])
                {
                    throw new Exception($"out of coordinates");
                }
            }
        }
    }
}
