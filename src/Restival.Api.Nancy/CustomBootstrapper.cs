using Nancy;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;

public class CustomBootstrapper : DefaultNancyBootstrapper
{
    protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
    {
        StaticConfiguration.DisableErrorTraces = false;
    }
}