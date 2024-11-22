using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Microsoft.Maui.Graphics.Text;

namespace NursingProblemApplicationALPHA;

public enum RenderMode
{
    Solid,
    Wireframe
};

public class Shape3D : GraphicsView, IDrawable
{
    public static readonly BindableProperty PointsProperty
        = BindableProperty.Create(nameof(Points), typeof(IList<Vector3>), typeof(Shape3D), propertyChanged: RequestInvalidate);

    public static readonly BindableProperty FacesProperty
        = BindableProperty.Create(nameof(Faces), typeof(IList<Face>), typeof(Shape3D), propertyChanged: RequestInvalidate);

    public static readonly BindableProperty TransformProperty
        = BindableProperty.Create(nameof(Transform), typeof(Matrix4x4), typeof(Shape3D), propertyChanged: RequestInvalidate);

    public static readonly BindableProperty HiddenFaceDashPatternProperty
        = BindableProperty.Create(nameof(HiddenFaceDashPattern), typeof(float[]), typeof(Shape3D), propertyChanged: RequestInvalidate);

    public static readonly BindableProperty ModeProperty
        = BindableProperty.Create(nameof(Mode), typeof(RenderMode), typeof(Shape3D), propertyChanged: RequestInvalidate);

    public static readonly BindableProperty UserAxisTagsProperty
        = BindableProperty.Create(nameof(UserAxisTags), typeof(IList<String>), typeof(Shape3D), propertyChanged: RequestInvalidate);

    public IList<Vector3> Points 
    {
        get => (List<Vector3>)GetValue(PointsProperty);
        set => SetValue(PointsProperty, value);
    }
    public IList<Face> Faces
    {
        get => (List<Face>)GetValue(FacesProperty);
        set => SetValue(FacesProperty, value);
    }
    public Matrix4x4 Transform
    {
        get => (Matrix4x4)GetValue(TransformProperty);
        set => SetValue(TransformProperty, value);
    }
    public float[] HiddenFaceDashPattern
    {
        get => (float[])GetValue(HiddenFaceDashPatternProperty); 
        set => SetValue(HiddenFaceDashPatternProperty, value);
    }
    public RenderMode Mode
    {
        get => (RenderMode)GetValue(ModeProperty); 
        set => SetValue(ModeProperty, value);
    }

    public IList<String> UserAxisTags
    {
        get => (IList<String>)GetValue(UserAxisTagsProperty);
        set => SetValue(UserAxisTagsProperty, value);
    }

    private static void RequestInvalidate(BindableObject obj, object oldVal, object newVal)
    {
        if (obj is GraphicsView)
        {
            ((GraphicsView)obj).Invalidate();
        }
    }

    public Point Project(Vector3 point, Point center)
    {
        Vector3 point2 = Vector3.Transform(point, Transform);
        return new Point(
            Math.Round(center.X + point2.X / (1 - point2.Z / 800)),
            Math.Round(center.Y + point2.Y / (1 - point2.Z / 800))
            );
    }

    public Shape3D()
    {
        Drawable = this;
        BackgroundColor = Colors.Black;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
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
        int count = 0;
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

                canvas.StrokeColor = Colors.White;
                canvas.StrokeSize = 1;
                canvas.DrawPath(path);

                /*
                    // Put user name in bottom left
                if (count == 0)
                {
                    canvas.FontColor = Colors.Red;
                    canvas.FontSize = 8;
                    canvas.DrawString("aBcD", (float)points[f.Vertices[0]].X, (float)points[f.Vertices[0]].Y, HorizontalAlignment.Right);
                }
                    // Reset count for next "user"
                if (count == ((14 * 3) - 1))
                {
                    count = 0;
                }

                count += 1;
                */
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