using SimulacionTallerMarcos.Entidades;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SimulacionTallerMarcos.Estaciones
{
    public class Carpinteria
    {
        public List<Carpintero> Carpinteros { get; set; }
        public Recepcion Recepcion { get; set; }
        public Almacen Almacen { get; set; }

        public Carpinteria()
        {
            Carpinteros = new List<Carpintero>(); 

            for(int i = 0; i < 5; i++)
            {
                Carpintero carpintero = new(i+1);
                Carpinteros.Add(carpintero);
            }
        }

        public void CambiarVelocidad()
        {
            foreach(Carpintero carpintero in Carpinteros)
            {
                carpintero.CambiarVelocidadTrabajo();
            }    
        }

        public void ComenzarATrabajar()
        {
            foreach (Carpintero carpintero in Carpinteros)
            {
                carpintero.ComenzarATrabajar();
            }
        }

        public void PausarTrabajo()
        {
            foreach (Carpintero carpintero in Carpinteros)
            {
                carpintero.PausarTrabajo();
            }
        }

        public void RecibirMarcos(Recepcion recepcion)
        {
            foreach (Carpintero carpintero in Carpinteros)
            {
                if (carpintero.MarcoTrabajando == null)
                {
                    carpintero.RecibirMarco(recepcion.ObtenerSigteMarco());
                }
            }
        }

        public void AlmacenarMarcos(Almacen almacen)
        {
            foreach (Carpintero carpintero in Carpinteros)
            {
                if (carpintero.MarcoTrabajando != null && carpintero.MarcoTrabajando.Estado == EstadoMarco.EnsambladoPegamentoFresco)
                {
                    almacen.MarcosEnEspera.Add(carpintero.EntregarMarco());
                }
            }
        }
    }
}
