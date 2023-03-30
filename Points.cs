using System.Collections.Generic;


namespace Lab1Gluschenko
{
    class Point3D
    {
        public double x;
        public double y;
        public double z;
        public double w;

        public Point3D(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Point3D(List<double> rawArray)
        {
            this.x = rawArray[0];
            this.y = rawArray[1];
            this.z = rawArray[2];
            this.w = rawArray[3];
        }

        public List<double> ToListOfDouble()
        {
            return new List<double>() { x, y, z, w };
        }
    }

    class Point2D
    {
        public double x;
        public double y;

        public Point2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Point2D(List<double> rawArray)
        {
            this.x = rawArray[0];
            this.y = rawArray[1];
        }
    }
}
