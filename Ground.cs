using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace stick_man
{
    public class Ground : GameObject
    {

    Bitmap bitmap = new Bitmap("madeira.jpg");
    int texture;
        public Ground(List<Ponto4D> vertices) : base(vertices) {
            base.SetTag(Tag.GROUND);
            base.SetVertices(vertices);
        }

        public override void Draw()
        {
            //GL.Color3(Color.Black);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            GL.Enable(EnableCap.Texture2D);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            GL.GenTextures(1, out texture);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                 OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);        

            GL.Begin(PrimitiveType.Polygon);
                foreach(Ponto4D vertex in base.GetVertices())
                    GL.Vertex2(vertex.X, vertex.Y);

            GL.BindTexture(TextureTarget.Texture2D, texture);
           
            GL.End();
            base.GetBoundBox().DesenharBBox();
        }

    }
}