namespace PayRollFormProject
{
    partial class PayStubForm
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
            lblTitle = new Label();
            lblPayStubDetails = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.Red;
            lblTitle.Location = new Point(23, 29);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(150, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Pay Stub";
            // 
            // lblPayStubDetails
            // 
            lblPayStubDetails.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPayStubDetails.ForeColor = Color.Blue;
            lblPayStubDetails.Location = new Point(48, 87);
            lblPayStubDetails.Name = "lblPayStubDetails";
            lblPayStubDetails.Size = new Size(470, 320);
            lblPayStubDetails.TabIndex = 1;
            // 
            // PayStubForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(578, 464);
            Controls.Add(lblPayStubDetails);
            Controls.Add(lblTitle);
            Name = "PayStubForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Pay Stub";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblPayStubDetails;
    }
}