using System.Collections.Generic;
using System.Drawing;

namespace stick_man
{
    public class Box : Rectangle
    {

        public Box(List<Ponto4D> vertices, double extrudeDistance) : base(vertices, extrudeDistance) {
            SetHasGravity(true);
            SetColor(Color.Blue);
        }

    }
}