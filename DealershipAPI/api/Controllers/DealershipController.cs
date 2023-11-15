using Microsoft.AspNetCore.Mvc;
using api.Models.Dealership;
using api.Data;
using api.Services;

namespace api.Controllers;

[Route("api/dealership")]
[ApiController]

public class DealershipController : ControllerBase
{

    public static DealershipServices services = new DealershipServices();


    [HttpPost("create-dealership")]
    public IActionResult CreateDealership(DealershipCreate dealershipCreate)
    {


        Dealership dealership = services.CreateDealership(dealershipCreate);


        return CreatedAtAction(nameof(CreateDealership), dealership);

    }


    [HttpPost("search-vehicle")]
    public IActionResult SearchVehicle(DealershipSearch dealershipSearch)
    {


        List<string> searchedVehicles = services.SearchDealership(dealershipSearch);



        if (searchedVehicles.Count == 0)
        {
            return NotFound("No vehicles could be found.");
        }
        else
        {
            return Ok(searchedVehicles);
        }
    }

    [HttpPost("list-vehicle")]
    public IActionResult ListVehicle(DealershipList dealershipList)
    {
        List<string> listVehicles = services.ListVehicles(dealershipList);
        if (listVehicles.Count == 0)
        {
            return NotFound("No vehicles could be found.");
        }
        else
        {

            return Ok(listVehicles);
        }
    }


}
