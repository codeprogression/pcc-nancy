# NancyFX presentation at Portland Code Camp 

This is the basic flow of the presentation I gave at Portland Code Camp. There are no slides, just code.

I have included (below) the flow of the presentation.

* Create a root route that returns a response
* Create a set of routes to list/view/create data
* Create a view wired up with Twitter Bootstrap and KnockoutJS

__The 'completed' project is included__

#First things first...installing Nancy

1. Create a new Empty Aspnet web application (or WCF, Self, Owin)
2. Open Nuget Package Manager
3. Install-Package Nancy.Hosting.AspNet (or whatever hosting package that fits your project type)
4. (optional) Install Nancy.ViewEngines.Razor (again, you can pick from a range of View Engines)
	* Super Simple View Engine is built-in

#Create a web application

1. Add a class that inherits from NancyModule
2. Add a constructor
3. Create a route
4. Return a response - 'Hello Portland Code Camp'

##Woo-hoo - A Hello world app.

Ok, it may be a hello world app...but its a hello world app with one class and a minimal amount of code

In MVC, we would need a 
* global.asax
* a minimum of 2 folders (many more are installed by default)
* a bunch of references that we didn't need.

#An HTTP API

Since this is a http api, we should start returning some data

How about we get a list of places to eat and drink here in Portland.

For instance, yesterday I ate at Pastini's, a beer at Deschutes Brewery, and this morning I got a coffee at Stumptown. I want to try out Voodoo Dougnuts, and I've enjoyed breakfast at J Cafe before.

So we could return some of this data:


    [
    	{ "id": "0", "name": "Pastini Pastaria",  "tags": ["Lunch", "Dinner", "Pasta", "Beer", "Wine"] },
    	{ "id": "1", "name": "Deschutes Brewery", "tags": ["Lunch", "Dinner, "Beer", "Wine"] },
    	{ "id": "2", "name": "Stumptown Coffee",  "tags": ["Breakfast", "Coffee"] },
    	{ "id": "3", "name": "Voodoo Doughnuts",  "tags": ["Breakfast", "Coffee"] },
    	{ "id": "4", "name": "J Cafe",            "tags": ["Breakfast", "Lunch", "Coffee"] }
    ]


#Place module

Here is some code very similar to what we used in the demo.

    public class PlacesModule : NancyModule
    {
    	public PlacesModule(Repository repository) // : base("/places")
    	{
    	    Get["/places"] = p => Response.AsJson(repository.GetPlaces());
    	    Get["/places/{id}"] = p => Response.AsJson(repository.GetPlace((int)p.Id));
    	    Post["/places"] = p =>{ var place = this.Bind<Place>();
                    repository.AddPlace(place);
                    return Response.AsJson(place,HttpStatusCode.Created);
                }};
    	}
    }
    
    public class Place 
    {
        internal Place(int id, string name, params string[] tags)
        {
            Id = id;
        	Name = name;
        	Tags = tags;
        } 
    
    	public int Id        { get; internal set; }
    	public string Name   { get; private set; }
    	public string[] Tags { get; private set; }
    }
    
    public class Repository
    {
    	private static readonly IList<Place> Places = new List<Place>
    	{
    		new Place(0, "Pastini Pastaria", "Lunch", "Dinner", "Pasta", "Beer", "Wine"),
    		new Place(1, "Deschutes Brewery", "Lunch", "Dinner", "Pasta", "Beer", "Wine"),
    		new Place(2, "Stumptown Coffee", "Breakfast", "Coffee"),
    		new Place(3, "Voodoo Doughnuts", "Breakfast", "Coffee"),
    		new Place(4, "J Cafe", "Breakfast", "Lunch", "Coffee"),
    	};
    
    	public void AddPlace(string name, params string[] tags)
    	{
    		var id = Places.Any()? Places.Max(x=>x.Id) + 1 : 0;
    		var place = new Place(id, name,tags);
    		Places.Add(place);
    	}
    
        public ReadOnlyCollection<Place> GetPlaces() 
        { 
            return new ReadOnlyCollection<Place>(Places); 
        }
    
        public Place GetPlace(int id)
        {
            return Places.SingleOrDefault(x=>x.Id == id);            
        }
    }

#Posting

Using cUrl

# Before/After Pipeline

* Performance Testing	
* Authorization
* Server-side Caching
* Send back Cache headers

<!---->

    Before.AddToStartOfPipeline(ctx=>
    {
       	ctx.Items.Add("request_start_time", DateTime.Now); return null;
    });
    
    After += ctx => 
    {
    	var processingTime = (DateTime.Now - ctx.Items["request_start_time"]).Milliseconds;
    	System.Diagnostics.Debug.WriteLine(string.Format("Request to '{0}' Processed in: {1} milliseconds ", ctx.Request.Url.ToString(), processingTime.ToString()));
    	ctx.Response.WithHeader("x-server-processing-time", processingTime.ToString());
    }
    

#Views

* Twitter.Bootstrap
* KnockoutJS

#Posting on a single page app and Knockout Data-Binding

1. Install-Package Twitter.Bootstrap
2. Install-Package Twitter.Bootstrap.Less
3. Install-Package JQuery
4. Install-Package KnockoutJS
5. Install-Package underscore.js
6. Install-Package UnderscoreKO

##Add convention for Scripts folder

Nancy exposes the `Content` folder as the only static folder in a project (by convention). Because NuGet installs all javascript libraries to the `Scripts` folder, we need to create an additional static folder convention.  _(We could also move the `Scripts` folder to the `Content` folder and update the paths in the views)_

    nancyConventions.StaticContentsConventions
        .Add(StaticContentConventionBuilder.AddDirectory("Scripts"));