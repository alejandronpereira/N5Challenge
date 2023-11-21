using Application.Dto;
using Application.Interfaces;
using Application.Services;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Security;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        private readonly IKafkaProducerService _kafkaProducerService;
        private readonly ILogger<PermissionController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionController" /> class.
        /// </summary>
        public PermissionController(IPermissionService permissionService, IKafkaProducerService kafkaProducer, ILogger<PermissionController> logger)
        {
            _permissionService = permissionService;
            _kafkaProducerService = kafkaProducer;
            _logger = logger;   
        }

        /// <summary>
        /// Gets all the Permissions
        /// </summary>
        /// <returns>A list of all the roles</returns>
        [HttpGet]
        [KafkaProducer("testtopic", "get")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll Api executing...");

            var permissions = await _permissionService.GetAll();

            _logger.LogInformation("GetAll Api finished...");

            return Ok(permissions);
        }

        /// <summary>
        /// Gets a permission given a certain id
        /// </summary>
        /// <param name="id">id of the permission we want to return</param>
        /// <returns>A particular permission by id</returns>
        [ProducesResponseType(typeof(PermissionViewModel), (int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        [KafkaProducer("testtopic", "get")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("GetById Api executing...");

            var permission = await _permissionService.GetById(id);

            _logger.LogInformation("GetById Api finished");

            return Ok(permission);
        }

        /// <summary>
        /// Creates a Permission
        /// </summary>
        /// <param name="model">permission model we want to create</param>
        /// <returns>The permission model created</returns>
        [ProducesResponseType(typeof(PermissionViewModel), (int)HttpStatusCode.OK)]
        [HttpPost]
        [KafkaProducer("testtopic", "create")]
        public async Task<IActionResult> Create([FromBody] PermissionModel model)
        {
            _logger.LogInformation("Create permission Api executing...");

            var permission = await _permissionService.Create(model);

            _logger.LogInformation("Create permission Api finished");

            return Ok(permission);
        }

        /// <summary>
        /// Updates a Permission
        /// </summary>
        /// <param name="model">Complete object model of the permission we want to update</param>
        /// <returns>The model of the permission updated</returns>
        [ProducesResponseType(typeof(Permission), (int)HttpStatusCode.OK)]
        [HttpPut]
        [KafkaProducer("testtopic", "update")]
        public async Task<IActionResult> Update([FromBody] PermissionViewModel model)
        {
            _logger.LogInformation("Update permission Api executing...");

            var permissionUpdated = await _permissionService.Update(model);

            _logger.LogInformation("Update permission Api finished");

            return Ok(permissionUpdated);
        }

        /// <summary>
        /// Deletes a permission
        /// </summary>
        /// <param name="id">id of the permission we want to delete.</param>
        /// <returns>amount of permissions affected with the delete</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpDelete("{id}")]
        [KafkaProducer("testtopic", "delete")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Delete permission Api executing...");

            var permissionsDeleted = await _permissionService.Delete(id);

            _logger.LogInformation("Delete permission Api finished");

            return Ok(permissionsDeleted);
        }
    }
}
