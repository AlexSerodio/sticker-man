using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace stick_man
{
  public class Render : GameWindow
  {

    private World world;
    private Stickman player;
    private Background background;

    private Vector3 eye;
    private Vector3 target;
    private Vector3 up = Vector3.UnitY;
    private int mouseXOffset = 300;
    private int mouseYOffset = 350;
    private double objectSpeed = 5.0;

    public Render(int width, int height, int distance) : base(width, height)
    { 
      eye = new Vector3(0, 0, distance);
      target = new Vector3(0, 0, 0);

      world = new World(width, height);
      player = new Stickman(new Ponto4D(0, 0), 0.2, world);

      background = new Background();
    }

    protected override void OnLoad(EventArgs e)
    { 
      base.OnLoad(e); 

      GL.ClearColor(Color.Gray);
      GL.Enable(EnableCap.DepthTest);
      GL.Enable(EnableCap.CullFace);
      GL.Enable(EnableCap.Texture2D);
    }

    protected override void OnResize(EventArgs e) 
    {
      base.OnResize(e);

      GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

      Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 1000.0f);
      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadMatrix(ref projection);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);

      HandlePlayerMovement();

      if(world.HasSelectedObject())
        HandleObjectMovement();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
      Matrix4 modelview = Matrix4.LookAt(eye, target, up);
      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadMatrix(ref modelview);

      background.Draw();

      world.HandleObjects();
      player.Draw();
      player.Gravity();

      if(player.ReachedJumpLimit())
          player.MoveUp();
      else
          player.SetIsJumping(false);

      this.SwapBuffers();
    }

    protected override void OnKeyDown(KeyboardKeyEventArgs e) { 
      base.OnKeyDown(e);

      HandlePressedKeys(e);
    }

    protected override void OnKeyUp(KeyboardKeyEventArgs e) { 
      base.OnKeyUp(e);

      if(e.Key == Key.W || e.Key == Key.S || e.Key == Key.A || e.Key == Key.D)
        player.SetWalking(false);
    }

    protected override void OnMouseDown(MouseButtonEventArgs e) {
      base.OnMouseDown(e);

      Ponto4D clickedPoint = new Ponto4D(e.X-mouseXOffset, Height-e.Y-mouseYOffset);

      if(e.Button == MouseButton.Left) {

        if(world.IsCreatingPlataform())
          world.CreatePlataform(clickedPoint);
        else if(world.IsCreatingRampLeft())
          world.CreateRampLeft(clickedPoint);
        else if(world.IsCreatingRampRight())
          world.CreateRampRight(clickedPoint);
        else if(world.IsCreatingBox())
          world.CreateBox(clickedPoint);
        else
          world.SetSelectedObject(world.SelectObject(clickedPoint));

      }
    }

    private void HandlePlayerMovement() 
    {
      KeyboardState keyState = Keyboard.GetState();

      if(keyState.IsKeyDown(Key.A))
        player.MoveLeft();

      if(keyState.IsKeyDown(Key.D))
        player.MoveRight();

      if(keyState.IsKeyDown(Key.S))
        player.MoveDown();
      
      if(keyState.IsKeyDown(Key.W)) {
        if(!player.IsJumping())
          player.StartJump();
      }
    }

    private void HandleObjectMovement()
    {  
      KeyboardState keyState = Keyboard.GetState();

      if(keyState.IsKeyDown(Key.Up)) {
        world.GetSelectedObject().Translate(0, objectSpeed, 0);
        if(world.GetSelectedObject().Collided())
          world.GetSelectedObject().Translate(0, -objectSpeed, 0);
      }
      
      if(keyState.IsKeyDown(Key.Down)) {
        world.GetSelectedObject().Translate(0, -objectSpeed, 0);
        if(world.GetSelectedObject().Collided())
          world.GetSelectedObject().Translate(0, objectSpeed, 0);
      }

      if(keyState.IsKeyDown(Key.Right)) {
        world.GetSelectedObject().Translate(objectSpeed, 0, 0);
        if(world.GetSelectedObject().Collided())
          world.GetSelectedObject().Translate(-objectSpeed, 0, 0);
      }

      if(keyState.IsKeyDown(Key.Left)) {
        world.GetSelectedObject().Translate(-objectSpeed, 0, 0);
        if(world.GetSelectedObject().Collided())
          world.GetSelectedObject().Translate(objectSpeed, 0, 0);
      }
    }

    private void HandlePressedKeys(KeyboardKeyEventArgs e) {
      switch(e.Key) {
        case Key.Number1:
          world.SetCreatingPlataform(true);
          world.SetCreatingRampLeft(false);
          world.SetCreatingRampRight(false);
          world.SetCreatingBox(false);
          break;
        case Key.Number2:
          world.SetCreatingRampRight(true);
          world.SetCreatingRampLeft(false);
          world.SetCreatingPlataform(false);
          world.SetCreatingBox(false);
          break;
        case Key.Number3:
          world.SetCreatingRampLeft(true);
          world.SetCreatingRampRight(false);
          world.SetCreatingPlataform(false);
          world.SetCreatingBox(false);
          break;
        case Key.Number4:
          world.SetCreatingBox(true);
          world.SetCreatingRampLeft(false);
          world.SetCreatingRampRight(false);
          world.SetCreatingPlataform(false);
          break;
        case Key.Escape:
          this.Exit();
          break;
        case Key.R:
          if(world.HasSelectedObject())
            world.GetSelectedObject().Scale(1.1);
          break;
        case Key.T:
          if(world.HasSelectedObject())
            world.GetSelectedObject().Scale(0.9);
          break;
        case Key.F:
          if(world.HasSelectedObject())
            world.GetSelectedObject().Rotate(1.1);
          break;
        case Key.G:
          if(world.HasSelectedObject())
            world.GetSelectedObject().Rotate(-1.1);
          break;
      }
    }
  }
}