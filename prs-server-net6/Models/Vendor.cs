using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace prs_server_net6.Models {

    [Index("Code", IsUnique = true)]
    public record Vendor {
        [Key]
        public int Id { get; set; } = 0;
        [StringLength(30), Required]
        public string Code { get; set; } = string.Empty;
        [StringLength(30), Required]
        public string Name { get; set; } = string.Empty;
        [StringLength(30), Required]
        public string Address { get; set; } = string.Empty;
        [StringLength(30), Required]
        public string City { get; set; } = string.Empty;
        [StringLength(2), Required]
        public string State { get; set; } = string.Empty;
        [StringLength(5), Required]
        public string Zip { get; set; } = string.Empty;
        [StringLength(12)]
        public string? Phone { get; set; } = default;
        [StringLength(120)]
        public string? Email { get; set; } = default;
    }
}

