using System.Drawing;


namespace Lab1Gluschenko
{
    class Triangle
    {
        public (int i, int j) point1Index;
        public (int i, int j) point2Index;
        public (int i, int j) point3Index;
        public Color color;
        public bool isOutColor;

        public Triangle((int i, int j) point1Index, (int i, int j) point2Index, (int i, int j) point3Index)
        {
            this.point1Index = point1Index;
            this.point2Index = point2Index;
            this.point3Index = point3Index;
        }

        public void FillColor(double cos)
        {
            /*
             * Изменяет значение параметра цвета в соответствии с косинусом угла
             */

            if (cos <= 0)  // TODO может быть наоборот
            {
                isOutColor = false;
                this.color = Color.FromArgb(
                    (int)(-cos * FigureColors.inColor.R),
                    (int)(-cos * FigureColors.inColor.G),
                    (int)(-cos * FigureColors.inColor.B)
                    );
            }
            else
            {
                isOutColor = true;
                this.color = Color.FromArgb(
                    (int)(cos * FigureColors.outColor.R),
                    (int)(cos * FigureColors.outColor.G),
                    (int)(cos * FigureColors.outColor.B)
                    );
            }
        }
    }
}
