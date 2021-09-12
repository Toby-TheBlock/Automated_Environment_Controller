using System;
using System.Collections.Generic;
using System.Drawing;

namespace Data_Logging_and_Management_Application
{
    class LoadingIcon
    {
        private double angleStep;
        private List<Circle> circles;

        public LoadingIcon(int amountOfCircles, double angleStep, Color circleColor, int circleRadius)
        {
            this.angleStep = angleStep;
            double currentAngle = 0.0;

            circles = new List<Circle>()
            {
                new Circle(160, 150, circleColor, circleRadius, currentAngle)
            };

            for (int i = 0; i < amountOfCircles; i++)
            {
                currentAngle += angleStep;
                (int xCoord, int yCoord) = GetXYCoords(currentAngle);
                circles.Insert(0, new Circle(xCoord, yCoord, circleColor, circleRadius, currentAngle));
            }
        }

        private (int, int) GetXYCoords(double currentAngle)
        {
            int xCoord = circles[0].XCoord + Convert.ToInt32(15 * Math.Cos(currentAngle));
            int yCoord = circles[0].YCoord + Convert.ToInt32(15 * Math.Sin(currentAngle));

            return (xCoord, yCoord);
        }

        private void DrawLoadingIcon(Graphics g)
        {
            foreach (Circle c in circles)
            {
                g.FillEllipse(new SolidBrush(c.Color), c.XCoord, c.YCoord, c.Radius, c.Radius);
            }
        }

        public void UpdateLoadingIcon(Graphics g)
        {
            double currentAngle = circles[0].Angle + angleStep;
            (int xCoord, int yCoord) = GetXYCoords(currentAngle);
            circles.Insert(0, new Circle(xCoord, yCoord, circles[0].Color, circles[0].Radius, currentAngle));
            circles.RemoveAt(circles.Count - 1);

            DrawLoadingIcon(g);
        }

    }
}
