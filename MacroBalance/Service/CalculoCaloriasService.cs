using System;

namespace MacroBalance.Service
{
    public static class CalculoCaloriasService
    {
        public static double CalcularTDEE(double pesoKg, double alturaCm, int edad, string genero, string nivelActividad)
        {
            double tmb;
            if (!string.IsNullOrEmpty(genero) && genero.ToLower().Contains("masc"))
            {
                tmb = (10 * pesoKg) + (6.25 * alturaCm) - (5 * edad) + 5;
            }
            else
            {
                tmb = (10 * pesoKg) + (6.25 * alturaCm) - (5 * edad) - 161;
            }

            double factor = ObtenerFactorActividad(nivelActividad);
            return tmb * factor;
        }

        private static double ObtenerFactorActividad(string nivelActividad)
        {
            if (string.IsNullOrWhiteSpace(nivelActividad))
                return 1.2;

            switch (nivelActividad.ToLower())
            {
                case "sedentario": return 1.2;
                case "ligero": return 1.375;
                case "moderado": return 1.55;
                case "activo": return 1.725;
                case "muy activo": return 1.9;
                default: return 1.2;
            }
        }
    }
}
