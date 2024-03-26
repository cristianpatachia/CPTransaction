using CPServer.Domain.ViewSql.Customer;
using CPServer.Domain.ViewSql.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPServer.Domain.ViewSql.Customer;

[Table("Customers")]
public class CustomerSqlView
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    public string Name { get; set; }
}
