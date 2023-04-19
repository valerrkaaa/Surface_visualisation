using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Lab1Gluschenko
{
    public partial class Form1 : Form
    {
        private List<Triangle> triangles;

        private readonly List<TrackBarWithTextBox> _tb_pairs = new List<TrackBarWithTextBox>();  // хранит объекты - пары: текстбокс + трекбар
        private readonly List<(string fieldType, int id, int value)> historyLog = new List<(string fieldType, int id, int value)>();  //  хранит историю действий пользователя
        private readonly List<(string fieldType, int id, int value)> futureHistoryLog = new List<(string fieldType, int id, int value)>();  // хранит события, которые пользователь откатил

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // прослушивание событий
            _tb_pairs.Clear();
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarX, textBoxX, RefreshPictureBox, AddEventToHistoryLog, 0));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarY, textBoxY, RefreshPictureBox, AddEventToHistoryLog, 1));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarZ, textBoxZ, RefreshPictureBox, AddEventToHistoryLog, 2));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarU, textBoxU, RefreshPictureBox, AddEventToHistoryLog, 3));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarV, textBoxV, RefreshPictureBox, AddEventToHistoryLog, 4));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarLimitU, textBoxLimitU, RefreshPictureBox, AddEventToHistoryLog, 5));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarLimitV, textBoxLimitV, RefreshPictureBox, AddEventToHistoryLog, 6));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarR, textBoxR, RefreshPictureBox, AddEventToHistoryLog, 7));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBar3, textBox3, RefreshPictureBox, AddEventToHistoryLog, 8));

            // панель цветов
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarColorR1, textBoxColorR1, RefreshPictureBox, AddEventToHistoryLog, 9));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarColorG1, textBoxColorG1, RefreshPictureBox, AddEventToHistoryLog, 10));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarColorB1, textBoxColorB1, RefreshPictureBox, AddEventToHistoryLog, 11));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarColorR2, textBoxColorR2, RefreshPictureBox, AddEventToHistoryLog, 12));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarColorG2, textBoxColorG2, RefreshPictureBox, AddEventToHistoryLog, 13));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarColorB2, textBoxColorB2, RefreshPictureBox, AddEventToHistoryLog, 14));
            coloredPanel1.BackColor = FigureColors.inColor;
            coloredPanel2.BackColor = FigureColors.outColor;
            _tb_pairs[9].SetValue(FigureColors.inColor.R);
            _tb_pairs[10].SetValue(FigureColors.inColor.G);
            _tb_pairs[11].SetValue(FigureColors.inColor.B);
            _tb_pairs[12].SetValue(FigureColors.outColor.R);
            _tb_pairs[13].SetValue(FigureColors.outColor.G);
            _tb_pairs[14].SetValue(FigureColors.outColor.B);

            panelColorOut.Location = panelColorUn.Location;
            radioButtonUnColor.Checked = true;
            panelColorOut.Visible = false;

            RefreshPictureBox();
        }

        private void RefreshPictureBox()
        {
            /*
             * Отрисовывает картинку
             */

            // Перерисовывает предпросмотр цветов
            FigureColors.inColor = Color.FromArgb(_tb_pairs[9].GetValue(), _tb_pairs[10].GetValue(), _tb_pairs[11].GetValue());
            FigureColors.outColor = Color.FromArgb(_tb_pairs[12].GetValue(), _tb_pairs[13].GetValue(), _tb_pairs[14].GetValue());
            coloredPanel1.BackColor = FigureColors.inColor;
            coloredPanel2.BackColor = FigureColors.outColor;

            // Получение всех необходимых параметров из формы
            double psi = _tb_pairs[0].GetValue();
            double hi = _tb_pairs[1].GetValue();
            double fi = _tb_pairs[2].GetValue();
            double uN = _tb_pairs[3].GetValue();
            double vN = _tb_pairs[4].GetValue();
            double uMax = _tb_pairs[5].GetValue() / 180.0 * Math.PI;
            double vMax = _tb_pairs[6].GetValue() / 180.0 * Math.PI;
            int R = _tb_pairs[7].GetValue();
            int r = _tb_pairs[8].GetValue();
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;

            // Тип закраски
            bool needFillColor = radioButtonFlat.Checked;

            // получение массива спроецированных точек
            Point2D[][] screenPoints = Generate2dFigure(uN, vN, uMax, vMax, psi, fi, hi, R, r, centerX, centerY);

            Draw(pictureBox1, screenPoints, needFillColor);
        }

        private void Draw(PictureBox pictureBox, Point2D[][] screenPoints, bool needFillColor)
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                if (needFillColor)
                {
                    // Закраска цветом

                    Point[] poligons = new Point[3];
                    List<Triangle> sorted_triangles = Calculations.TrianglesSort(triangles);
                    foreach (Triangle triangle in sorted_triangles)
                    {
                        poligons = new Point[3];

                        poligons[0] = new Point((int)screenPoints[triangle.point1Index.i][triangle.point1Index.j].x, (int)screenPoints[triangle.point1Index.i][triangle.point1Index.j].y);
                        poligons[1] = new Point((int)screenPoints[triangle.point2Index.i][triangle.point2Index.j].x, (int)screenPoints[triangle.point2Index.i][triangle.point2Index.j].y);
                        poligons[2] = new Point((int)screenPoints[triangle.point3Index.i][triangle.point3Index.j].x, (int)screenPoints[triangle.point3Index.i][triangle.point3Index.j].y);

                        g.FillPolygon(new SolidBrush(triangle.color), poligons);
                    }
                }
                else
                {
                    // Каркасный вид
                    foreach (Triangle triangle in triangles)
                    {
                        g.DrawLine(new Pen(Color.Black, 1),
                            (float)(screenPoints[triangle.point1Index.i][triangle.point1Index.j].x),
                            (float)(screenPoints[triangle.point1Index.i][triangle.point1Index.j].y),

                            (float)(screenPoints[triangle.point2Index.i][triangle.point2Index.j].x),
                            (float)(screenPoints[triangle.point2Index.i][triangle.point2Index.j].y)
                            );
                        g.DrawLine(new Pen(Color.Black, 1),
                            (float)(screenPoints[triangle.point1Index.i][triangle.point1Index.j].x),
                            (float)(screenPoints[triangle.point1Index.i][triangle.point1Index.j].y),

                            (float)(screenPoints[triangle.point3Index.i][triangle.point3Index.j].x),
                            (float)(screenPoints[triangle.point3Index.i][triangle.point3Index.j].y)
                            );
                        g.DrawLine(new Pen(Color.Black, 1),
                            (float)(screenPoints[triangle.point2Index.i][triangle.point2Index.j].x),
                            (float)(screenPoints[triangle.point2Index.i][triangle.point2Index.j].y),

                            (float)(screenPoints[triangle.point3Index.i][triangle.point3Index.j].x),
                            (float)(screenPoints[triangle.point3Index.i][triangle.point3Index.j].y)
                            );
                    }
                }
            }
            pictureBox.Image = bitmap;
        }

        private Point2D[][] Generate2dFigure(double uN, double vN, double uMax, double vMax, double psi, double fi, double hi, int R, int r, int centerX, int centerY)
        {
            /*
             * Создание трёхмерной фигуры с последующей её проекцией на двухмерный холст для отрисовки
             */
            new PointStorage(uN, vN);
            triangles = Calculations.GeneratePointsAndPolygons(uN, vN, uMax, vMax, R, r);


            // Создание матриц поворота
            MatrixCalculation rotatedMatrix = new MatrixCalculation();
            rotatedMatrix.RotateX(psi);
            rotatedMatrix.RotateY(hi);
            rotatedMatrix.RotateZ(fi);
            Point3D[][] rotatedPoints = rotatedMatrix.CreateRotatedPointsArray(PointStorage.Get2DArray());

            Calculations.NewellMethod(triangles, rotatedPoints);
            return Calculations.Proection(rotatedMatrix, rotatedPoints, psi, fi, hi, centerX, centerY);
        }

        private void AddEventToHistoryLog(string fieldType, int id, int value)
        {
            /*
             * Регистрирует (запоминает) событие для возможности откатить
             */

            historyLog.Add((fieldType, id, value));
            futureHistoryLog.Clear();
        }

        private void Undo()
        {
            /*
             * Откатывает действие
             */


            if (historyLog.Count > 0)
            {
                string fieldType = historyLog[historyLog.Count - 1].fieldType;
                int id = historyLog[historyLog.Count - 1].id;
                int currentValue = _tb_pairs[id].GetValue();
                int value = historyLog[historyLog.Count - 1].value;

                switch (fieldType)
                {
                    case "tbtb":
                        _tb_pairs[id].SetValue(value);
                        break;
                }

                futureHistoryLog.Add((historyLog[historyLog.Count - 1].fieldType, id, currentValue));
                historyLog.RemoveAt(historyLog.Count - 1);
                RefreshPictureBox();
            }
        }

        private void Redo()
        {
            /*
             * Возвращает отменённое действие назад
             */

            if (futureHistoryLog.Count > 0)
            {
                string fieldType = futureHistoryLog[futureHistoryLog.Count - 1].fieldType;
                int id = futureHistoryLog[futureHistoryLog.Count - 1].id;
                int currentValue = _tb_pairs[id].GetValue();
                int value = futureHistoryLog[futureHistoryLog.Count - 1].value;

                switch (fieldType)
                {
                    case "tbtb":
                        _tb_pairs[id].SetValue(value);
                        break;
                }

                historyLog.Add((futureHistoryLog[futureHistoryLog.Count - 1].fieldType, id, currentValue));
                futureHistoryLog.RemoveAt(futureHistoryLog.Count - 1);
                RefreshPictureBox();
            }
        }

        private void buttonUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void buttonRedo_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void radioButtonColor_CheckedChanged(object sender, EventArgs e)
        {
            /*
             * Переключение панелей с выбором цвета
             */

            if (radioButtonUnColor.Checked)
            {
                panelColorUn.Visible = true;
                panelColorOut.Visible = false;
            }
            else
            {
                panelColorOut.Visible = true;
                panelColorUn.Visible = false;
            }
        }

        private void radioButtonVisualizeType_CheckedChanged(object sender, EventArgs e)
        {
            RefreshPictureBox();
        }
    }
}