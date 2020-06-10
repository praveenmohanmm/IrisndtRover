using System;
namespace IrisndtMarsRover.Core.Models
{
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
