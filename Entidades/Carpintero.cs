using SimulacionTallerMarcos.Estaciones;
using System;
using System.Timers;

namespace SimulacionTallerMarcos.Entidades
{
    public class Carpintero
    {
        public int Numero { get; set; }
        public Marco MarcoTrabajando { get; set; }
        public int MinutosTrabajados { get; set; }
        public int CantMarcosTrabajados { get; set; }
        private Timer TemporizadorTrabajo { get; set; }

        public Carpintero(int numero)
        {
            Numero = numero;
            TemporizadorTrabajo = new Timer(Utilidades.VelocidadSimulacion);
            TemporizadorTrabajo.Elapsed += TrabajarMarco;
        }

        public void ComenzarATrabajar()
        {
            TemporizadorTrabajo.Enabled = true;
        }

        public void PausarTrabajo()
        {
            TemporizadorTrabajo.Enabled = false;
        }

        public void CambiarVelocidadTrabajo()
        {
            TemporizadorTrabajo.Interval = Utilidades.VelocidadSimulacion;
        }

        public void RecibirMarco(Marco marco)
        {
            if(marco != null)
            {
                MarcoTrabajando = marco;
                MarcoTrabajando.Estado = EstadoMarco.Ensamblando;
            }
        }

        public Marco EntregarMarco()
        {
            Marco marco = MarcoTrabajando;
            MarcoTrabajando = null;
            marco.Estado = EstadoMarco.EnAlmacen;
            return marco;
        }

        public void TrabajarMarco(object source, ElapsedEventArgs e)
        {
            if(MarcoTrabajando != null && MarcoTrabajando.Estado == EstadoMarco.Ensamblando)
            {
                MarcoTrabajando.MinutosTrabajados++;
                MinutosTrabajados++;

                if(MarcoTrabajando.MinutosParaEnsamblaje == MarcoTrabajando.MinutosTrabajados)
                {
                    MarcoTrabajando.Estado = EstadoMarco.EnsambladoPegamentoFresco;
                    CantMarcosTrabajados++;
                }
            }
        }
    }
}
