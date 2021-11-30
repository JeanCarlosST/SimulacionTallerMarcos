﻿using SimulacionTallerMarcos.Entidades;
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
        public Timer TemporizadorMaquina { get; set; }
        public enum EstadoMaquinaPintura { Funcionando, Mantenimiento }
        public EstadoMaquinaPintura Estado { get; set; }
        public Almacen Almacen { get; set; }

        public Pintura(Almacen almacen)
        {
            MarcosEnEspera = new Queue<Marco>();
            Estado = EstadoMaquinaPintura.Funcionando;
            TemporizadorMaquina = new Timer(Utilidades.VelocidadSimulacion);
            TemporizadorMaquina.Elapsed += TemporizadorMaquina_Elapsed;
            Almacen = almacen;
        }

        public Marco EntregarMarcoPintado()
        {
            Marco temp = MarcoPintado;
            MarcoPintado = null;
            return temp;
        }

        private void TemporizadorMaquina_Elapsed(object sender, ElapsedEventArgs e)
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

                    if (MarcoEnProceso.MinutosParaPintado == MinutosConMarcoEnProceso && MarcoPintado == null)
                    {
                        MarcosPintados++;
                        MinutosConMarcoEnProceso = 0;

                        MarcoEnProceso.Estado = EstadoMarco.Pintado;
                        MarcoPintado = MarcoEnProceso;
                        MarcoEnProceso = null;

                        if(MarcosPintados % 20 == 0)
                        {
                            MinutosParaMantenimiento = Utilidades.AleatorioEntre(2, 4) * 15;
                            Estado = EstadoMaquinaPintura.Mantenimiento;
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

            if(MinutosEnMantenimiento >= MinutosParaMantenimiento)
            {
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

        public void ComenzarTemporizador()
        {
            TemporizadorMaquina.Enabled = true;
        }

        public void PausarTemporizador()
        {
            TemporizadorMaquina.Enabled = false;
        }
        public void CambiarVelocidad()
        {
            TemporizadorMaquina.Interval = Utilidades.VelocidadSimulacion;
        }
    }
}
