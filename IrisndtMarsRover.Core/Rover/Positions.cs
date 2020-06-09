using System;
using System.Collections.Generic;

namespace IrisndtMarsRover.Core
{
    public enum RoverDirection
    {
        N = 1,
        S = 2,
        E = 3,
        W = 4
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
        void ProcessMovements(List<int> maxPoints, string commands);
    }

    public class Position : IPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public List<RoverPoints> FlowPath { get; set; }
        public RoverDirection Direction { get; set; }
        

        public Position()
        {

            X = Y = 0;
            Direction = RoverDirection.N;
            FlowPath = new List<RoverPoints>();
        }

        private void LeftRotation()
        {
            switch (this.Direction)
            {
                case RoverDirection.N:
                    this.Direction = RoverDirection.W;
                    break;
                case RoverDirection.S:
                    this.Direction = RoverDirection.E;
                    break;
                case RoverDirection.E:
                    this.Direction = RoverDirection.N;
                    break;
                case RoverDirection.W:
                    this.Direction = RoverDirection.S;
                    break;
                default:
                    break;
            }
        }

        private void RightRotation()
        {
            switch (this.Direction)
            {
                case RoverDirection.N:
                    this.Direction = RoverDirection.E;
                    break;
                case RoverDirection.S:
                    this.Direction = RoverDirection.W;
                    break;
                case RoverDirection.E:
                    this.Direction = RoverDirection.S;
                    break;
                case RoverDirection.W:
                    this.Direction = RoverDirection.N;
                    break;
                default:
                    break;
            }
        }

        private void MoveForward()
        {
            switch (this.Direction)
            {
                case RoverDirection.N:
                    this.Y += 1;
                    FlowPath.Add(new RoverPoints(X, Y));
                    break;
                case RoverDirection.S:
                    this.Y -= 1;
                    FlowPath.Add(new RoverPoints(X, Y));
                    break;
                case RoverDirection.E:
                    this.X += 1;
                    FlowPath.Add(new RoverPoints(X, Y));
                    break; 
                case RoverDirection.W:
                    this.X -= 1;
                    FlowPath.Add(new RoverPoints(X, Y));
                    break;
                default:
                    break;
            }
        }

        public void ProcessMovements(List<int> maxPoints, string moves)
        {
            foreach (var move in moves)
            {
                switch (move)
                {
                    case 'M':
                        this.MoveForward();
                        break;
                    case 'L':
                        this.LeftRotation();
                        break;
                    case 'R':
                        this.RightRotation();
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
