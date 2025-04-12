using Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHealth.WebApi.Interfaces.Services;

namespace SmartHealth.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController(IDatabaseRepository repo, IAuthenticationService auth) : ControllerBase
    {
        [HttpPost("Environments", Name = "NewEnv")]
        [Authorize]
        public async Task<ActionResult> StoreNewEnvironment(Environment2DDto env)
        {
            try
            {
                await repo.InsertNewEnvironment(env, auth.GetCurrentAuthenticatedUserID());
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets the json value of the world
        /// </summary>
        /// <returns>Returns the actionresult with therein the string of the json value</returns>
        [HttpGet("Environments", Name = "Environments")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Environment2DDto>>> GetEnvironments()
        {
            var result =
                await repo.GetEnvironment2D(id: auth.GetCurrentAuthenticatedUserID());
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        /// <summary>
        /// Stores the world in the database
        /// </summary>
        /// <param name="objects">The list of 2D objects to be stored</param>
        /// <returns>Result</returns>
        [HttpPost("WorldObjects/{EnvironmentId}", Name = "Store2DObjects")]
        [Authorize]
        public async Task<ActionResult> StoreWorld(Object2DDto[] objects, string EnvironmentId)
        {
            try
            {
                await repo.Insert2DObjects(objects, EnvironmentId);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("WorldObjects/{EnvironmentId}", Name = "Get2DObjects")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Environment2DDto>>> GetObjects2D(string EnvironmentId)
        {
            var result =
                await repo.Get2DObjects(EnvironmentId);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        /// <summary>
        /// Update values in the DB
        /// </summary>
        /// <param name="jsonValue">update</param>
        /// <returns>Yis or nis</returns>
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] object jsonValue)
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
