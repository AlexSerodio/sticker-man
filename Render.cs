using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace sticker_man
{
  public class Render : GameWindow
  {

    private StickerMan srPalito;
    private Camera camera;
    private double moveSpeed = 2.0;
    private double jumpSpeed = 2.0;
    private double jumpLimit = 50.0;
    private bool jumping = false;

    public Render(int width, int height) : base(width, height) { 

      camera = new Camera(-width/2, width/2, -height/2, height/2, -1, 1);
      srPalito = new StickerMan(new Ponto4D(-200, -200));

    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);

      TratarTeclasPressionadas();


      camera.Update();
    }

    protected override void OnKeyDown(KeyboardKeyEventArgs e) 
    {
      base.OnKeyDown(e);

    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.ClearColor(Color.Gray);
      GL.MatrixMode(MatrixMode.Modelview);

      srPalito.Draw();

      this.SwapBuffers();
    }

    private void TratarTeclasPressionadas() 
    {
      KeyboardState keyState = Keyboard.GetState();

      if(keyState.IsKeyDown(Key.A))
        srPalito.Translate(-moveSpeed, 0, 0);

      if(keyState.IsKeyDown(Key.D))
        srPalito.Translate(moveSpeed, 0, 0);

      if(keyState.IsKeyDown(Key.W))
        srPalito.Translate(0, jumpSpeed, 0);

      if(keyState.IsKeyDown(Key.S))
        srPalito.Translate(0, -jumpSpeed, 0);

    }
  }
}