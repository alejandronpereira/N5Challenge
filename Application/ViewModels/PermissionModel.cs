using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class PermissionModel
    {
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }
        public DateTime PermissionDate { get; set; }
        public int PermissionType { get; set; }
    }
}
