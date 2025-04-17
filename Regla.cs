using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoProlog;
class Regla
{
    public List<string> Cuerpo { get; set; } = [];
    public string Cabeza = "";

    public bool EsHecho()
    {
        return Cuerpo.Count == 0;
    }
    public override string ToString()
    {
        if (EsHecho())
            return Cabeza + "."; // Hecho
        return $"{Cabeza} :- {string.Join(", ", Cuerpo)}."; // Regla
    }
}
