using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("")]
public class CountriesController : ControllerBase
{
    private readonly ICountryService countryService;

    public CountriesController(ICountryService countryService)
    {
        this.countryService = countryService;
    }

    [HttpGet("{code}")]
    public IActionResult GetPathTo([FromRoute] string code)
    {
        string startingCountry = "USA";

        List<Country>? countryPath = countryService.GetPath(startingCountry, code);
        if (countryPath == null)
        {
            return BadRequest($"Path not found from {startingCountry} to {code}");
        }

        foreach (var country in countryPath)
        {
            Console.WriteLine($"{country.Code}");
        }

        return Ok(countryPath);
    }
}
