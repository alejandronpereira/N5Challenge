using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPermissionRepository
    {
        Task<ICollection<Permission>> GetAll();
        Task<Permission> GetById(int id);
        Task<Permission> Create(Permission model);
        Task<Permission> Update(Permission model);
        Task<int> Delete(int id);
    }
}
