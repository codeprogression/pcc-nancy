using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace NancyFX.PCC
{
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
        public void AddPlace(Place place)
        {
            var id = Places.Any() ? Places.Max(x => x.Id) + 1 : 0;
            place.Id = id;
            Places.Add(place);
        }
        public void AddPlace(string name, params string[] tags)
        {
            var id = Places.Any() ? Places.Max(x => x.Id) + 1 : 0;
            var place = new Place(id, name, tags);
            Places.Add(place);
        }

        public ReadOnlyCollection<Place> GetPlaces()
        {
            return new ReadOnlyCollection<Place>(Places);
        }

        public Place GetPlace(int id)
        {
            return Places.SingleOrDefault(x => x.Id == id);
        }

        public void RemovePlace(int id)
        {
            var single = Places.SingleOrDefault(x => x.Id == id);
            if (single==null)
                throw new FileNotFoundException();
            Places.Remove(single);
        }
    }
}