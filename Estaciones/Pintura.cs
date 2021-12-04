using SimulacionTallerMarcos.Entidades;
using System;
using System.Collections.Generic;
using System.Timers;

namespace SimulacionTallerMarcos.Estaciones
{
    public class Pintura
    {
        public Queue<Marco> MarcosEnEspera { get; set; }
        public Marco MarcoEnProceso { get; set; }
        private Marco MarcoPintado { get; set; }
        public int MinutosConMarcoEnProceso { get; set; }
        public int MinutosEnMantenimiento { get; set; }
        public int MinutosParaMantenimiento { get; set; }
        public int MarcosPintados { get; set; }
        public int VecesEnMantenimiento { get; set; }
        public int TotalMinutosPintando { get; set; }
        public int TotalMinutosEnMantenimiento { get; set; }
        public enum EstadoMaquinaPintura { Funcionando, Mantenimiento }
        public EstadoMaquinaPintura Estado { get; set; }
        public Almacen Almacen { get; set; }

        public Pintura(Almacen almacen)
        {
            MarcosEnEspera = new Queue<Marco>();
            Estado = EstadoMaquinaPintura.Funcionando;
            Almacen = almacen;
        }

        public Marco EntregarMarcoPintado()
        {
            Marco temp = MarcoPintado;
            MarcoPintado = null;
            return temp;
        }

        public void Pintar()
        {
            if(Estado == EstadoMaquinaPintura.Mantenimiento)
            {
                RealizarMantenimiento();
            }
            else if(Estado == EstadoMaquinaPintura.Funcionando)
            {
                if(MarcoEnProceso != null && MarcoEnProceso.Estado != EstadoMarco.Pintado)
                {
                    MinutosConMarcoEnProceso++;
                    MarcoEnProceso.MinutosTrabajados++;
                    TotalMinutosPintando++;

                    if (MarcoEnProceso.MinutosParaPintado == MinutosConMarcoEnProceso && MarcoPintado == null)
                    {
                        MarcosPintados++;
                        MinutosConMarcoEnProceso = 0;

                        MarcoEnProceso.Estado = EstadoMarco.Pintado;
                        MarcoPintado = MarcoEnProceso;
                        MarcoEnProceso = null;

                        if(MarcosPintados % Configuracion.CantMarcosParaMantenimiento == 0)
                        {
                            MinutosParaMantenimiento = Utilidades.AleatorioEntre(2, 4) * 15;
                            Estado = EstadoMaquinaPintura.Mantenimiento;
                            VecesEnMantenimiento++;
                        }
                    }
                }
                else if(MarcoEnProceso == null && MarcosEnEspera.Count > 0)
                {
                    MarcoEnProceso = MarcosEnEspera.Dequeue();
                    MarcoEnProceso.Estado = EstadoMarco.Pintando;
                }
            }

            AgregarMarcosACola(Almacen.ObtenerMarcosSecos());
        }

        private void RealizarMantenimiento()
        {
            MinutosEnMantenimiento++;
            TotalMinutosEnMantenimiento++;

            if(MinutosEnMantenimiento >= MinutosParaMantenimiento)
            {
                MinutosEnMantenimiento = 0;
                Estado = EstadoMaquinaPintura.Funcionando;
            }
        }

        private void AgregarMarcosACola(List<Marco> marcos)
        {
            if(marcos != null)
            {
                foreach(Marco marco in marcos)
                {
                    MarcosEnEspera.Enqueue(marco);
                }
            }
        }
    }
}
