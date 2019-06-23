using System.Collections.Generic;

namespace sticker_man
{
    public interface Objeto
    {

        void Mover(double tx, double ty, double tz);
        List<Ponto4D> GetPontos();

    }
}