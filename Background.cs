using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace stick_man
{
    public class Background
    {

        private double limit = 500;
        private int texture;

        public Background() {
            texture = Texture.GetTexture("sky.jpg");
        }

        public void Draw() {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texture);

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.White);
            GL.TexCoord2(1.0f, 0.0f);GL.Vertex3(-limit, -limit, -80);   // UR
            GL.TexCoord2(0.0f, 0.0f);GL.Vertex3(limit, -limit, -80);    // UL
            GL.TexCoord2(0.0f, 1.0f);GL.Vertex3(limit, limit, -80);     // DL
            GL.TexCoord2(1.0f, 1.0f);GL.Vertex3(-limit, limit, -80);    // DR
            GL.End();

            GL.Disable(EnableCap.Texture2D);
        }

    }
}