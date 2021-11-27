using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prs_server_net6.Models {

    public record Request {
        [Key]
        public int Id { get; init; } = 0;
        [StringLength(80), Required]
        public string Description { get; init; } = string.Empty;
        [StringLength(80), Required]
        public string Justification { get; init; } = string.Empty;
        [StringLength(80)]
        public string? RejectionReason { get; set; } = null;
        [StringLength(30), Required]
        public string DeliveryMode { get; init; } = string.Empty;
        [StringLength(30), Required]
        public string Status { get; set; } = string.Empty;
        [Column(TypeName = "decimal(11,2)")]
        public decimal Total { get; set; } = 0;

        public int UserId { get; init; } = 0;
        public virtual User? User { get; init; } = default;

        public virtual IEnumerable<Requestline>? Requestlines { get; init; } = null;
    }
}

