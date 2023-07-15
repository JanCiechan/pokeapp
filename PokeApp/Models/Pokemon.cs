namespace PokeApp.Models
{
    public class Pokemon
    {
        public string Name { get; set; }    
        public string Description { get; set; }

        public string Url { get; set; }

        public int Rating { get; set; } = 1;

        public int PokemonId { get; set; }

        public Pokemon(int ID,string desc, string url, int rate,string name)
        {
            Name = name;
            Description = desc; 
            Url = url;
            Rating = rate;
            PokemonId = ID;

        }

        
    }
    
}
