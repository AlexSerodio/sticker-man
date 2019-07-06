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

    private Stickman stickman;
    private Ground ground;
    private Camera camera;

    private double moveSpeed = 2.0;
    private bool creationMode = false;
    private bool creatingObject = false;

    public Render(int width, int height) : base(width, height) { 

      camera = new Camera(0, width, 0, height, -1, 1);

      stickman = new Stickman(new Ponto4D(200, 1200));

      ground = new Ground(new List<Ponto4D> { 
        new Ponto4D(0, 0),
        new Ponto4D(0, 50),
        new Ponto4D(width, 0),
        new Ponto4D(width, 50),
      });

      World.objects.Add(ground);
    }

    protected override void OnLoad(EventArgs e) { 
      base.OnLoad(e); 
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);

      if(!creationMode)
        HandlePlayerMovement();

      camera.Update();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.ClearColor(Color.Gray);
      GL.MatrixMode(MatrixMode.Modelview);

      stickman.Draw();

      foreach(GameObject obj in World.objects)
        obj.Draw();

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
        if(creationMode) {
          if(!creatingObject) {
            World.objects.Add(new GeometricObject(new List<Ponto4D>(){ clickedPoint, clickedPoint }));
            creatingObject = true;
          } else {
            World.objects[World.objects.Count-1].AddVertice(clickedPoint);
          }
        }
      }
    }

    protected override void OnMouseMove(MouseMoveEventArgs e) {
      base.OnMouseMove(e);
    
      if(creationMode) {
          if(creatingObject) {
            Ponto4D point = new Ponto4D(e.X, Height - e.Y);
            GameObject lastObject = World.objects[World.objects.Count-1];
            int lastVerticePosition = lastObject.GetVertices().Count-1;
            World.objects[World.objects.Count-1].UpdateVertice(point, lastVerticePosition);
          }
      }
    }

    private void HandlePlayerMovement() 
    {
      KeyboardState keyState = Keyboard.GetState();

      if(keyState.IsKeyDown(Key.A)) {
        stickman.Translate(-moveSpeed, 0, 0);

        if(stickman.Collided())
          stickman.Translate(moveSpeed, 0, 0);
      }

      if(keyState.IsKeyDown(Key.D)) {
        stickman.Translate(moveSpeed, 0, 0);

        if(stickman.Collided())
          stickman.Translate(-moveSpeed, 0, 0);
      }

      if(keyState.IsKeyDown(Key.W)) {
        stickman.Translate(0, moveSpeed, 0);

        if(stickman.Collided())
          stickman.Translate(0, -moveSpeed, 0);
      }

      if(keyState.IsKeyDown(Key.S)) {
        stickman.Translate(0, -moveSpeed, 0);

        if(stickman.Collided())
          stickman.Translate(0, moveSpeed, 0);
      }
    }

    private void HandlePressedKeys(KeyboardKeyEventArgs e) {
      switch(e.Key) {
        case Key.Z:
          creationMode = !creationMode;
          
          Console.WriteLine("CreationMode: {0}", creationMode);

          if(creatingObject) {
            World.objects[World.objects.Count-1].SetPrimitive(PrimitiveType.LineLoop);
            creatingObject = false;
          }
          break;
        case Key.C:
          World.objects[World.objects.Count-1].SetPrimitive(PrimitiveType.LineLoop);
          creatingObject = false;
          break;
      }
    }
  }
}