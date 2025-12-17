using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TasksAPI.Context;

namespace TasksAPI.Controllers
{
    //_____________________________________________________________________________________
    //BaseController like a "Parent Class" that contains common stuff all controllers need.|
    //Benefits of BaseController:                                                          |
    //Don't Repeat Yourself (DRY) - Common code in one place                               |
    //Consistent Responses - All APIs return same format                                   |
    //Easy Updates - Change all controllers at once                                        |
    //Cleaner Code - Controllers focus on business logic                                   |
    //Shared Features - Authentication, logging, validation                                |
    //-------------------------------------------------------------------------------------

    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly AppDbContext _db;

        public BaseController(AppDbContext db)
        {
            _db = db;
        }
    }
}
