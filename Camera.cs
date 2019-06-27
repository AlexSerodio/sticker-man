using OpenTK.Graphics.OpenGL;

namespace sticker_man
{
  public class Camera
  {

    private double left;
    private double right;
    private double bottom;
    private double top;
    private double near;
    private double far;

    public Camera(double left, double right, double bottom, double top, double near, double far) 
    {
      this.left = left;
      this.right = right;
      this.bottom = bottom;
      this.top = top;
      this.near = near;
      this.far = far;
    }

    public void Update() 
    {
      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();

      GL.Ortho(this.left, this.right, this.bottom, this.top, this.near, this.far);
    }
  }
}