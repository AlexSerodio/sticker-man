using System.Collections.Generic;

namespace stick_man
{
    public class Limb : GameObject
    {
        
        private BodyPart bodyPart;

        public void SetBodyPart(BodyPart bodyPart) => this.bodyPart = bodyPart;
        public BodyPart GetBodyPart() => this.bodyPart;

        public Limb(Ponto4D root, Ponto4D final, BodyPart bodyPart = BodyPart.NONE)
            : base(new List<Ponto4D>{ root, final }){
                this.SetBodyPart(bodyPart);
        }

        public Ponto4D GetRoot() => base.GetVertices()[0];
        public Ponto4D GetFinal() => base.GetVertices()[1];

    }

}