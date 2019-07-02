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
    private double jumpSpeed = 2.0;
    private double jumpLimit = 50.0;
    private bool jumping = false;

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

      HandlePressedKeys();

      camera.Update();
    }

    protected override void OnKeyDown(KeyboardKeyEventArgs e) { 
      base.OnKeyDown(e); 
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

    private void HandlePressedKeys() 
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
        stickman.Translate(0, jumpSpeed, 0);

        if(stickman.Collided())
          stickman.Translate(0, -jumpSpeed, 0);
      }

      if(keyState.IsKeyDown(Key.S)) {
        stickman.Translate(0, -jumpSpeed, 0);

        if(stickman.Collided())
          stickman.Translate(0, jumpSpeed, 0);
      }
    }
  }
}