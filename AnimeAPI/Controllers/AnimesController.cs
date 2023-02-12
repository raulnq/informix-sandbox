using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Data;

namespace AnimeAPI.Controllers;

[ApiController]
[Route("animes")]
public class AnimesController : ControllerBase
{
    private readonly Repository _repository;

    public AnimesController(Repository repository)
    {
        _repository = repository;
    }

    [HttpPost()]
    public async Task<Guid> Post([FromBody] RegisterAnime command)
    {
        var id = Guid.NewGuid();

        var anime = new Anime() { Id = id.ToString(), Name = command.Name };

        await _repository.Save(anime);

        return id;
    }

    [HttpGet()]
    public Task<IEnumerable<Anime>> Get()
    {
        return _repository.List();
    }

    public record RegisterAnime(string Name);
}



