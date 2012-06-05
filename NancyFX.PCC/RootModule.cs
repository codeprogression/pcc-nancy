using Nancy;
using Nancy.Responses;

namespace NancyFX.PCC
{
    public class RootModule : NancyModule
    {
        public RootModule()
        {
            Get["/"] = p => View["index"];
        }
    }
}