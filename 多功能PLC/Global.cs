using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Timers;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace PLC
{
    class Global
    {
        public static Socket socket;
        public static IPEndPoint IPep = new IPEndPoint(IPAddress.Any, 0);
        public static bool setPLC = false;
        public static string PLC_message = "9600已開啟";
        public static string PLC_Random_message = "";
        public static int start ;
        public static int Number;
        public static string memory_name;
        
    }
}
