from flask import Flask, request, jsonify
from datetime import datetime

app = Flask(__name__)

# Lista para armazenar as mensagens como instâncias da classe Mensagem
messages = []

# Mensagem inicial do bot
messages.append({"date": datetime.now().strftime("%Y-%m-%d %H:%M:%S"), "nickname": "Bot", "text": "Olá! Como posso ajudar você?"})

class Mensagem:
    def __init__(self, nickname, text):
        self.nickname = nickname
        self.text = text
        self.date = datetime.now().strftime("%Y-%m-%d %H:%M:%S")

@app.route('/chat', methods=['GET'])
def get_messages():
    try:
        return jsonify([{"date": msg.date, "nickname": msg.nickname, "text": msg.text} for msg in messages if isinstance(msg, Mensagem)])
    except Exception as e:
        print(f"Erro ao buscar mensagens: {e}")
        return jsonify({"error": "Erro ao buscar mensagens."}), 500

@app.route('/chat', methods=['POST'])
def post_message():
    data = request.get_json()

    if not data or 'nickname' not in data or 'text' not in data:
        return jsonify({"error": "Mensagem inválida."}), 400

    new_message = Mensagem(data['nickname'], data['text'])
    messages.append(new_message)

    # Resposta do bot
    response_message = get_bot_response(new_message.text)
    if response_message:
        bot_message = Mensagem("Bot", response_message)
        messages.append(bot_message)

    return jsonify({"message": "Mensagem recebida."}), 200

def get_bot_response(message):
    responses = {
        "oi": "Olá, somos do (SENAI). Gostaria de saber as opções de cursos? (SIM / NÃO)",
        "não": "Obrigado por nos contactar, espero ter ajudado até mais!",
        "sim": "Temos cursos de Programação, Design Gráfico e Eletrônica. Qual deles você gostaria de saber mais?",
        "programação": "O curso de Programação está disponível à noite a partir das 18h com o professor Givanio.",
        "design": "O curso de Design Gráfico foi suspenso temporariamente, mas logo voltamos com mais informações.",
        "eletrônica": "O curso de Eletrônica está disponível pela manhã das 8h às 12h e das 13h às 17h com o professor Jairo."
    }
    return responses.get(message.lower(), None)

if __name__ == '__main__':
    app.run(debug=True)
