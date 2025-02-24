﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManagementApi.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
