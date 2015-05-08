FROM mono:4.0.0-onbuild
RUN apt-get update && apt-get install -y mono-xsp4 && rm -rf /var/lib/apt/lists/*
EXPOSE 80
ENTRYPOINT ["/usr/bin/xsp4"]
CMD ["--nonstop", "--port=80", "--applications=/api.webapi:_PublishedWebsites/Restival.Api.WebApi,/api.openrasta:_PublishedWebsites/Restival.Api.OpenRasta,/api.nancy:_PublishedWebsites/Restival.Api.Nancy,/api.servicestack:_PublishedWebsites/Restival.Api.ServiceStack", "-v", "--printlog", "--loglevels=all"]