using System;
using System.Collections.Generic;
using System.Drawing;

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
                    // sphere:
                    PointStorage.Add(new Point3D(
                        R * Math.Sin(u) * Math.Cos(v),
                        R * Math.Sin(u) * Math.Sin(v),
                        r * Math.Cos(u),
                        1),
                        i, j);
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
                    triangles.Add(new Triangle((i + 1, j), (i, j + 1), (i + 1, j + 1)));
                    //triangles.Add(new Triangle((i + 1, j + 1), (i, j + 1), (i + 1, j)));
                }
            }
            return triangles;
        }


        private static (double x, double y, double z) NewellMethod(Triangle triangle, Point3D[][] rotatedPoints)
        {
            double Nx = (rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].y - rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].y) * (rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].z + rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].z);
            double Ny = (rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].z - rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].z) * (rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].x + rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].x);
            double Nz = (rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].x - rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].x) * (rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].y + rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].y);

            Nx += (rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].y - rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].y) * (rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].z + rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].z);
            Ny += (rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].z - rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].z) * (rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].x + rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].x);
            Nz += (rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].x - rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].x) * (rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].y + rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].y);

            Nx += (rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].y - rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].y) * (rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].z + rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].z);
            Ny += (rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].z - rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].z) * (rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].x + rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].x);
            Nz += (rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].x - rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].x) * (rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].y + rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].y);

            return (Nx, Ny, Nz);
        }

        private static double CosBetweenVectorNAndZ((double x, double y, double z) vectorN)
        {
            if (Math.Sqrt(Math.Pow(vectorN.x, 2) + Math.Pow(vectorN.y, 2) + Math.Pow(vectorN.z, 2)) != 0)
                return vectorN.z / (Math.Sqrt(Math.Pow(vectorN.x, 2) + Math.Pow(vectorN.y, 2) + Math.Pow(vectorN.z, 2)));
            else
                return 0;
        }


        public static void SetColorForTriangles(List<Triangle> triangles, Point3D[][] rotatedPoints)
        {
            (double x, double y, double z) vectorN;
            double cos;

            foreach (Triangle triangle in triangles)
            {
                vectorN = NewellMethod(triangle, rotatedPoints);
                cos = CosBetweenVectorNAndZ(vectorN);
                if (cos >= 0)
                    triangle.FillOutColor(cos);
                else
                    triangle.FillInColor(-cos);
            }
        }

        public static Point2D[][] Proection(MatrixCalculation rotatedMatrix, Point3D[][] points, int centerX, int centerY)
        {
            Point2D[][] rotatedPoints = rotatedMatrix.CreateScreenpoints(points, centerX, centerY);
            return rotatedPoints;
        }

        public static List<Triangle> TrianglesSort(List<Triangle> triangles, bool needSort)
        {
            if (needSort)
            {
                List<Triangle> insideTriangle = new List<Triangle>();
                List<Triangle> outsideTriangle = new List<Triangle>();

                foreach (Triangle triangle in triangles)
                {
                    if (triangle.isOutColor)
                        outsideTriangle.Add(triangle);
                    else
                        insideTriangle.Add(triangle);
                }
                insideTriangle.AddRange(outsideTriangle);

                return insideTriangle;
            }
            return triangles;
        }
    }
}