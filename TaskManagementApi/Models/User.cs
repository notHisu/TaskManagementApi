using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManagementApi.Models
{
    public class User : IdentityUser
    {
        public ICollection<TaskItem>? Tasks { get; set; }
        public ICollection<TaskComment>? Comments { get; set; }

    }
}
