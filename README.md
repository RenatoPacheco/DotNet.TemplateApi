# Template API

[![Licensed under the MIT License](https://img.shields.io/badge/License-MIT-blue.svg)](./LICENSE)

Projeto para fazer alguns testes para uma `API REST` usando `.NET Core 3.1`.

## Para mais informações consulte a [Wiki] do projeto.
___

Outras versões do projeto:

- [.NET Framework](https://github.com/RenatoPacheco/DotNet.TemplateApi/tree/dot-net-framework) - modelo de projeto usando .Net Framework 4.7
- [.NET Core 6.0](https://github.com/RenatoPacheco/DotNet.TemplateApi/tree/dot-net-core-6) - modelo de projeto usando .Net Core 6.0
- [.NET Core 7.0](https://github.com/RenatoPacheco/DotNet.TemplateApi/tree/dot-net-core-7) - modelo de projeto usando .Net Core 7.0

## Testes

```
dotnet test --no-build --verbosity normal
```

## Report Generator

[Coverlet] é usado como um pacote no projeto de teste, para gerar o arquivo de cobertura de teste. Mas para gerar um relatório, o [Report Generator] deve estar instalado no computador, neste caso em escopo global. Neste projeto estou usando a versão 4.8.6.

```	
dotnet tool install --global dotnet-reportgenerator-globaltool --version 4.8.6
```

# Build e teste

Usando o Visual Studio Code, você pode compilar e testar o projeto a partir do terminal.

## Build e restauração do projeto

```
dotnet restore
dotnet build --no-restore
```

## Coverage

Para gerar o relatório de teste, execute o comando abaixo:

```
rm -rf ./TestResults ./coverage ./test/bin ./src/bin
dotnet restore
dotnet build --no-restore
dotnet test --no-build --verbosity=normal --collect:"XPlat Code Coverage" --results-directory ./coverage
reportgenerator "-reports:coverage/**/coverage.cobertura.xml" "-targetdir:coverage/report" -reporttypes:Html
```

Após a execução, o comando irá gerar um relatório de teste no **./coverage/report**.

Para não precisar executar cada comando a cada teste, que queira gerar o relatório, criei um **./buildReport.sh** para facilitar, então basta executar o shell script abaixo:

```	
./buildReport.sh
```

<!-- Links -->

[Wiki]: <https://github.com/RenatoPacheco/DotNet.TemplateApi/wiki>
[Visual Studio]:<https://visualstudio.microsoft.com/>
[VSCode]:<https://code.visualstudio.com/>
[.Net Core 3.1]:<https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-1>
[.NET 5]:<https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-5>
[Report Generator]:<https://github.com/danielpalme/ReportGenerator>
[Coverlet]:<https://github.com/coverlet-coverage/coverlet>
[shields.io]:<https://shields.io/category/coverage>