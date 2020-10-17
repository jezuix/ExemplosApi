# **API de Exemplo**

### **1. Estrutura Básica**

   1.1 Este projeto não possui nenhuma arquitetura, então não se apegue muito a isso.

   1.2 O banco de dados é em memória e eu só desenvolvi pra API não ser tão vazia, toda vez que o sistema é buildado ele monta a base em memória e gera novos Ids então cuidado em manter os GUID salvos em algum arquivo, pois na próxima execução eles não serão mais válidos.

   1.3 Pra rodar o projeto é só dar play, o endereço do swagger dele é: https://localhost:44375/index.html, caso não funcione nesse endereço click com o botão direito no projeto "Teste.API" selecione "Propriedades" e vá na aba "Debug" e veja o endereço correto ao lado da opção "Enable SSL".

   1.4 A única injeção de dependência do projeto é do DbContext que controla a conexão com o banco de dados e ela está Singleton para que só seja criada uma única conexão já que toda conexão nova cria um banco novo em memória.

### **2. Verbos Básicos**

##### 2.1 Get

   Este verbo é utilizado para fazer uma requisição dentro da API, um detalhe que o GET não lê informações do Corpo da requisição então o [FromBody] não funciona nele.

##### 2.2 Post

   Post geralmente é utilizado para Inserir, fazer alguma requisição que precise passar objetos para a API pelo corpo da requisição [FromBody].

##### 2.3 Put

   O Put é basicamente utilizado para fazer requisição de atualização de informações.

##### 2.4 Delete

   O Delete é utilizado para requisição de deletar algum dado.

### **2. Rotas**

   2.1 Não foi mexido na rota da API, então a rota padrão é "{endereço}/api/Teste/{Método}"

   2.2 As rotas de uma API não podem ter sobre carga, por mais que sejam métodos com parâmetros diferentes a API irá quebrar, porém ela builda mas não vai aceitar requisição (a não ser que além de parâmetros diferentes você também defina rotas diferentes).

   2.3 Você pode ter 1 método sem rota por verbo (Ex: posso ter um método GET e um POST sem rota definida a API vai saber diferenciar os métodos pelo verbo).
   
   2.4 Diferenças entre [FromRoute] e [FromQuery] os parâmetros na rota são basicamente assim: "api/Teste/{parametro}/Método/{parametro2}@{parametro3}" sendo que cada uma dessas palavras entre chaves {} tem que ter um correspondente tageado com [FromRoute] como parâmetro, os parâmetros [FromQuery] são os que após a URL ficam basicamente assim: "api/Teste/Método?parametro=""&parametro2=""&parametro3="" ".

### **4. Métodos da API (na ordem da controller)**

##### 4.1 Get

  Todos os parâmetros deste método são passados via query e não é necessário passar a tag [FromQuery] a API já busca na query por padrão, mas isso faz com que objetos complexos não possam ser utilizados como parâmetros de métodos GET.

    4.1.1 EsseMetodoGetNaoServePraNada
      Apesar do nome ser uma brincadeira feita pra mostrar que este nome não é exibido no swagger e nem é necessário para fazer esta chamada, pois esse método não possui rota definida logo se torna o método padrão do verbo, este método retorna um usuário aleatório do banco e suas informações. 
      Um detalhe deste método é que é o único que eu uso o [FromService] para instanciar o ApiContext, apesar de já existir um objeto deste sendo criado no construtor da Controller eu criei ele aqui pra mostrar que você pode resolver injeção de dependência dentro do método (repare que esse parâmetro não é pedido na requisição do swagger).
      
   4.1.2 GetUsuarios
      Este método retorna todos os usuários do banco e suas carteiras, porém, ele não retorna o objeto do banco, pois se você entrar na pasta de modelos você verá que ele tem referencia cíclica (usuário possuí carteira e carteira possuí usuário) dentro do backend isso não é problema, porém na hora de serializar pra retornar pro fronte isso gera uma recursividade infinita e quebra o sistema, então a solução foi gerar um objeto anônimo só pra retorno (new {}).
      
   4.1.3 GetCarteirasPraDeletar
      Como todas execuções o banco é recriado com novos IDs para as carteiras então criei esse método que já volta a lista de todas as carteiras no formato Json pra usar na requisição de delete.
      
   4.1.4 GetUsuariosPraDeletar
      Como todas execuções o banco é recriado com novos IDs para os usuários então criei esse método que já volta a lista de todos os usuários no formato Json pra usar na requisição de delete.
      
   4.1.5 GetCarteira
      Este método recebe como parâmetro um Guid e irá retornar a carteira com o Id correspondente, também é gerado um objeto anônimo pra evitar recursividade de retornar o model usado no banco.
      
   4.1.6 GetCarteiraPorUsuario
      Este método recebe como parâmetro um Guid e irá retornar a carteira cujo o Id do usuário a qual ela pertença seja o Id passado como parâmetro, também é gerado um objeto anônimo pra evitar recursividade de retornar o model usado no banco.
      
   4.1.6 GetUsuario
      Este método recebe como parâmetro um Guid e irá retornar o usuário com o Id correspondente, também é gerado um objeto anônimo pra evitar recursividade de retornar o model usado no banco.

