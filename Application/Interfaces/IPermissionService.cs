using Application.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPermissionService
    {
        Task<ICollection<Permission>> GetAll();
        Task<Permission> GetById(int id);
        Task<Permission> Create(PermissionModel model);
        Task<Permission> Update(PermissionViewModel model);
        Task<int> Delete(int id);
    }
}
