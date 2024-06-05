1)create project:  dotnet new sln - projectName
2)dotnet new classlib -o Domain -f net7.0
3)dotnet new classlib -o Application -f net7.0
4)dotnet new classlib -o Infrastructure -f net7.0
5)dotnet new webapi -o Web.API -f net7.0
6)dotnet add Application/Application.csproj reference Domain/Domain.csproj
7)dotnet add Infrastructure/Infrastructure.csproj reference Domain/Domain.csproj
8)dotnet add Infrastructure/Infrastructure.csproj reference Application/Application.csproj
9)dotnet add Web.API/Web.API.csproj reference Infrastructure/Infrastructure.csproj Application/Application.csproj
10) dotnet sln add Web.API/Web.API.csproj
11)dotnet sln add  Application/Application.csproj
12)dotnet sln add  Infrastructure/Infrastructure.csproj
13)dotnet sln add  Domain/Domain.csproj
dotnet build