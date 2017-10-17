namespace UserAendern.WindowsForm
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_get_users = new System.Windows.Forms.Button();
            this.listbox_users = new System.Windows.Forms.ListBox();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_unlock = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_add_user = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.btn_lock = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_get_users
            // 
            this.btn_get_users.Location = new System.Drawing.Point(10, 10);
            this.btn_get_users.Name = "btn_get_users";
            this.btn_get_users.Size = new System.Drawing.Size(75, 23);
            this.btn_get_users.TabIndex = 0;
            this.btn_get_users.Text = "Lade User";
            this.btn_get_users.UseVisualStyleBackColor = true;
            this.btn_get_users.Click += new System.EventHandler(this.button1_Click);
            // 
            // listbox_users
            // 
            this.listbox_users.FormattingEnabled = true;
            this.listbox_users.Location = new System.Drawing.Point(460, 10);
            this.listbox_users.Name = "listbox_users";
            this.listbox_users.Size = new System.Drawing.Size(371, 537);
            this.listbox_users.TabIndex = 1;
            this.listbox_users.SelectedIndexChanged += new System.EventHandler(this.listbox_users_SelectedIndexChanged);
            this.listbox_users.SelectedValueChanged += new System.EventHandler(this.listbox_users_SelectedValueChanged);
            // 
            // txt_username
            // 
            this.txt_username.Location = new System.Drawing.Point(66, 19);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(191, 20);
            this.txt_username.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_username);
            this.groupBox1.Location = new System.Drawing.Point(10, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 463);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Benutzerinformationen";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_delete);
            this.groupBox2.Controls.Add(this.btn_lock);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btn_unlock);
            this.groupBox2.Location = new System.Drawing.Point(7, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(431, 405);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Details";
            // 
            // btn_unlock
            // 
            this.btn_unlock.Location = new System.Drawing.Point(304, 374);
            this.btn_unlock.Name = "btn_unlock";
            this.btn_unlock.Size = new System.Drawing.Size(121, 23);
            this.btn_unlock.TabIndex = 0;
            this.btn_unlock.Text = "Benutzer Entsperren";
            this.btn_unlock.UseVisualStyleBackColor = true;
            this.btn_unlock.Click += new System.EventHandler(this.btn_unlock_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "username";
            // 
            // btn_add_user
            // 
            this.btn_add_user.Location = new System.Drawing.Point(10, 518);
            this.btn_add_user.Name = "btn_add_user";
            this.btn_add_user.Size = new System.Drawing.Size(444, 27);
            this.btn_add_user.TabIndex = 4;
            this.btn_add_user.Text = "Benutzer hinzufügen";
            this.btn_add_user.UseVisualStyleBackColor = true;
            this.btn_add_user.Click += new System.EventHandler(this.btn_add_user_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Straße / Hausnummer";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(124, 17);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(219, 20);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(367, 17);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(58, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(349, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "/";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Postleitzahl";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(124, 48);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Stadt";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(124, 78);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 8;
            // 
            // btn_lock
            // 
            this.btn_lock.Location = new System.Drawing.Point(159, 374);
            this.btn_lock.Name = "btn_lock";
            this.btn_lock.Size = new System.Drawing.Size(129, 23);
            this.btn_lock.TabIndex = 9;
            this.btn_lock.Text = "Benutzer Sperren";
            this.btn_lock.UseVisualStyleBackColor = true;
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(7, 374);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(136, 23);
            this.btn_delete.TabIndex = 10;
            this.btn_delete.Text = "Benutzer Löschen";
            this.btn_delete.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 557);
            this.Controls.Add(this.btn_add_user);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listbox_users);
            this.Controls.Add(this.btn_get_users);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_get_users;
        private System.Windows.Forms.ListBox listbox_users;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_unlock;
        private System.Windows.Forms.Button btn_add_user;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_lock;
    }
}

