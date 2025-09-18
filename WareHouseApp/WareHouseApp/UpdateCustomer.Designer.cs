namespace WareHouseApp
{
    partial class UpdateCustomer
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
            this.labelCustomerIdSearch = new System.Windows.Forms.Label();
            this.txtCustomerIdSearch = new System.Windows.Forms.TextBox();
            this.btnLoadCustomer = new System.Windows.Forms.Button();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.labelLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.labelPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.labelAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnUpdateCustomer = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // labelTitle
            //
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(200, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(217, 30);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Update Customer Details";
            //
            // labelCustomerIdSearch
            //
            this.labelCustomerIdSearch.AutoSize = true;
            this.labelCustomerIdSearch.Location = new System.Drawing.Point(50, 70);
            this.labelCustomerIdSearch.Name = "labelCustomerIdSearch";
            this.labelCustomerIdSearch.Size = new System.Drawing.Size(68, 13);
            this.labelCustomerIdSearch.TabIndex = 1;
            this.labelCustomerIdSearch.Text = "Customer ID:";
            //
            // txtCustomerIdSearch
            //
            this.txtCustomerIdSearch.Location = new System.Drawing.Point(150, 67);
            this.txtCustomerIdSearch.Name = "txtCustomerIdSearch";
            this.txtCustomerIdSearch.Size = new System.Drawing.Size(150, 20);
            this.txtCustomerIdSearch.TabIndex = 2;
            //
            // btnLoadCustomer
            //
            this.btnLoadCustomer.Location = new System.Drawing.Point(310, 65);
            this.btnLoadCustomer.Name = "btnLoadCustomer";
            this.btnLoadCustomer.Size = new System.Drawing.Size(100, 23);
            this.btnLoadCustomer.TabIndex = 3;
            this.btnLoadCustomer.Text = "Load Customer";
            this.btnLoadCustomer.UseVisualStyleBackColor = true;
            this.btnLoadCustomer.Click += new System.EventHandler(this.btnLoadCustomer_Click); // Event handler
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
            this.txtFirstName.Location = new System.Drawing.Point(150, 117);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(300, 20);
            this.txtFirstName.TabIndex = 5;
            //
            // labelLastName
            //
            this.labelLastName.AutoSize = true;
            this.labelLastName.Location = new System.Drawing.Point(50, 160);
            this.labelLastName.Name = "labelLastName";
            this.labelLastName.Size = new System.Drawing.Size(61, 13);
            this.labelLastName.TabIndex = 6;
            this.labelLastName.Text = "Last Name:";
            //
            // txtLastName
            //
            this.txtLastName.Location = new System.Drawing.Point(150, 157);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(300, 20);
            this.txtLastName.TabIndex = 7;
            //
            // labelEmail
            //
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(50, 200);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(35, 13);
            this.labelEmail.TabIndex = 8;
            this.labelEmail.Text = "Email:";
            //
            // txtEmail
            //
            this.txtEmail.Location = new System.Drawing.Point(150, 197);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(300, 20);
            this.txtEmail.TabIndex = 9;
            //
            // labelPhone
            //
            this.labelPhone.AutoSize = true;
            this.labelPhone.Location = new System.Drawing.Point(50, 240);
            this.labelPhone.Name = "labelPhone";
            this.labelPhone.Size = new System.Drawing.Size(41, 13);
            this.labelPhone.TabIndex = 10;
            this.labelPhone.Text = "Phone:";
            //
            // txtPhone
            //
            this.txtPhone.Location = new System.Drawing.Point(150, 237);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(150, 20);
            this.txtPhone.TabIndex = 11;
            //
            // labelAddress
            //
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(50, 280);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(48, 13);
            this.labelAddress.TabIndex = 12;
            this.labelAddress.Text = "Address:";
            //
            // txtAddress
            //
            this.txtAddress.Location = new System.Drawing.Point(150, 277);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(300, 70);
            this.txtAddress.TabIndex = 13;
            //
            // btnUpdateCustomer
            //
            this.btnUpdateCustomer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateCustomer.Location = new System.Drawing.Point(150, 360);
            this.btnUpdateCustomer.Name = "btnUpdateCustomer";
            this.btnUpdateCustomer.Size = new System.Drawing.Size(120, 35);
            this.btnUpdateCustomer.TabIndex = 14;
            this.btnUpdateCustomer.Text = "Update Customer";
            this.btnUpdateCustomer.UseVisualStyleBackColor = true;
            this.btnUpdateCustomer.Click += new System.EventHandler(this.btnUpdateCustomer_Click); // Event handler
            //
            // btnCancel
            //
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(280, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click); // Event handler
            //
            // UpdateCustomer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 420); // Adjusted form size
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdateCustomer);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.labelAddress);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.labelPhone);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.labelLastName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.labelFirstName);
            this.Controls.Add(this.btnLoadCustomer);
            this.Controls.Add(this.txtCustomerIdSearch);
            this.Controls.Add(this.labelCustomerIdSearch);
            this.Controls.Add(this.labelTitle);
            this.Name = "UpdateCustomer";
            this.Text = "Update Customer Details"; // Updated form title
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelCustomerIdSearch;
        private System.Windows.Forms.TextBox txtCustomerIdSearch;
        private System.Windows.Forms.Button btnLoadCustomer;
        private System.Windows.Forms.Label labelFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label labelLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label labelPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnUpdateCustomer;
        private System.Windows.Forms.Button btnCancel;
    }
}