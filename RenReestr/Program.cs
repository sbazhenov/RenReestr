using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
//using System.Windows.Controls;
using System.Data;
using System.Xml;

namespace RenReestr
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RenameReestr());
        }
    
    }
}
