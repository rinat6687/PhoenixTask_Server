using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoenixTaskApp.Data.Modals
{
    [Table("OPERATIONS_TYPE")]
    public class OperationType
    {
        [Key]
        public int OperationId { get; set; }
        public string OperationDesc { get; set; }
    }
}
