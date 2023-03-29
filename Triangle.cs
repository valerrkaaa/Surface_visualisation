using System.Drawing;


namespace Lab1Gluschenko
{
    class Triangle
    {
        public (int i, int j) point1Index;
        public (int i, int j) point2Index;
        public (int i, int j) point3Index;
        public Color color;

        public Triangle((int i, int j) point1Index, (int i, int j) point2Index, (int i, int j) point3Index)
        {
            this.point1Index = point1Index;
            this.point2Index = point2Index;
            this.point3Index = point3Index;
        }

        public void FillColor(double cos, Color color)
        {
            /*
             * Изменяет значение параметра цвета в соответствии с косинусом угла
             */

            this.color = Color.FromArgb(
                (int)cos * color.R,
                (int)cos * color.G,
                (int)cos * color.B
                );
        }
    }
}
