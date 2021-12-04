using System.Collections.Generic;

namespace SimulacionTallerMarcos.Entidades
{
    public class Configuracion
    {
        public static List<Grupo> Grupos { get; set; }
        public static int MinTiempoLlegada { get; set; }
        public static int MaxTiempoLlegada { get; set; }

        public static int CantCarpinteros { get; set; }
        public static int MinTiempoTrabajarMarco { get; set; }
        public static int MaxTiempoTrabajarMarco { get; set; }

        public static int TiempoSecado { get; set; }

        public static int CantMarcosParaMantenimiento { get; set; }
        public static int MinTiempoPintado { get; set; }
        public static int MaxTiempoPintado { get; set; }

        public static int ProbAciertoInspeccion { get; set; }
        public static int MinTiempoInspeccion { get; set; }
        public static int MaxTiempoInspeccion { get; set; }

        public static int MinTiempoEmpaquetado { get; set; }
        public static int MaxTiempoEmpaquetado { get; set; }

        public static void EstablecerConfiguracion()
        {
            Grupos = new List<Grupo>();
            Grupos.Add(new Grupo(4, 60));
            Grupos.Add(new Grupo(6, 40));

            MinTiempoLlegada = 3;
            MaxTiempoLlegada = 7;

            CantCarpinteros = 5;
            MinTiempoTrabajarMarco = 2;
            MaxTiempoTrabajarMarco = 6;

            TiempoSecado = 24;

            CantMarcosParaMantenimiento = 20;
            MinTiempoPintado = 10;
            MaxTiempoPintado = 20;

            ProbAciertoInspeccion = 90;
            MinTiempoInspeccion = 10;
            MaxTiempoInspeccion = 30;

            MinTiempoEmpaquetado = 10;
            MaxTiempoEmpaquetado = 15;
        }
    }
}
