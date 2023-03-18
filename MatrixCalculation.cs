using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Gluschenko
{
    class MatrixCalculation
    {
        private List<List<double>> _generalMatrix = new List<List<double>>() {
            new List<double>{1, 0, 0, 0},
            new List<double>{0, 1, 0, 0},
            new List<double>{0, 0, 1, 0},
            new List<double>{0, 0, 0, 1},
        };

        private Point3D pointMultiply(Point3D point, List<List<double>> matrix)
        {
            return new Point3D(MatrixMultiply(new List<List<double>>() { point.ToListOfDouble() }, matrix)[0]);
        }

        private List<List<double>> MatrixMultiply(List<List<double>> A, List<List<double>> B)
        {
            List<List<double>> outputArray = new List<List<double>>();
            for (int row = 0; row < A.Count; row++)
            {
                outputArray.Add(new List<double>());
                for (int col = 0; col < B[0].Count; col++)
                {
                    for (int i = 0; i < A.Count; i++)
                    {
                        outputArray[row][col] += A[row][i] * B[i][col];
                    }
                }
            }
            return outputArray;
        }

        public void RotateX(double psi)
        {
            List<List<double>> temp = new List<List<double>>() {
                new List<double>{1, 0, 0, 0},
                new List<double>{0, Math.Cos(psi), 0, 0},
                new List<double>{0, 0, 1, 0},
                new List<double>{0, 0, 0, 1},
            };
            this._generalMatrix = MatrixMultiply(this._generalMatrix, temp);
        }
        public void RotateY(double hi)
        {
            List<List<double>> temp = new List<List<double>>() {
                new List<double>{Math.Cos(hi), 0, -Math.Sin(hi), 0},
                new List<double>{0, 1, 0, 0},
                new List<double>{Math.Sin(hi), 0, Math.Cos(hi), 0},
                new List<double>{0, 0, 0, 1},
            };
            this._generalMatrix = MatrixMultiply(this._generalMatrix, temp);
        }
        public void RotateZ(double fi)
        {
            List<List<double>> temp = new List<List<double>>() {
                new List<double>{Math.Cos(fi), Math.Sin(fi), 0, 0},
                new List<double>{-Math.Sin(fi), Math.Cos(fi), 0, 0},
                new List<double>{0, 0, 1, 0},
                new List<double>{0, 0, 0, 1},
            };
            this._generalMatrix = MatrixMultiply(this._generalMatrix, temp);
        }

        public Point3D[][] CreateRotatedPointsArray(List<List<Point3D>> oldPoints, int uN, int vN)
        {
            Point3D[][] rotatedPoints = new Point3D[uN][];
            for (int i = 0; i < uN; i++)
            {
                rotatedPoints[i] = new Point3D[vN];
                for (int j = 0; j < vN; j++)
                {
                    rotatedPoints[i][j] = pointMultiply(oldPoints[i][j], _generalMatrix);
                }
            }
            return rotatedPoints;
        }

        public void createScreenpoints(Point3D[][] rotadedPoints)
        {
            List<List<double>> ones = new List<List<double>>() {
                new List<double>{1, 0, 0, 0},
                new List<double>{0, 1, 0, 0},
                new List<double>{0, 0, 0, 0},
                new List<double>{0, 0, 0, 1},
            };

            Point3D[][] screenPoints = new Point3D[rotadedPoints.GetLength(0)][];
            for (int i = 0; i < rotadedPoints.GetLength(0); i++)
            {
                screenPoints[i] = new Point3D[rotadedPoints.GetLength(1)];
                for (int j = 0; j < rotadedPoints.GetLength(1); j++)
                {
                    screenPoints[i][j] = pointMultiply(rotadedPoints[i][j], ones);
                }
            }
        }
    }
}
