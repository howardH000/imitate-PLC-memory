#define  memor
#undef DBmemory 
#define UDP
using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Data.OleDb;
using System.Data.SqlClient; 

namespace PLC
{
    class PLC_InterSetting
    {
        public static bool check = true;
        public static DateTime first_set_time;
        public static int first_set_time_mill;
        public static DateTime second_set_time;
        public static int second_set_time_mill;
        enum memory : byte         //記憶體byte
        {
            CIO = 176,
            HR = 177,
            AR = 178,
            DM = 130,
            E0 = 160
        }
        public static int[] memoryDM = new int[65536];
        public static int[] memoryE0 = new int[65536];
        public static int[] memoryCIO = new int[12288];
        public static int[] memoryHR = new int[1024];
        public static int[] memoryAR = new int[1920];
#if UDP
        public static void PLC_Receive()
        {
            while (true)
            {
                #region 判定port是否有開啟
                try
                {
                    Global.socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    Global.socket.Bind(Global.IPep);
                    Global.setPLC = false;

                    Global.PLC_message = Convert.ToString(Global.IPep.Port) + "已開啟";
                    break;
                }
                catch
                {
                    if (Global.setPLC == false)
                    {
                        Global.IPep.Port = Global.IPep.Port + 1;
                    }
                    if (Global.setPLC == true)
                    {
                        Global.PLC_Random_message = Convert.ToString(Global.IPep.Port) + "已被開啟因此開起隨機port";
                        Random rand = new Random();
                        Global.IPep.Port = rand.Next(65535);
                    }
                }
                #endregion
            }

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 5555);
            EndPoint Remote = (EndPoint)(sender);
            while (true)
            {
                try
                {
                    byte[] content = new byte[2014];
                    int recv = Global.socket.ReceiveFrom(content, ref Remote);//接收訊息
                    first_set_time = System.DateTime.Now;
                    first_set_time_mill = System.DateTime.Now.Millisecond;
        #region 發出訊息端IP位置
                    string IP_add = Convert.ToString(Remote);
                    int a = IP_add.LastIndexOf(".");
                    int b = IP_add.LastIndexOf(":");
                    string IPs = "";
                    for (int i = a + 1; i < b; i++)
                    {
                        IPs += IP_add[i];
                    }
                    #endregion
                    byte fins_cmnd_0 = 0xc0;/*ICF*/
                    byte fins_cmnd_1 = 0x00;/*RSV*/
                    byte fins_cmnd_2 = 0x02;/*GCT*/
                    byte fins_cmnd_3 = 0x00;/*DNA*/
                    byte fins_cmnd_5 = 0xb1;/*DAT2*/
                    byte fins_cmnd_6 = 0x00;/*SNA*/
                    byte[] PLC_content = new byte[14];
                    PLC_content[0] = fins_cmnd_0;
                    PLC_content[1] = fins_cmnd_1;
                    PLC_content[2] = fins_cmnd_2;
                    PLC_content[3] = fins_cmnd_3;
                    PLC_content[4] = Convert.ToByte(Convert.ToInt32(IPs));
                    PLC_content[5] = fins_cmnd_5;
                    PLC_content[6] = fins_cmnd_6;
                    PLC_content[7] = content[4];
                    PLC_content[8] = 0x00;
                    PLC_content[9] = content[9];
#if memory
                    if (content[10] == 0x01)
                    {
        #region 內部陣列

                        if (content[11] == 0x01)
                        {
        #region 讀
                            PLC_content[11] = content[11];
                            PLC_content[12] = 0x00; ;
                            PLC_content[13] = 0x00;

                            int start_value = content[13] * 256 + content[14];
                            int value_row = start_value / 10;
                            int value_colum = start_value % 10;
                            int k = 0;
                            int i = value_row;
                            int j = value_colum * 2 + 1;
                            int value = content[16] * 256 + content[17];
                            byte[] PLC_search_send = new byte[value * 2];
                            byte[] PLC_send = new byte[value * 2 + 14];
                            switch (content[12])  //選擇要使用的記憶體
                            {
                                case (int)memory.DM:
                                    for (int X = 0; X < value * 2; X++)
                                    {
                                        PLC_search_send[X] = Convert.ToByte(memoryDM[start_value * 2 + X]);
                                    }
                                    break;
                                case (int)memory.CIO:
                                    for (int X = 0; X < value * 2; X++)
                                    {
                                        PLC_search_send[X] = Convert.ToByte(memoryCIO[start_value * 2 + X]);
                                    }
                                    break;
                                case (int)memory.AR:
                                    for (int X = 0; X < value * 2; X++)
                                    {
                                        PLC_search_send[X] = Convert.ToByte(memoryAR[start_value * 2 + k]);
                                    }
                                    break;
                                case (int)memory.HR:
                                    for (int X = 0; X < value * 2; X++)
                                    {
                                        PLC_search_send[X] = Convert.ToByte(memoryHR[start_value * 2 + X]);
                                    }
                                    break;
                                case (int)memory.E0:
                                    for (int X = 0; X < value * 2; X++)
                                    {
                                        PLC_search_send[X] = Convert.ToByte(memoryE0[start_value * 2 + X]);
                                    }

                                    break;
                            }

                            int content_value = start_value * 2;
                            Array.Copy(PLC_content, PLC_send, 14);
                            Array.Copy(PLC_search_send, 0, PLC_send, 14, PLC_search_send.Length);
                            #region 延遲
                            int delay = 13;
                            if (value <= 101)
                            { delay = 14; }
                            else if (value <= 434 && value > 101)
                            { delay = 15; }
                            else if (value <= 767 && value > 434)
                            { delay = 16; }
                            else if (value <= 1000 && value > 767)
                            { delay = 17; }
                            Thread.Sleep(delay);
                            #endregion
                            second_set_time = System.DateTime.Now;
                            second_set_time_mill = System.DateTime.Now.Millisecond;
                            Global.socket.SendTo(PLC_send, Remote);
                            #endregion
                        }
                        else if (content[11] == 0x02)
                        {
        #region 寫入
                            PLC_content[11] = content[11];
                            PLC_content[12] = 0x00;
                            PLC_content[13] = 0x00;
                            byte[] PLC_send = new byte[14];
                            int i = 18;
                            int k = 0;
                            int start_value = content[13] * 256 + content[14];
                            int value_sum = recv - 18;
                            int pos_value = start_value;
                            switch (content[12])      //選擇要使用的記憶體
                            {
                                case (int)memory.DM:
                                    while (i < recv)
                                    {
                                        memoryDM[start_value * 2 + k] = content[i];
                                        memoryDM[start_value * 2 + (k + 1)] = content[(i + 1)];
                                        i = i + 2;
                                        k = k + 2;
                                    }
                                    break;
                                case (int)memory.CIO:
                                    while (i < recv)
                                    {
                                        memoryDM[start_value * 2 + k] = content[i];
                                        memoryDM[start_value * 2 + (k + 1)] = content[(i + 1)];
                                        i = i + 2;
                                        k = k + 2;
                                    }

                                    break;
                                case (int)memory.AR:
                                    while (i < recv)
                                    {
                                        memoryAR[start_value * 2 + k] = content[i];
                                        memoryAR[start_value * 2 + (k + 1)] = content[(i + 1)];
                                        i = i + 2;
                                        k = k + 2;
                                    }
                                    break;
                                case (int)memory.HR:
                                    while (i < recv)
                                    {
                                        memoryHR[start_value * 2 + k] = content[i];
                                        memoryHR[start_value * 2 + (k + 1)] = content[(i + 1)];
                                        i = i + 2;
                                        k = k + 2;
                                    }
                                    break;
                                case (int)memory.E0:
                                    while (i < recv)
                                    {
                                        memoryE0[start_value * 2 + k] = content[i];
                                        i = i + 1;
                                        if (i >= content.Length) { break; }
                                        memoryE0[start_value * 2 + (k + 1)] = content[i];
                                        i = i + 1;
                                        k = k + 2;
                                    }

                                    break;
                            }
                            #region 延遲
                            int delay = 13;
                            if (recv <= 101)
                            { delay = 14; }
                            else if (recv <= 434 && recv > 101)
                            { delay = 15; }
                            else if (recv <= 767 && recv > 434)
                            { delay = 16; }
                            else if (recv <= 1000 && recv > 767)
                            { delay = 17; }
                            Thread.Sleep(delay);
                            #endregion
                            Array.Copy(PLC_content, PLC_send, 13);
                            Global.socket.SendTo(PLC_send, Remote);
                            #endregion
                        }
                        #endregion
                    }
                    else if (content[10] == 0x07) 
                    {
        #region 讀時間
                        if (content[11] == 0x01)
                        { 
                            PLC_content[10]=content[10];
                            PLC_content[11] = content[11];
                            PLC_content[12] = 0x00;
                            PLC_content[13] = 0x00;
                            byte[] PLC_time = new byte[7];
                            byte[] PLC_send = new byte[21];
                            int year = System.DateTime.Now.Year;
                            if (year >= 2000)
                                year = year - 2000;
                            else
                                year = year - 1900;
                            string year_str = Convert.ToString(year);
                            int years = Convert.ToInt32(year_str, 16);

                            PLC_time[0] = Convert.ToByte(Convert.ToInt32(Convert.ToString( year),16));
                            PLC_time[1] = Convert.ToByte(Convert.ToInt32(Convert.ToString(System.DateTime.Now.Month), 16));
                            PLC_time[2] = Convert.ToByte(Convert.ToInt32(Convert.ToString(System.DateTime.Now.Day), 16));
                            PLC_time[3] = Convert.ToByte(Convert.ToInt32(Convert.ToString(System.DateTime.Now.Hour), 16));
                            PLC_time[4] = Convert.ToByte(Convert.ToInt32(Convert.ToString(System.DateTime.Now.Minute), 16));
                            PLC_time[5] = Convert.ToByte(Convert.ToInt32(Convert.ToString(System.DateTime.Now.Second), 16));
                            PLC_time[6] = Convert.ToByte(Convert.ToInt32(Convert.ToString(Convert.ToInt32( System.DateTime.Now.DayOfWeek)), 16));
                            Array.Copy(PLC_content, PLC_send, 14);
                            Array.Copy(PLC_time, 0, PLC_send, 14, PLC_time.Length);
                            Thread.Sleep(14);
                            Global.socket.SendTo(PLC_send, Remote);

                        }
                    #endregion
                    }
#else
                    if (content[10] == 0x01)
                    {
        #region 資料庫
                        PLC_content[10] = content[10];
                        String dbpath = "PLCmemory.mdb";   //資料庫當記憶體用
                        String Source;//C:\\Users\\mei.yu\\Desktop\\20131104\\WindowsFormsApplication4\\WindowsFormsApplication4\\bin\\Debug
                        Source = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbpath;
                        OleDbConnection conn;
                        conn = new OleDbConnection(Source);

                        conn.Open();
                        if (content[11] == 0x01)
                        {
        #region 讀資料庫
                            PLC_content[11] = content[11];
                            PLC_content[12] = 0x00; ;
                            PLC_content[13] = 0x00;
                            string memory_string = "";
                            DataTable out_dt;
                            switch (content[12])  //選擇要使用的記憶體
                            {
                                case (int)memory.DM:
                                    memory_string = "DM";
                                    break;
                                case (int)memory.CIO:
                                    memory_string = "CIO";
                                    break;
                                case (int)memory.AR:
                                    memory_string = "AR";
                                    break;
                                case (int)memory.HR:
                                    memory_string = "HR";
                                    break;
                                case (int)memory.E0:
                                    memory_string = "E0";

                                    break;
                            }

                            string out_DM = "SELECT * from " + memory_string;
                            DataSet out_DM_ds = new DataSet();
                            OleDbDataAdapter IN_DtApter = new OleDbDataAdapter(out_DM, conn);
                            IN_DtApter.Fill(out_DM_ds, memory_string);
                            out_dt = out_DM_ds.Tables[memory_string];
                            int start_value = content[13] * 256 + content[14];
                            int value_row = start_value / 10;
                            int value_colum = start_value % 10;
                            int k = 0;
                            int i = value_row;
                            int j = value_colum * 2 + 1;
                            int value = content[16] * 256 + content[17];
                            int[] PLC_search = new int[value * 2];
                            byte[] PLC_search_send = new byte[value * 2];
                            byte[] PLC_send = new byte[value * 2 + 14];
                            while (k < value * 2)
                            {
                                try
                                {
                                    PLC_search[k] = Convert.ToInt32((string)out_dt.Rows[i][j], 16);
                                }
                                catch
                                {
                                    PLC_search[k] = 0;
                                }
                                j++;
                                if (j > 20)
                                {
                                    i++;
                                    j = 1;
                                }
                                k++;
                            }
                            for (int X = 0; X < value * 2; X++)
                            {
                                PLC_search_send[X] = Convert.ToByte(PLC_search[X]);
                            }
                            int content_value = start_value * 2;
                            Array.Copy(PLC_content, PLC_send, 14);
                            Array.Copy(PLC_search_send, 0, PLC_send, 14, PLC_search_send.Length);
                            Global.socket.SendTo(PLC_send, Remote);
                    #endregion
                        }
                        else if (content[11] == 0x02)
                        {
        #region 寫入
                            PLC_content[11] = content[11];
                            PLC_content[12] = 0x00;
                            PLC_content[13] = 0x00;
                            byte[] PLC_send = new byte[14];
                            string memory_string = "";
                            switch (content[12])      //選擇要使用的記憶體
                            {
                                case (int)memory.DM:
                                    memory_string = "DM";
                                    break;
                                case (int)memory.CIO:
                                    memory_string = "CIO";
                                    break;
                                case (int)memory.AR:
                                    memory_string = "AR";
                                    break;
                                case (int)memory.HR:
                                    memory_string = "HR";
                                    break;
                                case (int)memory.E0:
                                    memory_string = "E0";
                                    break;
                            }
                            int i = 18;
                            int start_value = content[13] * 256 + content[14];
                            int value_sum = content.Length - 18;
                            int pos_value = start_value;
                            OleDbCommand Cmd1 = new OleDbCommand();

                            while (i < content.Length)
                            {
                                int load_value = pos_value - start_value;
                                int value_row = pos_value / 10;
                                int value_colum = pos_value % 10;
                                string update = "update " + memory_string + " set " + Convert.ToString(value_colum * 2) + "='" + Convert.ToString(content[i], 16) + "'," + Convert.ToString(value_colum * 2 + 1) + "='" + Convert.ToString(content[i + 1], 16) + "' where " + memory_string + "=" + (value_row * 10);
                                pos_value++;
                                i = i + 2;
                                Cmd1 = new OleDbCommand(update, conn);
                                Cmd1.ExecuteNonQuery();

                            }
                            Array.Copy(PLC_content, PLC_send, 13);
                            Global.socket.SendTo(PLC_send, Remote);
                    #endregion
                        }
                        conn.Close();
                    #endregion
                    }
#endif
                }
                catch { ;}
            }
        }
#else
        public static void PLC_Receive()
        {
            while (true)
            {
                IPAddress ipaddress = IPAddress.Parse("192.168.250.120");
                TcpListener tcplistener = new TcpListener(ipaddress, 9600);
                tcplistener.Start();
                Socket tcpsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                tcpsocket= tcplistener.AcceptSocket();
                
                if (check == true)
                {
                     IPEndPoint sender = new IPEndPoint(IPAddress.Any, 5555);
                     EndPoint Remote = (EndPoint)(sender);
                    byte[] channel_check = new byte[30];

                    int recv_channel_check = tcpsocket.ReceiveFrom(channel_check,ref Remote);




                    #region 發出訊息端IP位置
                    string IP_add = Convert.ToString(tcpsocket.RemoteEndPoint);
                    int a = IP_add.LastIndexOf(".");
                    int b = IP_add.LastIndexOf(":");
                    string IPs = "";
                    for (int i = a + 1; i < b; i++)
                    {
                        IPs += IP_add[i];
                    }
                    #endregion
                    #region 取得本機IP
                    string local_IP_add = Convert.ToString(tcpsocket.LocalEndPoint);
                    int aa = IP_add.LastIndexOf(".");
                    int bb = IP_add.LastIndexOf(":");
                    string local_IPs = "";
                    for (int i = a + 1; i < b; i++)
                    {
                        local_IPs += IP_add[i];
                    }
                    #endregion

                    byte[] channel_contemt = new byte[24];
                    channel_contemt[0] = (byte)'F';
                    channel_contemt[1] = (byte)'I';
                    channel_contemt[2] = (byte)'N';
                    channel_contemt[3] = (byte)'S';
                    channel_contemt[7] = 0x10;
                    channel_contemt[11] = 0x01;
                    channel_contemt[19] = 0x00;
                    channel_contemt[23] = Convert.ToByte(Convert.ToInt32(local_IPs));
                    tcpsocket.Send(channel_contemt, 24, 0);
                }
                    while (true)
                    {
                        check = false;
                        try
                        {

                            if (true)
                            {

                                byte[] channel_contemt = new byte[24];
                                byte[] check_content = new byte[16];
                                int recv = tcpsocket.Receive(check_content);//接收訊息

                                
                                channel_contemt[0] = (byte)'F';
                                channel_contemt[1] = (byte)'I';
                                channel_contemt[2] = (byte)'N';
                                channel_contemt[3] = (byte)'S';
                                channel_contemt[7] = 0x10;
                                channel_contemt[11] = 0x01;
                                channel_contemt[19] = 0x78;
                                channel_contemt[23] = 0x78;
                                tcpsocket.Send(channel_contemt, 24, 0);
                                if (
                                    true
                                    )
                                {

                                    byte[] content = new byte[2040];
                                    int recv2 = tcpsocket.Receive(content);//接收訊息

                                    int con = recv2;
                                    byte fins_cmnd_0 = 0xc0;/*ICF*/
                                    byte fins_cmnd_1 = 0x00;/*RSV*/
                                    byte fins_cmnd_2 = 0x02;/*GCT*/
                                    byte fins_cmnd_3 = 0x00;/*DNA*/
                                    byte fins_cmnd_5 = 0xb1;/*DAT2*/
                                    byte fins_cmnd_6 = 0x00;/*SNA*/
                                    byte[] PLC_content = new byte[30];
                                    PLC_content[0] = (byte)'F';
                                    PLC_content[1] = (byte)'I';
                                    PLC_content[2] = (byte)'N';
                                    PLC_content[3] = (byte)'S';
                                    #region 發出訊息端IP位置
                                    string IP_add = Convert.ToString(tcpsocket.RemoteEndPoint);
                                    int a= IP_add.LastIndexOf(".");
                                    int b = IP_add.LastIndexOf(":");
                                    string IPs = "";
                                    for (int i = a + 1; i < b; i++)
                                    {
                                        IPs += IP_add[i];
                                    }
                                    #endregion
                                    PLC_content[6] = 0x00;
                                    PLC_content[7] = 0x00;
                                    PLC_content[11] = 0x02;
                                    PLC_content[16] = fins_cmnd_0;
                                    PLC_content[17] = fins_cmnd_1;
                                    PLC_content[18] = fins_cmnd_2;
                                    PLC_content[19] = fins_cmnd_3;
                                    PLC_content[20] = Convert.ToByte(Convert.ToInt32(IPs));
                                    PLC_content[21] = fins_cmnd_5;
                                    PLC_content[22] = fins_cmnd_6;
                                    PLC_content[23] = content[4];
                                    PLC_content[24] = 0x00;
                                    PLC_content[25] = content[9];

                                    if (content[10] == 0x01)
                                    {
                                        #region 內部陣列
                                        PLC_content[26] = content[10];
                                        if (content[11] == 0x01)
                                        {
                                            #region 讀
                                            PLC_content[27] = content[11];
                                            PLC_content[28] = 0x00; ;
                                            PLC_content[29] = 0x00;

                                            int start_value = content[13] * 256 + content[14];
                                            int value_row = start_value / 10;
                                            int value_colum = start_value % 10;
                                            int k = 0;
                                            int i = value_row;
                                            int j = value_colum * 2 + 1;
                                            int value = content[16] * 256 + content[17];
                                            byte[] PLC_search_send = new byte[value * 2];
                                            byte[] PLC_send = new byte[value * 2 + 30];
                                            switch (content[12])  //選擇要使用的記憶體
                                            {
                                                case (int)memory.DM:
                                                    for (int X = 0; X < value * 2; X++)
                                                    {
                                                        PLC_search_send[X] = Convert.ToByte(memoryDM[start_value * 2 + X]);
                                                    }
                                                    break;
                                                case (int)memory.CIO:
                                                    for (int X = 0; X < value * 2; X++)
                                                    {
                                                        PLC_search_send[X] = Convert.ToByte(memoryCIO[start_value * 2 + X]);
                                                    }
                                                    break;
                                                case (int)memory.AR:
                                                    for (int X = 0; X < value * 2; X++)
                                                    {
                                                        PLC_search_send[X] = Convert.ToByte(memoryAR[start_value * 2 + k]);
                                                    }
                                                    break;
                                                case (int)memory.HR:
                                                    for (int X = 0; X < value * 2; X++)
                                                    {
                                                        PLC_search_send[X] = Convert.ToByte(memoryHR[start_value * 2 + X]);
                                                    }
                                                    break;
                                                case (int)memory.E0:
                                                    for (int X = 0; X < value * 2; X++)
                                                    {
                                                        PLC_search_send[X] = Convert.ToByte(memoryE0[start_value * 2 + X]);
                                                    }

                                                    break;
                                            }

                                            int content_value = start_value * 2;
                                            Array.Copy(PLC_content, PLC_send, 30);
                                            Array.Copy(PLC_search_send, 0, PLC_send, 30, PLC_search_send.Length);
                                            int delay = 13;
                                            if (value <= 101)
                                            { delay = 14; }
                                            else if (value <= 434 && value > 101)
                                            { delay = 15; }
                                            else if (value <= 767 && value > 434)
                                            { delay = 16; }
                                            else if (value <= 1000 && value > 767)
                                            { delay = 17; }
                                            Thread.Sleep(delay);
                                            second_set_time = System.DateTime.Now;
                                            second_set_time_mill = System.DateTime.Now.Millisecond;
                                            tcpsocket.Send(PLC_send, PLC_send.Length, 0);
                                            #endregion
                                        }
                                        else if (content[11] == 0x02)
                                        {
                                            #region 寫入
                                            PLC_content[27] = content[11];
                                            PLC_content[28] = 0x00;
                                            PLC_content[29] = 0x00;
                                            byte[] PLC_send = new byte[14];
                                            int i = 18;
                                            int k = 0;
                                            int start_value = content[13] * 256 + content[14];
                                            int value_sum = recv - 18;
                                            int pos_value = start_value;
                                            switch (content[12])      //選擇要使用的記憶體
                                            {
                                                case (int)memory.DM:
                                                    while (i < recv)
                                                    {
                                                        memoryDM[start_value * 2 + k] = content[i];
                                                        memoryDM[start_value * 2 + (k + 1)] = content[(i + 1)];
                                                        i = i + 2;
                                                        k = k + 2;
                                                    }
                                                    break;
                                                case (int)memory.CIO:
                                                    while (i < recv)
                                                    {
                                                        memoryDM[start_value * 2 + k] = content[i];
                                                        memoryDM[start_value * 2 + (k + 1)] = content[(i + 1)];
                                                        i = i + 2;
                                                        k = k + 2;
                                                    }

                                                    break;
                                                case (int)memory.AR:
                                                    while (i < recv)
                                                    {
                                                        memoryAR[start_value * 2 + k] = content[i];
                                                        memoryAR[start_value * 2 + (k + 1)] = content[(i + 1)];
                                                        i = i + 2;
                                                        k = k + 2;
                                                    }
                                                    break;
                                                case (int)memory.HR:
                                                    while (i < recv)
                                                    {
                                                        memoryHR[start_value * 2 + k] = content[i];
                                                        memoryHR[start_value * 2 + (k + 1)] = content[(i + 1)];
                                                        i = i + 2;
                                                        k = k + 2;
                                                    }
                                                    break;
                                                case (int)memory.E0:
                                                    while (i < recv)
                                                    {
                                                        memoryE0[start_value * 2 + k] = content[i];
                                                        i = i + 1;
                                                        if (i >= content.Length) { break; }
                                                        memoryE0[start_value * 2 + (k + 1)] = content[i];
                                                        i = i + 1;
                                                        k = k + 2;
                                                    }

                                                    break;
                                            }
                                            int delay = 13;
                                            if (recv <= 101)
                                            { delay = 14; }
                                            else if (recv <= 434 && recv > 101)
                                            { delay = 15; }
                                            else if (recv <= 767 && recv > 434)
                                            { delay = 16; }
                                            else if (recv <= 1000 && recv > 767)
                                            { delay = 17; }

                                            Thread.Sleep(delay);
                                            Array.Copy(PLC_content, PLC_send, 13);
                                            tcpsocket.Send(PLC_send, PLC_send.Length, 0);
                                            #endregion
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }
                        catch
                        {
                            tcpsocket.Shutdown(SocketShutdown.Both);
                            tcpsocket.Close();
                            tcpsocket = null;
                            tcplistener.Stop();
                            tcplistener = null;

                            break;
                        }
                    }
                
            }
        }
#endif
    }
}
