namespace SimulacionTallerMarcos.Entidades
{
    public class HistorialFalloMarcos
    {
        public int NumFallas { get; set; }
        public int CantFallasAcumulado { get; set; }
        public int CantFallasUnico { get; set; }

        public HistorialFalloMarcos(int numFallas)
        {
            NumFallas = numFallas;
            CantFallasAcumulado = 1;
            CantFallasUnico = 1;
        }
    }
}
