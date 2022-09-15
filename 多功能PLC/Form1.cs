#define windows_min
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
    
    public partial class Form1 : Form
    {
        Thread thread;
        PLC_InterSetting newPLC = new PLC_InterSetting();
        int tim = 0;
        int memory_num(string memory_string) //記憶體byte
        {
            int num=0;
            if (memory_string == "DM")
                num = 130;
            else if (memory_string == "E0")
                num = 160;
            else if (memory_string == "AR")
                num = 178;
            else if (memory_string == "HR")
                num = 177;
            else if (memory_string == "CIO")
                num = 176;
            else
                num = 0;
            return num;
        }
        enum memory_a : byte
        {
            CIO = 176,
            HR = 177,
            AR = 178,
            DM = 130,
            E0 = 160,
            other = 0
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {            
            Global.IPep.Port = 9600;
            thread = new Thread(new ThreadStart(PLC_InterSetting.PLC_Receive));
            thread.IsBackground = true;
            thread.Start();
            Thread.Sleep(1000);
            PLCport.Text = Global.PLC_message;
            label1.Text = Global.PLC_Random_message;
            this.Text = Convert.ToString(Global.IPep.Port);
            new_setting();
                
        }    
        #region 設定port
        private void setting_PLC_click(object sender, EventArgs e) 
        {
            Global.PLC_Random_message = "";
            Global.setPLC = true;
            newPLC = new PLC_InterSetting();
            thread.Abort();
            thread = null;
            Global.socket.Shutdown(SocketShutdown.Both);
            Global.socket.Close();
            thread = new Thread(new ThreadStart(PLC_InterSetting.PLC_Receive));
            Global.IPep.Port =Convert.ToInt32( PLC_port.Value);
            thread.IsBackground = true;
            thread.Start();
            Thread.Sleep(1000);
            PLCport.Text = Global.PLC_message;
            label1.Text = Global.PLC_Random_message;
            this.Text = Convert.ToString(Global.IPep.Port);
           new_setting();
      
        }
        #endregion
        private void inter_Click(object sender, EventArgs e)  //內部設定
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
        }
        private void address_value_Click(object sender, EventArgs e)  //值等於位址
        {
            byte memory = Convert.ToByte(memory_num(memoryc.Text));
            int starts_address = (int)address_start.Value;
            int sum_address = (int)address_sum.Value;        
            setting_memory(memory, starts_address, (int)add_user.Value, sum_address);        
        }
        private void setting_memory(byte memory ,int address,int add,int sum)//副程式
        {
            int a = address+add;
            int j, f, g, h, l, k, m;
            switch ((int)memory)
            {
                case (int)memory_a.DM:
                    for (int i = 0; i < sum; i++)
                    {
                        while (a > 10000)
                        {
                            a = a - 10000;
                        }
                        j = a;
                        f = j / 1000;
                        g = (j % 1000) / 100;
                        h = (j % 100) / 10;
                        j = j % 10;
                        k = h * 16;
                        l = g;
                        m = f * 16;
                        PLC_InterSetting.memoryDM[address * 2] = Convert.ToByte(m + l);
                        PLC_InterSetting.memoryDM[address * 2 + 1] = Convert.ToByte(k + j);
                        a++;
                        address++;
                    }
                    break;
                case (int)memory_a.CIO:
                     for (int i = 0; i < sum; i++)
                    {
                    while (a > 10000)
                    {
                        a = a - 10000;
                    }
                    j = a;
                    f = j / 1000;
                    g = (j % 1000) / 100;
                    h = (j % 100) / 10;
                    j = j % 10;
                    k = h * 16;
                    l = g;
                    m = f * 16;
                    PLC_InterSetting.memoryCIO[address * 2] = Convert.ToByte(m + l);
                    PLC_InterSetting.memoryCIO[address * 2 + 1] = Convert.ToByte(k + j);
                    a++;
                    address++;

                    }
                    break;
                case (int)memory_a.AR:
                    for (int i = 0; i < sum; i++)
                    {
                    while (a > 10000)
                    {
                        a = a - 10000;
                    }
                    j = a;
                    f = j / 1000;
                    g = (j % 1000) / 100;
                    h = (j % 100) / 10;
                    j = j % 10;
                    k = h * 16;
                    l = g;
                    m = f * 16;
                    PLC_InterSetting.memoryAR[address * 2] = Convert.ToByte(m + l);
                    PLC_InterSetting.memoryAR[address * 2 + 1] = Convert.ToByte(k + j);
                    address++;
                    a++;
                    }
                    break;
                case (int)memory_a.E0:
                for (int i = 0; i < sum; i++)
                    {
                    while (a > 10000)
                    {
                        a = a - 10000;
                    }
                    j = a;
                    f = j / 1000;
                    g = (j % 1000) / 100;
                    h = (j % 100) / 10;
                    j = j % 10;
                    k = h * 16;
                    l = g;
                    m = f * 16;
                    PLC_InterSetting.memoryE0[address * 2] = Convert.ToByte(m + l);
                    PLC_InterSetting.memoryE0[address * 2 + 1] = Convert.ToByte(k + j);
                    address++;
                    a++;
                    }
                    break;
                case (int)memory_a.HR:
                   for (int i = 0; i < sum; i++)
                    {
                    while (a > 10000)
                    {
                        a = a - 10000;
                    }
                    j = a;
                    f = j / 1000;
                    g = (j % 1000) / 100;
                    h = (j % 100) / 10;
                    j = j % 10;
                    k = h * 16;
                    l = g;
                    m = f * 16;
                    PLC_InterSetting.memoryHR[address * 2] = Convert.ToByte(m + l);
                    PLC_InterSetting.memoryHR[address * 2 + 1] = Convert.ToByte(k + j);
                    address++;
                       a++;
                    }
                    break;
                default:
                    timer2.Enabled = false;
                    MessageBox.Show("記憶體輸入錯誤");
                    break;
            }
        }  
        private void timer2_Tick(object sender, EventArgs e)   //TIMER2
        {
            byte memory = Convert.ToByte(memory_num(memoryc.Text));            
            int starts_address = (int)address_start.Value;
            int sum_address = (int)address_sum.Value;           
            setting_memory(memory, starts_address, tim, sum_address);          
            tim++;
        }
        private void timeaddvalue_Click(object sender, EventArgs e)//隨時間跑值按鈕
        {
            int time_sec = (int)add_time.Value * 1000;
            timer2.Interval = time_sec;
            tim = 0;
            timer2.Enabled = true;            
        }
        private void write_string_Click(object sender, EventArgs e)//寫入字串按鈕
        {
            byte memory = Convert.ToByte(memory_num(memoryc.Text));
            string str = textBox1.Text;
            int starts_address = (int)address_start.Value;
            switch ((int)memory)
            {
                case (int)memory_a.DM:
                    for (int i = 0; i < str.Length; i++)
                    {
                        PLC_InterSetting.memoryDM[starts_address * 2 + i] = str[i];
                    }
                    break;
                case (int)memory_a.CIO:
                    for (int i = 0; i < str.Length; i++)
                    {
                        PLC_InterSetting.memoryCIO[starts_address * 2 + i] = str[i];
                    }
                    break;
                case (int)memory_a.E0:
                    for (int i = 0; i < str.Length; i++)
                    {
                        PLC_InterSetting.memoryE0[starts_address * 2 + i] = str[i];
                    }
                    break;
                case (int)memory_a.AR:
                    for (int i = 0; i < str.Length; i++)
                    {
                        PLC_InterSetting.memoryAR[starts_address * 2 + i] = str[i];
                    }
                    break;
                case (int)memory_a.HR:
                    for (int i = 0; i < str.Length; i++)
                    {
                        PLC_InterSetting.memoryHR[starts_address * 2 + i] = str[i];
                    }
                    break;
                default:
                    MessageBox.Show("記憶體輸入錯誤");
                    break;
            }            
        }
        private void write_num_Click(object sender, EventArgs e)//寫入數字按鈕
        {
            byte memory = Convert.ToByte(memory_num(memoryc.Text));
            int starts_address = (int)address_start.Value;
            int num = (int)write_value.Value;
            if (num<65536)
            {
                switch ((int)memory)
                {
                    case (int)memory_a.DM:
                        PLC_InterSetting.memoryDM[starts_address * 2] = (byte)Convert.ToInt16(num/ 256);
                        PLC_InterSetting.memoryDM[starts_address * 2 + 1] = (byte)Convert.ToInt16(num % 256);
                        break;
                    case (int)memory_a.CIO:
                        PLC_InterSetting.memoryCIO[starts_address * 2] = (byte)Convert.ToInt16(num / 256);
                        PLC_InterSetting.memoryCIO[starts_address * 2 + 1] = (byte)Convert.ToInt16(num % 256);
                        break;
                    case (int)memory_a.E0:
                        PLC_InterSetting.memoryE0[starts_address * 2] = (byte)Convert.ToInt16(num / 256);
                        PLC_InterSetting.memoryE0[starts_address * 2 + 1] = (byte)Convert.ToInt16(num % 256);
                        break;
                    case (int)memory_a.AR:
                        PLC_InterSetting.memoryAR[starts_address * 2] = (byte)Convert.ToInt16(num / 256);
                        PLC_InterSetting.memoryAR[starts_address * 2 + 1] = (byte)Convert.ToInt16(num % 256);
                        break;
                    case (int)memory_a.HR:
                        PLC_InterSetting.memoryHR[starts_address * 2] = (byte)Convert.ToInt16(num / 256);
                        PLC_InterSetting.memoryHR[starts_address * 2 + 1] = (byte)Convert.ToInt16(num % 256);
                        break;
                    default:
                        MessageBox.Show("記憶體輸入錯誤");
                        break;
                }
            }
            else
            {
               int d = num / 16777216;
                    int f = num % 16777216;
                    int g = f / 65536;
                    int h = f % 65536;
                    switch ((int)memory)
                    {
                        case (int)memory_a.DM:
                            PLC_InterSetting.memoryDM[starts_address * 2] = (byte)Convert.ToInt16(h / 256);
                            PLC_InterSetting.memoryDM[starts_address * 2 + 1] = (byte)Convert.ToInt16(h % 256);
                            PLC_InterSetting.memoryDM[starts_address * 2 + 2] = (byte)Convert.ToInt16(d);
                            PLC_InterSetting.memoryDM[starts_address * 2 + 3] = (byte)Convert.ToInt16(g);
                            break;
                        case (int)memory_a.CIO:
                            PLC_InterSetting.memoryCIO[starts_address * 2] = (byte)Convert.ToInt16(h / 256);
                            PLC_InterSetting.memoryCIO[starts_address * 2 + 1] = (byte)Convert.ToInt16(h % 256);
                            PLC_InterSetting.memoryCIO[starts_address * 2 + 2] = (byte)Convert.ToInt16(d);
                            PLC_InterSetting.memoryCIO[starts_address * 2 + 3] = (byte)Convert.ToInt16(g);
                            break;
                        case (int)memory_a.E0:

                            PLC_InterSetting.memoryE0[starts_address * 2] = (byte)Convert.ToInt16(h / 256);
                            PLC_InterSetting.memoryE0[starts_address * 2 + 1] = (byte)Convert.ToInt16(h % 256);
                            PLC_InterSetting.memoryE0[starts_address * 2 + 2] = (byte)Convert.ToInt16(d);
                            PLC_InterSetting.memoryE0[starts_address * 2 + 3] = (byte)Convert.ToInt16(g);
                            break;
                        case (int)memory_a.AR:
                            PLC_InterSetting.memoryAR[starts_address * 2] = (byte)Convert.ToInt16(h / 256);
                            PLC_InterSetting.memoryAR[starts_address * 2 + 1] = (byte)Convert.ToInt16(h % 256);
                            PLC_InterSetting.memoryAR[starts_address * 2 + 2] = (byte)Convert.ToInt16(d);
                            PLC_InterSetting.memoryAR[starts_address * 2 + 3] = (byte)Convert.ToInt16(g);
                            break;
                        case (int)memory_a.HR:
                            PLC_InterSetting.memoryHR[starts_address * 2] = (byte)Convert.ToInt16(h / 256);
                            PLC_InterSetting.memoryHR[starts_address * 2 + 1] = (byte)Convert.ToInt16(h % 256);
                            PLC_InterSetting.memoryHR[starts_address * 2 + 2] = (byte)Convert.ToInt16(d);
                            PLC_InterSetting.memoryHR[starts_address * 2 + 3] = (byte)Convert.ToInt16(g);
                            break;
                        default:
                            MessageBox.Show("記憶體輸入錯誤");
                            break;
                    }
                    }
        }
        private void clear_Click(object sender, EventArgs e)//清空按鈕
        {
            byte memory = Convert.ToByte(memory_num(memoryc.Text));
            int starts_address = (int)address_start.Value;
              int sum_address = (int)address_sum.Value;
               switch ((int)memory)
            {
                case (int)memory_a.DM:
                    for (int i = 0; i < sum_address; i++)
                    {
                         PLC_InterSetting.memoryDM[starts_address * 2 + (2*i)] = 0;
                         PLC_InterSetting.memoryDM[starts_address * 2 + (2*i+1)] = 0;

                    }
                    break;
                case (int)memory_a.CIO:
                    for (int i = 0; i < sum_address; i++)
                    {
                         PLC_InterSetting.memoryCIO[starts_address * 2 + (2*i)] = 0;
                         PLC_InterSetting.memoryCIO[starts_address * 2 + (2*i+1)] = 0;
                    }
                    break;
                case (int)memory_a.E0:
                    for (int i = 0; i < sum_address; i++)
                    {
                         PLC_InterSetting.memoryE0[starts_address * 2 + (2*i)] = 0;
                         PLC_InterSetting.memoryE0[starts_address * 2 + (2*i+1)] = 0;
                    }
                    break;
                case (int)memory_a.AR:
                    for (int i = 0; i < sum_address; i++)
                    {
                         PLC_InterSetting.memoryAR[starts_address * 2 + (2*i)] = 0;
                         PLC_InterSetting.memoryAR[starts_address * 2 + (2*i+1)] = 0;
                    }
                    break;
                case (int)memory_a.HR:
                    for (int i = 0; i < sum_address; i++)
                    {
                         PLC_InterSetting.memoryHR[starts_address * 2 + (2*i)] = 0;
                         PLC_InterSetting.memoryHR[starts_address * 2 + (2*i+1)] = 0;
                    }
                    break;
                default:
                    MessageBox.Show("記憶體輸入錯誤");
                    break;
               }           
        }
        
       private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.notifyIcon1.Visible = false;
            this.Show();
            this.WindowState = FormWindowState.Normal;//Normal=0;預設大小的視窗。
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Text = Convert.ToString(Global.IPep.Port);
                this.notifyIcon1.Visible = true;
                this.Hide();
            }
        }
        private void time_close_Click(object sender, EventArgs e)//關閉時間按鈕
        {
            timer2.Enabled = false;
        }        
        private void new_setting()//初始化設定
        {
            PLC_InterSetting.memoryE0[30000] = 77;
            PLC_InterSetting.memoryE0[30001] = 117;
            PLC_InterSetting.memoryE0[30002] = 108;
            PLC_InterSetting.memoryE0[30003] = 116;
            PLC_InterSetting.memoryE0[30004] = 105;
            PLC_InterSetting.memoryE0[30005] = 112;
            PLC_InterSetting.memoryE0[30006] = 108;
            PLC_InterSetting.memoryE0[30007] = 97;
            PLC_InterSetting.memoryE0[30008] = 83;
            PLC_InterSetting.memoryE0[30020] = 50;
            PLC_InterSetting.memoryE0[30021] = 48;
            PLC_InterSetting.memoryE0[30022] = 49;
            PLC_InterSetting.memoryE0[30023] = 52;
            string port_str = Convert.ToString(Global.IPep.Port);
            int EID = 30024;
            for (int j = 0; j < port_str.Length; j++)
            {
                PLC_InterSetting.memoryE0[EID] =port_str[j];
                EID++;
            }            
            
        }

        private void memory_textChanged(object sender, EventArgs e)
        {
            address_start.Maximum = 32767;
            address_start.Minimum = 0;
            address_sum.Maximum=1000;
            if (memoryc.Text == "DM")
                address_start.Maximum = 32767;
            else if (memoryc.Text == "E0")
                address_start.Maximum = 32767;
            else if (memoryc.Text == "AR")
            {
                address_start.Minimum = 448;
                address_start.Maximum = 959;
            }
            else if (memoryc.Text == "HR")
                address_start.Maximum = 511;
            else if (memoryc.Text == "CIO")
                address_start.Maximum = 6143;
            if (address_start.Maximum - address_start.Value < 1000)
                address_sum.Maximum = address_start.Maximum - address_start.Value + 1;
        }

        private void memory_content_Click(object sender, EventArgs e)
        {
            Global.start = (int)address_start.Value;
            Global.Number =(int)address_sum.Value;
            Global.memory_name=memoryc.Text;
            memory frm = new memory();
            frm.Show();
        }

        private void address_sum_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void address_start_ValueChanged(object sender, EventArgs e)
        {
            if (address_start.Maximum - address_start.Value < 1000)
                address_sum.Maximum = address_start.Maximum - address_start.Value + 1;
        }      
    }
}
