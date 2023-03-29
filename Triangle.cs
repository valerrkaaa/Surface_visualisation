

namespace Lab1Gluschenko
{
    class Triangle
    {
        public (int i, int j) point1Index;
        public (int i, int j) point2Index;
        public (int i, int j) point3Index;

        public Triangle((int i, int j) point1Index, (int i, int j) point2Index, (int i, int j) point3Index)
        {
            this.point1Index = point1Index;
            this.point2Index = point2Index;
            this.point3Index = point3Index;
        }
    }
}
