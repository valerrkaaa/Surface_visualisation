using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1Gluschenko
{
    public partial class Form1 : Form
    {
        private readonly List<TrackBarWithTextBox> _tb_pairs = new List<TrackBarWithTextBox>();  // хранит объекты - пары: текстбокс + трекбар
        private List<(string fieldType, int id, int value)> historyLog = new List<(string fieldType, int id, int value)>();  //  хранит историю действий пользователя
        private List<(string fieldType, int id, int value)> futureHistoryLog = new List<(string fieldType, int id, int value)>();  // хранит события, которые пользователь откатил

        private List<Triangle> triangles;

        private Color unColor = Color.Red;
        private Color outColor = Color.Blue;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //List<String> elementNames = new List<String>() { "X", "Y", "Z", "U", "V", "LimitU", "LimitV", "R", "3", };

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
            
            // пенель цветов
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarColorR1, textBoxColorR1, RefreshPictureBox, AddEventToHistoryLog, 9));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarColorG1, textBoxColorG1, RefreshPictureBox, AddEventToHistoryLog, 10));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarColorB1, textBoxColorB1, RefreshPictureBox, AddEventToHistoryLog, 11));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarColorR2, textBoxColorR2, RefreshPictureBox, AddEventToHistoryLog, 12));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarColorG2, textBoxColorG2, RefreshPictureBox, AddEventToHistoryLog, 13));
            _tb_pairs.Add(new TrackBarWithTextBox(trackBarColorB2, textBoxColorB2, RefreshPictureBox, AddEventToHistoryLog, 14));

            panelColorOut.Location = panelColorUn.Location;
            radioButtonUnColor.Checked = true;
            panelColorOut.Visible = false;

            coloredPanel1.BackColor = unColor;
            coloredPanel2.BackColor = outColor;

            _tb_pairs[9].SetValue(unColor.R);
            _tb_pairs[10].SetValue(unColor.G);
            _tb_pairs[11].SetValue(unColor.B);
            _tb_pairs[12].SetValue(outColor.R);
            _tb_pairs[13].SetValue(outColor.G);
            _tb_pairs[14].SetValue(outColor.B);
        }

        private void RefreshPictureBox()
        {
            /*
             * Отрисовывает картинку
             */
            Console.WriteLine("refresh");

            // Перерисовывает предпросмотр цветов
            unColor = Color.FromArgb(_tb_pairs[9].GetValue(), _tb_pairs[10].GetValue(), _tb_pairs[11].GetValue());
            outColor = Color.FromArgb(_tb_pairs[12].GetValue(), _tb_pairs[13].GetValue(), _tb_pairs[14].GetValue());

            coloredPanel1.BackColor = unColor;
            coloredPanel2.BackColor = outColor;

            int psi = _tb_pairs[0].GetValue();
            int hi = _tb_pairs[1].GetValue();
            int fi = _tb_pairs[2].GetValue();
            int uN = _tb_pairs[3].GetValue();
            int vN = _tb_pairs[4].GetValue();
            int uMin = _tb_pairs[5].GetMinValue();
            int uMax = uN;
            int vMin = _tb_pairs[6].GetMinValue();
            int vMax = vN;

            //new MatrixCalculation().Test();

            new PointStorage(uN, vN);
            triangles = Calculations.GeneratePointsAndTriangles(uN, vN, uMax, vMax, uMin, vMin);
            var a = PointStorage.Get2DArray();
            Point2D[][] screenPoints = Calculations.Proection(PointStorage.Get2DArray(), psi, fi, hi, uN, vN);

            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;

            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                Console.WriteLine((float)(screenPoints[triangles[0].point1Index.i][triangles[0].point1Index.j].x));
                Console.WriteLine((float)(screenPoints[triangles[0].point1Index.i][triangles[0].point1Index.j].y));
                Console.WriteLine((float)(screenPoints[triangles[0].point2Index.i][triangles[0].point2Index.j].x));
                Console.WriteLine((float)(screenPoints[triangles[0].point2Index.i][triangles[0].point2Index.j].y));
                foreach (Triangle triangle in triangles)
                {
                    g.DrawLine(new Pen(Color.Black, 2), 
                        (float)(screenPoints[triangle.point1Index.i][triangle.point1Index.j].x + centerX), 
                        (float)(screenPoints[triangle.point1Index.i][triangle.point1Index.j].y + centerY),
                        
                        (float)(screenPoints[triangle.point2Index.i][triangle.point2Index.j].x + centerX), 
                        (float)(screenPoints[triangle.point2Index.i][triangle.point2Index.j].y + centerY)
                        );
                    g.DrawLine(new Pen(Color.Black, 2), 
                        (float)(screenPoints[triangle.point1Index.i][triangle.point1Index.j].x + centerX), 
                        (float)(screenPoints[triangle.point1Index.i][triangle.point1Index.j].y + centerY),
                        
                        (float)(screenPoints[triangle.point3Index.i][triangle.point3Index.j].x + centerX), 
                        (float)(screenPoints[triangle.point3Index.i][triangle.point3Index.j].y + centerY)
                        );
                    g.DrawLine(new Pen(Color.Black, 2), 
                        (float)(screenPoints[triangle.point2Index.i][triangle.point2Index.j].x + centerX), 
                        (float)(screenPoints[triangle.point2Index.i][triangle.point2Index.j].y + centerY),
                        
                        (float)(screenPoints[triangle.point3Index.i][triangle.point3Index.j].x + centerX), 
                        (float)(screenPoints[triangle.point3Index.i][triangle.point3Index.j].y + centerY)
                        );
                }
                //g.DrawLine(new Pen(Color.Black, 5), 0, 0, 100, 100);
            }
            pictureBox1.Image = bitmap;
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
    }
}
