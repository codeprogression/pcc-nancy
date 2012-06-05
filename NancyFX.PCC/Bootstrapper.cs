using System.Collections.Generic;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;

namespace NancyFX.PCC
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(Nancy.Conventions.NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("Scripts"));
        }
    }

}