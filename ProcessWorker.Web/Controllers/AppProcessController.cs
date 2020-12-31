using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessWorker.Bl.Interfaces;
using ProcessWorker.Entity;
using ProcessWorker.Web.Models;
using ProcessWorker.Web.Models.Wrappers;

namespace ProcessWorker.Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AppProcessController : ControllerBase
    {
        private readonly IProcessManager _manager;

        public AppProcessController(IProcessManager manager)
        {
            _manager = manager;
        }
        
        [HttpGet]
        public ActionResult<PagingResponse<List<AppProcessModel>>> GetAllProcess([FromQuery] PagingFilter filter)
        {
            //TODO: validate user paging input
            var data = _manager.GetProcesses(filter);
            var response = new PagingResponse<List<AppProcessModel>>
            {
                Data = data.Data.Select(p => new AppProcessModel
                {
                    Name = p.Name,
                    CreatedDate = p.CreatedDate,
                    AuthorUsername = p.Author.UserName,
                    FinishedDate = p.FinishedDate,
                    IsFinished = p.IsFinished
                }).ToList(),
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalItems = data.TotalItems
            };
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<int> CreateProcess([FromBody] ProcessModel model)
        {
            return Ok(_manager.Create(model.Name, int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))));
        }
    }
}