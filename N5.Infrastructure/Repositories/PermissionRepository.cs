using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly N5DbContext _context;
        public PermissionRepository(N5DbContext context)
        {
            _context = context;
        }
        public async Task<Permission> Create(Permission model)
        {
            var existingPermissionType = await _context.PermissionType.FindAsync(model.PermissionType.Id);

            if (existingPermissionType == null)
            {
                throw new InvalidOperationException("PermissionType with the given Id does not exist.");
            }

            // Associate the existing PermissionType Id to the Permission
            model.PermissionType = existingPermissionType;

            var permission = await _context.Permission.AddAsync(model);
            await _context.SaveChangesAsync();

            return permission.Entity;
        }

        public async Task<int> Delete(int id)
        {
            var permissionToDelete = await _context.Permission.FirstOrDefaultAsync(x => x.Id == id);
            int permisionsDeleted = 0;

            if (permissionToDelete == null) throw new Exception($"Permission with id:{id} was not found when trying to delete");

            _context.Permission.Remove(permissionToDelete);
            permisionsDeleted = await _context.SaveChangesAsync();
            return permisionsDeleted;


        }

        public async Task<ICollection<Permission>> GetAll()
        {
            return await _context.Permission.Include(p => p.PermissionType).ToListAsync();
        }

        public async Task<Permission> GetById(int id)
        {
            var permission = await _context.Permission.Include(p => p.PermissionType).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (permission == null)
            {
                throw new InvalidOperationException("Permission not found");
            }

            return permission;
        }

        public async Task<Permission> Update(Permission model)
        {
            var permission = await _context.Permission.Include(p => p.PermissionType).FirstOrDefaultAsync(x => x.Id == model.Id);

            if (permission == null) throw new Exception($"Permission with id:{model.Id} was not found when trying to update a Permission");

            _context.Entry(permission).CurrentValues.SetValues(model);
            var result = await _context.SaveChangesAsync();

            return permission;
        }
    }
}
