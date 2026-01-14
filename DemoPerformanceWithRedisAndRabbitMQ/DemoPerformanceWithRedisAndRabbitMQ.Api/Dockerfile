FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY DemoPerformanceWithRedisAndRabbitMQ.Api/ DemoPerformanceWithRedisAndRabbitMQ.Api/
COPY DemoPerformanceWithRedisAndRabbitMQ.Bussiness/ DemoPerformanceWithRedisAndRabbitMQ.Bussiness/
COPY DemoPerformanceWithRedisAndRabbitMQ.DAL/ DemoPerformanceWithRedisAndRabbitMQ.DAL/
COPY DemoPerformanceWithRedisAndRabbitMQ.Share/ DemoPerformanceWithRedisAndRabbitMQ.Share/

RUN dotnet restore DemoPerformanceWithRedisAndRabbitMQ.Api/DemoPerformanceWithRedisAndRabbitMQ.Api.csproj

RUN dotnet publish DemoPerformanceWithRedisAndRabbitMQ.Api/DemoPerformanceWithRedisAndRabbitMQ.Api.csproj \
    -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "DemoPerformanceWithRedisAndRabbitMQ.Api.dll"]
