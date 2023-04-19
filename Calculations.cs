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
                    //PointStorage.Add(new Point3D(
                    //    (double)(50 * Math.Sin(v) * Math.Cos(v) * Math.Sin(u)),
                    //    (double)(50 * Math.Sin(v) * Math.Cos(u)),
                    //    (double)(50 * Math.Cos(u)),
                    //    1),
                    //    i, j);
                    
                    // sphere:
                    //PointStorage.Add(new Point3D(
                      //  (double)(R * Math.Cos(u) * Math.Cos(v)),
                        //(double)(R * Math.Sin(u) * Math.Cos(v)),
                       // (double)(r * Math.Sin(v)),
                       // 1),
                       // i, j);

                    PointStorage.Add(new Point3D(
                        150 * Math.Sin(u) * Math.Cos(v),
                        150 * Math.Sin(u) * Math.Sin(v),
                        150 * Math.Cos(u),
                        1),
                        i, j);
                    /*PointStorage.Add(new Point3D(
                        20 * Math.Cos(u) *(Math.Cos(v) + 10),
                        20 * Math.Sin(u) * (Math.Cos(v) + 10),
                        20 * Math.Sin(v) + u,
                        1),
                        i, j);
                    */
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
                    triangles.Add(new Triangle((i + 1, j), (i, j + 1), (i + 1, j + 1)));
                    //triangles.Add(new Triangle((i + 1, j + 1), (i, j + 1), (i + 1, j)));
                }
            }
            return triangles;
        }

        private static Point3D getPointFromTriangle(Triangle triangle, int pointId)
        {
            switch (pointId)
            {
                case 0:
                    return PointStorage.Get(triangle.point1Index.i, triangle.point1Index.j);
                case 1:
                    return PointStorage.Get(triangle.point2Index.i, triangle.point2Index.j);
                case 2:
                    return PointStorage.Get(triangle.point3Index.i, triangle.point3Index.j);
            }
            return PointStorage.Get(triangle.point1Index.i, triangle.point1Index.j);
        }


        public static void NewellMethod(List<Triangle> triangles, Point3D[][] rotatedPoints)
        {
            /*
             * 
             * Nx = (point1.y - point2.y)*(point1.z + point2.z)
             * 
             * 
             * 
             * Nx = (point2.y - point3.y)*(point2.z + point3.z)
             * 
             * 
             * 
             * Nx = (point3.y - point1.y)*(point3.z + point1.z)
             * 
             */

            double Nx;
            double Ny;
            double Nz;
            double cos;

            int t = 0;
            foreach (Triangle triangle in triangles)
            {
                //Nx = (PointStorage.Get(triangle.point1Index.i, triangle.point1Index.j).y - PointStorage.Get(triangle.point2Index.i, triangle.point2Index.j).y) * (PointStorage.Get(triangle.point1Index.i, triangle.point1Index.j).z + PointStorage.Get(triangle.point2Index.i, triangle.point2Index.j).z);
                //Ny = (PointStorage.Get(triangle.point1Index.i, triangle.point1Index.j).z - PointStorage.Get(triangle.point2Index.i, triangle.point2Index.j).z) * (PointStorage.Get(triangle.point1Index.i, triangle.point1Index.j).x + PointStorage.Get(triangle.point2Index.i, triangle.point2Index.j).x);
                //Nz = (PointStorage.Get(triangle.point1Index.i, triangle.point1Index.j).x - PointStorage.Get(triangle.point2Index.i, triangle.point2Index.j).x) * (PointStorage.Get(triangle.point1Index.i, triangle.point1Index.j).y + PointStorage.Get(triangle.point2Index.i, triangle.point2Index.j).y);

                //Nx += (PointStorage.Get(triangle.point2Index.i, triangle.point2Index.j).y - PointStorage.Get(triangle.point3Index.i, triangle.point3Index.j).y) * (PointStorage.Get(triangle.point2Index.i, triangle.point2Index.j).z + PointStorage.Get(triangle.point3Index.i, triangle.point3Index.j).z);
                //Ny += (PointStorage.Get(triangle.point2Index.i, triangle.point2Index.j).z - PointStorage.Get(triangle.point3Index.i, triangle.point3Index.j).z) * (PointStorage.Get(triangle.point2Index.i, triangle.point2Index.j).x + PointStorage.Get(triangle.point3Index.i, triangle.point3Index.j).x);
                //Nz += (PointStorage.Get(triangle.point2Index.i, triangle.point2Index.j).x - PointStorage.Get(triangle.point3Index.i, triangle.point3Index.j).x) * (PointStorage.Get(triangle.point2Index.i, triangle.point2Index.j).y + PointStorage.Get(triangle.point3Index.i, triangle.point3Index.j).y);

                //Nx += (PointStorage.Get(triangle.point3Index.i, triangle.point3Index.j).y - PointStorage.Get(triangle.point1Index.i, triangle.point1Index.j).y) * (PointStorage.Get(triangle.point3Index.i, triangle.point3Index.j).z + PointStorage.Get(triangle.point1Index.i, triangle.point1Index.j).z);
                //Ny += (PointStorage.Get(triangle.point3Index.i, triangle.point3Index.j).z - PointStorage.Get(triangle.point1Index.i, triangle.point1Index.j).z) * (PointStorage.Get(triangle.point3Index.i, triangle.point3Index.j).x + PointStorage.Get(triangle.point1Index.i, triangle.point1Index.j).x);
                //Nz += (PointStorage.Get(triangle.point3Index.i, triangle.point3Index.j).x - PointStorage.Get(triangle.point1Index.i, triangle.point1Index.j).x) * (PointStorage.Get(triangle.point3Index.i, triangle.point3Index.j).y + PointStorage.Get(triangle.point1Index.i, triangle.point1Index.j).y);
                

                Nx = (rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].y - rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].y) * (rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].z + rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].z);
                Ny = (rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].z - rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].z) * (rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].x + rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].x);
                Nz = (rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].x - rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].x) * (rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].y + rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].y);

                Nx += (rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].y - rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].y) * (rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].z + rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].z);
                Ny += (rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].z - rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].z) * (rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].x + rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].x);
                Nz += (rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].x - rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].x) * (rotatedPoints[triangle.point2Index.i][triangle.point2Index.j].y + rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].y);

                Nx += (rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].y - rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].y) * (rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].z + rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].z);
                Ny += (rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].z - rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].z) * (rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].x + rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].x);
                Nz += (rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].x - rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].x) * (rotatedPoints[triangle.point3Index.i][triangle.point3Index.j].y + rotatedPoints[triangle.point1Index.i][triangle.point1Index.j].y);
                

                if (Math.Sqrt(Nx * Nx + Ny * Ny + Nz * Nz) != 0)
                    cos = Nz / (Math.Sqrt(Nx * Nx + Ny * Ny + Nz * Nz));
                else
                  cos = 0;
                triangle.FillColor(cos);
                
            }
        }

        public static Point2D[][] Proection(MatrixCalculation rotatedMatrix, Point3D[][] points, double psi, double hi, double fi, int centerX, int centerY)
        {
            // Создание матриц поворота

            Point2D[][] screenPoints = rotatedMatrix.CreateScreenpoints(points, centerX, centerY);
            return screenPoints;
        }

        public static List<Triangle> TrianglesSort(List<Triangle> triangles)
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
    }
}