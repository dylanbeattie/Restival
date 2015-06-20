FROM mono:4.0.0-onbuild
RUN apt-get update && apt-get install -y mono-xsp4 && rm -rf /var/lib/apt/lists/*
EXPOSE 80
ENTRYPOINT ["/usr/bin/xsp4"]
CMD ["--nonstop", "--port=80", "--applications=/:_PublishedWebsites/Restival.Website,/Api.WebAPI:_PublishedWebsites/Restival.Api.WebApi,/Api.OpenRasta:_PublishedWebsites/Restival.Api.OpenRasta,/Api.Nancy:_PublishedWebsites/Restival.Api.Nancy,/Api.ServiceStack:_PublishedWebsites/Restival.Api.ServiceStack", "-v", "--printlog", "--loglevels=all"]