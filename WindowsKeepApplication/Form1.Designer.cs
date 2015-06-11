namespace WindowsKeepApplication
{
    partial class KeepIt
    {
        /// <summary>
        /// required designer variable.
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
            this.noteCreatorGroup = new System.Windows.Forms.GroupBox();
            this.noteTitle = new MetroFramework.Controls.MetroTextBox();
            this.noteSubmitButton = new MetroFramework.Controls.MetroButton();
            this.noteFormOpener = new MetroFramework.Controls.MetroTextBox();
            this.noteCreatorGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // noteCreatorGroup
            // 
            this.noteCreatorGroup.Controls.Add(this.noteSubmitButton);
            this.noteCreatorGroup.Controls.Add(this.noteTitle);
            this.noteCreatorGroup.Location = new System.Drawing.Point(23, 63);
            this.noteCreatorGroup.Name = "noteCreatorGroup";
            this.noteCreatorGroup.Size = new System.Drawing.Size(554, 61);
            this.noteCreatorGroup.TabIndex = 0;
            this.noteCreatorGroup.TabStop = false;
            this.noteCreatorGroup.Visible = false;
            // 
            // noteTitle
            // 
            this.noteTitle.Lines = new string[0];
            this.noteTitle.Location = new System.Drawing.Point(0, 7);
            this.noteTitle.MaxLength = 32767;
            this.noteTitle.Name = "noteTitle";
            this.noteTitle.PasswordChar = '\0';
            this.noteTitle.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.noteTitle.SelectedText = "";
            this.noteTitle.Size = new System.Drawing.Size(554, 23);
            this.noteTitle.TabIndex = 0;
            this.noteTitle.UseSelectable = true;
            // 
            // noteSubmitButton
            // 
            this.noteSubmitButton.Location = new System.Drawing.Point(408, 36);
            this.noteSubmitButton.Name = "noteSubmitButton";
            this.noteSubmitButton.Size = new System.Drawing.Size(140, 23);
            this.noteSubmitButton.TabIndex = 1;
            this.noteSubmitButton.Text = "READY";
            this.noteSubmitButton.UseSelectable = true;
            this.noteSubmitButton.Click += new System.EventHandler(this.noteSubmitButton_Click);
            // 
            // noteFormOpener
            // 
            this.noteFormOpener.Lines = new string[] {
        "Add a note..."};
            this.noteFormOpener.Location = new System.Drawing.Point(23, 70);
            this.noteFormOpener.MaxLength = 32767;
            this.noteFormOpener.Name = "noteFormOpener";
            this.noteFormOpener.PasswordChar = '\0';
            this.noteFormOpener.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.noteFormOpener.SelectedText = "";
            this.noteFormOpener.Size = new System.Drawing.Size(554, 23);
            this.noteFormOpener.TabIndex = 1;
            this.noteFormOpener.Text = "Add a note...";
            this.noteFormOpener.UseSelectable = true;
            this.noteFormOpener.Click += new System.EventHandler(this.noteFormOpener_Click);
            // 
            // KeepIt
            // 
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.noteFormOpener);
            this.Controls.Add(this.noteCreatorGroup);
            this.Name = "KeepIt";
            this.Text = "KeepIt";
            this.noteCreatorGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox noteCreatorGroup;
        private MetroFramework.Controls.MetroButton noteSubmitButton;
        private MetroFramework.Controls.MetroTextBox noteTitle;
        private MetroFramework.Controls.MetroTextBox noteFormOpener;
    }
}


