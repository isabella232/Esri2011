namespace EsriDE.Trials.CastleWindsor.LifestyleBehaviour.Implementations
{
	partial class FormView
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				System.Console.WriteLine("FormView.Dispose(true)");
			}
			else
			{
				System.Console.WriteLine("FormView.Dispose(false)");
			}

			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// FormView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Name = "FormView";
			this.Text = "FormView";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SomeForm_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SomeForm_FormClosed);
			this.ResumeLayout(false);

		}

		#endregion
	}
}