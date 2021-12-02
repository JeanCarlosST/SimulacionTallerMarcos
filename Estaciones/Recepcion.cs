using SimulacionTallerMarcos.Entidades;
using System;
using System.Collections.Generic;

namespace SimulacionTallerMarcos.Estaciones
{
    public class Recepcion
    {
        private Queue<Marco> Marcos { get; set; } = new();
        public int CantGruposDeCuatro { get; set; }
        public int CantGruposDeSeis { get; set; }

        public void GenerarMarcos()
        {
            int cantMarcos;
            int prob = Utilidades.AleatorioEntre(1,10);
            
            if(prob <= 4)
            {
                cantMarcos = 6;
                CantGruposDeSeis++;
            }
            else
            {
                cantMarcos = 4;
                CantGruposDeCuatro++;
            }

            for(int i = 0; i < cantMarcos; i++)
            {
                Marco marco = new Marco();
                Marcos.Enqueue(marco);
            }
        }

        public int ObtenerCantMarcos()
        {
            return Marcos.Count;
        }

        public void RecibirMarcoDefectuoso(Marco marco)
        {
            Marcos.Enqueue(marco);
        }

        public Marco ObtenerSigteMarco()
        {
            if(ObtenerCantMarcos() > 0 )
            {
                return Marcos.Dequeue();
            }

            return null;
        }
    }
}
