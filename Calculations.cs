using System;
using System.Collections.Generic;

namespace Lab1Gluschenko
{
    class Calculations
    {
        public static List<Triangle> GeneratePointsAndPolygons(double uN, double vN, double uMax, double vMax, int R, int r)
        {
            double du = uMax / uN;
            double dv = vMax / vN;
            double u = 0;
            double v = 0;


            // генерация точек в соответствии с параметрическим уравнением
            for (int i = 0; i < uN + 1; i++)
            {
                for (int j = 0; j < vN + 1; j++)
                {
                    //PointStorage.Add(new Point3D(
                    //    (double)(50 * Math.Sin(v) * Math.Cos(v) * Math.Sin(u)),
                    //    (double)(50 * Math.Sin(v) * Math.Cos(u)),
                    //    (double)(50 * Math.Cos(u)),
                    //    1),
                    //    i, j);
                    
                    // sphere:
                    PointStorage.Add(new Point3D(
                        (double)(R * Math.Cos(u) * Math.Cos(v)),
                        (double)(R * Math.Sin(u) * Math.Cos(v)),
                        (double)(r * Math.Sin(v)),
                        1),
                        i, j);

                    //PointStorage.Add(new Point3D(
                    //    150 * Math.Sin(u) * Math.Cos(v),
                    //    150 * Math.Sin(u) * Math.Sin(v),
                    //    150 * Math.Cos(u),
                    //    1),
                    //    i, j);
                    //PointStorage.Add(new Point3D(
                    //    20 * Math.Cos(u) *(Math.Cos(v) + 10),
                    //    20 * Math.Sin(u) * (Math.Cos(v) + 10),
                    //    20 * Math.Sin(v) + u,
                    //    1),
                    //    i, j);
                    //PointStorage.Add(new Point3D(
                    //   2 *u * Math.Cos(u) * (Math.Cos(v) + 2),
                    //   2 * u* Math.Sin(u) * (Math.Cos(v) + 2),
                    //   2 * u * Math.Sin(v) - Math.Pow(((u+3)/8 * Math.PI), 2) - 50,
                    //   1),
                    //   i, j);
                    //PointStorage.Add(new Point3D(
                    //   20 * (1 + v / 2 * Math.Cos(u / 2)) * Math.Cos(u),
                    //   20 * (1 + v / 2 * Math.Cos(u / 2)) * Math.Sin(u),
                    //   20 * v / 2 * Math.Sin(u / 2),
                    //   1),
                    //   i, j);

                    v += dv;
                }
                v = 0;
                u += du;
            }

            // Создание полигонов
            List<Triangle> triangles = new List<Triangle>();
            for (int i = 0; i < uN; i++)
            {
                for (int j = 0; j < vN; j++)
                {
                    triangles.Add(new Triangle((i, j), (i, j + 1), (i + 1, j)));
                    triangles.Add(new Triangle((i + 1, j + 1), (i, j + 1), (i + 1, j)));
                }
            }
            return triangles;
        }

        public static Point2D[][] Proection(Point3D[][] points, double psi, double hi, double fi)
        {
            // Создание матриц поворота
            MatrixCalculation rotatedMatrix = new MatrixCalculation();
            rotatedMatrix.RotateX(psi);
            rotatedMatrix.RotateY(hi);
            rotatedMatrix.RotateZ(fi);

            Point3D[][] rotatedPoints = rotatedMatrix.CreateRotatedPointsArray(points);
            Point2D[][] screenPoints = rotatedMatrix.CreateScreenpoints(rotatedPoints);
            return screenPoints;
        }
    }
}