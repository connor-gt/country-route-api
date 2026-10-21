using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("")]
public class CountriesController : ControllerBase
{
    private readonly CountryService countryService;

    public CountriesController(CountryService countryService)
    {
        this.countryService = countryService;
    }

    [HttpGet]
    public IActionResult Hello()
    {
        return Ok("Hello from CountriesController");
    }
}
