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

        try
        {
            List<Country>? countryPath = countryService.GetPath(startingCountry, code);
            if (countryPath == null)
            {
                return NotFound($"Path not found from {startingCountry} to {code}");
            }

            // Format the list to a more readable array
            string[] path = countryPath.Select(c => c.Code).ToArray();
            return Ok(path);
        }
        catch (ArgumentException)
        {
            return NotFound($"Country with code {code} not found.");
        }
    }
}
