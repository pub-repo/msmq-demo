using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Messaging;
using System.Threading;

namespace MyQueue
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtMsg;
		private System.Windows.Forms.Button btnMsg;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox MsgBox;
		private System.Windows.Forms.Label Messages;
		private System.Windows.Forms.Button btnRcv;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public System.Messaging.MessageQueue mq;
		public static Int32 j=0;

		public Form1()
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

			Queue2 q2 = new Queue2();
			q2.Show();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.btnMsg = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MsgBox = new System.Windows.Forms.ListBox();
            this.Messages = new System.Windows.Forms.Label();
            this.btnRcv = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(179, 23);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(448, 26);
            this.txtMsg.TabIndex = 0;
            // 
            // btnMsg
            // 
            this.btnMsg.Location = new System.Drawing.Point(320, 70);
            this.btnMsg.Name = "btnMsg";
            this.btnMsg.Size = new System.Drawing.Size(120, 34);
            this.btnMsg.TabIndex = 1;
            this.btnMsg.Text = "&Send";
            this.btnMsg.Click += new System.EventHandler(this.btnMsg_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 34);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter Message :";
            // 
            // MsgBox
            // 
            this.MsgBox.ItemHeight = 20;
            this.MsgBox.Location = new System.Drawing.Point(13, 199);
            this.MsgBox.Name = "MsgBox";
            this.MsgBox.ScrollAlwaysVisible = true;
            this.MsgBox.Size = new System.Drawing.Size(627, 224);
            this.MsgBox.TabIndex = 2;
            // 
            // Messages
            // 
            this.Messages.Location = new System.Drawing.Point(26, 152);
            this.Messages.Name = "Messages";
            this.Messages.Size = new System.Drawing.Size(256, 34);
            this.Messages.TabIndex = 3;
            this.Messages.Text = "Messages : ";
            // 
            // btnRcv
            // 
            this.btnRcv.Location = new System.Drawing.Point(320, 129);
            this.btnRcv.Name = "btnRcv";
            this.btnRcv.Size = new System.Drawing.Size(120, 33);
            this.btnRcv.TabIndex = 5;
            this.btnRcv.Text = "&Receive";
            this.btnRcv.Click += new System.EventHandler(this.btnRcv_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(656, 445);
            this.Controls.Add(this.btnRcv);
            this.Controls.Add(this.Messages);
            this.Controls.Add(this.MsgBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMsg);
            this.Controls.Add(this.txtMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Queue1";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void btnMsg_Click(object sender, System.EventArgs e)
		{
//			SendMessage(Handle, 1, 0, IntPtr.Zero);
			System.Messaging.Message mm = new System.Messaging.Message();
			mm.Body = txtMsg.Text;
			mm.Label = "Msg" + j.ToString();
			j++;
			mq.Send(mm);
		}

		private void btnRcv_Click(object sender, System.EventArgs e)
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
			MsgBox.Items.Add(m.ToString());
		}
	}
}
