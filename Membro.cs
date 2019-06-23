namespace sticker_man
{
    public interface Membro : Desenho, Objeto
    {

        Ponto4D GetOrigem();
        Ponto4D GetFinal();

    }
}