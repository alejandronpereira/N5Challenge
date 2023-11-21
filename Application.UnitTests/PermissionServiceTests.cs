using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Application.Tests
{
    [TestClass]
    public class PermissionServiceTests
    {
        private PermissionService? _permissionService;
        private Mock<IPermissionRepository>? _permissionRepositoryMock;
        private Mock<IMapper>? _mapperMock;
        private Mock<IElasticsearchService>? _elasticsearchServiceMock;
        private Mock<ILogger<PermissionService>>? _loggerMock;

        [TestInitialize]
        public void Setup()
        {
            _permissionRepositoryMock = new Mock<IPermissionRepository>();
            _mapperMock = new Mock<IMapper>();
            _elasticsearchServiceMock = new Mock<IElasticsearchService>();
            _loggerMock = new Mock<ILogger<PermissionService>>();

            _permissionService = new PermissionService(
                _permissionRepositoryMock.Object,
                _mapperMock.Object,
                _elasticsearchServiceMock.Object,
                _loggerMock.Object);
        }

        [TestMethod]
        public async Task Create_ValidPermission_ReturnsMappedPermission()
        {
            // Arrange
            var permissionModel = new PermissionModel
            {
                // Initialize fields
                EmployeeForename = "Steven",
                EmployeeSurname = "Spielberg",
                PermissionDate = DateTime.Now,
                PermissionType = 1
            };

            var expectedPermission = new Permission
            {
                // Initialize fields
                EmployeeForename = "Steven",
                EmployeeSurname = "Spielberg",
                PermissionDate = DateTime.Now,
                PermissionType = new PermissionType { Id = 1, Description = "Admin" }
            };

            _mapperMock?
                .Setup(m => m.Map<Permission>(It.IsAny<PermissionModel>()))
                .Returns(expectedPermission);

            _permissionRepositoryMock?
                .Setup(repo => repo.Create(It.IsAny<Permission>()))
                .ReturnsAsync(expectedPermission);

            // Act
            var result = await _permissionService.Create(permissionModel);

            // Assert
            Assert.IsNotNull(result); // Ensure result is not null
            Assert.AreEqual(expectedPermission.EmployeeForename, result.EmployeeForename);
            Assert.AreEqual(expectedPermission.EmployeeSurname, result.EmployeeSurname);
            // Add additional property assertions as needed
        }

        [TestMethod]
        public async Task Delete_ValidId_ReturnsDeletedPermissionCount()
        {
            // Arrange
            var permissionId = 1;
            var expectedDeletedCount = 1;

            _permissionRepositoryMock?.Setup(repo => repo.Delete(permissionId)).ReturnsAsync(expectedDeletedCount);

            // Act
            var result = await _permissionService.Delete(permissionId);

            // Assert
            Assert.AreEqual(expectedDeletedCount, result);
        }

        [TestMethod]
        public async Task GetAll_ReturnsAllPermissions()
        {
            // Arrange
            var expectedPermissions = new List<Permission> {
                new Permission
                {
                    // Initialize fields
                    EmployeeForename = "Steven",
                    EmployeeSurname = "Spielberg",
                    PermissionDate = DateTime.Now,
                    PermissionType = new PermissionType { Id = 1, Description = "Admin" }
                },
                { 
                new Permission
                {
                    EmployeeForename = "Martin",
                    EmployeeSurname = "Scorsese",
                    PermissionDate = DateTime.Now,
                    PermissionType = new PermissionType { Id = 1, Description = "Admin" }
                }
              } 
            };

            _permissionRepositoryMock?.Setup(repo => repo.GetAll()).ReturnsAsync(expectedPermissions);

            // Act
            var result = await _permissionService.GetAll();

            // Assert
            CollectionAssert.AreEqual(expectedPermissions, (System.Collections.ICollection)result);
        }

        [TestMethod]
        public async Task GetById_ValidId_ReturnsPermission()
        {
            // Arrange
            var permissionId = 1;
            var expectedPermission = new Permission
            {                     // Initialize fields
                EmployeeForename = "Steven",
                EmployeeSurname = "Spielberg",
                PermissionDate = DateTime.Now,
                PermissionType = new PermissionType { Id = 1, Description = "Admin" }
            };

            _permissionRepositoryMock?.Setup(repo => repo.GetById(permissionId)).ReturnsAsync(expectedPermission);
            _elasticsearchServiceMock?.Setup(service => service.GetPermissionAsync(permissionId)).ReturnsAsync(expectedPermission);

            // Act
            var result = await _permissionService.GetById(permissionId);

            // Assert
            Assert.AreEqual(expectedPermission, result);
        }

        [TestMethod]
        public async Task Update_ValidPermission_ReturnsUpdatedPermission()
        {
            // Arrange
            var permissionViewModel = new PermissionViewModel
            {          
                Id = 1,// Initialize fields
                EmployeeForename = "Steven",
                EmployeeSurname = "Spielberg",
                PermissionDate = DateTime.Now
            };
            var expectedPermission = new Permission
            {                     // Initialize fields
                EmployeeForename = "Steven",
                EmployeeSurname = "Spielberg",
                PermissionDate = DateTime.Now,
                PermissionType = new PermissionType { Id = 1, Description = "Admin" }
            };

            _mapperMock?.Setup(m => m.Map<Permission>(permissionViewModel)).Returns(expectedPermission);
            _permissionRepositoryMock?.Setup(repo => repo.Update(It.IsAny<Permission>())).ReturnsAsync(expectedPermission);
            _elasticsearchServiceMock?.Setup(service => service.ModifyPermissionAsync(expectedPermission)).ReturnsAsync(true);

            // Act
            var result = await _permissionService.Update(permissionViewModel);

            // Assert
            Assert.AreEqual(expectedPermission, result);
        }
    }
}
