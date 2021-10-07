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


        /// <summary>
        /// Draws a circle based on each object currently present in the circles collection.
        /// </summary>
        /// <param name="g">The graphics object who's to been drawn on.</param>
        private void DrawLoadingIcon(Graphics g)
        {
            foreach (Circle c in circles)
            {
                g.FillEllipse(new SolidBrush(c.Color), c.XCoord, c.YCoord, c.Radius, c.Radius);
            }
        }


        /// <summary>
        /// Calculates the x- and y-coordinates for the position of a circle based on its angle.
        /// </summary>
        /// <param name="currentAngle">The angle of the circle i approximation to the first circle.</param>
        /// <returns>The x- and y-coordinates.</returns>
        private (int, int) GetXYCoords(double currentAngle)
        {
            int xCoord = circles[0].XCoord + Convert.ToInt32(15 * Math.Cos(currentAngle));
            int yCoord = circles[0].YCoord + Convert.ToInt32(15 * Math.Sin(currentAngle));

            return (xCoord, yCoord);
        }


        /// <summary>
        /// Updates the loading-icon by adding a new circle with a new position at the beginning of the icon, and removing the last circle. 
        /// </summary>
        /// <param name="g">The graphics object who's to been drawn on.</param>
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
