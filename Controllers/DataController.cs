using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHealth.WebApi.Interfaces.Services;

namespace SmartHealth.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController(IDatabaseRepository repo, IAuthenticationService auth) : ControllerBase
    {
        /// <summary>
        /// Gets the json value of the world
        /// </summary>
        /// <returns>Returns the actionresult with therein the string of the json value</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            string result =
                await repo.ReadAsync(id: auth.GetCurrentAuthenticatedUserID());
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        /// <summary>
        /// Sets the given json value in the database
        /// </summary>
        /// <param name="jsonValue">The world</param>
        /// <returns>Yay or nay</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> StoreNew([FromBody]object jsonValue)
        {
            if (jsonValue == null) return BadRequest();
            try
            {
                await repo.InsertAsync(jsonObject: jsonValue.ToString());
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update values in the DB
        /// </summary>
        /// <param name="jsonValue">update</param>
        /// <returns>Yis or nis</returns>
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Update([FromBody]object jsonValue)
        {
            if (jsonValue == null) return BadRequest();
            try
            {
                await repo.UpdateAsync(jsonObject: jsonValue.ToString());
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
