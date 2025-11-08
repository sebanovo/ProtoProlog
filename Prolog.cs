namespace ProtoProlog;

class Prolog
{
    private readonly LinkedList<Clausula> baseDeConocimiento = [];
    /// <summary>
    /// Representa un fallo forzado. Cuando aparece en el cuerpo de una cláusula,
    /// provoca el fallo inmediato de esa rama y activa el backtracking.
    /// </summary>
    private readonly string FAIL = "fail";

    /// <summary>
    /// Representa el corte de backtracking (!).
    /// Cancela las alternativas restantes dentro del mismo objetivo,
    /// impidiendo regresar a otras cláusulas para la misma cabeza.
    /// </summary>
    private readonly string CUT = "!";
    /// <summary>
    /// $ es true, pero si está después de un !, ejecuta el corte
    /// y cancela el backtracking solo en la rama actual.
    /// </summary>
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

                baseDeConocimiento.AddLast(new Clausula { Cabeza = cabeza, Cuerpo = cuerpo });
            }
            else
            {
                baseDeConocimiento.AddLast(new Clausula { Cabeza = texto });
            }
        }

    }

    public string MostrarReglas()
    {
        string output = "";
        foreach (var regla in baseDeConocimiento)
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
        Clausula consultaRegla = new();
        consultaRegla.Cabeza = "";
        consultaRegla.Cuerpo = cuerpo;

        bool hayCutLocal = false;
        foreach (var objetivo in consultaRegla.Cuerpo)
        {
            // validaciones
            // TODO: implemented for (!) in the cuerpo
            // example: ?- q, !, !. // si son ! saltar.
            // TODO: implemented for (fail) in the cuerpo
            // example: ?- q, fail, fail. // si son fallar.
            // TODO: implemented for ($) in the cuerpo
            // example: ?- q, !, $. // falla 

            if (objetivo == FAIL)
            {
                return false;
            }
            if (objetivo == CUT)
            {
                hayCutLocal = true;
                continue;
            }
            if (objetivo == PESO)
            {
                if (hayCutLocal)
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }
            // resolver 
            if (!Resolver(objetivo))
            {
                return false;
            }
        }
        return true;
    }


    public LinkedList<Clausula> ClausulasAplicables(string objetivo)
    {
        LinkedList<Clausula> L = new();
        foreach (var clausula in baseDeConocimiento)
        {
            if (clausula.Cabeza.Equals(objetivo))
            {
                L.AddLast(clausula);
            }
        }
        return L;
    }

    public Clausula ElegirClausula(LinkedList<Clausula> L)
    {
        Clausula Clausula = L.First!.Value;
        L.RemoveFirst();
        return Clausula;
    }

    public static int Vueltas = 0;
    public bool Resolver(string objetivo)
    {
        LinkedList<Clausula> clausulas = ClausulasAplicables(objetivo);
        while (clausulas.Count > 0)
        {
            Clausula clausula = ElegirClausula(clausulas);
            if (clausula.EsHecho())
            {
                return true;
            }

            bool cuerpoSatisfecho = true;
            bool hayCutLocal = false;
            bool hayCutAntesQuePeso = false;
            for (int i = 0; i < clausula.Cuerpo.Count; i++)
            {
                string subObjetivo = clausula.Cuerpo[i];
                if (subObjetivo == CUT) // cancela el BT
                {
                    hayCutLocal = true;
                    continue;
                }
                if (subObjetivo == PESO)
                {
                    if (hayCutLocal)
                    {
                        hayCutAntesQuePeso = true; // fuerza el BT
                        break;
                    }
                    else
                    {
                        continue; // es true seguir nomas
                    }
                }
                if (subObjetivo == FAIL) // fuerza el BT 
                {
                    cuerpoSatisfecho = false;
                    break;
                }
                if (!Resolver(subObjetivo)) // solo cuando falla hace BT
                {
                    cuerpoSatisfecho = false;
                    break;
                }
            }
            if (hayCutAntesQuePeso)
            {
                continue;
            }
            if (cuerpoSatisfecho)
            {
                return true;
            }
            if (hayCutLocal)
            {
                return false;
            }
            Vueltas++;
        }
        return false;
    }

    public void DescargarPrograma()
    {
        baseDeConocimiento.Clear();
    }
}
