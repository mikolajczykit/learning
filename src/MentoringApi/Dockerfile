FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /mentoringApplication
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /mentoringSource

COPY ["MentoringApi.csproj", "MentoringApi/"]

WORKDIR /mentoringSource/MentoringApi
RUN dotnet restore "MentoringApi.csproj"
COPY . .

RUN dotnet build "MentoringApi.csproj" -c Release -o /app/build
RUN dotnet publish "MentoringApi.csproj" -c Release -o /app/publish

FROM base AS run
WORKDIR /mentoringApplication
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MentoringApi.dll"]