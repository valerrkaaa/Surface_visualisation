using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Gluschenko
{
    class Calculations
    {
        public void CreatePoints(int uN, int vN, int uMax, int vMax, int uMin, int vMin)
        {
            double du = Math.Abs(uMax - uMin) / uN;
            double dv = Math.Abs(vMax - vMin) / vN;

            double u = uMin;
            double v = vMin;

            for (int i = 0; i < uN; i++)
            {
                for (int j = 0; j < vN; i++)
                {
                    PointStorage.Add(new Point3D(
                        (double)(5 * Math.Sin(u) * Math.Cos(v)),
                        (double)(5 * Math.Sin(u) * Math.Sin(v)),
                        (double)(5 * Math.Cos(u)),
                        1), 
                        i, j);
                    v += dv;
                }
                u += du;
            }

            List<Triangle> triangles = new List<Triangle>();

            for (int i = 0; i < u; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    triangles.Add(new Triangle((i, j), (i, j + 1), (i + 1, j)));
                    triangles.Add(new Triangle((i + 1, j + 1), (i, j + 1), (i + 1, j)));
                }
            }
        }
    }
}
