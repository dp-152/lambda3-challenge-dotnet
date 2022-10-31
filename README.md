
#  Copa Games

  

###  A Copa dos Games da Lambda3
Projeto desenvolvido em .NET 6 e React

##  Desenvolvimento
Este projeto requer [.NET](https://dotnet.microsoft.com/) 6.0.x e [Node.js](https://nodejs.org/) v18.x. 

O projeto possui um Makefile para ambientes que suportam make.

Targets disponíveis:
```sh
# Build
make build-back			# executa a build do back-end
make build-front		# executa a build do front-end
make buildall			# executa a build de ambos os projetos
make publish			# cria as builds de produção para ambos os projetos

# Docker
make compose			# Inicializa os serviços por docker-compose
make compose-cleanbuild	# Executa a build dos containers sem cache e inicializa os serviços

# Dependências
make restore			# restaura as dependências do projeto back-end
make npmci				# instala os pacotes de NPM do projeto front-end
make install-deps		# instala as dependências de ambos os projetos

# Limpeza
make clean				# limpa os artefatos de build de ambos os projetos
```

### Instale todas as dependências do projeto:
```sh
dotnet restore CopaGames.sln
cd copa-front
npm ci
```
### Ajuste as configurações do projeto:
#### Back-end:
Altere os parâmetros no arquivo CopaGames.API/appsettings.json conforme necessário:

```json
{
	"ExternalApis": { // Lista de APIs externas chamadas pelo App
		"CopaGamesApi": {
			"BaseUrl": "https://l3-processoseletivo.azurewebsites.net" // URL da API de games da Lambda3. Alterar conforme necessário
		},
	},
	"CorsAllowedOrigins": [
		<url> // Lista de URLs a serem autorizadas pela política de CORS
	]
}
```

#### Front-end:
Crie um arquivo .env na pasta copa-front com os seguintes parâmetros:

| Parâmetro | Tipo | Observação |
|--|--|--|
| REACT_APP_API_URL | string (URL) | URL pública do back-end, com prefixo /api e número de versão. Valor padrão: http://localhost:7000/api/v1 |

### Execute o projeto
#### Back-end:
```sh
# Executar normalmente
dotnet run --project CopaGames.API

# Executar com hot reload habilitado
dotnet watch --project CopaGames.API run
```
\
O projeto possui configuração de debug para [Visual Studio](https://visualstudio.microsoft.com/). Basta importar a solução e utilizar a configuração de debug CopaGames.API.

#### Front-end:
```sh
npm start
```
O projeto possui configuração de debug para [Visual Studio Code](https://code.visualstudio.com/). A configuração estará disponível ao abrir a pasta /copa-front no IDE. 

##  Deploy
### Deploy por Docker
O projeto inclui Dockerfiles para o back-end e para o front-end.
É possível deployar o projeto utilizando o arquivo docker-compose existente, caso haja suporte no ambiente:
```sh
docker-compose up
```
Caso contrário, é possível utilizar os Dockerfiles existentes:
#### Back-end:
Ajuste as [configurações](#ajuste-as-configurações-do-projeto) conforme necessário.

Crie a imagem:
```sh
docker build -t copa-back:<versão> .
```
Execute a imagem:
```sh
docker run -d -p <porta-externa>:80 --name copa-back copa-back:<versão>
```
#### Front-end:
Acesse o diretório contendo o projeto do front-end:
```sh
cd copa-front
```
Ajuste as [configurações](#ajuste-as-configurações-do-projeto) conforme necessário.

Crie a imagem:
```sh
docker build -t copa-front:<versão>
```

Execute a imagem:
```sh
docker run -d -p <porta-externa>:80 --name copa-front copa-front:<versão>
```

###  Deploy Manual
Este projeto requer [.NET](https://dotnet.microsoft.com/) 6.0.x e [Node.js](https://nodejs.org/) v18.x. 

#### Back-end:
Ajuste as [configurações](#ajuste-as-configurações-do-projeto) conforme necessário.

Instale as dependências do projeto:
```sh
dotnet restore CopaGames.sln
```
Publique o projeto:
```sh
dotnet publish CopaGames.sln -c Release -o ./out
```
Execute a API:
```sh
cd out
dotnet CopaGames.API.dll
```
#### Front-end:
Ajuste as [configurações](#ajuste-as-configurações-do-projeto) conforme necessário.

Instale as dependências do projeto:
```sh
cd copa-front
npm ci
```
Execute o script de build:
```sh
npm run build
```
Os arquivos de distribuição estarão disponíveis no diretório build. Sirva os arquivos com o servidor estático de sua preferência (serve, nginx, etc.)
```sh
npx serve -s build
```

## Referência da API
Quando em ambiente de desenvolvimento, a API disponibiliza a documentação dos endpoints em OpenAPI/Swagger.
A Swagger UI pode ser encontrada em http://\<URL-Backend>/swagger.
As definições de OpenAPI podem ser encontradas em http://\<URL-Backend>/swagger/v1/swagger.json
