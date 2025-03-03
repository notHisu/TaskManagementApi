using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManagementApi.Models
{
    public class User : IdentityUser<int>
    {
        public ICollection<TaskItem>? Tasks { get; set; }
        public ICollection<TaskComment>? Comments { get; set; }

    }
}
