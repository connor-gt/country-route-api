# Country Route API

Country Route API is a small C# backend that returns a route from the United States to a destination country. Countries are represented as nodes in a graph, with connections representing their shared borders.

This started as an interview project, and I thought it would be a really good opportunity to apply what I'm learning from my algorithms class and that's why I went with a graph search approach.

**Details:** C#, .NET 10, ASP.NET Core, Microsoft Azure

## Live API

The API is deployed on Azure. Add a supported three-letter country code to the end of the URL to get a route.

**[Try a route to Panama](https://country-route-api-awhrbwaae6eyfug3.canadacentral-01.azurewebsites.net/PAN)**

```http
GET /PAN
```

```json
["USA", "MEX", "GTM", "HND", "NIC", "CRI", "PAN"]
```

```http
GET /BLZ
```

```json
["USA", "MEX", "BLZ"]
```

Successful requests return HTTP 200 with a JSON array containing the starting country, any countries along the route, and the destination.

Country codes are case-insensitive, so `/pan` and `/PAN` return the same result. Requesting `/USA` returns `["USA"]`. Any unrecognized/unsupported country code returns `404 Not Found`.

## Assumptions I made and overall scope

- The countries and borders found in the requirements are the complete dataset. Other countries and connections are not supported.
- The best route is the one that passes through the fewest countries, which also means crossing the fewest borders (which is why I am doing a uniform cost search).
- Each border crossing has a cost of 1, so factors like distance, driving time, fuel costs, etc. are outside the scope of this project.
- Every listed border connection can be traveled in either direction.
- Countries are identified only by their three-letter codes. Full country names and other identifiers are not supported.

The starting country is always the USA, as specified in the requirements. The endpoint accepts a destination rather than a configurable starting location.

The supported country codes are:

`CAN`, `USA`, `MEX`, `BLZ`, `GTM`, `SLV`, `HND`, `NIC`, `CRI`, and `PAN`.

## Design Decisions

### Uniform-cost search

I learned about graph search methods in my algorithms class a few weeks before I started this project. Reading the requirements immediately made me think of a graph (the countries being nodes and the edges being borders), so I wanted to try implementing what I had been learning in an actual program.

I chose [uniform-cost search](https://www.geeksforgeeks.org/artificial-intelligence/uniform-cost-search-ucs-in-ai/), using a priority queue to explore countries in order of the lowest accumulated travel cost.

Breadth-first search would have been enough with every border crossing set to 1. I decided to include costs because I wanted something I could refer back to in future projects involving weighted connections. In this version, all costs are equal and set to 1, so the result is a route with the fewest border crossings.

### Controller and service

I separated the HTTP endpoint from the country service based on what I learned while building my Potion Shop project and from previous backend experiences involving other small projects and interviews.

The controller handles the request and response, while the service sets up the country graph and requests a path. I decided to use the interface `ICountryService` with dependency injection so another service implementation could be used without changing the controller (perhaps one without the graph search method).

### Country objects

A country currently stores only its code, so just using strings would have been enough.

I chose a `Country` class because types such as `Country` and `List<Country>` make the graph code clearer to read as opposed to something like `List<string>`. It also gives me somewhere to add country information later without changing the graph's basic structure. Although this flexibility is definitely less important for a project this small, but the readability was useful immediately.

### Error responses

I kept error handling simple. An unrecognized country code returns HTTP 404 with an explanatory message. The controller also returns HTTP 404 if a country exists in the graph but no route can be found.

All countries in the current dataset are connected, so an unreachable destination is not currently exposed by the supplied map but the capability to expose non-connected countries is there.

## Running Locally

Install the .NET 10 SDK, then run this command from the repository's root folder:

```powershell
dotnet run --project CountryRouteAPI/CountryRouteAPI.csproj --launch-profile http
```

Once the application starts, open [http://localhost:5174/PAN](http://localhost:5174/PAN) in a browser.

The countries and borders are defined in [CountryService.cs](CountryRouteAPI/Services/CountryService.cs) so no database is needed.

## Code Tour

| File | What it handles |
| --- | --- |
| [CountriesController.cs](CountryRouteAPI/Controllers/CountriesController.cs) | Receives a destination code, requests a route from the USA, and returns the country codes or an error response. |
| [CountryService.cs](CountryRouteAPI/Services/CountryService.cs) | Defines the supported countries and borders and calls the graph's path search. |
| [ICountryService.cs](CountryRouteAPI/Services/ICountryService.cs) | Defines the service contract used by the controller. |
| [Graph.cs](CountryRouteAPI/Model/Graph.cs) | Stores countries and connections and finds a path using uniform-cost search. |
| [Country.cs](CountryRouteAPI/Model/Country.cs) and [Connection.cs](CountryRouteAPI/Model/Connection.cs) | Represent countries and the weighted connections between them. |
| [Program.cs](CountryRouteAPI/Program.cs) | Registers the service with dependency injection and maps the controllers. |
