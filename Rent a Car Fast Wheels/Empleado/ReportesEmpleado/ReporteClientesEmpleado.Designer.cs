namespace Rent_a_Car_Fast_Wheels.Empleado.ReportesEmpleado
{
    partial class ReporteClientesEmpleado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteClientesEmpleado));
            this.ListarClientesReporteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSetClientesEmpleado = new Rent_a_Car_Fast_Wheels.Empleado.ReportesEmpleado.DataSetClientesEmpleado();
            this.DataSetAutosEmpleado = new Rent_a_Car_Fast_Wheels.Empleado.ReportesEmpleado.DataSetAutosEmpleado();
            this.AutosporMarcaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.AutosporMarcaTableAdapter = new Rent_a_Car_Fast_Wheels.Empleado.ReportesEmpleado.DataSetAutosEmpleadoTableAdapters.AutosporMarcaTableAdapter();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ListarClientesReporteTableAdapter = new Rent_a_Car_Fast_Wheels.Empleado.ReportesEmpleado.DataSetClientesEmpleadoTableAdapters.ListarClientesReporteTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ListarClientesReporteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetClientesEmpleado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetAutosEmpleado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AutosporMarcaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ListarClientesReporteBindingSource
            // 
            this.ListarClientesReporteBindingSource.DataMember = "ListarClientesReporte";
            this.ListarClientesReporteBindingSource.DataSource = this.DataSetClientesEmpleado;
            // 
            // DataSetClientesEmpleado
            // 
            this.DataSetClientesEmpleado.DataSetName = "DataSetClientesEmpleado";
            this.DataSetClientesEmpleado.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DataSetAutosEmpleado
            // 
            this.DataSetAutosEmpleado.DataSetName = "DataSetAutosEmpleado";
            this.DataSetAutosEmpleado.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // AutosporMarcaBindingSource
            // 
            this.AutosporMarcaBindingSource.DataMember = "AutosporMarca";
            this.AutosporMarcaBindingSource.DataSource = this.DataSetAutosEmpleado;
            // 
            // AutosporMarcaTableAdapter
            // 
            this.AutosporMarcaTableAdapter.ClearBeforeFill = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ListarClientesReporteBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Rent_a_Car_Fast_Wheels.Empleado.ReportesEmpleado.ReporteClientesEmpleado.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(836, 611);
            this.reportViewer1.TabIndex = 0;
            // 
            // ListarClientesReporteTableAdapter
            // 
            this.ListarClientesReporteTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteClientesEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 611);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ReporteClientesEmpleado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte Clientes";
            this.Load += new System.EventHandler(this.ReporteClientesEmpleado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListarClientesReporteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetClientesEmpleado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetAutosEmpleado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AutosporMarcaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource AutosporMarcaBindingSource;
        private DataSetAutosEmpleado DataSetAutosEmpleado;
        private DataSetAutosEmpleadoTableAdapters.AutosporMarcaTableAdapter AutosporMarcaTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ListarClientesReporteBindingSource;
        private DataSetClientesEmpleado DataSetClientesEmpleado;
        private DataSetClientesEmpleadoTableAdapters.ListarClientesReporteTableAdapter ListarClientesReporteTableAdapter;
    }
}