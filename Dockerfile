FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /sscms
RUN wget https://dl.sscms.com/cms/7.0.1/sscms-7.0.1-linux-x64.tar.gz
RUN tar -xzf sscms-7.0.1-linux-x64.tar.gz
RUN rm sscms-7.0.1-linux-x64.tar.gz -f
RUN cp -r /sscms/wwwroot/sitefiles/assets /sscms/assets
RUN rm -rf /sscms/wwwroot/sitefiles/assets

FROM base AS final
WORKDIR /app
COPY --from=build /sscms .
ENTRYPOINT ["dotnet", "SSCMS.Web.dll"]