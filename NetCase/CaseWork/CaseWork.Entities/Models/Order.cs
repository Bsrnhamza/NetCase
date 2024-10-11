using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseWork.Entities.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CarrierID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "OrderDesi must be greater than 0.")]
        public int OrderDesi { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderCarrierCost { get; set; }
    }
}
