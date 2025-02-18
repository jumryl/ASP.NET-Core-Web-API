﻿using Microsoft.EntityFrameworkCore;

namespace WebOopPrac_Api.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UniqueID { get; set; }
    }
}
