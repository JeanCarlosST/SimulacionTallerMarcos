using SimulacionTallerMarcos.Entidades;
using System.Collections.Generic;
using System.Timers;

namespace SimulacionTallerMarcos.Estaciones
{
    public class Empaquetadora
    {
        public Queue<Marco> MarcosPorEmpaquetar { get; set; }
        public Queue<Marco> MarcosEmpaquetados { get; set; }
        public Marco MarcoEnEmpaquetado { get; set; }
        public int MinutosConMarcoActual { get; set; }
        public int TotalMinutosEmpaquetando { get; set; }
        public int TotalMarcosEmpaquetados { get; set; }
        public Inspeccion Inspeccion { get; set; }

        public Empaquetadora(Inspeccion inspeccion)
        {
            Inspeccion = inspeccion;
            MarcosPorEmpaquetar = new();
            MarcosEmpaquetados = new();
        }

        public void Empaquetar()
        {
            if(MarcoEnEmpaquetado != null && MarcoEnEmpaquetado.Estado == EstadoMarco.Empacando)
            {
                MinutosConMarcoActual++;
                TotalMinutosEmpaquetando++;

                if(MarcoEnEmpaquetado.MinutosParaEmpaquetar == MinutosConMarcoActual)
                {
                    MinutosConMarcoActual = 0;
                    TotalMarcosEmpaquetados++;

                    MarcoEnEmpaquetado = null;
                }
            }
            else if(MarcoEnEmpaquetado == null && MarcosPorEmpaquetar.Count > 0)
            {
                MarcoEnEmpaquetado = MarcosPorEmpaquetar.Dequeue();
                MarcoEnEmpaquetado.Estado = EstadoMarco.Empacando;
                MarcoEnEmpaquetado.MinutosParaEmpaquetar = Utilidades.AleatorioEntre(10, 15);
            }

            Marco proxMarco = Inspeccion.EntregarMarcoInspeccionado();

            if (proxMarco != null)
            {
                MarcosPorEmpaquetar.Enqueue(proxMarco);
            }
        }
    }
}
