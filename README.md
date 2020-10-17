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

### **4. Métodos da API (em Ordem que está na controller)**

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
