namespace stick_man
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Render window = new Render(600, 600);
            window.Run(1.0/60.0);
        }
    }
}
