namespace Rent_a_Car_Fast_Wheels.Empleado.ReportesEmpleado
{
    partial class ReporteAutoEmpleado
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteAutoEmpleado));
            this.AutosporMarcaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSetAutosEmpleado = new Rent_a_Car_Fast_Wheels.Empleado.ReportesEmpleado.DataSetAutosEmpleado();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.AutosporMarcaTableAdapter = new Rent_a_Car_Fast_Wheels.Empleado.ReportesEmpleado.DataSetAutosEmpleadoTableAdapters.AutosporMarcaTableAdapter();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.cboMarca = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AutosporMarcaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetAutosEmpleado)).BeginInit();
            this.SuspendLayout();
            // 
            // AutosporMarcaBindingSource
            // 
            this.AutosporMarcaBindingSource.DataMember = "AutosporMarca";
            this.AutosporMarcaBindingSource.DataSource = this.DataSetAutosEmpleado;
            // 
            // DataSetAutosEmpleado
            // 
            this.DataSetAutosEmpleado.DataSetName = "DataSetAutosEmpleado";
            this.DataSetAutosEmpleado.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.AutosporMarcaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Rent_a_Car_Fast_Wheels.Empleado.ReportesEmpleado.ReportAutosEmpleado.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 69);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(836, 542);
            this.reportViewer1.TabIndex = 0;
            // 
            // AutosporMarcaTableAdapter
            // 
            this.AutosporMarcaTableAdapter.ClearBeforeFill = true;
            // 
            // btnGenerar
            // 
            this.btnGenerar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.btnGenerar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.btnGenerar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerar.ForeColor = System.Drawing.Color.White;
            this.btnGenerar.Location = new System.Drawing.Point(347, 34);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(83, 29);
            this.btnGenerar.TabIndex = 37;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = false;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // cboMarca
            // 
            this.cboMarca.FormattingEnabled = true;
            this.cboMarca.Location = new System.Drawing.Point(187, 38);
            this.cboMarca.Name = "cboMarca";
            this.cboMarca.Size = new System.Drawing.Size(134, 21);
            this.cboMarca.TabIndex = 36;
            this.cboMarca.Text = "Seleccione...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 20);
            this.label2.TabIndex = 35;
            this.label2.Text = "Estado marca del auto: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(329, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 25);
            this.label1.TabIndex = 34;
            this.label1.Text = "Reporte Autos";
            // 
            // ReporteAutoEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 611);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.cboMarca);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteAutoEmpleado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte Autos";
            this.Load += new System.EventHandler(this.ReporteAutoEmpleado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AutosporMarcaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetAutosEmpleado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource AutosporMarcaBindingSource;
        private DataSetAutosEmpleado DataSetAutosEmpleado;
        private DataSetAutosEmpleadoTableAdapters.AutosporMarcaTableAdapter AutosporMarcaTableAdapter;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.ComboBox cboMarca;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}