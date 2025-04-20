using MacroBalance.Database;
using MacroBalance.Service;
using System;
using System.Linq;

public static class NotificadorService
{
    public static void EjecutarNotificaciones()
    {
        using (var db = new MacroBalanceEntities())
        {
            var ahora = DateTime.Now.TimeOfDay;

            var recordatorios = db.Recordatorios
                .Where(r => r.Hora != null &&
                            r.Hora.Value.Hours == ahora.Hours &&
                            r.Hora.Value.Minutes == ahora.Minutes)
                .ToList();

            foreach (var recordatorio in recordatorios)
            {
                if (recordatorio.Usuario != null && !string.IsNullOrEmpty(recordatorio.Usuario.CorreoElectronico))
                {
                    string asunto = $"Recordatorio: {recordatorio.Nombre}";
                    string mensaje = ObtenerMensajePersonalizado(recordatorio.Nombre, recordatorio.Usuario.Nombre);

                    CorreoService.Enviar(
                        recordatorio.Usuario.CorreoElectronico,
                        asunto,
                        mensaje
                    );
                }
            }
        }
    }

    private static string ObtenerMensajePersonalizado(string nombreRecordatorio, string nombreUsuario)
    {
        switch (nombreRecordatorio)
        {
            case "Registro de Comidas":
                return $@"
¡Hola {nombreUsuario}!

Este es un recordatorio amistoso de MacroBalance para registrar tu comida.

Llevar un seguimiento constante de tus hábitos alimenticios te ayudará a alcanzar tus metas de salud más rápido y de forma más efectiva. ¡No dejes pasar el momento!

Haz clic aquí para registrar lo que comiste hoy 🥗

¡Sigue así, vas por buen camino!
Equipo de MacroBalance";

            case "Ejercicio Físico":
                return $@"
¡Hola {nombreUsuario}!

Es hora de activar tu cuerpo. ¡Tu salud te lo agradecerá!

Recordá que una rutina constante de ejercicio físico es clave para mejorar tu energía, tu estado de ánimo y tu bienestar general.

¡No lo pienses mucho! Ponete tu ropa deportiva y a moverse 💪

Seguimos contigo,
MacroBalance";

            case "Calorías Máximas":
                return $@"
¡Hola {nombreUsuario}!

Este es tu recordatorio para verificar tus calorías diarias.

Asegurate de mantener tu consumo dentro del objetivo para alcanzar tu meta sin perder de vista tu salud.

Podés revisar y ajustar tu alimentación en la app cuando gustés.

¡Tu constancia está marcando la diferencia!
El equipo de MacroBalance";

            default:
                return $@"
¡Hola {nombreUsuario}!

Este es un recordatorio de MacroBalance para seguir enfocado en tus objetivos de bienestar.

Recordá que cada pequeño paso suma. ¡Seguí así!

Saludos,
MacroBalance";
        }
    }
}
