using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DomainModels
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DepartmentId { get; set; }

        public string RowVersion { get; set; }
    }
}
