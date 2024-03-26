using CPServer.Domain.ValueObjects.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPServer.Domain.ViewSql.Transaction;

[Table("Transactions")]
public class TransactionSqlView
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    public DateTime Timestamp { get; set; }

    public decimal Amount { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid SenderId { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid RecipientId { get; set; }

    public string? Details { get; set; }

    public TransactionStatus Status { get; set; }

    public DateTime ReceivedUtcDateTime { get; set; } = DateTime.UtcNow;
}