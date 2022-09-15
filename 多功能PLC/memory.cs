using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace PLC
{
    public partial class memory : Form
    {
        int x = 0;
        int j= 0;
        int address = Global.start * 2;
        int address_ten = Global.start;
        int address_Number = Global.Number/10;
        int address_sum = Global.Number * 2;
        string memory_str = Global.memory_name;
        string mes_str_timer = "";
        string mes_str="";

        Thread thread;
        
        public memory()
        {
            
            InitializeComponent();
        }

        private void DM_Load(object sender, EventArgs e)
        {
            label1.Text = "                " + "+0" +
                          "            " + "+1" + 
                          "            " + "+2" + 
                          "            " + "+3" + 
                          "            " + "+4" + 
                          "            " + "+5" + 
                          "            " + "+6" +
                          "            " + "+7" + 
                          "            " + "+8" + 
                          "            " + "+9";

            this.Text = Global.IPep.Port+" "+ memory_str+" "+address/2+"~"+(address/2+address_sum/2-1);


            thread = new Thread(new ThreadStart(memeory));
            thread.Start();
          //  Thread.Sleep(20);
         //   richTextBox1.Text = mes_str;
        
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(memeory));
            thread.Start();
            Thread.Sleep(20);
            if (mes_str != mes_str_timer)
            {
                mes_str_timer = mes_str;
                richTextBox1.Text = mes_str_timer;
            }
        }
        public void memeory()
        {
            int sum = 0;
            mes_str = "";
            j = address;
            x = address_ten;

            for (int k = 0; k < address_Number+1; k++)
                {
                    if (sum*2 >= address_sum)
                        break;
                    mes_str += Convert.ToString(x) + "\t";
                    for (int i = 0; i < 20; i++)
                    {
                        if (sum*2 >= address_sum)
                            break;
                        if (memory_str == "DM")
                        {
                            if (Convert.ToString(PLC_InterSetting.memoryDM[j],16).Length == 1)
                                mes_str += "0";
                            mes_str += Convert.ToString(PLC_InterSetting.memoryDM[j], 16);
                            if (Convert.ToString(PLC_InterSetting.memoryDM[j + 1],16).Length == 1)
                                mes_str += "0";
                            mes_str += Convert.ToString(PLC_InterSetting.memoryDM[j + 1], 16);
                        }
                        else if (memory_str == "E0")
                        {
                            if (Convert.ToString(PLC_InterSetting.memoryE0[j],16).Length == 1)
                                mes_str += "0";
                            mes_str += Convert.ToString(PLC_InterSetting.memoryE0[j], 16);
                            if (Convert.ToString(PLC_InterSetting.memoryE0[j + 1],16).Length == 1)
                                mes_str += "0";
                            mes_str += Convert.ToString(PLC_InterSetting.memoryE0[j + 1], 16);
                        }
                        else if (memory_str == "CIO")
                        {
                            if (Convert.ToString(PLC_InterSetting.memoryCIO[j],16).Length == 1)
                                mes_str += "0";
                            mes_str += Convert.ToString(PLC_InterSetting.memoryCIO[j], 16);
                            if (Convert.ToString(PLC_InterSetting.memoryCIO[j + 1]).Length == 1)
                                mes_str += "0";
                            mes_str += Convert.ToString(PLC_InterSetting.memoryCIO[j + 1], 16);
                        }
                        else if (memory_str == "AR")
                        {
                            if (Convert.ToString(PLC_InterSetting.memoryAR[j],16).Length == 1)
                                mes_str += "0";
                            mes_str += Convert.ToString(PLC_InterSetting.memoryAR[j], 16);
                            if (Convert.ToString(PLC_InterSetting.memoryAR[j + 1],16).Length == 1)
                                mes_str += "0";
                            mes_str += Convert.ToString(PLC_InterSetting.memoryAR[j + 1], 16);
                        }
                        else if (memory_str == "HR")
                        {
                            if (Convert.ToString(PLC_InterSetting.memoryHR[j],16).Length == 1)
                                mes_str += "0";
                            mes_str += Convert.ToString(PLC_InterSetting.memoryHR[j], 16);
                            if (Convert.ToString(PLC_InterSetting.memoryHR[j + 1],16).Length == 1)
                                mes_str += "0";
                            mes_str += Convert.ToString(PLC_InterSetting.memoryHR[j + 1], 16);
                        }
                        mes_str += "\t";
                        sum++;
                        i++;
                        j = j + 2;
                    }
                    mes_str += "\n";
                    x = x + 10;
                  

                }
            
        }
    }
}
