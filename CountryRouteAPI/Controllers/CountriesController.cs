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

    [HttpGet]
    public IActionResult Hello()
    {
        return Ok("Hello from CountriesController");
    }
}
