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
        public Recepcion Recepcion { get; set; }
        private int MinutosConMarcoActual { get; set; }
        public int TotalMinutosInspeccionados { get; set; }
        public int CantMarcosInspeccionados { get; set; }
        public List<int> MarcosDefectuosos { get; set; }
        public Inspeccion(Pintura pintura, Recepcion recepcion)
        {
            MarcosPorInspeccionar = new();
            MarcosInspeccionados = new();
            MarcosDefectuosos = new();
            Pintura = pintura;
            Recepcion = recepcion;
            TemporizadorInspeccion = new Timer(Utilidades.VelocidadSimulacion);
            TemporizadorInspeccion.Elapsed += TemporizadorInspeccion_Elapsed;
        }

        private void TemporizadorInspeccion_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(MarcoEnInspeccion != null)
            {
                MinutosConMarcoActual++;
                TotalMinutosInspeccionados++;

                if (MinutosConMarcoActual == MarcoEnInspeccion.MinutosParaInspeccion)
                {
                    MinutosConMarcoActual = 0;
                    CantMarcosInspeccionados++;

                    if(Utilidades.AleatorioEntre(1,10) > 1) //Pasó la inspección
                    {
                        MarcoEnInspeccion.Estado = EstadoMarco.Correcto;
                        MarcosInspeccionados.Enqueue(MarcoEnInspeccion);
                    }
                    else
                    {
                        MarcoEnInspeccion.VecesRetrabajado++;
                        MarcoEnInspeccion.Estado = EstadoMarco.Defectuoso;
                        Recepcion.RecibirMarcoDefectuoso(MarcoEnInspeccion);
                    }

                    AgregarCasoMarco(MarcoEnInspeccion.VecesRetrabajado);
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

        public void AgregarCasoMarco(int cantFallada)
        {
            if (cantFallada + 1 > MarcosDefectuosos.Count)
            {
                MarcosDefectuosos.Add(1);
            }
            else
            {
                MarcosDefectuosos[cantFallada]++;
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

        public int ObtenerMinutosInspeccionActual()
        {
            return MinutosConMarcoActual;
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
