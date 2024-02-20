using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoDto
{
    public class Salida
    {
        // Modelo de los datos de salida, resultado para enviar a UI
        public string TipoCliente { get; set; }
        public double CuotaMensual { get; set; }
        public int CantidadMeses { get; set; }
        public double MontoComision { get; set; }
        public string PerfilRiesgo { get; set; }
        public string CriterioRiesgo { get; set; }
    }
}
