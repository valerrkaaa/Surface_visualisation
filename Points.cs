﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Gluschenko
{
    class Point3D
    {
        double x;
        double y;
        double z;
        double w;

        public Point3D(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Point3D(List<double> rawArray)
        {
            this.x = rawArray[0];
            this.y = rawArray[1];
            this.z = rawArray[2];
            this.w = rawArray[3];
        }

        public List<double> ToListOfDouble()
        {
            return new List<double>() { x, y, z, w };
        }
    }
}