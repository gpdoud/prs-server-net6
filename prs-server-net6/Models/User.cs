using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prs_server_net6.Models {

    [Index("Username", IsUnique = true)]
    public record User {
        [Key]
        public int Id { get; init; }
        [StringLength(30), Required]
        public string Username { get; init; } = string.Empty;
        [StringLength(30), Required]
        public string Password { get; init; } = string.Empty;
        [StringLength(30), Required]
        public string Firstname { get; init; } = string.Empty;
        [StringLength(30), Required]
        public string Lastname { get; init; } = string.Empty;
        [StringLength(12)]
        public string? Phone { get; init; } = null;
        [StringLength(120)]
        public string? Email { get; init; } = null;
        public bool IsReviewer { get; init; } = false;
        public bool IsAdmin { get; init; } = false;
    }
}

