namespace stick_man
{
    public class Physics
    {

        public static double GRAVITY_FORCE = -5;

        private Physics(){}

        public static void InvertGravity() => GRAVITY_FORCE*=-1;

    }
}