##### 4.2 Post

   4.2.1 OutroMetodoQueNaoFazNadaMasSeraQueVoceViu
      Este também é um método sem rota e seu nome não é necessário para ser feita a requisição, porém ao contrário do Get que tem algum retorno importante esse apenas gera números aleatórios e retorna em formato de JSON apenas pra simular um processamento.
      
   4.2.2 InsereUsuario
      Este método recebe um objeto do tipo "InsereUsuarioRequest" no body "[FromBody]" apesar de não existir problema em receber um objeto com referência cíclica na entrada da API, o ideal é usarmos objetos que trafeguem apenas as informações necessárias.
      
   4.2.3 InsereCarteira      
      Este método recebe um objeto do tipo "InsereCarteiraRequest" no body "[FromBody]" apesar de não existir problema em receber um objeto com referência cíclica na entrada da API, o ideal é usarmos objetos que trafeguem apenas as informações necessárias.
      
   4.2.4 AtualizaCarteira
      Este método recebe o id da carteira e o decimal do valor, porém, neste caso os 2 parâmetros estão vindo da URL [FromQuery] ou sem tag, dá pra ver como é essa requisição pelo swagger.
      
   4.2.5 SomaValor
      Este método recebe um id da carteira e um valor que será somado a carteira, porém, novamente é usado um objeto no corpo da requisição [FromBody], o objeto no caso é do tipo "AtualizaCarteiraRequest".
      
##### 4.3 Put

   4.3.1 NemAdiantaFalarQueNaoFazNada
      Método padrão do verbo, realmente não faz nada. 
      
   4.3.2 AtualizaCarteiraFromBody
      Este método também atualiza a carteira do usuário, mas ao contrário do método POST este recebe um objeto "AtualizaCarteiraRequest" pelo corpo da requisição [FromBody]. 
      Criei este método basicamente pra mostrar que muitas vezes as pessoas usam o POST pra atualização, mas o "certo" seria um PUT que é um verbo com essa "finalidade" (palavras entre aspas por que no fundo não tem muita diferença, mas é convenção/boa prática).
      
   4.3.3 AtualizaRangeCarteirasFromBody
      Este método atualiza várias carteiras, recebe uma lista de "AtualizarCarteiraRequest" no corpo da requisição [FromBody] e atualiza todas as carteiras em uma única requisição.
      
   4.3.4 AtualizaNomeFromBody
      Este método foi criado com a finalidade de mostrar que o nome do método e a rota não precisam serem iguais (lembre-se o nome da rota deve ser único por controller), neste caso o método se chama "AtualizaNomeFromBody" e a rota "AtualizaNome", mas se você observar no swagger a rota é utilizado para requisição.
      Outra coisa que este método faz é mostrar que os 2 parâmetros são via query, porém um deles está com a tag [FromQuery] e o outro não (também possível de observar que os dois parâmetros são enviados via URL pelo swagger).
      
##### 4.4 Delete

   Os métodos deste verbo excluem dados, porém como o banco está em memória e sendo criado a cada execução fique tranquilo.

   4.4.1 OutroMetodoQueNaoFazNadaMasSeraQueVoceViu
      Método padrão do verbo, realmente não faz nada só tira uma onda haha. 
      
   4.4.2 DeletaUsuario
      Método recebe como parâmetro o id do usuário e um boleano de confirmação (coloquei este método pra mostrar que APIs também podem ter parâmetros opcionais) e caso o id tenha um usuário correspondente ele o deleta (e deleta sua carteira também, mas não se apegue muito estas regras, já que o banco não existe de verdade eu só mantive esta lógica pois em teoria a carteira teria uma FK do usuário e o usuário não poderia ser deletado em quanto houvesse uma carteira apontando pra ele).
      Este método possui um extra na rota que é "{usuarioId}" e um dos parâmetros possui a tag "[FromRoute]" e o mesmo nome "usuarioId" isso indica pra API que esse parâmetro estará na URL, porém estará na rota ao invés da query.
      
   4.4.3 DeletaCarteira
      Método recebe como parâmetro o id da carteira e um boleano de confirmação (coloquei este método pra mostrar que APIs também podem ter parâmetros opcionais) e caso o id tenha uma carteira correspondente ele a deleta.
      
   4.4.4 DeletaCarteiraFromBody
      Este método recebe um objeto "DeletaCarteiraRequest" pelo corpo da requisição [FromBody] e apaga a carteira de id correspondente e retira a referência desta carteira da tabela usuário correspondente.
      
   4.4.5 DeletaRangeCarteirasFromBody
      Este método recebe uma lista de objetos "DeletaCarteiraRequest" e faz com um range de objetos a deleção e desassociação de carteiras deletadas e seus usuários.
            
   4.4.6 DeletaRangeUsuariosFromBody
      Este método recebe uma lista de ids de usuários e remove todas as carteiras que estes usuários possuem e o próprio usuário do banco.
      
### **5. Observações**

   Este projeto foi feito de forma rápida apenas como intuito de demonstração, caso exista dúvidas podem perguntar sem problemas.
   Existem várias melhorias a serem feitas, a ideia é evoluir essa API, conforme for surgindo necessidade de aprender novas coisas (Eu ia já implemntar o Flunt nela, mas realmente não deu tempo)
   Sim, tem bastante zuera no projeto ... mas a parte técnica não está primorosa, porém deve cumprir seu papel que é dar um ponta pé inicial no aprendizado de API.
