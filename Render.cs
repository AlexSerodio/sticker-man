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

    public Render(int width, int height) : base(width, height) { 

      world = new World(width, height);
      player = new Stickman(new Ponto4D(200, 1200), 0.2, world);

    }

    protected override void OnLoad(EventArgs e) { 
      base.OnLoad(e); 
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);

      if(!world.IsCreationModeOn())
        HandlePlayerMovement();

      world.UpdateCamera();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.ClearColor(Color.Gray);
      GL.MatrixMode(MatrixMode.Modelview);

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
      }
    }
  }
}