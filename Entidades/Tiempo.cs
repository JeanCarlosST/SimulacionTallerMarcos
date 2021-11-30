namespace SimulacionTallerMarcos.Entidades
{
    public class Tiempo
    {
        public int Semanas { get; set; }
        public int Dias { get; set; }
        public int Horas { get; set; }
        public int Minutos { get; set; }

        public void Avanzar()
        {
            Minutos++;

            if(Minutos == 60)
            {
                Minutos = 0;
                Horas++;
                if(Horas == 24)
                {
                    Horas = 0;
                    Dias++;
                    if(Dias == 7)
                    {
                        Dias = 0;
                        Semanas++;
                    }    
                }
            }
        }

        public override string ToString()
        {
            string tiempo = "";

            if (Semanas > 0)
            {
                tiempo += $"{Semanas.ToString("00")}S ";
                tiempo += $"{Dias.ToString("00")}D ";
            }
            if (Semanas == 0 && Dias > 0)
                tiempo += $"{Dias.ToString("00")}D ";

            tiempo += $"{Horas.ToString("00")}H {Minutos.ToString("00")}M";

            return tiempo;
        }
    }
}
