using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IElasticsearchService
    {
        Task IndexPermissionAsync(Permission permission);
        Task<Permission> GetPermissionAsync(int id);
        Task<bool> ModifyPermissionAsync(Permission modifiedPermission);
    }
}
