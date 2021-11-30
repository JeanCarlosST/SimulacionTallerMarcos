using SimulacionTallerMarcos.Entidades;
using System.Collections.Generic;
using System.Timers;

namespace SimulacionTallerMarcos.Estaciones
{
    public class Inspeccion
    {
        public Timer TemporizadorInspeccion { get; set; }
        public Marco MarcoEnInspeccion { get; set; }
        public Queue<Marco> MarcosPorInspeccionar { get; set; }
        public Queue<Marco> MarcosInspeccionados { get; set; }
        public Pintura Pintura { get; set; }
        public Carpinteria Carpinteria { get; set; }
        private int MinutosConMarcoActual { get; set; }

        public Inspeccion(Pintura pintura, Carpinteria carpinteria)
        {
            MarcosPorInspeccionar = new();
            MarcosInspeccionados = new();
            Pintura = pintura;
            Carpinteria = carpinteria;
            TemporizadorInspeccion = new Timer(Utilidades.VelocidadSimulacion);
            TemporizadorInspeccion.Elapsed += TemporizadorInspeccion_Elapsed;
        }

        private void TemporizadorInspeccion_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(MarcoEnInspeccion != null)
            {
                MinutosConMarcoActual++;

                if(MinutosConMarcoActual == MarcoEnInspeccion.MinutosParaInspeccion)
                {
                    MinutosConMarcoActual = 0;

                    if(Utilidades.AleatorioEntre(1,10) > 1) //PasZó la inspección
                    {
                        MarcoEnInspeccion.Estado = EstadoMarco.Correcto;
                    }
                    else
                    {
                        MarcoEnInspeccion.VecesRetrabajado++;
                        MarcoEnInspeccion.Estado = EstadoMarco.Defectuoso;
                    }

                    MarcosInspeccionados.Enqueue(MarcoEnInspeccion);
                    MarcoEnInspeccion = null;
                }
            }
            else if(MarcoEnInspeccion == null && MarcosPorInspeccionar.Count > 0)
            {
                MarcoEnInspeccion = MarcosPorInspeccionar.Dequeue();
                MarcoEnInspeccion.MinutosParaInspeccion = Utilidades.AleatorioEntre(10, 30);
            }

            Marco proxMarco = Pintura.EntregarMarcoPintado();

            if(proxMarco != null)
            {
                MarcosPorInspeccionar.Enqueue(proxMarco);
            }    
        }

        public Marco EntregarMarcoInspeccionado()
        {
            Marco marco = null;

            if(MarcosInspeccionados.Count > 0)
            {
                marco = MarcosInspeccionados.Dequeue();
            }

            return marco;
        }

        public void ComenzarTemporizador()
        {
            TemporizadorInspeccion.Enabled = true;
        }

        public void PausarTemporizador()
        {
            TemporizadorInspeccion.Enabled = false;
        }
        public void CambiarVelocidad()
        {
            TemporizadorInspeccion.Interval = Utilidades.VelocidadSimulacion;
        }
    }
}
