namespace stick_man
{
    public class Physics
    {

        private const double GRAVITY_FORCE = -2;

        private Physics(){}

        public static void Gravity(GameObject gameObject) {
            gameObject.Translate(0, GRAVITY_FORCE, 0);
            foreach(GameObject obj in Global.objects) {
                if(obj.IsColliding(gameObject))
                    gameObject.Translate(0, -GRAVITY_FORCE, 0);
            }
        }

    }
}