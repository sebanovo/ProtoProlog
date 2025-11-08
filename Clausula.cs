namespace ProtoProlog;
/*
Ejemplo: gato(tom). (Tom es un gato).

Ejemplo: progenitor(juan, ana). (Juan es progenitor de Ana).

Reglas (Rules): Definen relaciones que son verdaderas si se cumplen otras condiciones. Tienen una cabeza (la conclusión) y un cuerpo (las condiciones), separados por el operador :- (que se lee como "si").
*/
class Clausula
{
    public List<string> Cuerpo { get; set; } = [];
    public string Cabeza = "";
    /// <summary>
    /// <para>
    /// Son afirmaciones que se declaran como verdaderas.
    /// </para>
    /// <para>
    /// Ejemplo:
    /// p.
    /// </para>
    /// </summary>
    public bool EsHecho()
    {
        return Cuerpo.Count == 0;
    }

    /// <summary>
    /// <para>
    /// Definen relaciones que son verdaderas si se cumplen otras condiciones. Tienen una cabeza y un cuerpo, separados por el operador :- 
    /// </para>
    /// <para>
    /// Ejemplo:
    /// p :- q, r, s.
    /// </para>
    /// </summary>
    public bool EsRegla()
    {
        return !EsHecho();
    }

    public override string ToString()
    {
        if (EsHecho())
            return Cabeza + "."; // Hecho
        return $"{Cabeza} :- {string.Join(", ", Cuerpo)}."; // Regla
    }
}
