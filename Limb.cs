using System.Collections.Generic;

namespace sticker_man
{
    public class Limb : GameObject
    {

        public Limb(Ponto4D root, Ponto4D final)
            : base(new List<Ponto4D>{ root, final }){}

        public Ponto4D GetRoot() => base.GetVertices()[0];
        public Ponto4D GetFinal() => base.GetVertices()[1];

    }

}