using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Messaging;

namespace MyQueue
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class Queue2 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtMsg1;
		private System.Windows.Forms.Button btnMsg1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox MsgBox1;
		public System.Messaging.MessageQueue mq;
		public static Int32 i=0;
		private System.Windows.Forms.Button btnRcv1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Queue2()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			//Q Creation
			if(MessageQueue.Exists(@".\Private$\MyQueue"))
				mq = new System.Messaging.MessageQueue(@".\Private$\MyQueue");
			else
				mq = MessageQueue.Create(@".\Private$\MyQueue");

			
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.txtMsg1 = new System.Windows.Forms.TextBox();
            this.btnMsg1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.MsgBox1 = new System.Windows.Forms.ListBox();
            this.btnRcv1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 34);
            this.label1.TabIndex = 5;
            this.label1.Text = "Enter Message :";
            // 
            // txtMsg1
            // 
            this.txtMsg1.Location = new System.Drawing.Point(179, 23);
            this.txtMsg1.Name = "txtMsg1";
            this.txtMsg1.Size = new System.Drawing.Size(448, 26);
            this.txtMsg1.TabIndex = 0;
            // 
            // btnMsg1
            // 
            this.btnMsg1.Location = new System.Drawing.Point(320, 70);
            this.btnMsg1.Name = "btnMsg1";
            this.btnMsg1.Size = new System.Drawing.Size(120, 34);
            this.btnMsg1.TabIndex = 1;
            this.btnMsg1.Text = "&Send";
            this.btnMsg1.Click += new System.EventHandler(this.btnMsg1_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(26, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(256, 34);
            this.label2.TabIndex = 4;
            this.label2.Text = "Messages";
            // 
            // MsgBox1
            // 
            this.MsgBox1.ItemHeight = 20;
            this.MsgBox1.Location = new System.Drawing.Point(13, 199);
            this.MsgBox1.Name = "MsgBox1";
            this.MsgBox1.ScrollAlwaysVisible = true;
            this.MsgBox1.Size = new System.Drawing.Size(627, 224);
            this.MsgBox1.TabIndex = 2;
            // 
            // btnRcv1
            // 
            this.btnRcv1.Location = new System.Drawing.Point(320, 129);
            this.btnRcv1.Name = "btnRcv1";
            this.btnRcv1.Size = new System.Drawing.Size(120, 33);
            this.btnRcv1.TabIndex = 6;
            this.btnRcv1.Text = "&Receive";
            this.btnRcv1.Click += new System.EventHandler(this.btnRcv1_Click);
            // 
            // Queue2
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(652, 435);
            this.ControlBox = false;
            this.Controls.Add(this.btnRcv1);
            this.Controls.Add(this.MsgBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnMsg1);
            this.Controls.Add(this.txtMsg1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Queue2";
            this.Text = "Queue2";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnMsg1_Click(object sender, System.EventArgs e)
		{
//			SendMessage(Handle, 1, 0, IntPtr.Zero);
			System.Messaging.Message mm = new System.Messaging.Message();
			mm.Body = txtMsg1.Text;
			mm.Label = "MsgI" + i.ToString();
			i++;
			mq.Send(mm);
		}

		private void btnRcv1_Click(object sender, System.EventArgs e)
		{
			System.Messaging.Message mes;
			string m;

			try
			{
				mes = mq.Receive(new TimeSpan(0, 0, 3));
				mes.Formatter = new XmlMessageFormatter(new String[] {"System.String,mscorlib"});
				m = mes.Body.ToString();
			}
			catch
			{
				m = "No Message";
			}
			MsgBox1.Items.Add(m.ToString());
		}
	}
}
