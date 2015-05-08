# Restival
One API spec. One test suite. Four implementations, each showcasing a different .NET ReST API framework.

* Microsoft ASP.NET WebAPI (http://www.asp.net/web-api)
* ServiceStack (https://servicestack.net/)
* OpenRasta (http://openrasta.org/)
* NancyFX (http://nancyfx.org/)

http://dylanbeattie.blogspot.co.uk/search/label/restival

## Docker

docker build -t restival src
docker run -it -p 9000:80 --name restival restival