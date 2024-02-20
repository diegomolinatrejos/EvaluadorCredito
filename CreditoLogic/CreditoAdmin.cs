using CreditoDto;
using Newtonsoft.Json.Linq;
using System;

namespace CreditoLogic
{
    public class CreditoAdmin
    {
        public Salida CalculoCredito(Entrada entrada)
        {
            // definir y ajustar parametros de entrada
            string TipoCliente = entrada.TipoCliente;
            double MontoCredito = entrada.MontoCredito;
            double PorcentajeTasaMensual = (entrada.PorcentajeTasaAnual / 12) / 100;
            int PlazoPrestamoMeses = (entrada.PlazoPrestamoAnios) * 12;
            double PorcentajeComision = (entrada.PorcentajeComision) / 100;
            double IngresoNetoMensual = entrada.IngresoNetoMensual;
            double MensualidadTotalPrestamosActuales = entrada.MensualidadTotalPrestamosActuales;
            int CantidadPersonasDependientes = entrada.CantidadPersonasDependientes;
            // variables necesarias para calculos
            double MontoComision = MontoCredito * PorcentajeComision;
            double CuotaMensual = CalcularCuotaMensual(MontoCredito, PorcentajeComision, PorcentajeTasaMensual, PlazoPrestamoMeses);
            double NivelEndeudamiento = Math.Round((((CuotaMensual + MensualidadTotalPrestamosActuales) / IngresoNetoMensual) * 100), 2);

            // definir salida
            Salida salida = new Salida();

            // definir parametros de salida 
            salida.TipoCliente = TipoCliente;
            salida.CuotaMensual = Math.Round(CuotaMensual, 2);
            salida.CantidadMeses = PlazoPrestamoMeses;
            salida.MontoComision = Math.Round(MontoComision, 2);
            salida.PerfilRiesgo = PerfilRiesgo(TipoCliente, NivelEndeudamiento, CantidadPersonasDependientes);
            salida.CriterioRiesgo = CriterioRiesgo(CantidadPersonasDependientes, NivelEndeudamiento, salida.PerfilRiesgo);

            // retorno de salida que recibira CreditoApi para enviar a UI
            return salida;
        }

        // Cálculo de Mensualidad
        public double CalcularCuotaMensual(double MontoCredito, double MontoComision, double PorcentajeTasaMensual, int PlazoPrestamoMeses)
        {
            // calculo de monto solicitado mas comisión 
            double P = MontoCredito + MontoComision;

            // calculo de Cuota Mensual
            double CuotaMensual = (P * PorcentajeTasaMensual) / (1 - Math.Pow((1 + PorcentajeTasaMensual), -PlazoPrestamoMeses));

            return CuotaMensual;
        }

        // Calculo de Nivel Perfil de riesgo
        public string PerfilRiesgo(string TipoCliente, double NivelEndeudamiento, int CantidadPersonasDependientes)
        {
            // se determina el riesgo en funsion del nivel de endeudamiento
            // Si aplica el nivel se ve afectado por cantidad de dependientes con llamando al metodo CalculoDependientes(); 
            string PerfilRiesgo ="";

            switch(TipoCliente)
            {
                case "Físico":
                    if (NivelEndeudamiento > 50)
                    {
                        PerfilRiesgo = "Alto";
                        PerfilRiesgo = CalculoDependientes(PerfilRiesgo, CantidadPersonasDependientes);
                    }
                    else if (NivelEndeudamiento >= 30 && NivelEndeudamiento <= 50)
                    {
                        PerfilRiesgo = "Medio";
                        PerfilRiesgo = CalculoDependientes(PerfilRiesgo, CantidadPersonasDependientes);
                    }
                    else
                    {
                        PerfilRiesgo = "Bajo";
                        PerfilRiesgo = CalculoDependientes(PerfilRiesgo, CantidadPersonasDependientes);
                    }
                    break;

                case "Jurídico":
                    if (NivelEndeudamiento > 30)
                    {
                        PerfilRiesgo = "Alto";
                    }
                    else if (NivelEndeudamiento >= 10 && NivelEndeudamiento <= 30)
                    {
                        PerfilRiesgo = "Medio";
                    }
                    else
                    {
                        PerfilRiesgo = "Bajo";
                    }
                    break;

            }
            return PerfilRiesgo;
        }

        // Calculo de como afecta la cantidad de personas dependientes sumará o restará un nivel al nivel de riesgo 
        public string CalculoDependientes(string PerfilRiesgo, int CantidadPersonasDependientes)
        {
            if (CantidadPersonasDependientes == 0)
            {
                if (PerfilRiesgo=="Alto")
                {
                    PerfilRiesgo = "Medio";
                }
                else if (PerfilRiesgo=="Medio")
                {
                    PerfilRiesgo = "Bajo";
                }
            }
            else if (CantidadPersonasDependientes >= 5)
            {
                if (PerfilRiesgo == "Medio")
                {
                    PerfilRiesgo = "Alto";
                }
                else if (PerfilRiesgo == "Bajo")
                {
                    PerfilRiesgo = "Medio";
                }
            }

            return PerfilRiesgo;
        }

        // Determinar mensaje que justifica como afecta elnivel endeudamiento y la cantidad de personas dependientes para el criterio de riesgo
        public string CriterioRiesgo(int CantidadPersonasDependientes, double NivelEndeudamiento, string PerfilRiesgo)
        {
            string CriterioRiesgo;

            // Programar un IF que recibe la cantidad de dependientes
            if (CantidadPersonasDependientes == 0)
            {
                CriterioRiesgo = $"El perfil de riesgo es: {PerfilRiesgo} debido a que el nivel de endeudamiento es {NivelEndeudamiento} % del salario mensual, pero no tiene dependientes";
            }
            else if (CantidadPersonasDependientes >= 5)
            {
                CriterioRiesgo = $"El perfil de riesgo es: {PerfilRiesgo} debido a que el nivel de endeudamiento es {NivelEndeudamiento} % del salario mensual, pero tiene {CantidadPersonasDependientes} dependientes de su salario";
            }
            else
            {
                // Opción Default: cantidadDependientes está entre 1 y 4 inclusive (1, 2, 3, 4) no afecta
                CriterioRiesgo = $"El perfil de riesgo es: {PerfilRiesgo} debido a que el nivel de endeudamiento es {NivelEndeudamiento} % del salario mensual";
            }

            return CriterioRiesgo;
        }

    }// fin de la clase 

}// fin del namespace
