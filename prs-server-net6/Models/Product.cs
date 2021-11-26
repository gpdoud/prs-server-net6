using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prs_server_net6.Models {

    [Index("PartNbr", IsUnique = true)]
    public record Product {
        [Key]
        public int Id { get; init; } = 0;
        [StringLength(30), Required]
        public string PartNbr { get; init; } = string.Empty;
        [StringLength(30), Required]
        public string Name { get; init; } = string.Empty;
        [Column(TypeName = "decimal(11,2)")]
        public decimal Price { get; init; } = 0;
        [StringLength(30), Required]
        public string Unit { get; init; } = "Each";
        public string? Photopath { get; init; } = null;

        public int VendorId { get; init; } = 0;
        public Vendor? Vendor { get; init; } = null;
    }
}

