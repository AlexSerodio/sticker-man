using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace sticker_man
{
  class Render : GameWindow
  {

    private SrPalito srPalito = new SrPalito(new Ponto4D(-200, -200));
    private Mundo mundo = new Mundo();
    private Camera camera = new Camera(-400, 400, -400, 400, -1, 1);
    private double moveSpeed = 2.0;

    public Render(int width, int height) : base(width, height) { }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);

      camera.Update();
    }

    protected override void OnKeyDown(KeyboardKeyEventArgs e) 
    {
      base.OnKeyDown(e);

      TratarTeclasPressionadas(e);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.ClearColor(Color.Gray);
      GL.MatrixMode(MatrixMode.Modelview);

      srPalito.Desenhar();

      this.SwapBuffers();
    }

    private void TratarTeclasPressionadas(KeyboardKeyEventArgs e) 
    {
      switch(e.Key) {
        case Key.Space: 
          break;
      }
    }
  }
}