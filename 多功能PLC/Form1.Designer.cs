namespace PLC
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.inter = new System.Windows.Forms.Button();
            this.address_value = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.memoryc = new System.Windows.Forms.TextBox();
            this.PLCport = new System.Windows.Forms.Label();
            this.PLC_port = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.memory_content = new System.Windows.Forms.Button();
            this.time_close = new System.Windows.Forms.Button();
            this.write_value = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.add_time = new System.Windows.Forms.NumericUpDown();
            this.add_user = new System.Windows.Forms.NumericUpDown();
            this.address_sum = new System.Windows.Forms.NumericUpDown();
            this.address_start = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.write_num = new System.Windows.Forms.Button();
            this.write_string = new System.Windows.Forms.Button();
            this.timeaddvalue = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PLC_port)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.write_value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.add_time)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.add_user)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.address_sum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.address_start)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(132, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "設定PLC port";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.setting_PLC_click);
            // 
            // inter
            // 
            this.inter.Location = new System.Drawing.Point(140, 85);
            this.inter.Name = "inter";
            this.inter.Size = new System.Drawing.Size(86, 23);
            this.inter.TabIndex = 2;
            this.inter.Text = "PLC內部設定";
            this.inter.UseVisualStyleBackColor = true;
            this.inter.Click += new System.EventHandler(this.inter_Click);
            // 
            // address_value
            // 
            this.address_value.Location = new System.Drawing.Point(7, 120);
            this.address_value.Name = "address_value";
            this.address_value.Size = new System.Drawing.Size(92, 23);
            this.address_value.TabIndex = 3;
            this.address_value.Text = "位址等於值";
            this.address_value.UseVisualStyleBackColor = true;
            this.address_value.Click += new System.EventHandler(this.address_value_Click);
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(6, 237);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(75, 23);
            this.clear.TabIndex = 4;
            this.clear.Text = "清空";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // memoryc
            // 
            this.memoryc.Location = new System.Drawing.Point(110, 11);
            this.memoryc.Name = "memoryc";
            this.memoryc.Size = new System.Drawing.Size(59, 22);
            this.memoryc.TabIndex = 5;
            this.memoryc.Text = "DM";
            this.memoryc.TextChanged += new System.EventHandler(this.memory_textChanged);
            // 
            // PLCport
            // 
            this.PLCport.AutoSize = true;
            this.PLCport.Location = new System.Drawing.Point(222, 39);
            this.PLCport.Name = "PLCport";
            this.PLCport.Size = new System.Drawing.Size(65, 12);
            this.PLCport.TabIndex = 8;
            this.PLCport.Text = "9600已開啟";
            // 
            // PLC_port
            // 
            this.PLC_port.Location = new System.Drawing.Point(6, 22);
            this.PLC_port.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.PLC_port.Name = "PLC_port";
            this.PLC_port.Size = new System.Drawing.Size(120, 22);
            this.PLC_port.TabIndex = 9;
            this.PLC_port.Value = new decimal(new int[] {
            9600,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(222, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.memory_content);
            this.groupBox1.Controls.Add(this.time_close);
            this.groupBox1.Controls.Add(this.write_value);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.add_time);
            this.groupBox1.Controls.Add(this.add_user);
            this.groupBox1.Controls.Add(this.address_sum);
            this.groupBox1.Controls.Add(this.address_start);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.write_num);
            this.groupBox1.Controls.Add(this.write_string);
            this.groupBox1.Controls.Add(this.timeaddvalue);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.memoryc);
            this.groupBox1.Controls.Add(this.address_value);
            this.groupBox1.Controls.Add(this.clear);
            this.groupBox1.Location = new System.Drawing.Point(68, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 270);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "內部設定";
            this.groupBox1.Visible = false;
            // 
            // memory_content
            // 
            this.memory_content.Location = new System.Drawing.Point(87, 91);
            this.memory_content.Name = "memory_content";
            this.memory_content.Size = new System.Drawing.Size(75, 23);
            this.memory_content.TabIndex = 15;
            this.memory_content.Text = "記憶體內容";
            this.memory_content.UseVisualStyleBackColor = true;
            this.memory_content.Click += new System.EventHandler(this.memory_content_Click);
            // 
            // time_close
            // 
            this.time_close.Location = new System.Drawing.Point(215, 146);
            this.time_close.Name = "time_close";
            this.time_close.Size = new System.Drawing.Size(52, 23);
            this.time_close.TabIndex = 24;
            this.time_close.Text = "關";
            this.time_close.UseVisualStyleBackColor = true;
            this.time_close.Click += new System.EventHandler(this.time_close_Click);
            // 
            // write_value
            // 
            this.write_value.Location = new System.Drawing.Point(100, 208);
            this.write_value.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.write_value.Name = "write_value";
            this.write_value.Size = new System.Drawing.Size(115, 22);
            this.write_value.TabIndex = 23;
            this.write_value.ThousandsSeparator = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(190, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "sec";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(107, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "+";
            // 
            // add_time
            // 
            this.add_time.Location = new System.Drawing.Point(106, 149);
            this.add_time.Name = "add_time";
            this.add_time.Size = new System.Drawing.Size(63, 22);
            this.add_time.TabIndex = 19;
            this.add_time.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // add_user
            // 
            this.add_user.Location = new System.Drawing.Point(129, 121);
            this.add_user.Name = "add_user";
            this.add_user.Size = new System.Drawing.Size(100, 22);
            this.add_user.TabIndex = 18;
            // 
            // address_sum
            // 
            this.address_sum.Location = new System.Drawing.Point(108, 60);
            this.address_sum.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.address_sum.Name = "address_sum";
            this.address_sum.Size = new System.Drawing.Size(120, 22);
            this.address_sum.TabIndex = 16;
            this.address_sum.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.address_sum.ValueChanged += new System.EventHandler(this.address_sum_ValueChanged);
            // 
            // address_start
            // 
            this.address_start.Location = new System.Drawing.Point(109, 35);
            this.address_start.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.address_start.Name = "address_start";
            this.address_start.Size = new System.Drawing.Size(120, 22);
            this.address_start.TabIndex = 15;
            this.address_start.ValueChanged += new System.EventHandler(this.address_start_ValueChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(100, 180);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(115, 22);
            this.textBox1.TabIndex = 14;
            this.textBox1.Text = "MultiplaS";
            // 
            // write_num
            // 
            this.write_num.Location = new System.Drawing.Point(6, 208);
            this.write_num.Name = "write_num";
            this.write_num.Size = new System.Drawing.Size(75, 23);
            this.write_num.TabIndex = 13;
            this.write_num.Text = "寫入數字";
            this.write_num.UseVisualStyleBackColor = true;
            this.write_num.Click += new System.EventHandler(this.write_num_Click);
            // 
            // write_string
            // 
            this.write_string.Location = new System.Drawing.Point(6, 178);
            this.write_string.Name = "write_string";
            this.write_string.Size = new System.Drawing.Size(75, 23);
            this.write_string.TabIndex = 12;
            this.write_string.Text = "寫入字串";
            this.write_string.UseVisualStyleBackColor = true;
            this.write_string.Click += new System.EventHandler(this.write_string_Click);
            // 
            // timeaddvalue
            // 
            this.timeaddvalue.Location = new System.Drawing.Point(7, 149);
            this.timeaddvalue.Name = "timeaddvalue";
            this.timeaddvalue.Size = new System.Drawing.Size(75, 23);
            this.timeaddvalue.TabIndex = 11;
            this.timeaddvalue.Text = "值隨時間加";
            this.timeaddvalue.UseVisualStyleBackColor = true;
            this.timeaddvalue.Click += new System.EventHandler(this.timeaddvalue_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "多少個";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "位址啟始值";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "記憶體";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.PLC_port);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.PLCport);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(427, 67);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PLC連線設定";
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 386);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.inter);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.PLC_port)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.write_value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.add_time)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.add_user)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.address_sum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.address_start)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button inter;
        private System.Windows.Forms.Button address_value;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.TextBox memoryc;
        private System.Windows.Forms.Label PLCport;
        private System.Windows.Forms.NumericUpDown PLC_port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button write_num;
        private System.Windows.Forms.Button write_string;
        private System.Windows.Forms.Button timeaddvalue;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown address_sum;
        private System.Windows.Forms.NumericUpDown address_start;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown add_time;
        private System.Windows.Forms.NumericUpDown add_user;
        private System.Windows.Forms.NumericUpDown write_value;
        private System.Windows.Forms.Button time_close;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button memory_content;
    }
}

