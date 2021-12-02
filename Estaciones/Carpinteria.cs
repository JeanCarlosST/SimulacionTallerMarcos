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

        public Carpinteria(Recepcion recepcion, Almacen almacen)
        {
            Carpinteros = new List<Carpintero>(); 

            for(int i = 0; i < 5; i++)
            {
                Carpintero carpintero = new(i+1, recepcion, almacen);
                Carpinteros.Add(carpintero);
            }
        }

        public void Trabajar()
        {
            foreach (Carpintero carpintero in Carpinteros)
            {
                carpintero.TrabajarMarco();
            }
        }
    }
}
