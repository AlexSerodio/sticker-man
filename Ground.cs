using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace stick_man
{
    public class Ground : Rectangle
    {

        public Ground(List<Ponto4D> vertices) : base(vertices, 200) {
            base.SetTag(Tag.GROUND);
            base.SetVertices(vertices);
            base.SetColor(Color.Brown);

            base.SetHasGravity(false);
        }

    }
}