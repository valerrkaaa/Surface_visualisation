using System;
using System.Collections.Generic;

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

        private Point3D Point3DMultiply(Point3D point, List<List<double>> matrix)
        {
            // Умножает трехмерную точку на матрицу 4х4
            return new Point3D(MatrixMultiply(new List<List<double>>() { point.ToListOfDouble() }, matrix)[0]);
        }

        private Point2D Point2DMultiply(Point3D point, List<List<double>> matrix)
        {
            // Умножает двухмерную точку на матрицу 4х4
            return new Point2D(MatrixMultiply(new List<List<double>>() { point.ToListOfDouble() }, matrix)[0]);
        }

        private List<List<double>> MatrixMultiply(List<List<double>> A, List<List<double>> B)
        {
            /*
             * Алгоритм перемножения двух матриц
             */

            List<List<double>> outputArray = new List<List<double>>();
            for (int row = 0; row < A.Count; row++)
            {
                outputArray.Add(new List<double>());
                for (int col = 0; col < B[0].Count; col++)
                {
                    outputArray[row].Add(0);
                    for (int i = 0; i < B.Count; i++)
                    {
                        outputArray[row][col] += A[row][i] * B[i][col];
                    }
                }
            }
            return outputArray;
        }

        public void RotateX(double psi)
        {
            // матрица поворотов по оси X
            psi = psi * Math.PI / 180;
            List<List<double>> temp = new List<List<double>>() {
                new List<double>{1, 0, 0, 0},
                new List<double>{0, Math.Cos(psi), -Math.Sin(psi), 0},
                new List<double>{0, Math.Sin(psi), Math.Cos(psi), 0},
                new List<double>{0, 0, 0, 1},
            };
            this._generalMatrix = MatrixMultiply(this._generalMatrix, temp);
        }
        public void RotateY(double hi)
        {
            // матрица поворотов по оси Y
            hi = hi * Math.PI / 180;
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
            // матрица поворотов по оси Z
            fi = fi * Math.PI / 180;
            List<List<double>> temp = new List<List<double>>() {
                new List<double>{Math.Cos(fi), Math.Sin(fi), 0, 0},
                new List<double>{-Math.Sin(fi), Math.Cos(fi), 0, 0},
                new List<double>{0, 0, 1, 0},
                new List<double>{0, 0, 0, 1},
            };
            this._generalMatrix = MatrixMultiply(this._generalMatrix, temp);
        }

        public Point3D[][] CreateRotatedPointsArray(Point3D[][] oldPoints)
        {
            // Поворачивает точки с помощью generalMatrix в трёхмерном пространстве

            Point3D[][] rotatedPoints = new Point3D[oldPoints.Length][];
            for (int i = 0; i < oldPoints.Length; i++)
            {
                rotatedPoints[i] = new Point3D[oldPoints[0].Length];
                for (int j = 0; j < oldPoints[0].Length; j++)
                {
                    rotatedPoints[i][j] = Point3DMultiply(oldPoints[i][j], _generalMatrix);
                }
            }
            return rotatedPoints;
        }

        public Point2D[][] CreateScreenpoints(Point3D[][] rotadedPoints, int centerX, int centerY)
        {
            /* 
             * Проецирует трёхмерные точки в двухмерные
             */

            List<List<double>> ones = new List<List<double>>() {
                new List<double>{1, 0, 0, 0},
                new List<double>{0, 1, 0, 0},
                new List<double>{0, 0, 0, 0},
                new List<double>{0, 0, 0, 1},
            };

            Point2D[][] screenPoints = new Point2D[rotadedPoints.Length][];
            for (int i = 0; i < rotadedPoints.Length; i++)
            {
                screenPoints[i] = new Point2D[rotadedPoints[0].Length];
                for (int j = 0; j < rotadedPoints[0].Length; j++)
                {
                    screenPoints[i][j] = Point2DMultiply(rotadedPoints[i][j], ones);
                }
            }
            for (int i = 0; i < rotadedPoints.Length; i++)
            {
                for (int j = 0; j < rotadedPoints[0].Length; j++)
                {
                    screenPoints[i][j].x = screenPoints[i][j].x + centerX;
                    screenPoints[i][j].y = screenPoints[i][j].y + centerY;
                }
            }
            return screenPoints;
        }
    }
}