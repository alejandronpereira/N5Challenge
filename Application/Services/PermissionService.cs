using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        private readonly IElasticsearchService _elasticsearchService;
        private readonly ILogger<PermissionService> _logger;

        public PermissionService(IPermissionRepository permissionRepository,
                                IMapper mapper,
                                IElasticsearchService elasticsearchService,
                                ILogger<PermissionService> logger)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _elasticsearchService = elasticsearchService;
            _logger = logger;
        }

        public async Task<Permission> Create(PermissionModel model)
        {
            try
            {
                var entity = _mapper.Map<Permission>(model);
                var permission = await _permissionRepository.Create(entity);
                await _elasticsearchService.IndexPermissionAsync(permission);
                return permission;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in Create method.");
                throw;
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                return await _permissionRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in Delete method.");
                throw;
            }
        }

        public async Task<ICollection<Permission>> GetAll()
        {
            try
            {
                var permissions = await _permissionRepository.GetAll();
                return permissions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in GetAll method.");
                throw;
            }
        }

        public async Task<Permission> GetById(int id)
        {
            try
            {
                var permission = await _permissionRepository.GetById(id);

                if (permission == null)
                {
                    _logger.LogWarning("Role not found for id: {Id}", id);
                }

                return await _elasticsearchService.GetPermissionAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in GetById method for id: {Id}", id);
                throw;
            }
        }

        public async Task<Permission> Update(PermissionViewModel model)
        {
            try
            {
                var entity = _mapper.Map<Permission>(model);
                var permissionUpdated = await _permissionRepository.Update(entity);
                await _elasticsearchService.ModifyPermissionAsync(permissionUpdated);
                return permissionUpdated;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in Update method.");
                throw;
            }
        }
    }
}
