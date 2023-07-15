using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PokeApp.Models;
using System.Text.Json;
using System.Linq;
namespace PokeApp.Controllers
{
    public class PokemonController : Controller
    {
        static List<int> allPokemons = Enumerable.Range(2, 1000).ToList();
        static int currentPokemon = 1;
        private static readonly HttpClient client = new HttpClient();
        static List<Pokemon> pokemons = new List<Pokemon>() { new Pokemon(1, "", "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/dream-world/1.svg", 1, "bulbasaur") };
  
        public IActionResult Index()
        {

            Pokemon pokemon = pokemons.Find(p => p.PokemonId == currentPokemon);
           
            return View(pokemon);
        }

        public IActionResult Gallery()
        {
            return View(pokemons);
        }

        public IActionResult Pokedle()
        {

            Random random = new Random();
            int randomIndex = random.Next(pokemons.Count);
            return View(pokemons[randomIndex]);

        }
        [HttpPut] 
        public ActionResult UpdatePokemon(string argument,string pokeid)
        {
            int id = Int32.Parse(pokeid);
            int index = pokemons.FindIndex(a => a.PokemonId == id);
            pokemons[index].Rating = Int32.Parse(argument);
            currentPokemon= id; 
            return RedirectToAction("Index"); 
        }

        [HttpGet]
        public async Task<ActionResult> GetPokemon(string next, string pokeid)
        {

            int id = Int32.Parse(pokeid);
            int index = pokemons.FindIndex(a => a.PokemonId == id);
            int randomElement = id;
            if (next == "1")
            {
                if (index == pokemons.Count-1)
                {
                    Random random = new Random();
                    int randomIndex = random.Next(allPokemons.Count);
                    randomElement = allPokemons[randomIndex];
                    allPokemons.RemoveAt(randomIndex);
                    var response=await client.GetStringAsync("https://pokeapi.co/api/v2/pokemon/"+ randomElement);

                    dynamic data = JObject.Parse(response);
                    
                    
                    Pokemon newPok = new Pokemon(randomElement, "", data.sprites.other.dream_world.front_default.ToString(), 1, data.name.ToString());
                 
                    pokemons.Add(newPok);
                 

                }
                else
                {
                    randomElement = pokemons[index + 1].PokemonId;
                }
            }
            else
            {
                if(index != 0)
                {
                    randomElement = pokemons[index - 1].PokemonId;
                }
            }

            currentPokemon = randomElement;
            return RedirectToAction("Index");

        }

    }
}
