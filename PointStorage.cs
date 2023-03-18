using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Gluschenko
{
    class PointStorage
    {
        private static Point3D[][] _point3DList;

        public PointStorage(int N1, int N2)
        {
            _point3DList = new Point3D[N1][];
            for (int i = 0; i < N1; i++)
                _point3DList[i] = new Point3D[N2];
        }

        public static void Add(Point3D point, int i, int j)
        {
            _point3DList[i][j] = point;
        }

        public static Point3D Get(int i, int j)
        {
            return _point3DList[i][j];
        }
    }
}
