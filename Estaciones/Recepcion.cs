using SimulacionTallerMarcos.Entidades;
using System;
using System.Collections.Generic;

namespace SimulacionTallerMarcos.Estaciones
{
    public class Recepcion
    {
        private Queue<Marco> Marcos { get; set; } = new();

        public Dictionary<int, int> CantGrupos { get; set; } = new();

        public void GenerarMarcos()
        {
            int cantMarcos = 4;
            int prob = Utilidades.AleatorioEntre(1,100);
            int probAcumulada = 0;
            
            foreach(Grupo grupo in Configuracion.Grupos)
            {
                probAcumulada += grupo.Probabilidad;

                if(prob < probAcumulada)
                {
                    cantMarcos = grupo.CantMarcos;
                    if (CantGrupos.ContainsKey(cantMarcos))
                    {
                        CantGrupos[cantMarcos]++;
                    }
                    else
                    {
                        CantGrupos.Add(cantMarcos, 1);
                    }
                    break;
                }
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
            Marco marco = null;
            
            if(ObtenerCantMarcos() > 0 )
            {
                return Marcos.Dequeue();
            }

            return null;
        }
    }
}
