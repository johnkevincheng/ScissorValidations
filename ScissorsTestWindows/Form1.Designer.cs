namespace ScissorsTestWindows
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.firstNameInput = new System.Windows.Forms.TextBox();
            this.middleNameInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lastNameInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.emailInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.yearsOfExperienceInput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ratingInput = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.employmentDateInput = new System.Windows.Forms.DateTimePicker();
            this.submitButton = new System.Windows.Forms.Button();
            this.errorList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.errorList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "First Name";
            // 
            // firstNameInput
            // 
            this.firstNameInput.Location = new System.Drawing.Point(150, 12);
            this.firstNameInput.Name = "firstNameInput";
            this.firstNameInput.Size = new System.Drawing.Size(200, 20);
            this.firstNameInput.TabIndex = 1;
            // 
            // middleNameInput
            // 
            this.middleNameInput.Location = new System.Drawing.Point(150, 38);
            this.middleNameInput.Name = "middleNameInput";
            this.middleNameInput.Size = new System.Drawing.Size(200, 20);
            this.middleNameInput.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Middle Name";
            // 
            // lastNameInput
            // 
            this.lastNameInput.Location = new System.Drawing.Point(150, 64);
            this.lastNameInput.Name = "lastNameInput";
            this.lastNameInput.Size = new System.Drawing.Size(200, 20);
            this.lastNameInput.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Last Name";
            // 
            // emailInput
            // 
            this.emailInput.Location = new System.Drawing.Point(150, 90);
            this.emailInput.Name = "emailInput";
            this.emailInput.Size = new System.Drawing.Size(200, 20);
            this.emailInput.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Email";
            // 
            // yearsOfExperienceInput
            // 
            this.yearsOfExperienceInput.Location = new System.Drawing.Point(150, 116);
            this.yearsOfExperienceInput.Name = "yearsOfExperienceInput";
            this.yearsOfExperienceInput.Size = new System.Drawing.Size(200, 20);
            this.yearsOfExperienceInput.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Years of Experience";
            // 
            // ratingInput
            // 
            this.ratingInput.Location = new System.Drawing.Point(150, 142);
            this.ratingInput.Name = "ratingInput";
            this.ratingInput.Size = new System.Drawing.Size(200, 20);
            this.ratingInput.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Rating";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(42, 174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Date of Employment";
            // 
            // employmentDateInput
            // 
            this.employmentDateInput.Location = new System.Drawing.Point(150, 168);
            this.employmentDateInput.Name = "employmentDateInput";
            this.employmentDateInput.Size = new System.Drawing.Size(200, 20);
            this.employmentDateInput.TabIndex = 13;
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(275, 203);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 14;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // errorList
            // 
            this.errorList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.errorList.Location = new System.Drawing.Point(418, 12);
            this.errorList.Name = "errorList";
            this.errorList.Size = new System.Drawing.Size(296, 176);
            this.errorList.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 287);
            this.Controls.Add(this.errorList);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.employmentDateInput);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ratingInput);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.yearsOfExperienceInput);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.emailInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lastNameInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.middleNameInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.firstNameInput);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox firstNameInput;
        private System.Windows.Forms.TextBox middleNameInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox lastNameInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox emailInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox yearsOfExperienceInput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ratingInput;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker employmentDateInput;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.DataGridView errorList;
    }
}

