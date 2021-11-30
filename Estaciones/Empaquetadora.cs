using SimulacionTallerMarcos.Entidades;
using System.Collections.Generic;
using System.Timers;

namespace SimulacionTallerMarcos.Estaciones
{
    public class Empaquetadora
    {
        public Timer TemporizadorInspeccion { get; set; }
        public Queue<Marco> MarcosPorEmpaquetar { get; set; }
        public Queue<Marco> MarcosEmpaquetados { get; set; }
        public Marco MarcoEnEmpaquetado { get; set; }
        public int MinutosConMarcoActual { get; set; }
        public Inspeccion Inspeccion { get; set; }

        public Empaquetadora(Inspeccion inspeccion)
        {
            Inspeccion = inspeccion;
            MarcosPorEmpaquetar = new();
            MarcosEmpaquetados = new();

            TemporizadorInspeccion = new Timer(Utilidades.VelocidadSimulacion);
            TemporizadorInspeccion.Elapsed += TemporizadorInspeccion_Elapsed;
        }

        private void TemporizadorInspeccion_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(MarcoEnEmpaquetado != null && MarcoEnEmpaquetado.Estado == EstadoMarco.Empacando)
            {
                MinutosConMarcoActual++;

                if(MarcoEnEmpaquetado.MinutosParaEmpaquetar == MinutosConMarcoActual)
                {
                    MinutosConMarcoActual = 0;

                    MarcosEmpaquetados.Enqueue(MarcoEnEmpaquetado);
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
