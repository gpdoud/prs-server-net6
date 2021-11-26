using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace prs_server_net6.Models {

    public record Requestline {
        [Key]
        public int Id { get; init; } = 0;
        public int Quantity { get; init; } = 1;
        public int RequestId { get; init; } = 0;
        [JsonIgnore]
        public virtual Request? Request { get; init; } = null;
        public int ProductId { get; init; } = 0;
        public virtual Product? Product { get; init; } = null;

    }
}

