using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RTS
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// using System.Runtime.InteropServices;

        [DllImport("kernel32")]
        static extern bool AllocConsole();
        [STAThread]
        static void Main()
        {
            AllocConsole();
            using (var game = new Game1())
                game.Run();
            
        }
    }
#endif
}
