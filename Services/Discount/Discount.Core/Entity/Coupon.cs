using System.ComponentModel.DataAnnotations;

namespace Discount.Core.Entity
{
    public class Coupon : BaseEntity
    {
        public string ProductId { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpiredDate { get; set; }
        public bool isActivate { get; set; } = false;
    }
}
