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
                return BadRequest("Mensagem inválida.");
            }

            newMessage.Date = DateTime.Now.ToString("G");
            Messages.Add(newMessage);
            Console.WriteLine($"{newMessage.Nickname}: {newMessage.Text}");

            // Verifica se a mensagem é de um aluno e inicia a lógica do BOT
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
            // Lógica simples para gerar uma resposta com base na mensagem do aluno
            switch (message.ToLower())
            {
                case "oi":
                    return "Olá, Somos do (SENAI) gostaria de saber as opções de cursos? (SIM / NÃO)";
                case "não":
                    return "Obrigado por nos contactar, espero ter ajudado até mais!";
                case "sim":
                    return "Temos cursos de Programação, Design Gráfico e Eletrônica. Qual deles você gostaria de saber mais?";
                case "programação":
                    return "O curso de Programação temos disponivél a noite a partir das 18H com o professor Givanio, você pode se matricular em uma de nossas unidades em CABO-PE, posso ajudar em algo mais? (SIM/ NÃO)";
                case "design":
                    return "OPA! constamos em nosso sistema que o curso de Design Gráfico foi suspenso temporariamente, mas logo logo voltamos com mais informções, posso ajudar em algo mais? (SIM/ NÃO)";
                case "eletrônica":
                    return "O curso de Eletrônica temos disponivél pela manhã das 8:00 ás 12:00 e das 13:00 as 17:00 com o professor Jairo, você pode se matricular em uma de nossas unidades em CABO-PE, posso ajudar em algo mais? (SIM/ NÃO)"; ;
                default:
                    return "Desculpe, não entendi sua pergunta. Por favor, tente novamente para ver o que posso ajudar.";
            }
        }

    }
}
