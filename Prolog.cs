namespace ProtoProlog;

class Prolog
{
    private readonly List<Regla> reglas = [];
    private readonly string FAIL = "fail";
    private readonly string CUT = "!";
    private readonly string PESO = "$";

    public string EliminarComentario(string linea)
    {
        int index = linea.IndexOf('%');
        if (index == -1) return linea;
        return index == -1 ? linea : linea.Substring(0, index).Trim();
    }

    public void CargarPrograma(string programa)
    {
        var lineas = programa.Split(['\n'], StringSplitOptions.RemoveEmptyEntries);

        foreach (var linea in lineas)
        {
            string lineaSinComentario = EliminarComentario(linea);
            if (lineaSinComentario == "") continue;

            var texto = lineaSinComentario.Trim().TrimEnd('.');
            if (string.IsNullOrWhiteSpace(texto)) continue;

            if (texto.Contains(":-"))
            {
                var partes = texto.Split([":-"], StringSplitOptions.None);
                var cabeza = partes[0].Trim();
                var cuerpo = partes[1].Split(',').Select(p => p.Trim()).ToList();

                reglas.Add(new Regla { Cabeza = cabeza, Cuerpo = cuerpo });
            }
            else
            {
                reglas.Add(new Regla { Cabeza = texto });
            }
        }

    }

    public string MostrarReglas()
    {
        string output = "";
        foreach (var regla in reglas)
        {
            output += regla.ToString() + "\n";
        }
        return output;
    }

    public bool Consultar(string consulta)
    {
        List<string> cuerpo = consulta.TrimEnd('.').Split(',').Select(p => p.Trim()).ToList();

        // ?- q, r, s.
        // <- q, r, s.
        // TODO: implemented for (!) in the cuerpo
        // example: ?- q, !, r, s.
        Regla consultaClausula = new();
        consultaClausula.Cabeza = "";
        consultaClausula.Cuerpo = cuerpo;

        foreach (var objetivo in consultaClausula.Cuerpo)
        {
            bool hayCut = false;
            if (!Resolver(objetivo, ref hayCut))
            {
                return false;
            }
        }
        return true;
    }

    public bool Resolver(string objetivo, ref bool hayCut)
    {
        foreach (var regla in reglas)
        {
            if (regla.Cabeza != objetivo) continue;

            if (regla.EsHecho())
            {
                return true;
            }
            bool cutEnEstaRama = false;
            bool hayCutAntesDePeso = false;
            for (int i = 0; i < regla.Cuerpo.Count; i++)
            {
                if (regla.Cuerpo[i] == CUT)
                {
                    hayCutAntesDePeso = true;
                    break;
                }
            }
            if (ResolverCuerpo(regla.Cuerpo, 0, ref cutEnEstaRama, ref hayCutAntesDePeso))
            {
                hayCut = hayCut && cutEnEstaRama;
                return true;
            }

            if (cutEnEstaRama)
            {
                hayCut = true;
                return false;
            }
        }

        return false;
    }

    private bool ResolverCuerpo(List<string> cuerpo, int indice, ref bool hayCut, ref bool hayCutAntesDePeso)
    {
        if (indice >= cuerpo.Count)
            return true;
        string submeta = cuerpo[indice];
        if (submeta == PESO)
        {
            if (hayCutAntesDePeso)
            {
                hayCut = true;
                return ResolverCuerpo(cuerpo, indice + 1, ref hayCut, ref hayCutAntesDePeso);
            }
            else
            {
                return true;
            }
        }
        if (submeta == CUT)
        {
            hayCut = true;
            return ResolverCuerpo(cuerpo, indice + 1, ref hayCut, ref hayCutAntesDePeso);
        }
        if (submeta == FAIL)
            return false;
        if (Resolver(submeta, ref hayCut))
        {
            return ResolverCuerpo(cuerpo, indice + 1, ref hayCut, ref hayCutAntesDePeso);
        }
        return false;
    }
    public void DescargarPrograma()
    {
        reglas.Clear();
    }
}
