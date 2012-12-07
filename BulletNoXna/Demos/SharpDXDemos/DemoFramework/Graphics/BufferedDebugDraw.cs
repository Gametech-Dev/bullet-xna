﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using BulletXNA.LinearMath;

namespace DemoFramework
{
    public abstract class BufferedDebugDraw : DebugDraw
    {
        protected List<PositionColored> lines = new List<PositionColored>();

        public override DebugDrawModes DebugMode { get; set; }

        protected virtual int ColorToInt(ref Vector3 c)
        {
            return (int)c.X + ((int)c.Y << 8) + ((int)c.Z << 16);
        }

        public override void Draw3dText(ref Vector3 location, string textString)
        {
            throw new NotImplementedException();
        }

        public override void DrawLine(ref Vector3 from, ref Vector3 to, ref Vector3 fromColor, ref Vector3 toColor)
        {
            lines.Add(new PositionColored(ref from, ColorToInt(ref fromColor)));
            lines.Add(new PositionColored(ref to, ColorToInt(ref toColor)));
        }

        public override void DrawLine(Vector3 from, Vector3 to, Vector3 color)
        {
            int intColor = ColorToInt(ref color);
            lines.Add(new PositionColored(ref from, intColor));
            lines.Add(new PositionColored(ref to, intColor));
        }

        public override void DrawLine(ref Vector3 from, ref Vector3 to, ref Vector3 color)
        {
            int intColor = ColorToInt(ref color);
            lines.Add(new PositionColored(ref from, intColor));
            lines.Add(new PositionColored(ref to, intColor));
        }

        public override void DrawBox(ref Vector3 bbMin, ref Vector3 bbMax, ref Vector3 color)
        {
            var p1 = bbMin;
            var p2 = new Vector3(bbMax.X, bbMin.Y, bbMin.Z);
            var p3 = new Vector3(bbMax.X, bbMax.Y, bbMin.Z);
            var p4 = new Vector3(bbMin.X, bbMax.Y, bbMin.Z);
            var p5 = new Vector3(bbMin.X, bbMin.Y, bbMax.Z);
            var p6 = new Vector3(bbMax.X, bbMin.Y, bbMax.Z);
            var p7 = bbMax;
            var p8 = new Vector3(bbMin.X, bbMax.Y, bbMax.Z);

            int intColor = ColorToInt(ref color);
            lines.Add(new PositionColored(ref p1, intColor)); lines.Add(new PositionColored(ref p2, intColor));
            lines.Add(new PositionColored(ref p2, intColor)); lines.Add(new PositionColored(ref p3, intColor));
            lines.Add(new PositionColored(ref p3, intColor)); lines.Add(new PositionColored(ref p4, intColor));
            lines.Add(new PositionColored(ref p4, intColor)); lines.Add(new PositionColored(ref p1, intColor));

            lines.Add(new PositionColored(ref p1, intColor)); lines.Add(new PositionColored(ref p5, intColor));
            lines.Add(new PositionColored(ref p2, intColor)); lines.Add(new PositionColored(ref p6, intColor));
            lines.Add(new PositionColored(ref p3, intColor)); lines.Add(new PositionColored(ref p7, intColor));
            lines.Add(new PositionColored(ref p4, intColor)); lines.Add(new PositionColored(ref p8, intColor));

            lines.Add(new PositionColored(ref p5, intColor)); lines.Add(new PositionColored(ref p6, intColor));
            lines.Add(new PositionColored(ref p6, intColor)); lines.Add(new PositionColored(ref p7, intColor));
            lines.Add(new PositionColored(ref p7, intColor)); lines.Add(new PositionColored(ref p8, intColor));
            lines.Add(new PositionColored(ref p8, intColor)); lines.Add(new PositionColored(ref p5, intColor));
        }

