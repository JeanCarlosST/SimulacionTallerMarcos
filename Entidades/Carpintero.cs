using SimulacionTallerMarcos.Estaciones;
using System;
using System.Timers;

namespace SimulacionTallerMarcos.Entidades
{
    public class Carpintero
    {
        public int Numero { get; set; }
        public Marco MarcoTrabajando { get; set; }
        public int MinutosTrabajados { get; set; }
        public int CantMarcosTrabajados { get; set; }
        public Recepcion Recepcion { get; set; }
        public Almacen Almacen { get; set; }

        public Carpintero(int numero, Recepcion recepcion, Almacen almacen)
        { 
            Numero = numero;
            Recepcion = recepcion;
            Almacen = almacen;
        }

        public void RecibirMarco()
        {
            Marco marco = Recepcion.ObtenerSigteMarco();

            if(marco != null)
            {
                MarcoTrabajando = marco;
                MarcoTrabajando.Estado = EstadoMarco.Ensamblando;
            }
        }

        private Marco EntregarMarco()
        {
            Marco marco = MarcoTrabajando;
            MarcoTrabajando = null;
            marco.Estado = EstadoMarco.EnAlmacen;
            return marco;
        }

        public void TrabajarMarco()
        {
            if(MarcoTrabajando == null)
            {
                RecibirMarco();
            }
            else if(MarcoTrabajando != null && MarcoTrabajando.Estado == EstadoMarco.Ensamblando)
            {
                MarcoTrabajando.MinutosTrabajados++;
                MarcoTrabajando.MinutosEnEnsamblaje++;
                MinutosTrabajados++;

                if(MarcoTrabajando.MinutosEnEnsamblaje >= MarcoTrabajando.MinutosParaEnsamblaje)
                {
                    MarcoTrabajando.MinutosEnEnsamblaje = 0;
                    MarcoTrabajando.Estado = EstadoMarco.EnsambladoPegamentoFresco;
                    CantMarcosTrabajados++;
                    Almacen.MarcosEnEspera.Add(EntregarMarco());
                }
            }
        }
    }
}
