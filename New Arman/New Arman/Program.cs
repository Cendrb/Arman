using System;

namespace New_Arman
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Arman game = new Arman())
            {
                game.Run();
            }
        }
    }
#endif
}

