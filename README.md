# Restival
One API spec. One test suite. Four implementations, each showcasing a different .NET ReST API framework.

* Microsoft ASP.NET WebAPI (http://www.asp.net/web-api)
* ServiceStack (https://servicestack.net/)
* OpenRasta (http://openrasta.org/)
* NancyFX (http://nancyfx.org/)

http://dylanbeattie.blogspot.co.uk/search/label/restival

## Getting Started

To start hacking (and run the tests), you'll need to get your machine set up to respond on

* NancyFX - http://restival.local/api.nancy/
* ServiceStack - http://restival.local/api.servicestack/
* WebAPI - http://restival.local/api.webapi/
* OpenRasta - http://restival.local/api.openrasta/

The easiest way to do this is using the Powershell script included in the solution:

1.	Run the deploy.ps1 script in the src/  folder
2.	Edit your HOSTS file (C:\Windows\System32\drivers\etc\hosts) and add a line that points restival.local at 127.0.0.1
3.	Point a web browser at http://restival.local/ and verify you can see the landing page
4.	Run the unit tests in Restival.ApiTests and verify they're all green

That's it! Happy hacking.
