using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PokeApp.Models;
using System.Text.Json;
namespace PokeApp.Controllers
{
    public class PokemonController : Controller
    {
        const int total_pokemons = 1000;
        private static readonly HttpClient client = new HttpClient();
        static List<Pokemon> pokemons = new List<Pokemon>() { new Pokemon(1, "", "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/dream-world/1.svg", 0, "bulbasaur") };

        public IActionResult Index(int id = 1)
        {
// Pokemon pokemon= new Pokemon();
//Random rnd = new Random();
//int num = rnd.Next(1,total_pokemons);
//var response=await client.GetStringAsync("https://pokeapi.co/api/v2/pokemon/"+num);

//dynamic data = JObject.Parse(response);
//pokemon.Name = data.name;
//pokemon.Url = data.sprites.other.dream_world.front_default;



            Pokemon pokemon = pokemons.Find(p => p.PokemonId == id);
            Console.WriteLine(pokemon.Rating);
            return View(pokemon);
        }
        [HttpPut] // or [HttpPatch] depending on the request type
        public ActionResult UpdatePokemon(string argument,string pokeid)
        {
            int id = Int32.Parse(pokeid);
            int index = pokemons.FindIndex(a => a.PokemonId == id);
            pokemons[index].Rating = Int32.Parse(argument);
            return RedirectToAction("Index", new { id = id }); 
        }

    }
}
