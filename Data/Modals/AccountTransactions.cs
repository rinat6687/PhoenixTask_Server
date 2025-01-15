using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoenixTaskApp.Data.Modals
{
    [Table("ACCOUNT_TRANSACTIONS")]
    public class AccountTransactions
    {
        [Key]
        public int TransactionId { get; set; }
        public string FullName { get; set; }
        public string EnglishFullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [MaxLength(9)]
        public string UserId { get; set; }
        public int OperationId { get; set; }
        [MaxLength(20)]
        public string AccountNumber { get; set; } 
        public decimal Amount { get; set; }
        public DateTime? TransactionDate { get; set; }

    }
}
