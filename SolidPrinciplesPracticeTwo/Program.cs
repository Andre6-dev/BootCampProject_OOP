namespace SolidPrinciplesPracticeTwo;

class Program
{

    // Principio de Responsabilidad Unica
    public interface INotificacionService
    {
        void EnviarNotificacion(string mensaje, string destinatario);
    }

    // El principio de Sustitucion de Liskov
    public class EmailService : INotificacionService
    {
        public void EnviarNotificacion(string mensaje, string destinatario)
        {
            Console.WriteLine($"Enviando email a {destinatario}: {mensaje}");
            // Logica para enviar el email
        }
    }

    public class SmsService : INotificacionService
    {
        public void EnviarNotificacion(string mensaje, string destinatario)
        {
            Console.WriteLine($"Enviando SMS a {destinatario}: {mensaje}");
        }
    }

    // Inversion de Dependencias
    public class NotificationManager
    {
        private readonly INotificacionService _notificacionService;

        public NotificationManager(INotificacionService notificacionService)
        {
            _notificacionService = notificacionService;
        }

        public void NotificarUsuario(string mensaje, string destinatario)
        {
            _notificacionService.EnviarNotificacion(mensaje, destinatario);
        }
    }
    
    
    
    static void Main(string[] args)
    {
        
    }
}