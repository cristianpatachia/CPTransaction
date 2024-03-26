using CPServer.Domain.ViewSql.Customer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPServer.Domain.ViewSql.Account;

[Table("Accounts")]
public class AccountSqlView
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid CustomerId { get; set; }

    public decimal Balance { get; set; }
}
