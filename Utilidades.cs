using System;

namespace SimulacionTallerMarcos
{
    public class Utilidades
    {
        public const int MIN_VELOCIDAD = 1;
        public const int MAX_VELOCIDAD = 1000;

        public static int VelocidadSimulacion = MAX_VELOCIDAD;

        public static int AleatorioEntre(int min, int max)
        {
            Random ran = new Random();

            return ran.Next(min, max+1);
        }
    }
}
