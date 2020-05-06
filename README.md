# Party Thyme

This is a console app that allows users to track their gardens. This will let us track what plants we have planted, how long ago they were planted and other details.

# Objectives

- Create a console app that uses an ORM to talk to a database
- Working with EF Core
- Re-enforce SQL fundamentals

# Includes

- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [LINQ](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)
- [EF CORE](https://docs.microsoft.com/en-us/ef/core/)
- [POSTGRESQL](https://www.postgresql.org/)
- [MVC](https://dotnet.microsoft.com/apps/aspnet/mvc)

# Featured Code

## Creating our Database with a EF Core First Approach

```JSX
public partial class PlantContext : DbContext
  {
    public DbSet<Plant> Plants { get; set; }

    public PlantContext()
    {
    }

    public PlantContext(DbContextOptions<PlantContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        optionsBuilder.UseNpgsql("server=localhost;database=PlantDatabase");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
```
