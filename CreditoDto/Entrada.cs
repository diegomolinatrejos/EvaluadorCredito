namespace CreditoDto
{
    public class Entrada
    {
        // Modelo de los datos capturados en formulario de UI
        public string TipoCliente { get; set; }
        public double MontoCredito { get; set; }
        public double PorcentajeTasaAnual { get; set; }
        public int PlazoPrestamoAnios { get; set; }
        public double PorcentajeComision { get; set; }
        public double IngresoNetoMensual { get; set; }
        public double MensualidadTotalPrestamosActuales { get; set; }
        public int CantidadPersonasDependientes { get; set; }
    }
}