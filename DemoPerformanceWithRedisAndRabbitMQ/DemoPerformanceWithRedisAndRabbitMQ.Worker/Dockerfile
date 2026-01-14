# ======================
# BUILD STAGE
# ======================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# copy solution
COPY *.sln .

# copy all projects
COPY DemoPerformanceWithRedisAndRabbitMQ.Worker/ DemoPerformanceWithRedisAndRabbitMQ.Worker/
COPY DemoPerformanceWithRedisAndRabbitMQ.Bussiness/ DemoPerformanceWithRedisAndRabbitMQ.Bussiness/
COPY DemoPerformanceWithRedisAndRabbitMQ.DAL/ DemoPerformanceWithRedisAndRabbitMQ.DAL/
COPY DemoPerformanceWithRedisAndRabbitMQ.Share/ DemoPerformanceWithRedisAndRabbitMQ.Share/

# restore
RUN dotnet restore DemoPerformanceWithRedisAndRabbitMQ.Worker/DemoPerformanceWithRedisAndRabbitMQ.Worker.csproj

# build
RUN dotnet publish DemoPerformanceWithRedisAndRabbitMQ.Worker/DemoPerformanceWithRedisAndRabbitMQ.Worker.csproj \
    -c Release -o /out

# ======================
# RUNTIME STAGE
# ======================
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "DemoPerformanceWithRedisAndRabbitMQ.Worker.dll"]
