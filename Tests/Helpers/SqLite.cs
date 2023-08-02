using MedicineScheduler.DataAccessLayer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data.Common;

namespace MedicineScheduler.Tests.Helpers;

public static class SqLite
{
  public static EFContext GetEFContextInMemory()
  {
    var options = new DbContextOptionsBuilder<EFContext>();
    options.EnableSensitiveDataLogging();
    options.LogTo(message => System.Diagnostics.Debug.WriteLine(message));
    var connection = new SqliteConnection("Data Source=:memory:");
    connection.Open();
    options.UseSqlite(connection);
    return new SqLiteInMemoryContext(options.Options);
  }
}

public sealed class SqLiteInMemoryContext : EFContext, IDisposable
{
  readonly DbConnection? _connection;
  public SqLiteInMemoryContext(DbContextOptions<EFContext> options) : base(options)
  {
    _connection = RelationalOptionsExtension.Extract(options).Connection;
    Database.EnsureCreated();
  }

  public new void Dispose()
  {
    base.Dispose();
    _connection?.Dispose();
  }
}