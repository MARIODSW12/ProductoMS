FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
WORKDIR /src/Producto.API

RUN dotnet restore Producto.API.sln

RUN dotnet publish Producto.API.sln -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /out .

EXPOSE 80
ENTRYPOINT ["dotnet", "Producto.API.dll"]
