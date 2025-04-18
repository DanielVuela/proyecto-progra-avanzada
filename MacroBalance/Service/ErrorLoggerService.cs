using System;
using System.Web.Services.Description;
using MacroBalance.Database;
using MacroBalance.Models;   

namespace MacroBalance.Service
{
    public static class ErrorLoggerService
    {
        public static void Log(Exception ex, string modulo, int? usuarioId = null)
        {
            try
            {
                using (var db = new MacroBalanceEntities1())
                {
                    var log = new LogErrores
                    {
                        UsuarioId = usuarioId,
                        Modulo = modulo,
                        MensajeError = ex.Message,
                        Stacktrace = ex.ToString(),
                        FechaHora = DateTime.Now
                    };

                    db.LogErrores.Add(log);
                    db.SaveChanges();
                }
            }
            catch
            {
                Console.WriteLine("Error a la hora de guardar en la tabla LogError");
                Console.WriteLine($"Mensaje no loggeado:{ex.ToString()}");
            }
        }
        public static void Log(string mensaje, string modulo, int? usuarioId = null)
        {
            try
            {
                using (var db = new MacroBalanceEntities1())
                {
                    var log = new LogErrores
                    {
                        UsuarioId = usuarioId,
                        Modulo = modulo,
                        MensajeError = mensaje,
                        Stacktrace = null,
                        FechaHora = DateTime.Now
                    };

                    db.LogErrores.Add(log);
                    db.SaveChanges();
                }
            }
            catch 
            {
                Console.WriteLine("Error a la hora de guardar en la tabla LogError");
                Console.WriteLine($"Mensaje no loggeado:{mensaje}");
            }
        }
    }
}
