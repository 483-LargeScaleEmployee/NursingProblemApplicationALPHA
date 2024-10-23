using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace NursingProblemApplicationALPHA
{
    public class Shape3D
    {
        public IList<Vector3> Points { get; set; }
        public IList<Face> Faces { get; set; }
        public Matrix4x4 Transform { get; set; } = Matrix4x4.Identity;
        public float[] HiddenFaceDashPattern { get; set; } = new float[] { 2, 10 };

        public Point Project(Vector3 point, Point center)
        {
            Vector3 point2 = Vector3.Transform(point, Transform);
            return new Point(
                Math.Round(center.X + point2.X / (1 - point2.Z / 800)),
                Math.Round(center.Y + point2.Y / (1 - point2.Z / 800))
                );
        }

        public void Draw(ICanvas canvas, RectF dirtyRect, RenderMode Mode = RenderMode.Solid)
        {
            // If no points to draw.
            if (Points == null)
            {
                return;
            }

            // If no faces to draw.
            if (Faces == null)
            {
                return;
            }

            List<Point> points = new List<Point>(Points.Select(point => Project(point, dirtyRect.Center)));
            foreach (var f in Faces)
            {
                Point point0 = points[f.Vertices[0]];
                Point point1 = points[f.Vertices[1]];
                Point point2 = points[f.Vertices[2]];
                Point vertex1 = new Point(point1.X - point0.X, point1.Y - point0.Y);
                Point vertex2 = new Point(point2.X - point1.X, point2.Y - point1.Y);
                double vz = vertex1.X * vertex2.Y - vertex1.Y * vertex2.X;
                bool isFaceHidden = (vz <= 0);

                if (Mode == RenderMode.Solid)
                {
                        // Is the solid face transparent?                
                    if (f.Color == Colors.Transparent)
                    {
                        continue;
                    }

                        // Is the face hidden?
                    if (isFaceHidden)
                    {
                        continue;
                    }
                }

                PathF path = new PathF();
                path.MoveTo((float)points[f.Vertices[0]].X, (float)points[f.Vertices[0]].Y);
                for (int i = 1; i < f.Vertices.Count; i++)
                {
                    path.LineTo((float)points[f.Vertices[i]].X, (float)points[f.Vertices[i]].Y);
                }
                path.Close();

                    // For solid rendering
                if (Mode == RenderMode.Solid)
                {
                    canvas.FillColor = f.Color;
                    canvas.FillPath(path);
                }

                    // For wireframe rendering
                if (Mode == RenderMode.Wireframe)
                {
                    canvas.StrokeColor = f.Color;
                    canvas.StrokeSize = 1;
                    canvas.StrokeDashPattern = isFaceHidden ? HiddenFaceDashPattern : null;
                    canvas.DrawPath(path);
                }
            }
        }
    }
}

public enum RenderMode
{
    Solid,
    Wireframe
};
