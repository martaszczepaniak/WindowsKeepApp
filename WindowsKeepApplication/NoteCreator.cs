﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Data.SQLite;
using System.Drawing;

namespace WindowsKeepApplication
{
    class NoteCreator : GroupBox
    {
        public MetroFramework.Controls.MetroTextBox noteTitle;
        public TextBox noteText;
        public MetroFramework.Controls.MetroButton noteSubmitButton;
        public KeepIt.CreateNoteDelegate NoteCreatorDelegate;

        public NoteCreator(KeepIt.CreateNoteDelegate NoteCreatorDelegate)
        {
            SetDefaultAttributes();
            this.Controls.Add(CreateNoteTitleTextBox());
            this.Controls.Add(CreateNoteTextTextBox(0,23,500,38));

            this.NoteCreatorDelegate = NoteCreatorDelegate;
            this.Controls.Add(CreateNoteSubmitButton());
        }


        private MetroFramework.Controls.MetroButton CreateNoteSubmitButton()
        {
            noteSubmitButton = new MetroFramework.Controls.MetroButton();
            noteSubmitButton.Location = new Point(500, 23);
            noteSubmitButton.Name = "noteSubmitButton";
            noteSubmitButton.Size = new Size(140, 38);
            noteSubmitButton.UseCustomBackColor = true;
            noteSubmitButton.BackColor = Color.SkyBlue;
            noteSubmitButton.Text = "READY";
            noteSubmitButton.Click += new EventHandler(noteSubmitButton_Click);
            return noteSubmitButton;
        }

        private void SetDefaultAttributes()
        {
            this.Location = new Point(23, 63);
            this.Name = "NoteCreatorGroup";
            this.Size = new Size(640, 61);
            this.Visible = false;
        }

        private MetroFramework.Controls.MetroTextBox CreateNoteTitleTextBox()
        {
            noteTitle = new MetroFramework.Controls.MetroTextBox();
            noteTitle.Location = new Point(0, 0);
            noteTitle.Name = "noteTitle";
            noteTitle.Size = new Size(640, 23);
            return noteTitle;
        }

        private TextBox CreateNoteTextTextBox(int pointx, int pointy, int sizex, int sizey)
        {
            noteText = new TextBox();
            noteText.Location = new Point(pointx, pointy);
            noteText.Name = "noteText";
            noteText.Size = new Size(sizex, sizey);
            noteText.AcceptsReturn = true;
            noteText.Multiline = true;
            noteText.AcceptsTab = true;
            return noteText;
        }

        private void noteSubmitButton_Click(object sender, EventArgs e)
        {
            this.NoteCreatorDelegate(this.noteTitle.Text, this.noteText.Text, "Yellow");
            noteText.Text = "";
        }
    }
}
