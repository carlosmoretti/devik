# 🔥 Bem vindo ao Devik

Devik é uma biblioteca baseada em .NET Core 3.1, que consome, através de *web scrapping*, dados da entrega referente a sua encomenda.

## ⌛ Por que foi criado?
Os Correios, principal provedor de serviços de entregas no Brasil, possui uma API para rastreamento de entregas. Até poucos tempos atrás, havia uma chave publica, onde qualquer pessoa poderia consultar pacotes, porém, de acordo com o alto uso, foi removida e esta chave publica passou a ser utilizada apenas para testes. Quando era realizado uma tentativa de consulta a um determinado pacote, você recebia um erro da série 400 (não autorizado).

O Devik tem o objetivo de facilitar a vida do desenvolvedor, possibilitando que, através de uma consulta REST, você possa obter os últimos status do seu pacote, de acordo com a fonte oficial (Correios).

## 🌪️ Como é feito?
Utilizando o [Postman](https://www.postman.com/downloads/) e a biblioteca [RestSharp](http://restsharp.org/), foi possível interceptar as requests que eram feitas no  [site oficial](https://www2.correios.com.br/sistemas/rastreamento/default.cfm) dos correios para leitura dos dados de encomendas. No site, você informava na tela o código do rastreio e o mesmo, através de métodos HTTP, realizava a comunicação com o back-end deles e retornava em um HTML as informações já estilizadas.

Com o [HTML Agility Pack](https://html-agility-pack.net/) eu consegui manipular esse DOM, varrendo os itens necessários para filtragem dos dados, onde consegui obter o seguinte resultado:

```json
{
  "postagem": "2020-05-06T00:00:00",
  "ultimaMovimentacao": "2020-05-07T00:00:00",
  "previsao": "2020-05-13T00:00:00",
  "movimentacoes": [
    {
      "data": "2020-05-07T05:07:00",
      "descricao": "de Unidade de Tratamento em RIO DE JANEIRO / RJ  para Unidade de distribuição  em RIO DE JANEIRO / RJ ",
      "titulo": "Objeto encaminhado "
    },
    {
      "data": "2020-05-06T14:34:00",
      "descricao": " ",
      "titulo": "Objeto postado "
    }
  ]
}
```

## 🛩️ Utilização
O código fonte do mesmo encontra-se neste repositório. Você pode hospeda-lo e melhora-lo de acordo com o que for necessário.

A aplicação, por ser baseada em .NET Core 3.1, necessita de uma hospedagem que a suporte. Você não precisa de nenhum banco de dados ou addon adicional.

Para realizar a busca por um pacote, insira no seu browser:
```
http://<SEU_DOMINIO>/api/api/rastreio/track/<NUMERO_DO_RASTREIO>
```

[MIT](https://choosealicense.com/licenses/mit/)
