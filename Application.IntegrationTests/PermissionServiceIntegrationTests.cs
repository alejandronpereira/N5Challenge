using Application.Services;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nest;
using System.Threading.Tasks;

namespace Application.IntegrationTests
{
    [TestClass]
    public class PermissionServiceIntegrationTests
    {
        private ElasticsearchService _elasticsearchService;
        private Mock<ElasticClient> _mockElasticClient;

        [TestInitialize]
        public void SetUp()
        {
            _mockElasticClient = new Mock<ElasticClient>();
            _elasticsearchService = new ElasticsearchService();
        }

        [TestMethod]
        public async Task GetPermissionAsync_ExistingId_ReturnsExpectedPermission()
        {
            // Arrange
            var permissionId = 1; // Assuming this ID exists in the "permissions" index
            var expectedPermission = new Permission
            {
                Id = permissionId,
                // Set other fields as needed for the expected permission data
            };

            var getResponse = new GetResponse<Permission>();
            getResponse.Source = expectedPermission; // Assigning the expected permission to the Source property

            _mockElasticClient
                .Setup(client => client.GetAsync<Permission>(
                    It.IsAny<int>(),
                    It.IsAny<Func<GetDescriptor<Permission>, IGetRequest>>(),
                    default))
                .ReturnsAsync(getResponse);

            // Act
            var retrievedPermission = await _elasticsearchService.GetPermissionAsync(permissionId);

            // Assert
            Assert.IsNotNull(retrievedPermission);
            // Validate retrievedPermission fields against expectedPermission fields
            Assert.AreEqual(expectedPermission.Id, retrievedPermission.Id);
            // Add assertions for other fields as needed
        }
    }
}