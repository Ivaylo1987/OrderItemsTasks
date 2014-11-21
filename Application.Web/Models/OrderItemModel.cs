namespace Application.Web.Models
{
    using Application.Models;
    using System.ComponentModel.DataAnnotations;

    public class OrderItemModel
    {
        private const int MaxQuantity = 20;

        [Required]
        [Range(1, MaxQuantity)]
        public int Quantity { get; set; }

        [Required]
        public ShoeSizes Size { get; set; }

        [Required]
        public int ShoeModelId { get; set; }
    }
}