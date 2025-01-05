namespace TPWinForm_equipo_C
{
    partial class frmABM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmABM));
            this.lblABM = new System.Windows.Forms.Label();
            this.cboListar = new System.Windows.Forms.ComboBox();
            this.btnModificar = new System.Windows.Forms.Button();
            this.lblAgregar = new System.Windows.Forms.Label();
            this.txtAgregar = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.lblItemAModificar = new System.Windows.Forms.Label();
            this.txtModificar = new System.Windows.Forms.TextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblABM
            // 
            this.lblABM.AutoSize = true;
            this.lblABM.Location = new System.Drawing.Point(130, 90);
            this.lblABM.Name = "lblABM";
            this.lblABM.Size = new System.Drawing.Size(37, 16);
            this.lblABM.TabIndex = 0;
            this.lblABM.Text = "abm:";
            // 
            // cboListar
            // 
            this.cboListar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboListar.FormattingEnabled = true;
            this.cboListar.Location = new System.Drawing.Point(196, 82);
            this.cboListar.Name = "cboListar";
            this.cboListar.Size = new System.Drawing.Size(200, 24);
            this.cboListar.TabIndex = 1;
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnModificar.Location = new System.Drawing.Point(408, 133);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(80, 27);
            this.btnModificar.TabIndex = 2;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // lblAgregar
            // 
            this.lblAgregar.AutoSize = true;
            this.lblAgregar.Location = new System.Drawing.Point(78, 254);
            this.lblAgregar.Name = "lblAgregar";
            this.lblAgregar.Size = new System.Drawing.Size(89, 16);
            this.lblAgregar.TabIndex = 3;
            this.lblAgregar.Text = "Añadir nueva:";
            // 
            // txtAgregar
            // 
            this.txtAgregar.Location = new System.Drawing.Point(196, 248);
            this.txtAgregar.Name = "txtAgregar";
            this.txtAgregar.Size = new System.Drawing.Size(200, 22);
            this.txtAgregar.TabIndex = 4;
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAgregar.Location = new System.Drawing.Point(408, 246);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(80, 24);
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // lblItemAModificar
            // 
            this.lblItemAModificar.AutoSize = true;
            this.lblItemAModificar.Location = new System.Drawing.Point(12, 141);
            this.lblItemAModificar.Name = "lblItemAModificar";
            this.lblItemAModificar.Size = new System.Drawing.Size(178, 16);
            this.lblItemAModificar.TabIndex = 6;
            this.lblItemAModificar.Text = "Modificar item seleccionado:";
            // 
            // txtModificar
            // 
            this.txtModificar.Location = new System.Drawing.Point(196, 135);
            this.txtModificar.Name = "txtModificar";
            this.txtModificar.Size = new System.Drawing.Size(200, 22);
            this.txtModificar.TabIndex = 7;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(218, 214);
            this.lblTitulo.MinimumSize = new System.Drawing.Size(155, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(155, 20);
            this.lblTitulo.TabIndex = 8;
            this.lblTitulo.Text = "Agregar un nuevo item";
            // 
            // frmABM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(528, 442);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.txtModificar);
            this.Controls.Add(this.lblItemAModificar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.txtAgregar);
            this.Controls.Add(this.lblAgregar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.cboListar);
            this.Controls.Add(this.lblABM);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmABM";
            this.Text = "frmABMMarca";
            this.Load += new System.EventHandler(this.frmABMMarca_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblABM;
        private System.Windows.Forms.ComboBox cboListar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Label lblAgregar;
        private System.Windows.Forms.TextBox txtAgregar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label lblItemAModificar;
        private System.Windows.Forms.TextBox txtModificar;
        private System.Windows.Forms.Label lblTitulo;
    }
}