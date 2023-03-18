using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Gluschenko
{
    class Calculations
    {
        public static List<Triangle> GeneratePointsAndTriangles(int uN, int vN, int uMax, int vMax, int uMin, int vMin)
        {
            double du = (double)(Math.Abs(uMax - uMin)) / uN;
            double dv = (double)(Math.Abs(vMax - vMin)) / vN;

            double u = uMin;
            double v = vMin;

            for (int i = 0; i < uN; i++)
            {
                for (int j = 0; j < vN; j++)
                {
                    PointStorage.Add(new Point3D(
                        (double)(200 * Math.Sin(v) * Math.Cos(v) * Math.Sin(u)),
                        (double)(200 * Math.Sin(v) * Math.Sin(u)),
                        (double)(200 * Math.Cos(u)),
                        1), 
                        i, j);
                    v += dv;
                }
                u += du;
            }

            List<Triangle> triangles = new List<Triangle>();

            for (int i = 0; i < uN - 1; i++)
            {
                for (int j = 0; j < vN - 1; j++)
                {
                    triangles.Add(new Triangle((i, j), (i, j + 1), (i + 1, j)));
                    triangles.Add(new Triangle((i + 1, j + 1), (i, j + 1), (i + 1, j)));
                }
            }

            return triangles;
        }

        public static Point2D[][] Proection(Point3D[][] points, int psi, int hi, int fi, int uN, int vN)
        {
            // Создание матриц поворота
            MatrixCalculation rotatedMatrix = new MatrixCalculation();
            rotatedMatrix.RotateX(psi);
            rotatedMatrix.RotateY(hi);
            rotatedMatrix.RotateZ(fi);


            Point3D[][] rotatedPoints = rotatedMatrix.CreateRotatedPointsArray(points, uN, vN);
            Point2D[][] screenPoints = rotatedMatrix.CreateScreenpoints(rotatedPoints);
            return screenPoints;
        }
    }
}
