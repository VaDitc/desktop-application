namespace WCSC
{
    partial class PrintReport
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ReportForShift = new WCSC.ReportForShift();
            this.ReportShiftTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ReportForShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportShiftTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ReportShiftTableBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "WCSC.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.reportViewer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.reportViewer1.Size = new System.Drawing.Size(905, 638);
            this.reportViewer1.TabIndex = 0;
            // 
            // ReportForShift
            // 
            this.ReportForShift.DataSetName = "ReportForShift";
            this.ReportForShift.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportShiftTableBindingSource
            // 
            this.ReportShiftTableBindingSource.DataMember = "ReportShiftTable";
            this.ReportShiftTableBindingSource.DataSource = this.ReportForShift;
            // 
            // PrintReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 638);
            this.Controls.Add(this.reportViewer1);
            this.MinimumSize = new System.Drawing.Size(921, 677);
            this.Name = "PrintReport";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.PrintReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReportForShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportShiftTableBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ReportShiftTableBindingSource;
        private ReportForShift ReportForShift;
    }
}