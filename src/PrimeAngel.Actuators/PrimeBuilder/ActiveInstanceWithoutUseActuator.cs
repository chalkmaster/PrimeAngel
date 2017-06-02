using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using PrimeAngel.Actuators.Notify;
using PrimeAngel.Infrastructure.Verbose.TextFormartters;
using PrimeAngel.Perceptions;
using PrimeAngel.Perceptions.PrimeBuilder;
using PrimeAngel.PersistentMemory;

namespace PrimeAngel.Actuators.PrimeBuilder
{
    public class ActiveInstanceWithoutUseActuator: Actuator
    {
        private readonly string _mode;

        public ActiveInstanceWithoutUseActuator(string mode = "auto")
        {
            _mode = mode;
        }

        protected override IList<IAction> FindActionList(IList<IPerception> perceptions)
        {
            return perceptions.Any(p => p is ActiveInstancesWithoutUsePerception)
                ? _mode == "auto" ? new IAction[] {new EmailNotificationAction(GetMailMessage(perceptions))}
                : new IAction[] { new ConsoleAction(perceptions) }
                : new IAction[] {new NothingToDoAction()};
        }

        private static MailMessage GetMailMessage(IEnumerable<IPerception> perceptions)
        {
            //const string to = @"charles.fortes@primesystems.com.br; pssc@primesystems.com.br; roberto.azevedo@primesystems.com.br";
            const string to = @"charles.fortes@primesystems.com.br";

            var message = new MailMessage(@"angel.prime@primesystems.com.br", to);
            message.IsBodyHtml = true;
            message.Subject = "Instancias ativas sem uso no OSMobile";
            var sb = new StringBuilder();

            sb.Append("<html><body>");
            sb.Append(GetWelcomePhrase());
            perceptions.ToList().ForEach(p => sb.Append(p.Verbose(new HtmlTableTextFormarter())));
            sb.Append("</body></html>");
            sb.Append(GetRegardsPhrase());

            message.Body = sb.ToString();

            return message;
        }

        private static string GetRegardsPhrase()
        {
            return "<p>Atenciosamente,<br /><br />Angel</p>";
        }

        private static string GetWelcomePhrase()
        {
            return @"<p>Olá Amigos,</p>
                    <br />
                    <p>Encontrei algumas empresas na base que estão ativas mas tiveram zero de uso na aplicação.
                    <br />
                    Acredito que elas merecem atenção, vocês podem por favor solicitar à área de operações que verifique se 
                    elas deveriam realmente estar ativas ou se estão corretamentes classificadas no sistema como empresa de cliente?
                    <br /><br />
                    Caso estejam corretas, informem por favor a área de farmer/comercial.
                    </P>";
        }

        protected override IList<ActionResult> Act(IList<IAction> actions)
        {
            var actionsResult = new List<ActionResult>();

            actions.ToList().ForEach(a => actionsResult.Add(a.Act()));

            return actionsResult;
        }

        protected override void Learn(IList<ActionResult> toEvaluate, IMemory mainMemory)
        {
            
        }
    }
}
