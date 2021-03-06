namespace SimulacionTallerMarcos.Entidades
{
    public enum EstadoMarco { 
        Desensamblado, 
        Ensamblando,
        EnsambladoPegamentoFresco, 
        EnAlmacen,
        EnsambladoPegamentoSeco,
        Pintando,
        Pintado,
        Correcto,
        Defectuoso,
        Empacando,
        Empacado
    }

    public class Marco
    {
        public int MinutosTrabajados { get; set; }
        public int MinutosEnAlmacen { get; set; }
        public int MinutosEnEnsamblaje { get; set; }
        public int MinutosParaPintado { get; set; }
        public int MinutosParaEnsamblaje { get; set; }
        public int MinutosParaInspeccion { get; set; }
        public int MinutosParaEmpaquetar { get; set; }
        public EstadoMarco Estado { get; set; }
        public int VecesRetrabajado { get; set; }

        public Marco()
        {
            Estado = EstadoMarco.Desensamblado;
            MinutosParaEnsamblaje = Utilidades.AleatorioEntre(Configuracion.MinTiempoTrabajarMarco, Configuracion.MaxTiempoTrabajarMarco) * 60;
            MinutosParaPintado = Utilidades.AleatorioEntre(Configuracion.MinTiempoPintado, Configuracion.MaxTiempoPintado);
        }
    }
}
