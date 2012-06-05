using System;
using Nancy;
using Nancy.ModelBinding;

namespace NancyFX.PCC
{
    public class PlacesModule : NancyModule
    {
        public PlacesModule(Repository repository) : base("/places")
        {
            // Return list of places
            Get["/"] = p => Response.AsJson(repository.GetPlaces());

            // Return place with matching id
            Get["/{id}"] = p =>
                {
                    var place = repository.GetPlace((int) p.Id);
                    return place != null ? Response.AsJson(place) : new NotFoundResponse();
                };

            // Create a new place
            Post["/"] = p =>
                {
                    var place = this.Bind<Place>();
                    repository.AddPlace(place);
                    return Response.AsJson(place,HttpStatusCode.Created);
                };

            //Delete a place with matching id
            Delete["/{id}"] = p =>
                {
                    var place = repository.GetPlace((int) p.Id);
                    if (place != null)
                    {
                        repository.RemovePlace((int) p.Id);
                        return new Response { StatusCode = HttpStatusCode.NoContent };
                    }

                    return new NotFoundResponse();
                    // Could also return 410 (Gone) instead of 404 (Not Found) if you knew that the place had existed at some point
                };

            Before.AddItemToStartOfPipeline(ctx =>
            {
                var time = DateTime.Now;
                ctx.Items.Add("start_time", time);
                return null;
            });

            After += ctx =>
            {
                var time = DateTime.Now;
                var processTime =
                    (time - (DateTime)ctx.Items["start_time"]).Milliseconds;
                ctx.Response.WithHeader("x-process-time", processTime.ToString());
            };
        }
    }
}