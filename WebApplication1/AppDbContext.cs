using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Bu satırı kaldırabilirsiniz çünkü optionsBuilder parametresi constructor'da ayarlanacak.
        //var connectionString = "server=localhost;port=3306;database=db_staj;user=root;password=Td.dgn6+";
        //optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    public DbSet<User> Users { get; set; }
}