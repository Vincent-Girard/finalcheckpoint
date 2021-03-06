using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finalcheckpoint_server.Models;
using finalcheckpoint_server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using finalcheckpoint_server.Exceptions;
using Microsoft.AspNetCore.Authorization;
using CodeWorks.Auth0Provider;

namespace finalcheckpoint_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _ks;

        public KeepsController(KeepsService ks)
        {
            _ks = ks;
        }

        // GET api/keeps
        [HttpGet]
        public ActionResult<IEnumerable<Keep>> GetKeeps()
        {
            try
            {
                return Ok(_ks.GetKeeps());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/keeps/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Keep>> Get(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                // Profile profile = userInfo;
                Keep found = _ks.GetByIdNoProf(id);
                // Keep view = _ks.GetByIdNoProf(id);
                if (userInfo == null)
                {
                    _ks.IncreaseViews(found);
                    return found;
                }
                if (found.CreatorId != userInfo.Id)
                {
                    _ks.IncreaseViews(found);
                }
                return Ok(found);

                // if (profile == null)
                // {
                //     _ks.GetByIdNoProf(id);
                // }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<Keep>> IncreaseViews(int id)
        // {
        //     try
        //     {
        //         Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        //         Keep found = _ks.GetById(id);
        //         if (userInfo == null)
        //         {
        //             return Ok(_ks.IncreaseViewsNoProf(found));
        //         }
        //         if (userInfo != null && userInfo.Id != found.CreatorId)
        //         {
        //             return Ok(_ks.IncreaseViews(found, userInfo.Id));
        //         }
        //         return found;
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }


        // }

        // POST api/keeps
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keep>> Post([FromBody] Keep newKeep)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newKeep.CreatorId = userInfo.Id;
                Keep created = _ks.Create(newKeep);
                created.Creator = userInfo;
                return Ok(created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Keep>> Edit(int id, [FromBody] Keep editData)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editData.Id = id;
                editData.Creator = userInfo;
                return Ok(_ks.Edit(editData, userInfo.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/keeps/id
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_ks.Delete(id, userInfo.Id));
            }
            catch (Forbidden e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }

        }
    }
}

