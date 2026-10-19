namespace PayRollFormProject
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            lblEmployeeName = new Label();
            txtEmployeeName = new TextBox();
            txtSSN = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtWorkedHours = new TextBox();
            label4 = new Label();
            txtHourRate = new TextBox();
            label5 = new Label();
            txtTotalIncome = new TextBox();
            label6 = new Label();
            txtNetAmount = new TextBox();
            btnCalculate = new Button();
            btnPrintPayStub = new Button();
            btnExit = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Blue;
            label1.Location = new Point(29, 31);
            label1.Name = "label1";
            label1.Size = new Size(569, 48);
            label1.TabIndex = 0;
            label1.Text = "Welcome to PayRoll Department";
            // 
            // lblEmployeeName
            // 
            lblEmployeeName.AutoSize = true;
            lblEmployeeName.Location = new Point(46, 88);
            lblEmployeeName.Name = "lblEmployeeName";
            lblEmployeeName.Size = new Size(199, 25);
            lblEmployeeName.TabIndex = 1;
            lblEmployeeName.Text = "Enter Employee's Name";
            // 
            // txtEmployeeName
            // 
            txtEmployeeName.Location = new Point(267, 88);
            txtEmployeeName.Name = "txtEmployeeName";
            txtEmployeeName.Size = new Size(150, 31);
            txtEmployeeName.TabIndex = 2;
            // 
            // txtSSN
            // 
            txtSSN.Location = new Point(267, 144);
            txtSSN.Name = "txtSSN";
            txtSSN.Size = new Size(150, 31);
            txtSSN.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(46, 144);
            label2.Name = "label2";
            label2.Size = new Size(185, 25);
            label2.TabIndex = 4;
            label2.Text = "Enter Employee's SSN";
            // 
            // label3
            // 
            label3.Location = new Point(46, 198);
            label3.Name = "label3";
            label3.Size = new Size(256, 53);
            label3.TabIndex = 5;
            label3.Text = "Enter Number of Worked\r\n Hour\r\n";
            // 
            // txtWorkedHours
            // 
            txtWorkedHours.Location = new Point(267, 200);
            txtWorkedHours.Name = "txtWorkedHours";
            txtWorkedHours.Size = new Size(150, 31);
            txtWorkedHours.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(46, 264);
            label4.Name = "label4";
            label4.Size = new Size(167, 25);
            label4.TabIndex = 7;
            label4.Text = "Enter the Rate Hour";
            label4.Click += label4_Click;
            // 
            // txtHourRate
            // 
            txtHourRate.Location = new Point(267, 256);
            txtHourRate.Name = "txtHourRate";
            txtHourRate.Size = new Size(150, 31);
            txtHourRate.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(49, 313);
            label5.Name = "label5";
            label5.Size = new Size(113, 25);
            label5.TabIndex = 9;
            label5.Text = "Total Income";
            // 
            // txtTotalIncome
            // 
            txtTotalIncome.Location = new Point(267, 312);
            txtTotalIncome.Name = "txtTotalIncome";
            txtTotalIncome.Size = new Size(150, 31);
            txtTotalIncome.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(49, 374);
            label6.Name = "label6";
            label6.Size = new Size(110, 25);
            label6.TabIndex = 11;
            label6.Text = "Net Amount";
            // 
            // txtNetAmount
            // 
            txtNetAmount.Location = new Point(267, 368);
            txtNetAmount.Name = "txtNetAmount";
            txtNetAmount.ReadOnly = true;
            txtNetAmount.Size = new Size(150, 31);
            txtNetAmount.TabIndex = 12;
            // 
            // btnCalculate
            // 
            btnCalculate.Location = new Point(49, 435);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(126, 35);
            btnCalculate.TabIndex = 13;
            btnCalculate.Text = "Calculate";
            btnCalculate.UseVisualStyleBackColor = true;
            btnCalculate.Click += btnCalculate_Click;
            // 
            // btnPrintPayStub
            // 
            btnPrintPayStub.AutoSize = true;
            btnPrintPayStub.Location = new Point(260, 435);
            btnPrintPayStub.Name = "btnPrintPayStub";
            btnPrintPayStub.Size = new Size(126, 35);
            btnPrintPayStub.TabIndex = 14;
            btnPrintPayStub.Text = "Print PayStub";
            btnPrintPayStub.UseVisualStyleBackColor = true;
            btnPrintPayStub.Click += button1_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(471, 435);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(126, 35);
            btnExit.TabIndex = 15;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(628, 544);
            Controls.Add(btnExit);
            Controls.Add(btnPrintPayStub);
            Controls.Add(btnCalculate);
            Controls.Add(txtNetAmount);
            Controls.Add(label6);
            Controls.Add(txtTotalIncome);
            Controls.Add(label5);
            Controls.Add(txtHourRate);
            Controls.Add(label4);
            Controls.Add(txtWorkedHours);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtSSN);
            Controls.Add(txtEmployeeName);
            Controls.Add(lblEmployeeName);
            Controls.Add(label1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PayRoll Application";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblEmployeeName;
        private TextBox txtEmployeeName;
        private TextBox txtSSN;
        private Label label2;
        private Label label3;
        private TextBox txtWorkedHours;
        private Label label4;
        private TextBox txtHourRate;
        private Label label5;
        private TextBox txtTotalIncome;
        private Label label6;
        private TextBox txtNetAmount;
        private Button btnCalculate;
        private Button btnPrintPayStub;
        private Button btnExit;
    }
}
