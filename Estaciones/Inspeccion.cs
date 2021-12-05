using SimulacionTallerMarcos.Entidades;
using System.Collections.Generic;
using System.Timers;

namespace SimulacionTallerMarcos.Estaciones
{
    public class Inspeccion
    {
        public Marco MarcoEnInspeccion { get; set; }
        public Queue<Marco> MarcosPorInspeccionar { get; set; }
        public Queue<Marco> MarcosInspeccionados { get; set; }
        public Pintura Pintura { get; set; }
        public Recepcion Recepcion { get; set; }
        private int MinutosConMarcoActual { get; set; }
        public int TotalMinutosInspeccionados { get; set; }
        public int CantMarcosInspeccionados { get; set; }
        public List<HistorialFalloMarcos> HistorialFalloMarcos { get; set; }
        public Inspeccion(Pintura pintura, Recepcion recepcion)
        {
            MarcosPorInspeccionar = new();
            MarcosInspeccionados = new();
            HistorialFalloMarcos = new();
            Pintura = pintura;
            Recepcion = recepcion;
        }

        public void Inspeccionar()
        {
            if (MarcoEnInspeccion != null)
            {
                MinutosConMarcoActual++;
                TotalMinutosInspeccionados++;

                if (MinutosConMarcoActual == MarcoEnInspeccion.MinutosParaInspeccion)
                {
                    MinutosConMarcoActual = 0;
                    CantMarcosInspeccionados++;

                    if (Utilidades.AleatorioEntre(1, 100) < Configuracion.ProbAciertoInspeccion) //Pasó la inspección
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
            else if (MarcoEnInspeccion == null && MarcosPorInspeccionar.Count > 0)
            {
                MarcoEnInspeccion = MarcosPorInspeccionar.Dequeue();
                MarcoEnInspeccion.MinutosParaInspeccion = Utilidades.AleatorioEntre(Configuracion.MinTiempoInspeccion, Configuracion.MaxTiempoInspeccion);
            }

            Marco proxMarco = Pintura.EntregarMarcoPintado();

            if (proxMarco != null)
            {
                MarcosPorInspeccionar.Enqueue(proxMarco);
            }
        }

        public void AgregarCasoMarco(int cantFallada)
        {
            HistorialFalloMarcos historial = HistorialFalloMarcos.Find(h => h.NumFallas == cantFallada);

            if(historial != null)
            {
                historial.CantFallasAcumulado++;
                historial.CantFallasUnico++;
            }
            else
            {
                historial = new HistorialFalloMarcos(cantFallada);
                HistorialFalloMarcos.Add(historial);
            }

            if (cantFallada >= 2)
            {
                int casoAnterior = HistorialFalloMarcos.IndexOf(historial) - 1;
                HistorialFalloMarcos[casoAnterior].CantFallasUnico--;
            }
        }

        public Marco EntregarMarcoInspeccionado()
        {
            Marco marco = null;

            if (MarcosInspeccionados.Count > 0)
            {
                marco = MarcosInspeccionados.Dequeue();
            }

            return marco;
        }

        public int ObtenerMinutosInspeccionActual()
        {
            return MinutosConMarcoActual;
        }

    }
}
