namespace WareHouseApp
{
    partial class UpdateEmployee
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelEmployeeIdSearch = new System.Windows.Forms.Label();
            this.txtEmployeeIdSearch = new System.Windows.Forms.TextBox();
            this.btnLoadEmployee = new System.Windows.Forms.Button();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.labelLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.labelPosition = new System.Windows.Forms.Label();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.labelHireDate = new System.Windows.Forms.Label();
            this.dtpHireDate = new System.Windows.Forms.DateTimePicker();
            this.labelSalary = new System.Windows.Forms.Label();
            this.txtSalary = new System.Windows.Forms.TextBox();
            this.labelContactNumber = new System.Windows.Forms.Label();
            this.txtContactNumber = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnUpdateEmployee = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // labelTitle
            //
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(200, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(230, 30);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Update Employee Details";
            //
            // labelEmployeeIdSearch
            //
            this.labelEmployeeIdSearch.AutoSize = true;
            this.labelEmployeeIdSearch.Location = new System.Drawing.Point(50, 70);
            this.labelEmployeeIdSearch.Name = "labelEmployeeIdSearch";
            this.labelEmployeeIdSearch.Size = new System.Drawing.Size(68, 13);
            this.labelEmployeeIdSearch.TabIndex = 1;
            this.labelEmployeeIdSearch.Text = "Employee ID:";
            //
            // txtEmployeeIdSearch
            //
            this.txtEmployeeIdSearch.Location = new System.Drawing.Point(170, 67);
            this.txtEmployeeIdSearch.Name = "txtEmployeeIdSearch";
            this.txtEmployeeIdSearch.Size = new System.Drawing.Size(150, 20);
            this.txtEmployeeIdSearch.TabIndex = 2;
            //
            // btnLoadEmployee
            //
            this.btnLoadEmployee.Location = new System.Drawing.Point(330, 65);
            this.btnLoadEmployee.Name = "btnLoadEmployee";
            this.btnLoadEmployee.Size = new System.Drawing.Size(100, 23);
            this.btnLoadEmployee.TabIndex = 3;
            this.btnLoadEmployee.Text = "Load Employee";
            this.btnLoadEmployee.UseVisualStyleBackColor = true;
            this.btnLoadEmployee.Click += new System.EventHandler(this.btnLoadEmployee_Click); // Event handler
            //
            // labelFirstName
            //
            this.labelFirstName.AutoSize = true;
            this.labelFirstName.Location = new System.Drawing.Point(50, 120);
            this.labelFirstName.Name = "labelFirstName";
            this.labelFirstName.Size = new System.Drawing.Size(60, 13);
            this.labelFirstName.TabIndex = 4;
            this.labelFirstName.Text = "First Name:";
            //
            // txtFirstName
            //
            this.txtFirstName.Location = new System.Drawing.Point(170, 117);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(250, 20);
            this.txtFirstName.TabIndex = 5;
            //
            // labelLastName
            //
            this.labelLastName.AutoSize = true;
            this.labelLastName.Location = new System.Drawing.Point(50, 155);
            this.labelLastName.Name = "labelLastName";
            this.labelLastName.Size = new System.Drawing.Size(61, 13);
            this.labelLastName.TabIndex = 6;
            this.labelLastName.Text = "Last Name:";
            //
            // txtLastName
            //
            this.txtLastName.Location = new System.Drawing.Point(170, 152);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(250, 20);
            this.txtLastName.TabIndex = 7;
            //
            // labelPosition
            //
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(50, 190);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(47, 13);
            this.labelPosition.TabIndex = 8;
            this.labelPosition.Text = "Position:";
            //
            // txtPosition
            //
            this.txtPosition.Location = new System.Drawing.Point(170, 187);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Size = new System.Drawing.Size(250, 20);
            this.txtPosition.TabIndex = 9;
            //
            // labelHireDate
            //
            this.labelHireDate.AutoSize = true;
            this.labelHireDate.Location = new System.Drawing.Point(50, 225);
            this.labelHireDate.Name = "labelHireDate";
            this.labelHireDate.Size = new System.Drawing.Size(55, 13);
            this.labelHireDate.TabIndex = 10;
            this.labelHireDate.Text = "Hire Date:";
            //
            // dtpHireDate
            //
            this.dtpHireDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHireDate.Location = new System.Drawing.Point(170, 222);
            this.dtpHireDate.Name = "dtpHireDate";
            this.dtpHireDate.Size = new System.Drawing.Size(150, 20);
            this.dtpHireDate.TabIndex = 11;
            //
            // labelSalary
            //
            this.labelSalary.AutoSize = true;
            this.labelSalary.Location = new System.Drawing.Point(50, 260);
            this.labelSalary.Name = "labelSalary";
            this.labelSalary.Size = new System.Drawing.Size(39, 13);
            this.labelSalary.TabIndex = 12;
            this.labelSalary.Text = "Salary:";
            //
            // txtSalary
            //
            this.txtSalary.Location = new System.Drawing.Point(170, 257);
            this.txtSalary.Name = "txtSalary";
            this.txtSalary.Size = new System.Drawing.Size(150, 20);
            this.txtSalary.TabIndex = 13;
            //
            // labelContactNumber
            //
            this.labelContactNumber.AutoSize = true;
            this.labelContactNumber.Location = new System.Drawing.Point(50, 295);
            this.labelContactNumber.Name = "labelContactNumber";
            this.labelContactNumber.Size = new System.Drawing.Size(87, 13);
            this.labelContactNumber.TabIndex = 14;
            this.labelContactNumber.Text = "Contact Number:";
            //
            // txtContactNumber
            //
            this.txtContactNumber.Location = new System.Drawing.Point(170, 292);
            this.txtContactNumber.Name = "txtContactNumber";
            this.txtContactNumber.Size = new System.Drawing.Size(150, 20);
            this.txtContactNumber.TabIndex = 15;
            //
            // labelEmail
            //
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(50, 330);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(35, 13);
            this.labelEmail.TabIndex = 16;
            this.labelEmail.Text = "Email:";
            //
            // txtEmail
            //
            this.txtEmail.Location = new System.Drawing.Point(170, 327);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(250, 20);
            this.txtEmail.TabIndex = 17;
            //
            // btnUpdateEmployee
            //
            this.btnUpdateEmployee.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateEmployee.Location = new System.Drawing.Point(170, 370);
            this.btnUpdateEmployee.Name = "btnUpdateEmployee";
            this.btnUpdateEmployee.Size = new System.Drawing.Size(120, 35);
            this.btnUpdateEmployee.TabIndex = 18;
            this.btnUpdateEmployee.Text = "Update Employee";
            this.btnUpdateEmployee.UseVisualStyleBackColor = true;
            this.btnUpdateEmployee.Click += new System.EventHandler(this.btnUpdateEmployee_Click); // Event handler
            //
            // btnCancel
            //
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(300, 370);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click); // Event handler
            //
            // UpdateEmployee
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 430); // Adjusted form size
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdateEmployee);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.txtContactNumber);
            this.Controls.Add(this.labelContactNumber);
            this.Controls.Add(this.txtSalary);
            this.Controls.Add(this.labelSalary);
            this.Controls.Add(this.dtpHireDate);
            this.Controls.Add(this.labelHireDate);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.labelLastName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.labelFirstName);
            this.Controls.Add(this.btnLoadEmployee);
            this.Controls.Add(this.txtEmployeeIdSearch);
            this.Controls.Add(this.labelEmployeeIdSearch);
            this.Controls.Add(this.labelTitle);
            this.Name = "UpdateEmployee";
            this.Text = "Update Employee Details"; // Updated form title
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelEmployeeIdSearch;
        private System.Windows.Forms.TextBox txtEmployeeIdSearch;
        private System.Windows.Forms.Button btnLoadEmployee;
        private System.Windows.Forms.Label labelFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label labelLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.Label labelHireDate;
        private System.Windows.Forms.DateTimePicker dtpHireDate;
        private System.Windows.Forms.Label labelSalary;
        private System.Windows.Forms.TextBox txtSalary;
        private System.Windows.Forms.Label labelContactNumber;
        private System.Windows.Forms.TextBox txtContactNumber;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnUpdateEmployee;
        private System.Windows.Forms.Button btnCancel;
    }
}