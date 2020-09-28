namespace PuntosDominantesE
{
    partial class Form_BPs_Detection
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
            this.PB = new System.Windows.Forms.PictureBox();
            this.labelr = new System.Windows.Forms.Label();
            this.labelISE = new System.Windows.Forms.Label();
            this.labeln_DPs = new System.Windows.Forms.Label();
            this.buttonRegresar = new System.Windows.Forms.Button();
            this.textBoxISE = new System.Windows.Forms.TextBox();
            this.textBoxn_DPs = new System.Windows.Forms.TextBox();
            this.textBoxr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PB)).BeginInit();
            this.SuspendLayout();
            // 
            // PB
            // 
            this.PB.Location = new System.Drawing.Point(30, 64);
            this.PB.Name = "PB";
            this.PB.Size = new System.Drawing.Size(423, 322);
            this.PB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PB.TabIndex = 0;
            this.PB.TabStop = false;
            // 
            // labelr
            // 
            this.labelr.AutoSize = true;
            this.labelr.Location = new System.Drawing.Point(481, 303);
            this.labelr.Name = "labelr";
            this.labelr.Size = new System.Drawing.Size(13, 17);
            this.labelr.TabIndex = 1;
            this.labelr.Text = "r";
            // 
            // labelISE
            // 
            this.labelISE.AutoSize = true;
            this.labelISE.Location = new System.Drawing.Point(473, 130);
            this.labelISE.Name = "labelISE";
            this.labelISE.Size = new System.Drawing.Size(29, 17);
            this.labelISE.TabIndex = 2;
            this.labelISE.Text = "ISE";
            // 
            // labeln_DPs
            // 
            this.labeln_DPs.AutoSize = true;
            this.labeln_DPs.Location = new System.Drawing.Point(468, 219);
            this.labeln_DPs.Name = "labeln_DPs";
            this.labeln_DPs.Size = new System.Drawing.Size(33, 17);
            this.labeln_DPs.TabIndex = 3;
            this.labeln_DPs.Text = "BPs";
            // 
            // buttonRegresar
            // 
            this.buttonRegresar.Location = new System.Drawing.Point(470, 405);
            this.buttonRegresar.Name = "buttonRegresar";
            this.buttonRegresar.Size = new System.Drawing.Size(120, 28);
            this.buttonRegresar.TabIndex = 4;
            this.buttonRegresar.Text = "BPs Elimination";
            this.buttonRegresar.UseVisualStyleBackColor = true;
            this.buttonRegresar.Visible = false;
            this.buttonRegresar.Click += new System.EventHandler(this.buttonRegresar_Click);
            // 
            // textBoxISE
            // 
            this.textBoxISE.Enabled = false;
            this.textBoxISE.Location = new System.Drawing.Point(504, 130);
            this.textBoxISE.Name = "textBoxISE";
            this.textBoxISE.Size = new System.Drawing.Size(87, 22);
            this.textBoxISE.TabIndex = 5;
            // 
            // textBoxn_DPs
            // 
            this.textBoxn_DPs.Enabled = false;
            this.textBoxn_DPs.Location = new System.Drawing.Point(504, 219);
            this.textBoxn_DPs.Name = "textBoxn_DPs";
            this.textBoxn_DPs.Size = new System.Drawing.Size(87, 22);
            this.textBoxn_DPs.TabIndex = 6;
            // 
            // textBoxr
            // 
            this.textBoxr.Enabled = false;
            this.textBoxr.Location = new System.Drawing.Point(504, 303);
            this.textBoxr.Name = "textBoxr";
            this.textBoxr.Size = new System.Drawing.Size(87, 22);
            this.textBoxr.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(227, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "BPs Detection";
            // 
            // Form_BPs_Detection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxr);
            this.Controls.Add(this.textBoxn_DPs);
            this.Controls.Add(this.textBoxISE);
            this.Controls.Add(this.buttonRegresar);
            this.Controls.Add(this.labeln_DPs);
            this.Controls.Add(this.labelISE);
            this.Controls.Add(this.labelr);
            this.Controls.Add(this.PB);
            this.Name = "Form_BPs_Detection";
            this.Text = "BPs_Detection";
            ((System.ComponentModel.ISupportInitialize)(this.PB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PB;
        private System.Windows.Forms.Label labelr;
        private System.Windows.Forms.Label labelISE;
        private System.Windows.Forms.Label labeln_DPs;
        private System.Windows.Forms.Button buttonRegresar;
        private System.Windows.Forms.TextBox textBoxISE;
        private System.Windows.Forms.TextBox textBoxn_DPs;
        private System.Windows.Forms.TextBox textBoxr;
        private System.Windows.Forms.Label label1;
    }
}