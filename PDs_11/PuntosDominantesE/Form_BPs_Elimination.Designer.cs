namespace PuntosDominantesE
{
    partial class Form_BPs_Elimination
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
            this.textBoxn_DPs = new System.Windows.Forms.TextBox();
            this.textBoxISE = new System.Windows.Forms.TextBox();
            this.buttonRegresar = new System.Windows.Forms.Button();
            this.labeln_DPs = new System.Windows.Forms.Label();
            this.labelISE = new System.Windows.Forms.Label();
            this.PB = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PB)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxn_DPs
            // 
            this.textBoxn_DPs.Enabled = false;
            this.textBoxn_DPs.Location = new System.Drawing.Point(500, 254);
            this.textBoxn_DPs.Name = "textBoxn_DPs";
            this.textBoxn_DPs.Size = new System.Drawing.Size(87, 22);
            this.textBoxn_DPs.TabIndex = 14;
            // 
            // textBoxISE
            // 
            this.textBoxISE.Enabled = false;
            this.textBoxISE.Location = new System.Drawing.Point(500, 165);
            this.textBoxISE.Name = "textBoxISE";
            this.textBoxISE.Size = new System.Drawing.Size(87, 22);
            this.textBoxISE.TabIndex = 13;
            // 
            // buttonRegresar
            // 
            this.buttonRegresar.Location = new System.Drawing.Point(450, 405);
            this.buttonRegresar.Name = "buttonRegresar";
            this.buttonRegresar.Size = new System.Drawing.Size(150, 28);
            this.buttonRegresar.TabIndex = 12;
            this.buttonRegresar.Text = "BPs Rearrangement";
            this.buttonRegresar.UseVisualStyleBackColor = true;
            this.buttonRegresar.Visible = false;
            this.buttonRegresar.Click += new System.EventHandler(this.buttonRegresar_Click);
            // 
            // labeln_DPs
            // 
            this.labeln_DPs.AutoSize = true;
            this.labeln_DPs.Location = new System.Drawing.Point(464, 254);
            this.labeln_DPs.Name = "labeln_DPs";
            this.labeln_DPs.Size = new System.Drawing.Size(33, 17);
            this.labeln_DPs.TabIndex = 11;
            this.labeln_DPs.Text = "BPs";
            // 
            // labelISE
            // 
            this.labelISE.AutoSize = true;
            this.labelISE.Location = new System.Drawing.Point(469, 165);
            this.labelISE.Name = "labelISE";
            this.labelISE.Size = new System.Drawing.Size(29, 17);
            this.labelISE.TabIndex = 10;
            this.labelISE.Text = "ISE";
            // 
            // PB
            // 
            this.PB.Location = new System.Drawing.Point(26, 61);
            this.PB.Name = "PB";
            this.PB.Size = new System.Drawing.Size(423, 322);
            this.PB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PB.TabIndex = 8;
            this.PB.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(227, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 25);
            this.label1.TabIndex = 15;
            this.label1.Text = "BPs Elimination";
            // 
            // Form_BPs_Elimination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxn_DPs);
            this.Controls.Add(this.textBoxISE);
            this.Controls.Add(this.buttonRegresar);
            this.Controls.Add(this.labeln_DPs);
            this.Controls.Add(this.labelISE);
            this.Controls.Add(this.PB);
            this.Name = "Form_BPs_Elimination";
            this.Text = "Form_BPs_Elimination";
            ((System.ComponentModel.ISupportInitialize)(this.PB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxn_DPs;
        private System.Windows.Forms.TextBox textBoxISE;
        private System.Windows.Forms.Button buttonRegresar;
        private System.Windows.Forms.Label labeln_DPs;
        private System.Windows.Forms.Label labelISE;
        private System.Windows.Forms.PictureBox PB;
        private System.Windows.Forms.Label label1;
    }
}