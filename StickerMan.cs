using System.Collections.Generic;

namespace sticker_man
{
    public class StickerMan : GameObject
    {

        private Ponto4D root;
        private List<GameObject> limbs;

        public StickerMan(Ponto4D root = null)
        {
            this.root = (root != null) ? root : new Ponto4D();
            limbs = new List<GameObject>();

            CreateBody(root);
        }

        public Ponto4D GetRoot() => root;

        private void CreateBody(Ponto4D raiz)
        {
            Ponto4D topoTronco = new Ponto4D(raiz.X, raiz.Y+130);
            Ponto4D origemBraco = new Ponto4D(raiz.X, raiz.Y+100);

            limbs.Add(new Limb(raiz, topoTronco));
            limbs.Add(new Limb(raiz, new Ponto4D(raiz.X-80, raiz.Y-100)));
            limbs.Add(new Limb(raiz, new Ponto4D(raiz.X+80, raiz.Y-100)));
            limbs.Add(new Limb(origemBraco, new Ponto4D(raiz.X-80, raiz.Y)));
            limbs.Add(new Limb(origemBraco, new Ponto4D(raiz.X+80, raiz.Y)));
            limbs.Add(new Head(topoTronco, 50));

            SetVertices();
        }

        private void SetVertices()
        {
            List<Ponto4D> vertices = new List<Ponto4D>();
            foreach(var limb in limbs)
                vertices.AddRange(limb.GetVertices());

            base.SetVertices(vertices);
        }

    }

}