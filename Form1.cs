namespace ProtoProlog;

public partial class Form1 : Form
{
    Prolog prolog;
    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        prolog = new Prolog();
    }

    private void inferirBtn_Click(object sender, EventArgs e)
    {
        string programa = textBox1.Text;
        string consulta = textBox2.Text;
        prolog.CargarPrograma(programa);
        MessageBox.Show(prolog.Consultar(consulta).ToString());
        prolog.DescargarPrograma();
    }
}
