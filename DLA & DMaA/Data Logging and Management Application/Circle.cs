using System.Drawing;

namespace Data_Logging_and_Management_Application
{
    class Circle
    {
        private int xCoord;
        private int yCoord;
        private Color color;
        private int radius;
        private double angle;

        public Circle(int xCoord, int yCoord, Color color, int radius, double angle)
        {
            this.xCoord = xCoord;
            this.yCoord = yCoord;
            this.color = color;
            this.radius = radius;
            this.angle = angle;
        }

        public int XCoord
        {
            get { return xCoord; }
        }

        public int YCoord
        {
            get { return yCoord; }
        }

        public Color Color
        {
            get { return color; }
        }

        public int Radius 
        { 
            get { return radius; }
        }

        public double Angle
        {
            get { return angle; }
        }
    }
}
