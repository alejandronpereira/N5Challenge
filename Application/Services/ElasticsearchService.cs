using Application.Interfaces;
using Domain.Entities;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly ElasticClient _elasticClient;

        public ElasticsearchService()
        {
            var elasticsearchUri = "http://localhost:9200";
            var defaultIndex = "permissions";

            var settings = new ConnectionSettings(new Uri(elasticsearchUri))
                .DefaultIndex(defaultIndex);

            _elasticClient = new ElasticClient(settings);
        }

        public async Task IndexPermissionAsync(Permission permission)
        {
            var indexResponse = await _elasticClient.IndexDocumentAsync(permission);

            if (!indexResponse.IsValid)
            {
                Console.WriteLine($"Failed to index document: {indexResponse.DebugInformation}");
            }
        }

        public async Task<Permission> GetPermissionAsync(int id)
        {
            var response = await _elasticClient.GetAsync<Permission>(id, idx => idx.Index("permissions"));
            return response.Source;
        }

        public async Task<bool> ModifyPermissionAsync(Permission modifiedPermission)
        {
            var updateResponse = await _elasticClient.UpdateAsync<Permission, object>(
                modifiedPermission.Id,
                descriptor => descriptor.Doc(modifiedPermission).Index("permissions")
            );
            return updateResponse.IsValid;
        }
    }
}
