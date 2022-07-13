using APICore.API.BasicResponses;
using APICore.Common.DTO.Request;
using APICore.Common.DTO.Response;
using APICore.Data;
using APICore.Data.Entities;
using APICore.Data.UoW;
using APICore.Services.Exceptions.NotFound;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace APICore.API.Controllers
{
    [Route("api/task")]
    public class TaskMController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IStringLocalizer<TaskMController> _localizer;

        public TaskMController(IMapper mapper, IUnitOfWork uow, IStringLocalizer<TaskMController> localizer)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        [HttpGet]
        public async Task<ActionResult> GetTaskItems()
        {
            var response = await _uow.TaskMRepository.GetAllAsync();

            var result = _mapper.Map<List<TaskMResponse>>(response);

            return Ok(new ApiOkResponse(result)); ;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTaskItem(int id)
        {
            var taskItem = await _uow.TaskMRepository.GetAsync(id);
            if (taskItem == null)
            {
                throw new TaskMNotFoundException(_localizer);
            }
            var response = _mapper.Map<TaskMResponse>(taskItem);

            return Ok(new ApiOkResponse(response)); ;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> PostTaskItem([FromBody] TaskMRequest item)
        {
            var taskItem = new TaskM()
            {
                Name = item.Name,
                State = false
            };
            await _uow.TaskMRepository.AddAsync(taskItem);
            await _uow.CommitAsync();
            var response = _mapper.Map<TaskMResponse>(taskItem);

            return Ok(new ApiOkResponse(response));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> UpdateStateTaskItem(int id)
        {
            var taskItem = await _uow.TaskMRepository.GetAsync(id);
            if (taskItem == null)
                throw new TaskMNotFoundException(_localizer);
            taskItem.State = true;
            await _uow.CommitAsync();
            var response = _mapper.Map<TaskMResponse>(taskItem);
            return Ok(new ApiOkResponse(response));
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> UpdateTaskItem(int id, [FromBody] TaskMRequest request)
        {
            var taskItem = await _uow.TaskMRepository.GetAsync(id);
            if (taskItem == null)
                throw new TaskMNotFoundException(_localizer);
            taskItem.Name = request.Name;
            await _uow.TaskMRepository.UpdateAsync(taskItem, id);
            await _uow.CommitAsync();
            var response = _mapper.Map<TaskMResponse>(taskItem);
            return Ok(new ApiOkResponse(response));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteTaskItem(int id)
        {
            var taskItem = await _uow.TaskMRepository.GetAsync(id);

            if (taskItem == null)
            {
                throw new TaskMNotFoundException(_localizer);
            }

            _uow.TaskMRepository.Delete(taskItem);
            await _uow.CommitAsync();
            return Ok(new ApiOkResponse(200));
        }
    }
}