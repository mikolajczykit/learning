#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#WORKDIR /mentoringApplication
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#WORKDIR /mentoringSource
#
#COPY ["MentoringApi/MentoringApi.csproj", "MentoringApi/"]
#COPY ["MentoringCore/MentoringCore.csproj", "MentoringCore/"]
#
#RUN dotnet restore "MentoringApi/MentoringApi.csproj"
#
#WORKDIR /mentoringSource
#COPY . .
#
#WORKDIR /mentoringSource/MentoringApi
#RUN dotnet build "MentoringApi.csproj" -c Release -o /app/build
#RUN dotnet test
#RUN dotnet publish "MentoringApi.csproj" -c Release -o /app/publish
#
#FROM base AS run
#WORKDIR /mentoringApplication
#COPY --from=build /app/publish .
#ENTRYPOINT ["dotnet", "MentoringApi.dll"]

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /mentoringApplication
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /mentoringSource

COPY ["src/MentoringApi/MentoringApi.csproj", "MentoringApi/"]
COPY ["src/MentoringCore/MentoringCore.csproj", "MentoringCore/"]

RUN dotnet restore "MentoringApi/MentoringApi.csproj"
COPY "src/" .

WORKDIR /mentoringSource/MentoringApi
RUN dotnet build "MentoringApi.csproj" -c Release -o /app/build
RUN dotnet test
RUN dotnet publish "MentoringApi.csproj" -c Release -o /app/publish

FROM base AS run
WORKDIR /mentoringApplication
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MentoringApi.dll"]