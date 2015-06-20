# Restival
One API spec. One test suite. Four implementations, each showcasing a different .NET ReST API framework.

* Microsoft ASP.NET WebAPI (http://www.asp.net/web-api)
* ServiceStack (https://servicestack.net/)
* OpenRasta (http://openrasta.org/)
* NancyFX (http://nancyfx.org/)

http://dylanbeattie.blogspot.co.uk/search/label/restival

## Docker/Mono

Restival can also be run as a Dockerized application using Mono. You'll need to have [installed Docker](https://docs.docker.com/installation/#installation) first.

1. `docker build -t restival src`
   * This step needs to be run from the Restival root
2. `docker run -p 9000:80 --name restival -d restival`
   * This runs Restival on port 9000, but you can change that if you wish.

You can now go to `http://localhost:9000` (or whatever `boot2docker ip` says if you're not on a Linux machine) and `/api.webapi`, `/api.openrasta`, `/api.nancy` and `/api.servicestack` will now respond with the different frameworks.