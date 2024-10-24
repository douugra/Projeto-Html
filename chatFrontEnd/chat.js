const apiUrl = 'http://localhost:5079/Chat';

function carregarMensagens() {
    fetch(apiUrl)
        .then(response => response.json())
        .then(data => {
            const messagesDiv = document.getElementById('messages');
            messagesDiv.innerHTML = '';
            data.forEach(msg => {
                const messageElement = document.createElement('div');
                messageElement.classList.add('message');
                messageElement.textContent = `[${msg.date}] ${msg.nickname}: ${msg.text}`;
                messagesDiv.appendChild(messageElement);
            });
        })
        .catch(error => console.error('Error loading messages:', error));
}

function enviarMensagen() {
    const nickname = document.getElementById('nickname').value;
    const message = document.getElementById('message').value;

    if (!nickname || !message) {
        alert('Please enter both nickname and message');
        return;
    }

    const newMessage = {date:"", nickname: nickname, text: message };

    fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newMessage)
    })
    .then(response => {
        if (response.ok) {
            document.getElementById('message').value = '';
            carregarMensagens();
        } else {
            console.error('Failed to send message');
        }
    })
    .catch(error => console.error('Error sending message:', error));
}