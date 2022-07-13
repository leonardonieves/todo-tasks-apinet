using System.ComponentModel.DataAnnotations;

namespace APICore.Common.DTO.Request
{
    public class TaskMRequest
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}