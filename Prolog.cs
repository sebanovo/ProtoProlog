using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoProlog;

class Prolog
{
    private List<Regla> reglas = [];
    private string FAIL = "fail";
    private string CUT = "!";

    public void CargarPrograma(string programa)
    {
        var lineas = programa.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);

        foreach (var linea in lineas)
        {
            if (linea.Contains('%')) continue;
            var texto = linea.Trim().TrimEnd('.');
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

    public void MostrarReglas()
    {
        string output = "";
        foreach (var regla in reglas)
        {
            output += regla.ToString() + "\n";
        }
        MessageBox.Show(output);
    }

    public bool Consultar(string objetivo)
    {
        var cuerpo = objetivo.TrimEnd('.').Split(',').Select(p => p.Trim()).ToList();

        Regla regla = new();
        regla.Cabeza = "";
        regla.Cuerpo = cuerpo;

        foreach(var o in regla.Cuerpo)
        {
            bool hayCut = false;
            if (!Resolver(o, ref hayCut)) return false;
        }
        return true;
    }

    public bool Resolver(string objetivo, ref bool  hayCut)
    {
        foreach (var regla in reglas)
        {
            if (regla.Cabeza != objetivo) continue;

            if (regla.EsHecho())
            {
                return true;
            }
            else
            {
                bool cutEnEstaRama = false;
                if (ResolverCuerpo(regla.Cuerpo, 0, ref cutEnEstaRama))
                {
                    hayCut &= cutEnEstaRama;
                    return true;
                }

                if(cutEnEstaRama)
                {
                    hayCut = true;
                    return false;
                }
            }
        }

        return false; 
    }

    private bool ResolverCuerpo(List<string> cuerpo, int indice, ref bool hayCut)
    {
        if (indice >= cuerpo.Count)
            return true;
        string submeta = cuerpo[indice];
        if(submeta == CUT)
        {
            hayCut = true;
            return ResolverCuerpo(cuerpo, indice + 1, ref hayCut);
        }
        if(submeta == FAIL)
            return false; 
        if (Resolver(submeta, ref hayCut))
        {
            return ResolverCuerpo(cuerpo, indice + 1, ref hayCut);
        }
        return false;
    }
    public void DescargarPrograma()
    {
        reglas.Clear();
    }
}
