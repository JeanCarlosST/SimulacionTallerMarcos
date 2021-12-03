namespace SimulacionTallerMarcos.Entidades
{
    public class Grupo
    {
        public int CantMarcos { get; set; }
        public int Probabilidad { get; set; }

        public Grupo(int cantMarcos, int probabilidad)
        {
            CantMarcos = cantMarcos;
            Probabilidad = probabilidad;
        }
    }
}
