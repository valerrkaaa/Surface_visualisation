using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Gluschenko
{
    class Triangle
    {
        int point1Index;
        int point2Index;
        int point3Index;

        public Triangle(int point1Index, int point2Index, int point3Index)
        {
            this.point1Index = point1Index;
            this.point2Index = point2Index;
            this.point3Index = point3Index;
        }

        public Triangle(Point3D point1, Point3D point2, Point3D point3)
        {
            this.point1Index = PointStorage.AddPoint3DListIndex(point1);
            this.point2Index = PointStorage.AddPoint3DListIndex(point2);
            this.point3Index = PointStorage.AddPoint3DListIndex(point3);
        }
    }
}
