﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sample.Persistence.EF.DbContexts;
using System.Transactions;

namespace Sample.Test.Tools.Infrastructure.DataBaseConfig.Integration;

public class BusinessIntegrationTest : EFDataContextDatabaseFixture
{
    protected EFDataContext DbContext { get; set; }
    protected EFDataContext SetupContext { get; set; }
    protected EFDataContext ReadContext { get; set; }

    public BusinessIntegrationTest()
    {
        DbContext = CreateDataContext();
        ReadContext = CreateDataContext();
        SetupContext = CreateDataContext();
    }
    protected void Save<T>(params T[] entities)
      where T : class
    {
        foreach (var entity in entities)
        {
            DbContext.Save(entity);
        }
    }
}

public class EFDataContextDatabaseFixture : DataBaseFixture
{
    protected static EFDataContext CreateDataContext()
    {
        var settings = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("testAppSettings.json", true, false)
            .AddEnvironmentVariables()
            .AddCommandLine(Environment.GetCommandLineArgs())
            .Build();
        var testSettings = new PersistenceConfig();
        settings.Bind("PersistenceConfig", testSettings);

        var optionsBuilder = new DbContextOptionsBuilder<EFDataContext>();
        optionsBuilder.UseSqlServer(testSettings.ConnectionString);

        return new EFDataContext(optionsBuilder.Options);
    }
}

public class PersistenceConfig
{
    public string ConnectionString { get; set; } = default!;//= "Server=.;Database=Medis-Sample-Test;user id=sa;password=123@medis;Integrated Security=False;Encrypt=True;TrustServerCertificate=True;";

}


public class DataBaseFixture : IDisposable
{
    private readonly TransactionScope _transactionScope;

    public DataBaseFixture()
    {
        _transactionScope = new TransactionScope(
            TransactionScopeOption.Required,
            TransactionScopeAsyncFlowOption.Enabled);
    }
    public void Dispose()
    {
        _transactionScope?.Dispose();
    }
}
