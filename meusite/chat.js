const apiUrl = 'http://127.0.0.1:5000/chat'; // URL da sua API

function carregarMensagens() {
    fetch(apiUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log('Mensagens carregadas:', data);
            messagesDiv.innerHTML = ''; // Limpa mensagens antigas
            data.forEach(msg => {
                const messageElement = document.createElement('div');
                messageElement.classList.add('message');
                messageElement.textContent = `[${msg.date}] ${msg.nickname}: ${msg.text}`;
                messagesDiv.appendChild(messageElement);
            });
        })
        .catch(error => console.error('Erro ao carregar mensagens:', error));
}

function enviarMensagen() {
    const nickname = document.getElementById('nickname').value;
    const message = document.getElementById('message').value;

    if (!nickname || !message) {
        alert('Por favor, insira tanto o apelido quanto a mensagem');
        return;
    }

    const newMessage = { nickname: nickname, text: message };

    fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newMessage)
    })
    .then(response => {
        if (response.ok) {
            document.getElementById('message').value = ''; // Limpa o campo de mensagem
            carregarMensagens(); // Carregar mensagens novamente após enviar
        } else {
            console.error('Falha ao enviar a mensagem');
        }
    })
    .catch(error => console.error('Erro ao enviar a mensagem:', error));
}
