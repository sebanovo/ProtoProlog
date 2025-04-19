using ScintillaNET;
using System.Text.RegularExpressions;
namespace ProtoProlog;

public partial class Form1 : Form
{
    Prolog prolog;
    public Form1()
    {
        InitializeComponent();
        BackColor = Color.FromArgb(18, 18, 18);
        ForeColor = Color.White;

        menuStrip1.BackColor = Color.FromArgb(18, 18, 18);
        menuStrip1.ForeColor = Color.White;

        archivoToolStripMenuItem.BackColor = Color.FromArgb(18, 18, 18);
        archivoToolStripMenuItem.ForeColor = Color.White;

        cargarToolStripMenuItem.BackColor = Color.FromArgb(0x1f, 0x1f, 0x1f);
        cargarToolStripMenuItem.ForeColor = Color.White;

        guardarToolStripMenuItem.BackColor = Color.FromArgb(0x1f, 0x1f, 0x1f);
        guardarToolStripMenuItem.ForeColor = Color.White;

        inferirBtn.BackColor = Color.FromArgb(0x1f, 0x1f, 0x1f);
        inferirBtn.ForeColor = Color.White;
    }

    public void InitScintellaPrologStyles(Scintilla scintella, bool mostrarLineas)
    {
        scintella.Styles[Style.Default].BackColor = Color.FromArgb(30, 30, 30);
        scintella.Styles[Style.Default].ForeColor = Color.FromArgb(212, 212, 212);
        scintella.StyleClearAll();

        // Caret y selección
        scintella.CaretForeColor = Color.White;
        scintella.SelectionBackColor = Color.FromArgb(51, 153, 255);

        if (mostrarLineas)
        {
            // Márgenes y líneas
            scintella.Margins[0].Width = 30;
            scintella.Styles[Style.LineNumber].ForeColor = Color.Gray;
            scintella.Styles[Style.LineNumber].BackColor = Color.FromArgb(30, 30, 30);
        }

        // Estilos personalizados simples
        const int STYLE_PREDICATE = 1;
        const int STYLE_OPERATOR = 2;
        const int STYLE_COMMENT = 3;

        scintella.Styles[STYLE_PREDICATE].ForeColor = Color.FromArgb(0xdc,0xdc,0xaa);
        scintella.Styles[STYLE_OPERATOR].ForeColor = Color.FromArgb(0xc5,0x86,0xc0);
        scintella.Styles[STYLE_COMMENT].ForeColor = Color.FromArgb(16, 153, 85);  
        scintella.Styles[STYLE_COMMENT].Italic = true;

        scintella.TextChanged += (sender, e) =>
        {
            scintella.StartStyling(0);
            scintella.SetStyling(scintella.TextLength, 0);
            var text = scintella.Text;

            foreach (Match m in Regex.Matches(text, @"\b[a-z]+\b"))
            {
                scintella.StartStyling(m.Index);
                scintella.SetStyling(m.Length, STYLE_PREDICATE);
            }

            foreach (Match m in Regex.Matches(text, @"(:-|,)"))
            {
                scintella.StartStyling(m.Index);
                scintella.SetStyling(m.Length, STYLE_OPERATOR);
            }

            foreach (Match m in Regex.Matches(text, @"%.*"))
            {
                scintella.StartStyling(m.Index);
                scintella.SetStyling(m.Length, STYLE_COMMENT);
            }
        };
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        prolog = new Prolog();
        InitScintellaPrologStyles(editor, true);
        InitScintellaPrologStyles(consultaTextBox, false);
    }

    private void inferirBtn_Click(object sender, EventArgs e)
    {
        string programa = editor.Text;
        string consulta = consultaTextBox.Text;
        prolog.CargarPrograma(programa);
        MessageBox.Show(prolog.Consultar(consulta).ToString());
        prolog.DescargarPrograma();
    }
}
