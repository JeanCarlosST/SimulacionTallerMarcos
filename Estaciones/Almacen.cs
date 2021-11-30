using SimulacionTallerMarcos.Entidades;
using System;
using System.Collections.Generic;
using System.Timers;

namespace SimulacionTallerMarcos.Estaciones
{
    public class Almacen
    {
        public List<Marco> MarcosEnEspera { get; set; }
        public List<Marco> MarcosEnAlmacen { get; set; }
        public List<Marco> MarcosSecos { get; set; }
        public Timer Temporizador { get; set; }
        public Carpinteria Carpinteria { get; set; }
        public Pintura Pintura { get; set; }
        public int MaxHistoricoAlmacenado { get; set; }

        public Almacen()
        {
            MarcosEnEspera = new();
            MarcosEnAlmacen = new();
            MarcosSecos = new();

            Temporizador = new Timer(Utilidades.VelocidadSimulacion);
            Temporizador.Elapsed += new ElapsedEventHandler(async (source, e) =>
            {
                foreach(Marco marco in MarcosEnAlmacen)
                {
                    if(marco != null)
                    {
                        marco.MinutosEnAlmacen++;
                        marco.MinutosTrabajados++;

                        if(marco.MinutosEnAlmacen == 24*60)
                        {
                            marco.Estado = EstadoMarco.EnsambladoPegamentoSeco;
                            MarcosSecos.Add(marco);
                        }
                    }
                    else
                    {
                        var a = MarcosEnAlmacen.Find(m => m == null);
                    }
                }

                MarcosEnAlmacen.RemoveAll(m => m != null && m.Estado == EstadoMarco.EnsambladoPegamentoSeco);

                MarcosEnAlmacen.AddRange(MarcosEnEspera);
                MarcosEnEspera.Clear();

                if(MarcosEnAlmacen.Count > MaxHistoricoAlmacenado)
                {
                    MaxHistoricoAlmacenado = MarcosEnAlmacen.Count;
                }
            });
        }

        public void ComenzarTemporizador()
        {
            Temporizador.Enabled = true;
        }

        public void PausarTemporizador()
        {
            Temporizador.Enabled = false;
        }
        public void CambiarVelocidad()
        {
            Temporizador.Interval = Utilidades.VelocidadSimulacion;
        }

        public List<Marco> ObtenerMarcosSecos()
        {
            if(MarcosSecos.Count > 0)
            {
                List<Marco> MarcosSecosCopia = MarcosSecos.FindAll(m => true);
                MarcosSecos.RemoveAll(m => true);

                return MarcosSecosCopia;
            }

            return null;
        }
    }
}
