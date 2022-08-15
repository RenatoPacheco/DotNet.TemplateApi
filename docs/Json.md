# Json

Para esse projeto é usado .NET Core 3.1, e neste caso, foi tentado usar [System.Text.Json] para gerenciar a manipulação de dados para Json. Até foi feita a configuração com essa biblioteca, mas ela não atende todos os requisitos necessários, neste caso houve problema com o [JsonConstructorAttribute], que não existe para .NET Core 3.1, somente a partir do .NET Core 5.

Devido a esse problema, foi refeita a configuração para usar o [Newtonsoft.Json], que foi recomendado pela própria documentação da Microsoft.

[System.Text.Json]: <https://docs.microsoft.com/pt-br/dotnet/standard/serialization/system-text-json-overview?pivots=dotnet-core-3-1>

[JsonConstructorAttribute]: <https://docs.microsoft.com/pt-br/dotnet/standard/serialization/system-text-json-immutability?pivots=dotnet-core-3-1>

[Newtonsoft.Json]: <https://www.newtonsoft.com/json>