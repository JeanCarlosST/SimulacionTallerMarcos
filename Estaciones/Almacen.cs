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
        public Carpinteria Carpinteria { get; set; }
        public Pintura Pintura { get; set; }
        public int MaxHistoricoAlmacenado { get; set; }

        public Almacen()
        {
            MarcosEnEspera = new();
            MarcosEnAlmacen = new();
            MarcosSecos = new();
        }

        public void Almacenar()
        {
            foreach (Marco marco in MarcosEnAlmacen)
            {
                marco.MinutosEnAlmacen++;
                marco.MinutosTrabajados++;

                if (marco.MinutosEnAlmacen >= Configuracion.TiempoSecado * 60)
                {
                    marco.Estado = EstadoMarco.EnsambladoPegamentoSeco;
                    MarcosSecos.Add(marco);
                }
            }

            MarcosEnAlmacen.RemoveAll(m => m.Estado == EstadoMarco.EnsambladoPegamentoSeco);

            MarcosEnAlmacen.AddRange(MarcosEnEspera);
            MarcosEnEspera.Clear();

            if (MarcosEnAlmacen.Count > MaxHistoricoAlmacenado)
            {
                MaxHistoricoAlmacenado = MarcosEnAlmacen.Count;
            }
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
