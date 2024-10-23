function criarProduto() {
    const inputNome = document.getElementById("inputNome");
    const inputFabricante = document.getElementById("inputFabricante");
    const inputPrecoCompra = document.getElementById("inputPrecoCompra");
    const inputPrecoVenda = document.getElementById("inputPrecoVenda");

    const produto = {
        Nome: inputNome.value,
        Fabricante: inputFabricante.value,
        PrecoCompra: parseFloat(inputPrecoCompra.value),
        PrecoVenda: parseFloat(inputPrecoVenda.value)
    };

    // Calcular lucro
    const lucro = produto.PrecoVenda - produto.PrecoCompra;

    // Criar um contêiner para o novo produto
    const produtoDiv = document.createElement("div");
    produtoDiv.className = "produto";

    // Adicionar nome do produto
    const nomeTag = document.createElement("p");
    nomeTag.textContent = `Produto: ${produto.Nome}`;
    produtoDiv.appendChild(nomeTag);

    // Adicionar fabricante
    const fabricanteTag = document.createElement("p");
    fabricanteTag.textContent = `Fabricante: ${produto.Fabricante}`;
    produtoDiv.appendChild(fabricanteTag);

    // Adicionar preço de compra
    const precoCompraTag = document.createElement("p");
    precoCompraTag.textContent = `Preço de Compra: R$${produto.PrecoCompra.toFixed(2)}`;
    produtoDiv.appendChild(precoCompraTag);

    // Adicionar preço de venda
    const precoVendaTag = document.createElement("p");
    precoVendaTag.textContent = `Preço de Venda: R$${produto.PrecoVenda.toFixed(2)}`;
    produtoDiv.appendChild(precoVendaTag);

    // Adicionar lucro
    const lucroTag = document.createElement("p");
    lucroTag.textContent = `Lucro: R$${lucro.toFixed(2)}`;
    lucroTag.style.color = lucro > 0 ? 'green' : 'red'; // Verde para lucro positivo, vermelho para negativo
    produtoDiv.appendChild(lucroTag);

    // Adicionar o contêiner do produto à lista
    const divListagem = document.getElementById("listDiv");
    divListagem.appendChild(produtoDiv);
}
