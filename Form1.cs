using ScintillaNET;
using System.Text.RegularExpressions;
namespace ProtoProlog;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        EstilarFormulario();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        EstilarScintella(editor, true);
        EstilarScintella(consultaTextBox, false);

        editor.Zoom = 10;
        consultaTextBox.Zoom = 10;
    }

    private void inferirBtn_Click(object sender, EventArgs e)
    {
        Prolog prolog = new Prolog();
        string programa = editor.Text;
        string consulta = consultaTextBox.Text;
        prolog.CargarPrograma(programa);
        MessageBox.Show(prolog.Consultar(consulta).ToString());
        prolog.DescargarPrograma();
    }

    private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
    {
        openFileDialog1.Filter = "Archivos Prolog (*.pl)|*.pl|Documentos de texto (*.txt)|*.txt";
        openFileDialog1.Title = "Abrir archivo Prolog";
        if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

        string filePath = openFileDialog1.FileName;
        editor.Text = File.ReadAllText(filePath);
        labelFileName.Text = Path.GetFileName(filePath);
    }

    private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
    {
        saveFileDialog1.Filter = "Archivos Prolog (*.pl)|*.pl|Documentos de text (*.txt)|*.txt";
        saveFileDialog1.Title = "Guardar archivo Prolog";
        if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
        File.WriteAllText(saveFileDialog1.FileName, editor.Text);
    }

    public void EstilarFormulario()
    {
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

    public void EstilarScintella(Scintilla scintella, bool mostrarLineas)
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
        const int STYLE_FAIL = 4;

        scintella.Styles[STYLE_PREDICATE].ForeColor = Color.FromArgb(0xdc, 0xdc, 0xaa);
        scintella.Styles[STYLE_OPERATOR].ForeColor = Color.FromArgb(0xc5, 0x86, 0xc0);
        scintella.Styles[STYLE_COMMENT].ForeColor = Color.FromArgb(16, 153, 85);
        scintella.Styles[STYLE_COMMENT].Italic = true;
        scintella.Styles[STYLE_FAIL].ForeColor = Color.FromArgb(0x56, 0x9c, 0xd6);
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

            foreach (Match m in Regex.Matches(text, @"(:-|,|\.|!)"))
            {
                scintella.StartStyling(m.Index);
                scintella.SetStyling(m.Length, STYLE_OPERATOR);
            }

            foreach (Match m in Regex.Matches(text, @"fail"))
            {
                scintella.StartStyling(m.Index);
                scintella.SetStyling(m.Length, STYLE_FAIL);
            }

            foreach (Match m in Regex.Matches(text, @"%.*"))
            {
                scintella.StartStyling(m.Index);
                scintella.SetStyling(m.Length, STYLE_COMMENT);
            }
        };
    }
}
