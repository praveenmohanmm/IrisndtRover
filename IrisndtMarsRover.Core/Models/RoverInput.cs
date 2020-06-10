using System;
namespace IrisndtMarsRover.Core.Models
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
        public RoverPoints(float x, float y)
        {
            XPos = x;
            YPos = y;
        }
        public float XPos { get; set; }
        public float YPos { get; set; }
    }

    public class RoverInput
    {
        public RoverInput()
        {
        }
        public string commands { get; set; }
        public double startXPos { get; set; }
        public double startYPos { get; set; }
        public int max { get; set; }
        public int startDirection { get; set; }

    }
}
