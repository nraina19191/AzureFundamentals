using System.ComponentModel.DataAnnotations;

namespace AzureBlobProject.Models
{
    public class AzureContainer
    {
        [Required]
        public string ContainerName { get; set; }
    }
}
