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
        public string Username { get; set; } = string.Empty;
        [StringLength(30), Required]
        public string Password { get; set; } = string.Empty;
        [StringLength(30), Required]
        public string Firstname { get; set; } = string.Empty;
        [StringLength(30), Required]
        public string Lastname { get; set; } = string.Empty;
        [StringLength(12)]
        public string? Phone { get; set; } = null;
        [StringLength(120)]
        public string? Email { get; set; } = null;
        public bool IsReviewer { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
        [StringLength(80)]
        public string? Apikey { get; set; } = null;
    }
}

