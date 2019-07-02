using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace sticker_man
{
    public class Ground : GameObject
    {

        public Ground(List<Ponto4D> vertices) : base(vertices) {
            base.SetTag(Tag.GROUND);
            base.SetVertices(vertices);
        }

        public override void Draw()
        {
            GL.Color3(Color.Black);
            GL.Begin(PrimitiveType.QuadStrip);
                foreach(Ponto4D vertex in base.GetVertices())
                    GL.Vertex2(vertex.X, vertex.Y);
            GL.End();
            base.GetBoundBox().DesenharBBox();
        }

    }
}