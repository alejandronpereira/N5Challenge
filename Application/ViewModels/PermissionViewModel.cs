using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class PermissionViewModel
    {
        public int Id { get; set; }
        public required string EmployeeForename { get; set; }
        public required string EmployeeSurname { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
