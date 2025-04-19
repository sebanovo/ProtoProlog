namespace ProtoProlog
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
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            cargarToolStripMenuItem = new ToolStripMenuItem();
            guardarToolStripMenuItem = new ToolStripMenuItem();
            inferirBtn = new Button();
            editor = new ScintillaNET.Scintilla();
            label1 = new Label();
            consultaTextBox = new ScintillaNET.Scintilla();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(581, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cargarToolStripMenuItem, guardarToolStripMenuItem });
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(73, 24);
            archivoToolStripMenuItem.Text = "Archivo";
            // 
            // cargarToolStripMenuItem
            // 
            cargarToolStripMenuItem.Name = "cargarToolStripMenuItem";
            cargarToolStripMenuItem.Size = new Size(145, 26);
            cargarToolStripMenuItem.Text = "Cargar";
            // 
            // guardarToolStripMenuItem
            // 
            guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            guardarToolStripMenuItem.Size = new Size(145, 26);
            guardarToolStripMenuItem.Text = "Guardar";
            // 
            // inferirBtn
            // 
            inferirBtn.Location = new Point(463, 447);
            inferirBtn.Name = "inferirBtn";
            inferirBtn.Size = new Size(94, 29);
            inferirBtn.TabIndex = 3;
            inferirBtn.Text = "Inferir";
            inferirBtn.UseVisualStyleBackColor = true;
            inferirBtn.Click += inferirBtn_Click;
            // 
            // editor
            // 
            editor.AutocompleteListSelectedBackColor = Color.FromArgb(0, 120, 215);
            editor.LexerName = null;
            editor.Location = new Point(73, 53);
            editor.Name = "editor";
            editor.ScrollWidth = 166;
            editor.Size = new Size(349, 359);
            editor.TabIndex = 5;
            editor.Text = "p :- a, b, c.\r\na.\r\n% Esto es un comentario\r\na :- fail.\r\na :- !.";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 451);
            label1.Name = "label1";
            label1.Size = new Size(22, 20);
            label1.TabIndex = 6;
            label1.Text = "?-";
            // 
            // consultaTextBox
            // 
            consultaTextBox.AutocompleteListSelectedBackColor = Color.FromArgb(0, 120, 215);
            consultaTextBox.LexerName = null;
            consultaTextBox.Location = new Point(73, 451);
            consultaTextBox.Name = "consultaTextBox";
            consultaTextBox.ScrollWidth = 57;
            consultaTextBox.Size = new Size(87, 24);
            consultaTextBox.TabIndex = 7;
            consultaTextBox.Text = "p";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(581, 514);
            Controls.Add(consultaTextBox);
            Controls.Add(label1);
            Controls.Add(editor);
            Controls.Add(inferirBtn);
            Controls.Add(menuStrip1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem cargarToolStripMenuItem;
        private ToolStripMenuItem guardarToolStripMenuItem;
        private Button inferirBtn;
        private ScintillaNET.Scintilla editor;
        private Label label1;
        private ScintillaNET.Scintilla consultaTextBox;
    }
}