        public override void DrawBox(ref Vector3 bbMin, ref Vector3 bbMax, ref Matrix trans, ref Vector3 color)
        {
            var p1 = trans * bbMin;
            var p2 = trans * new Vector3(bbMax.X, bbMin.Y, bbMin.Z);
            var p3 = trans * new Vector3(bbMax.X, bbMax.Y, bbMin.Z);
            var p4 = trans * new Vector3(bbMin.X, bbMax.Y, bbMin.Z);
            var p5 = trans * new Vector3(bbMin.X, bbMin.Y, bbMax.Z);
            var p6 = trans * new Vector3(bbMax.X, bbMin.Y, bbMax.Z);
            var p7 = trans * bbMax;
            var p8 = trans * new Vector3(bbMin.X, bbMax.Y, bbMax.Z);

            int intColor = ColorToInt(ref color);
            lines.Add(new PositionColored(ref p1, intColor)); lines.Add(new PositionColored(ref p2, intColor));
            lines.Add(new PositionColored(ref p2, intColor)); lines.Add(new PositionColored(ref p3, intColor));
            lines.Add(new PositionColored(ref p3, intColor)); lines.Add(new PositionColored(ref p4, intColor));
            lines.Add(new PositionColored(ref p4, intColor)); lines.Add(new PositionColored(ref p1, intColor));

            lines.Add(new PositionColored(ref p1, intColor)); lines.Add(new PositionColored(ref p5, intColor));
            lines.Add(new PositionColored(ref p2, intColor)); lines.Add(new PositionColored(ref p6, intColor));
            lines.Add(new PositionColored(ref p3, intColor)); lines.Add(new PositionColored(ref p7, intColor));
            lines.Add(new PositionColored(ref p4, intColor)); lines.Add(new PositionColored(ref p8, intColor));

            lines.Add(new PositionColored(ref p5, intColor)); lines.Add(new PositionColored(ref p6, intColor));
            lines.Add(new PositionColored(ref p6, intColor)); lines.Add(new PositionColored(ref p7, intColor));
            lines.Add(new PositionColored(ref p7, intColor)); lines.Add(new PositionColored(ref p8, intColor));
            lines.Add(new PositionColored(ref p8, intColor)); lines.Add(new PositionColored(ref p5, intColor));
        }

        public override void DrawTriangle(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, ref Vector3 color, float __unnamed004)
        {
            int intColor = ColorToInt(ref color);
            lines.Add(new PositionColored(ref v0, intColor));
            lines.Add(new PositionColored(ref v1, intColor));
            lines.Add(new PositionColored(ref v2, intColor));
            lines.Add(new PositionColored(ref v0, intColor));
        }

        public override void DrawTransform(ref Matrix transform, float orthoLen)
        {
            Vector3 start = transform.Translation;

            lines.Add(new PositionColored(ref start, 0xff0000));
            lines.Add(new PositionColored(start + new Vector3(orthoLen, 0, 0), 0xff0000));
            lines.Add(new PositionColored(ref start, 0x00ff00));
            lines.Add(new PositionColored(start + new Vector3(0, orthoLen, 0), 0x00ff00));
            lines.Add(new PositionColored(ref start, 0x0000ff));
            lines.Add(new PositionColored(start + new Vector3(0, 0, orthoLen), 0x0000ff));
        }

        public override void DrawArc(ref Vector3 center, ref Vector3 normal, ref Vector3 axis, float radiusA, float radiusB, float minAngle, float maxAngle, ref Vector3 color, bool drawSect)
        {
            DrawArc(ref center, ref normal, ref axis, radiusA, radiusB, minAngle, maxAngle, ref color, drawSect, 10.0f);
        }

        public override void DrawArc(ref Vector3 center, ref Vector3 normal, ref Vector3 axis, float radiusA, float radiusB, float minAngle, float maxAngle, ref Vector3 color, bool drawSect, float stepDegrees)
        {
            Vector3 vx = axis;
            Vector3 vy = Vector3.Cross(normal, axis);
            float step = stepDegrees * ((float)Math.PI / 180.0f);
            int nSteps = (int)((maxAngle - minAngle) / step);
            if (nSteps == 0)
                nSteps = 1;

            Vector3 next = center + radiusA * vx * (float)Math.Cos(minAngle) + radiusB * vy * (float)Math.Sin(minAngle);

            if (drawSect)
                DrawLine(ref center, ref next, ref color);

            int intColor = ColorToInt(ref color);
            PositionColored last = new PositionColored(ref next, intColor);
            for (int i = 1; i <= nSteps; i++)
            {
                lines.Add(last);
                float angle = minAngle + (maxAngle - minAngle) * i / nSteps;
                next = center + radiusA * vx * (float)Math.Cos(angle) + radiusB * vy * (float)Math.Sin(angle);
                last = new PositionColored(ref next, intColor);
                lines.Add(last);
            }

            if (drawSect)
                DrawLine(ref center, ref next, ref color);
        }

        public override void DrawContactPoint(Vector3 pointOnB, Vector3 normalOnB, float distance, int lifeTime, Vector3 color)
        {
            DrawContactPoint(pointOnB, normalOnB, distance, lifeTime, color);
        }

        public override void DrawContactPoint(ref Vector3 pointOnB, ref Vector3 normalOnB, float distance, int lifeTime, ref Vector3 color)
        {
            int intColor = ColorToInt(ref color);
            Vector3 to = pointOnB + normalOnB * 1; // distance
            lines.Add(new PositionColored(ref pointOnB, intColor));
            lines.Add(new PositionColored(ref to, intColor));
        }

        public override void ReportErrorWarning(string warningString)
        {
            throw new NotImplementedException();
        }
    }
};