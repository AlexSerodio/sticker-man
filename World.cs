using System;
using System.Collections.Generic;

namespace sticker_man
{
    public class World
    {
        private Camera camera;

        public World() 
        {
            camera = new Camera(-400, 400, -400, 400, -1, 1);
        }

    }
}
