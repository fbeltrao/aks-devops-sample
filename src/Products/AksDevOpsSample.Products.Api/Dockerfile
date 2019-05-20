FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Products/AksDevOpsSample.Products.Api/AksDevOpsSample.Products.Api.csproj", "src/Products/AksDevOpsSample.Products.Api/"]
RUN dotnet restore "src/Products/AksDevOpsSample.Products.Api/AksDevOpsSample.Products.Api.csproj"
COPY . .

FROM build AS publish
RUN dotnet publish "src/Products/AksDevOpsSample.Products.Api/AksDevOpsSample.Products.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AksDevOpsSample.Products.Api.dll"]
