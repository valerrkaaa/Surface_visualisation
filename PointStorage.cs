

namespace Lab1Gluschenko
{
    class PointStorage
    {
        private static Point3D[][] _point3DList;

        public PointStorage(double N1, double N2)
        {
            _point3DList = new Point3D[(int)N1 + 1][];
            for (int i = 0; i < N1 + 1; i++)
                _point3DList[i] = new Point3D[(int)N2 + 1];
        }

        public static void Add(Point3D point, int i, int j)
        {
            _point3DList[i][j] = point;
        }

        public static Point3D Get(int i, int j)
        {
            return _point3DList[i][j];
        }

        public static Point3D[][] Get2DArray()
        {
            return _point3DList;
        }
    }
}
