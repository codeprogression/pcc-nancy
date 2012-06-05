namespace NancyFX.PCC
{
    public class Place
    {
        public Place()
        {
        }

        public Place(int id, string name, params string[] tags)
        {
            Id = id;
            Name = name;
            Tags = tags;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Tags { get; set; }
    }
}