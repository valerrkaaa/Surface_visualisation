using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Gluschenko
{
    static class PointStorage
    {
        static List<Point3D> _point3DList = new List<Point3D>();

        public static void Add(Point3D point)
        {
            _point3DList.Add(point);
        }

        public static int AddPoint3DListIndex(Point3D point)
        {
            _point3DList.Add(point);
            return _point3DList.Count - 1;
        }

        public static Point3D Get(int index)
        {
            return _point3DList[index];
        }
    }
}
