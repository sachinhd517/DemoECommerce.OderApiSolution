using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OderApi.Application.DTOs
{
    public record ProductDTO
    (
        int Id,
        [Required] string Name,
        [Required, Range(0, int.MaxValue)] int Quantity,
        [Required, DataType(DataType.Currency)] decimal Price
    );
}
