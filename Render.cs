using System;
using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace stick_man
{
  public class Render : GameWindow
  {

    private World world;
    private Stickman player;

    // Eye: (87, 246, 257)
    // Target: (78, 255, -74)
    Vector3 eye = new Vector3(0, 0, 500);
    Vector3 target = Vector3.Zero;
    Vector3 up = Vector3.UnitY;

    public Render(int width, int height) : base(width, height)
    { 

      world = new World(width, height);
      // player = new Stickman(new Ponto4D(200, 1200), 0.2, world);
      player = new Stickman(new Ponto4D(0, 0), 0.2, world);

    }

    protected override void OnLoad(EventArgs e)
    { 
      base.OnLoad(e); 

      GL.ClearColor(Color.Gray);
      GL.Enable(EnableCap.DepthTest);
      GL.Enable(EnableCap.CullFace);

    }

    protected override void OnResize(EventArgs e) 
    {
      base.OnResize(e);

      GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

      Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 600.0f);
      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadMatrix(ref projection);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);

      if(!world.IsCreationModeOn())
        HandlePlayerMovement();

      // world.UpdateCamera();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
      Matrix4 modelview = Matrix4.LookAt(eye, target, up);
      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadMatrix(ref modelview);

      world.DrawObjects();
      player.Draw();

      this.SwapBuffers();
    }

    protected override void OnKeyDown(KeyboardKeyEventArgs e) { 
      base.OnKeyDown(e);

      HandlePressedKeys(e);
    }

    protected override void OnMouseDown(MouseButtonEventArgs e) {
      base.OnMouseDown(e);

      Ponto4D clickedPoint = new Ponto4D(e.X, Height - e.Y);

      if(e.Button == MouseButton.Left) {

      } else if(e.Button == MouseButton.Right) {
        if(world.IsCreationModeOn()) {
          if(!world.IsCreatingObject()) {
            world.AddObject(new GeometricObject(new List<Ponto4D>(){ clickedPoint, clickedPoint }));
            world.SetCreatingObject(true);
          } else {
            world.GetLastObject().AddVertice(clickedPoint);
          }
        }
      }
    }

    protected override void OnMouseMove(MouseMoveEventArgs e) {
      base.OnMouseMove(e);
    
      if(world.IsCreationModeOn()) {
          if(world.IsCreatingObject()) {
            Ponto4D point = new Ponto4D(e.X, Height - e.Y);
            GameObject lastObject = world.GetLastObject();
            int lastVerticePosition = lastObject.GetVertices().Count-1;
            lastObject.UpdateVertice(point, lastVerticePosition);
          }
      }
    }

    private void HandlePlayerMovement() 
    {
      KeyboardState keyState = Keyboard.GetState();

      if(keyState.IsKeyDown(Key.A))
        player.MoveLeft();

      if(keyState.IsKeyDown(Key.D))
        player.MoveRight();

      if(keyState.IsKeyDown(Key.W))
        player.MoveUp();

      if(keyState.IsKeyDown(Key.S))
        player.MoveDown();
    }

    private void HandlePressedKeys(KeyboardKeyEventArgs e) {
      switch(e.Key) {
        case Key.Z:
          world.SwitchCreationMode();

          if(world.IsCreationModeOn()) {
            world.GetLastObject().SetPrimitive(PrimitiveType.LineLoop);
            world.SetCreatingObject(false);
          }
          break;
        case Key.C:
          world.GetLastObject().SetPrimitive(PrimitiveType.LineLoop);
          world.SetCreatingObject(false);
          break;
        case Key.Right:
          eye.X += 1;
          break;
        case Key.Left:
          eye.X -= 1;
          break;
        case Key.Up:
          eye.Y += 1;
          break;
        case Key.Down:
          eye.Y -= 1;
          break;
        case Key.Home:
          eye.Z += 1;
          break;
        case Key.End:
          eye.Z -= 1;
          break;
        
        case Key.L:
          target.X += 1;
          break;
        case Key.J:
          target.X -= 1;
          break;
        case Key.I:
          target.Y += 1;
          break;
        case Key.K:
          target.Y -= 1;
          break;
        case Key.N:
          target.Z += 1;
          break;
        case Key.M:
          target.Z -= 1;
          break;        

        case Key.Escape:
          this.Exit();
          break;

        case Key.P:
          Console.WriteLine("Eye: ({0}, {1}, {2})", eye.X, eye.Y, eye.Z);
          Console.WriteLine("Target: ({0}, {1}, {2})", target.X, target.Y, target.Z);
        break;
      }
    }
  }
}