namespace UserAendern.WindowsForm
{
    partial class Form1
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
            this.SuspendLayout();
            // 
            // btn_get_users
            // 
            this.btn_get_users.Location = new System.Drawing.Point(13, 12);
            this.btn_get_users.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_get_users.Name = "btn_get_users";
            this.btn_get_users.Size = new System.Drawing.Size(100, 28);
            this.btn_get_users.TabIndex = 0;
            this.btn_get_users.Text = "Lade User";
            this.btn_get_users.UseVisualStyleBackColor = true;
            this.btn_get_users.Click += new System.EventHandler(this.button1_Click);
            // 
            // listbox_users
            // 
            this.listbox_users.FormattingEnabled = true;
            this.listbox_users.ItemHeight = 16;
            this.listbox_users.Location = new System.Drawing.Point(247, 12);
            this.listbox_users.Name = "listbox_users";
            this.listbox_users.Size = new System.Drawing.Size(1019, 900);
            this.listbox_users.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 946);
            this.Controls.Add(this.listbox_users);
            this.Controls.Add(this.btn_get_users);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_get_users;
        private System.Windows.Forms.ListBox listbox_users;
    }
}

