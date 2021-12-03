using System.Collections.Generic;

namespace SimulacionTallerMarcos.Entidades
{
    public class Configuracion
    {
        public static List<Grupo> Grupos { get; set; }
        public static int MinTiempoLlegada { get; set; }
        public static int MaxTiempoLlegada { get; set; }

        public static int CantCarpinteros { get; set; }

        public static void EstablecerConfiguracion()
        {
            Grupos = new List<Grupo>();
            Grupos.Add(new Grupo(4, 60));
            Grupos.Add(new Grupo(6, 40));

            MinTiempoLlegada = 180;
            MaxTiempoLlegada = 420;

            CantCarpinteros = 5;
        }
    }
}
