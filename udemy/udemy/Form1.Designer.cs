namespace udemy
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
            btnsend = new Button();
            textEmployeId = new TextBox();
            EmployeeName = new Label();
            EmployeeAge = new Label();
            EmployeePosition = new Label();
            textEmplyeeName = new TextBox();
            textemployeeAge = new TextBox();
            textEmployeePosition = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(83, 63);
            label1.Name = "label1";
            label1.Size = new Size(72, 15);
            label1.TabIndex = 0;
            label1.Text = "EmployeeId";
            // 
            // btnsend
            // 
            btnsend.Location = new Point(182, 285);
            btnsend.Name = "btnsend";
            btnsend.Size = new Size(114, 45);
            btnsend.TabIndex = 1;
            btnsend.Text = "Send ";
            btnsend.UseVisualStyleBackColor = true;
            btnsend.Click += btnsend_Click;
            // 
            // textEmployeId
            // 
            textEmployeId.Location = new Point(220, 60);
            textEmployeId.Name = "textEmployeId";
            textEmployeId.Size = new Size(136, 23);
            textEmployeId.TabIndex = 2;
            // 
            // EmployeeName
            // 
            EmployeeName.AutoSize = true;
            EmployeeName.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            EmployeeName.Location = new Point(83, 120);
            EmployeeName.Name = "EmployeeName";
            EmployeeName.Size = new Size(94, 15);
            EmployeeName.TabIndex = 3;
            EmployeeName.Text = "EmployeeName";
            EmployeeName.Click += EmployeeName_Click;
            // 
            // EmployeeAge
            // 
            EmployeeAge.AutoSize = true;
            EmployeeAge.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            EmployeeAge.Location = new Point(83, 185);
            EmployeeAge.Name = "EmployeeAge";
            EmployeeAge.Size = new Size(83, 15);
            EmployeeAge.TabIndex = 4;
            EmployeeAge.Text = "EmployeeAge";
            // 
            // EmployeePosition
            // 
            EmployeePosition.AutoSize = true;
            EmployeePosition.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            EmployeePosition.Location = new Point(83, 239);
            EmployeePosition.Name = "EmployeePosition";
            EmployeePosition.Size = new Size(105, 15);
            EmployeePosition.TabIndex = 5;
            EmployeePosition.Text = "EmployeePosition";
            EmployeePosition.Click += EmployeePosition_Click;
            // 
            // textEmplyeeName
            // 
            textEmplyeeName.Location = new Point(220, 112);
            textEmplyeeName.Name = "textEmplyeeName";
            textEmplyeeName.Size = new Size(136, 23);
            textEmplyeeName.TabIndex = 6;
            // 
            // textemployeeAge
            // 
            textemployeeAge.Location = new Point(220, 182);
            textemployeeAge.Name = "textemployeeAge";
            textemployeeAge.Size = new Size(136, 23);
            textemployeeAge.TabIndex = 7;
            // 
            // textEmployeePosition
            // 
            textEmployeePosition.Location = new Point(220, 236);
            textEmployeePosition.Name = "textEmployeePosition";
            textEmployeePosition.Size = new Size(136, 23);
            textEmployeePosition.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(452, 450);
            Controls.Add(textEmployeePosition);
            Controls.Add(textemployeeAge);
            Controls.Add(textEmplyeeName);
            Controls.Add(EmployeePosition);
            Controls.Add(EmployeeAge);
            Controls.Add(EmployeeName);
            Controls.Add(textEmployeId);
            Controls.Add(btnsend);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnsend;
        private Label EmployeeName;
        private Label label4;
        private TextBox textEmplyeeName;
        private TextBox textEmployeePosition;
        public Label EmployeeAge;
        public TextBox textEmployeId;
        public TextBox textemployeeAge;
        public Label EmployeePosition;
    }
}
