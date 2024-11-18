using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Numerics;
using System.Windows.Input;
using System.Drawing;

namespace NursingProblemApplicationALPHA;

public class MainViewModel : INotifyPropertyChanged
{
    public bool Wireframe { get; set; } = false;
    public RenderMode Mode => Wireframe ? RenderMode.Solid : RenderMode.Wireframe;

    /*
     *  Starting view: 
     *          Y |__ 
     *              X   
     * 
     *           Z is dept
     */


    private const int CUBE_X = 14; // Days
    private const int CUBE_Y = 3;  // Shifts
    private const int CUBE_Z = 10; // Users
    private const int POINTS_PER_USER = (CUBE_X + 1) * (CUBE_Y + 1);
    private const double POINT_DIST = 25;

    public event PropertyChangedEventHandler PropertyChanged;

    IList<Vector3> _cubePoints = new List<Vector3>() { };
    public IList<Vector3> CubePoints
    {
        get => _cubePoints;
        set
        {
            _cubePoints = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CubePoints)));
        }
    }

    private IList<Face> _cubeFaces = new List<Face>() { };
    public IList<Face> CubeFaces
    {
        get => _cubeFaces;
        set
        {
            _cubeFaces = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CubeFaces)));
        }
    }

    private Matrix4x4 _cubeTransform = Matrix4x4.Identity;
    public Matrix4x4 CubeTransform
    {
        get => _cubeTransform;
        set
        {
            _cubeTransform = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CubeTransform)));
        }
    }

    public String PointArraySize => getCubePointsSize().ToString();

    private IDispatcherTimer _timer;

    public Matrix4x4 Delta { get; set; } =
        Matrix4x4.Multiply(
            Matrix4x4.CreateRotationX((float)(0.0 * (Math.PI / 180))),
            Matrix4x4.CreateRotationY((float)(0.0 * (Math.PI / 180)))
        );

    public MainViewModel()
    {
        CubePoints = makeCubePoints();
        CubeFaces = makeCubeFaces(CubePoints);
        ToggleRenderModeCommand = new Command(() => ToggleRenderMode());
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToggleRenderModeCommand)));
        _timer = App.Current.Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(10);
        // Apply slight cube rotation periodically
        _timer.Tick += (s, e) =>
        {
            CubeTransform = Matrix4x4.Multiply(CubeTransform, Delta);
        };

        /*
         * A couple test color cases, remove!.
         * 
         */
        modifyFaceColor(CUBE_Z, 4, 2, Colors.LimeGreen);
        modifyFaceColor(CUBE_Z, 5, 2, Colors.LimeGreen);
        modifyFaceColor(CUBE_Z, 3, 2, Colors.LimeGreen);

        _timer.Start();
    }

    public ICommand ToggleRenderModeCommand { get; set; }

    public void ToggleRenderMode()
    {
        Wireframe = !Wireframe;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Wireframe)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Mode)));
    }

    public void setDelta(Matrix4x4 newDelta)
    {
        Delta = newDelta;
    }

    public Matrix4x4 getDelta()
    {
        return Delta;
    }

    public int getCubePointsSize()
    {
        return CubePoints.Count;
    }


    /* Uses Cube dimension constants to generate point array.
     * 
     * 
     */
    public IList<Vector3> makeCubePoints()
    {

        IList<Vector3> points = new List<Vector3>();

        /*  BOUNDS */

        // CUBE_X, CUBE_Y, CUBE_Z
        // Upper bounds are non-inclusive.
        int x_higher = ((CUBE_X + 1) / 2);             //((CUBE_X + 1)); 
        int x_lower = -1 * x_higher;                   // 0;

        int y_higher = ((CUBE_Y + 1) / 2);             //((CUBE_Y + 1));
        int y_lower = -1 * y_higher;                   // 0;

        int z_higher = (CUBE_Z + 1) / 2; 
        int z_lower = -1 * z_higher;

        for (int user = z_lower; user <= z_higher; user++)
        {
            for (int day = x_lower; day <= x_higher; day++)
            {
                for (int shift = y_lower; shift < y_higher; shift++)
                {
                    points.Add(new Vector3()
                    {
                        X = (float)(day * POINT_DIST),
                        Y = (float)(shift * POINT_DIST),
                        Z = (float)(user * POINT_DIST)
                    });
                }
            }
        }

        return points;

    }

    
    // Takes point set and generates faces iteratively
    public IList<Face> makeCubeFaces(IList<Vector3> points)
    {
        IList<Face> faces = new List<Face>();
        int user_offset;

        for (int user = 0; user < (CUBE_Z + 1); user++)
        {
            user_offset = (POINTS_PER_USER * user);

                // At this point here, we have a 14 x 3 grid of faces to create.
                // (14 + 1) * (3 + 1) = 60
            for (int i = 0; i < POINTS_PER_USER; i++)
            {
                if (((i % 4) > 0) && (i > (CUBE_Y + 1))) 
                {
                    faces.Add(new Face()
                    {
                        Vertices = new List<int>() { user_offset + ((i - (CUBE_Y + 1)) - 1), user_offset + ((i - (CUBE_Y + 1))), user_offset + (i), user_offset + (i - 1) },
                        Color = (user % 2 == 0) ? Colors.DarkRed : Colors.Blue
                    });
                }
            }
        }

            // At this point we have a { USER COUNT } amount of 14x3 grids

        return faces;
    }

    // Faces are referenced by their top right point
    public void modifyFaceColor(int user, int day, int shift, Microsoft.Maui.Graphics.Color desiredColor)
    {
        user = user * (CUBE_X) * (CUBE_Y);
        day = day * (CUBE_Y);

        CubeFaces.ElementAt(user+day+shift).Color = Colors.Green;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CubeFaces)));
    }
}