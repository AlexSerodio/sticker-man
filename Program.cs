using System;

namespace sticker_man
{
    class Program
    {
        static void Main(string[] args)
        {
            Render window = new Render(600, 600);
            window.Run();
            window.Run(1.0/60.0);
        }
    }
}
