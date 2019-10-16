﻿using System;
using System.Collections.Generic;
using System.Text;
using CADPadDB;
using CADPadDB.Maths;
using CADPadServices.ESelection;
using CADPadServices.PickupBox;

namespace CADPadServices
{
    public static class MathUtils
    {
        /// <summary>
        /// 点是否在矩形内
        /// </summary>
        public static bool IsPointInRectangle(CADPoint point, Rectangle2 rect)
        {
            CADPoint rectLeftBottom = rect.leftBottom;
            CADPoint rectRightTop = rect.rightTop;

            if (point.X >= rectLeftBottom.X
                && point.X <= rectRightTop.X
                && point.Y >= rectLeftBottom.Y
                && point.Y <= rectRightTop.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        ///// <summary>
        ///// Cross window
        ///// https://yal.cc/rectangle-circle-intersection-test/
        ///// </summary>
        //public static bool BoundingCross(Bounding bounding, Circle circle)
        //{
        //    Vector2 nearestPntOnBound = new Vector2(
        //        System.Math.Max(bounding.left, System.Math.Min(circle.center.x, bounding.right)),
        //        System.Math.Max(bounding.bottom, System.Math.Min(circle.center.y, bounding.top)));

        //    if (Vector2.Distance(nearestPntOnBound, circle.center) <= circle.radius)
        //    {
        //        double bdLeft = bounding.left;
        //        double bdRight = bounding.right;
        //        double bdTop = bounding.top;
        //        double bdBottom = bounding.bottom;

        //        return Vector2.Distance(new Vector2(bdLeft, bdTop), circle.center) >= circle.radius
        //            || Vector2.Distance(new Vector2(bdLeft, bdBottom), circle.center) >= circle.radius
        //            || Vector2.Distance(new Vector2(bdRight, bdTop), circle.center) >= circle.radius
        //            || Vector2.Distance(new Vector2(bdRight, bdBottom), circle.center) >= circle.radius;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// 值是否在范围内
        /// </summary>
        internal static bool IsValueInRange(double value, double min, double max)
        {
            return value >= min && value <= max;
        }

        /// <summary>
        /// 规整化弧度
        /// 返回值范围:[0, 2*PI)
        /// </summary>
        public static double NormalizeRadianAngle(double rad)
        {
            double value = rad % (2 * Utils.PI);
            if (value < 0)
                value += 2 * Utils.PI;
            return value;
        }

        /// <summary>
        /// 镜像矩阵
        /// </summary>
        internal static Matrix3 MirrorMatrix(Line2 mirrorLine)
        {
            CADVector lineDir = mirrorLine.direction;
            Matrix3 matPos1 = Matrix3.Translate(-mirrorLine.startPoint);
            double rotAngle = CADVector.SignedAngle(lineDir, new CADVector(1, 0));
            Matrix3 matRot1 = Matrix3.Rotate(rotAngle);

            Matrix3 mirrorMatX = new Matrix3(
                1, 0, 0,
                0, -1, 0,
                0, 0, 1);

            Matrix3 matRot2 = Matrix3.Rotate(-rotAngle);
            Matrix3 matPos2 = Matrix3.Translate(mirrorLine.startPoint);

            return matPos2 * matRot2 * mirrorMatX * matRot1 * matPos1;
        }
    }
}
