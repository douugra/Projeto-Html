using Microsoft.AspNetCore.Mvc;

namespace chatApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private static List<Mensagem> Messages = new List<Mensagem>();

        // Endpoint GET para recuperar todas as mensagens.
        [HttpGet]
        public ActionResult<List<Mensagem>> GetMessages()
        {
            return Messages;
        }

        // Endpoint POST para enviar uma nova mensagem.
        [HttpPost]
        public IActionResult PostMessage([FromBody] Mensagem newMessage)
        {
            if (newMessage == null || string.IsNullOrWhiteSpace(newMessage.Nickname) || string.IsNullOrWhiteSpace(newMessage.Text))
            {
                return BadRequest("Mensagem inv�lida.");
            }

            newMessage.Date = DateTime.Now.ToString("G");
            Messages.Add(newMessage);
            Console.WriteLine($"{newMessage.Nickname}: {newMessage.Text}");

            // Verifica se a mensagem � de um aluno e inicia a l�gica do BOT
            if (newMessage.Nickname.Equals("Aluno", StringComparison.OrdinalIgnoreCase) && newMessage.Text.StartsWith(""))
            {
                string responseMessage = GetBotResponse(newMessage.Text);
                Mensagem botMessage = new Mensagem
                {
                    Nickname = "",
                    Text = responseMessage,
                    Date = DateTime.Now.ToString("G")
                };
                Messages.Add(botMessage);
                Console.WriteLine($"{botMessage.Nickname}: {botMessage.Text}");
            }

            return Ok();
        }

        private string GetBotResponse(string message)
        {
            // L�gica simples para gerar uma resposta com base na mensagem do aluno
            switch (message.ToLower())
            {
                case "oi":
                    return "Ol�, Somos do (SENAI) gostaria de saber as op��es de cursos? (SIM / N�O)";
                case "n�o":
                    return "Obrigado por nos contactar, espero ter ajudado at� mais!";
                case "sim":
                    return "Temos cursos de Programa��o, Design Gr�fico e Eletr�nica. Qual deles voc� gostaria de saber mais?";
                case "programa��o":
                    return "O curso de Programa��o temos disponiv�l a noite a partir das 18H com o professor Givanio, voc� pode se matricular em uma de nossas unidades em CABO-PE, posso ajudar em algo mais? (SIM/ N�O)";
                case "design":
                    return "OPA! constamos em nosso sistema que o curso de Design Gr�fico foi suspenso temporariamente, mas logo logo voltamos com mais inform��es, posso ajudar em algo mais? (SIM/ N�O)";
                case "eletr�nica":
                    return "O curso de Eletr�nica temos disponiv�l pela manh� das 8:00 �s 12:00 e das 13:00 as 17:00 com o professor Jairo, voc� pode se matricular em uma de nossas unidades em CABO-PE, posso ajudar em algo mais? (SIM/ N�O)"; ;
                default:
                    return "Desculpe, n�o entendi sua pergunta. Por favor, tente novamente para ver o que posso ajudar.";
            }
        }

    }
}
