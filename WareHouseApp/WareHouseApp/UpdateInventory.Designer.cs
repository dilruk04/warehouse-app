namespace WareHouseApp
{
    partial class UpdateInventory
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
            this.labelItemIdSearch = new System.Windows.Forms.Label();
            this.txtItemIdSearch = new System.Windows.Forms.TextBox();
            this.btnLoadItem = new System.Windows.Forms.Button();
            this.labelItemName = new System.Windows.Forms.Label();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.labelPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.btnUpdateItem = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // labelTitle
            //
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(200, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(209, 30);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Update Inventory Item";
            //
            // labelItemIdSearch
            //
            this.labelItemIdSearch.AutoSize = true;
            this.labelItemIdSearch.Location = new System.Drawing.Point(50, 70);
            this.labelItemIdSearch.Name = "labelItemIdSearch";
            this.labelItemIdSearch.Size = new System.Drawing.Size(44, 13);
            this.labelItemIdSearch.TabIndex = 1;
            this.labelItemIdSearch.Text = "Item ID:";
            //
            // txtItemIdSearch
            //
            this.txtItemIdSearch.Location = new System.Drawing.Point(150, 67);
            this.txtItemIdSearch.Name = "txtItemIdSearch";
            this.txtItemIdSearch.Size = new System.Drawing.Size(150, 20);
            this.txtItemIdSearch.TabIndex = 2;
            //
            // btnLoadItem
            //
            this.btnLoadItem.Location = new System.Drawing.Point(310, 65);
            this.btnLoadItem.Name = "btnLoadItem";
            this.btnLoadItem.Size = new System.Drawing.Size(90, 23);
            this.btnLoadItem.TabIndex = 3;
            this.btnLoadItem.Text = "Load Item";
            this.btnLoadItem.UseVisualStyleBackColor = true;
            this.btnLoadItem.Click += new System.EventHandler(this.btnLoadItem_Click); // Event handler
            //
            // labelItemName
            //
            this.labelItemName.AutoSize = true;
            this.labelItemName.Location = new System.Drawing.Point(50, 120);
            this.labelItemName.Name = "labelItemName";
            this.labelItemName.Size = new System.Drawing.Size(61, 13);
            this.labelItemName.TabIndex = 4;
            this.labelItemName.Text = "Item Name:";
            //
            // txtItemName
            //
            this.txtItemName.Location = new System.Drawing.Point(150, 117);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(300, 20);
            this.txtItemName.TabIndex = 5;
            //
            // labelDescription
            //
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(50, 160);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(63, 13);
            this.labelDescription.TabIndex = 6;
            this.labelDescription.Text = "Description:";
            //
            // txtDescription
            //
            this.txtDescription.Location = new System.Drawing.Point(150, 157);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(300, 70);
            this.txtDescription.TabIndex = 7;
            //
            // labelQuantity
            //
            this.labelQuantity.AutoSize = true;
            this.labelQuantity.Location = new System.Drawing.Point(50, 250);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(49, 13);
            this.labelQuantity.TabIndex = 8;
            this.labelQuantity.Text = "Quantity:";
            //
            // txtQuantity
            //
            this.txtQuantity.Location = new System.Drawing.Point(150, 247);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(150, 20);
            this.txtQuantity.TabIndex = 9;
            //
            // labelPrice
            //
            this.labelPrice.AutoSize = true;
            this.labelPrice.Location = new System.Drawing.Point(50, 290);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(34, 13);
            this.labelPrice.TabIndex = 10;
            this.labelPrice.Text = "Price:";
            //
            // txtPrice
            //
            this.txtPrice.Location = new System.Drawing.Point(150, 287);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(150, 20);
            this.txtPrice.TabIndex = 11;
            //
            // btnUpdateItem
            //
            this.btnUpdateItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateItem.Location = new System.Drawing.Point(150, 340);
            this.btnUpdateItem.Name = "btnUpdateItem";
            this.btnUpdateItem.Size = new System.Drawing.Size(120, 35);
            this.btnUpdateItem.TabIndex = 12;
            this.btnUpdateItem.Text = "Update Item";
            this.btnUpdateItem.UseVisualStyleBackColor = true;
            this.btnUpdateItem.Click += new System.EventHandler(this.btnUpdateItem_Click); // Event handler
            //
            // btnCancel
            //
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(280, 340);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click); // Event handler
            //
            // UpdateInventory
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 420); // Adjusted form size
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdateItem);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.labelPrice);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.labelQuantity);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.labelItemName);
            this.Controls.Add(this.btnLoadItem);
            this.Controls.Add(this.txtItemIdSearch);
            this.Controls.Add(this.labelItemIdSearch);
            this.Controls.Add(this.labelTitle);
            this.Name = "UpdateInventory";
            this.Text = "Update Inventory Item"; // Updated form title
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelItemIdSearch;
        private System.Windows.Forms.TextBox txtItemIdSearch;
        private System.Windows.Forms.Button btnLoadItem;
        private System.Windows.Forms.Label labelItemName;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Button btnUpdateItem;
        private System.Windows.Forms.Button btnCancel;
    }
}