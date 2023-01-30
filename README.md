
# Chart Account

Código criado para teste de conhecimentos técnicos da empresa uCondo


## ChartAccount.API

API em .NET 6 (C#) responsável por:
- Listar Plano de Contas
- Inserir Plano de Contas
- Excluir Plano de Contas
- Identificar próximo código


## Instalação ambiente desenvolvimento

- Pré-requisitos
    
    [Visual Studio 2019 or mais atual](https://visualstudio.microsoft.com/pt-br/vs/community/)
    
    [Docker Desktop](https://www.docker.com/get-started/)

    [Imagem Docker Sql Server](https://hub.docker.com/_/microsoft-mssql-server)
    
    [Git 2.38.1 ou mais atual](https://git-scm.com/downloads)

    [Azure Data Studio](https://azure.microsoft.com/pt-br/products/data-studio/) ou Sql Management Express
   

- Configurando

      Instale as ferramentas de pré-requisitos e reinicie o PC

  *1. Baixando os fontes*      

       1.1 Crie um diretório para armazenar os fontes, por exemplo: C:\Trabalho\ChartAccount*
       1.2 Abra o Prompt de Comando em modo administrador e navegue até o diretório criado
       1.3 Clone o repositório (git clone https://github.com/humbertopalaia/ucondotest.git)

  *2. Banco de dados*

        2.1 Através do docker suba um container com o banco de dados Sql Server
        2.2 Crie um novo banco de dados de nome UCONDOTEST
        2.3 Conecte no banco de dados com um usuário master
        2.4 Execute o script 01_CREATEDATABASE.sql localizado na pasta SqlScripts na raiz do diretório clonado


*3. Configurando appsettings da aplicação*

        - Na raiz do projeto ChartAccountAPI, alterar no appsettings a connection string Default de acordo com os dados do seu ambiente local. 

        - É possível, mas não obrigatório, fazer alteração do usuário e senha para geração de autenticação, por padrão são: admin/admin.
## Execução e chamada dos métodos

- Selecione o projeto ChartAccountAPI como projeto de inicialização (Set as Startup Project).
- Clique em Run(dev) na barra de ferramentas do Visual Studio.
- Um swagger será exibido com os métodos disponíveis.

**Importante: Para realizar a chamada dos métodos, é necessário a geração de Bearer token através do endpoint api/auth/gettoken, o usuário e a senha padrão são admin/admin, mas podem ser alterados no appsettings**
## Deploy local

    **Ambiente Windows x64**
    
      dotnet publish -o deploy -f net6.0 -r win-x64 -c Release --self-contained true

    **Ambiente Windows x86 (NÃO RECOMENDADO)**

      dotnet publish -o deploy -f net6.0 -r win-x86 -c Release --self-contained true

    **Ambiente Linux x64**
        
        dotnet publish -o deploy -f net6.0 -r linux-x64 -c Release --self-contained true

