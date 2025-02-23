using Sample.Persistence.EF.Persistence;

namespace Sample.Test.Tools.Infrastructure.DataBaseConfig.Unit;

public class BusinessUnitTest
{
    protected EFDataContext DbContext { get; set; }
    protected EFDataContext SetupContext { get; set; }
    protected EFDataContext ReadContext { get; set; }

    public BusinessUnitTest()
    {
        var db = CreateDatabase();

        DbContext = db.CreateDataContext<EFDataContext>();

        SetupContext = db.CreateDataContext<EFDataContext>();

        ReadContext = db.CreateDataContext<EFDataContext>();
    }

    protected EFInMemoryDataBase CreateDatabase()
    {
        return new EFInMemoryDataBase();
    }

    protected void Save<T>(T entity)
    {
        if (entity != null)
        {
            DbContext.Manipulate(_ => _.Add(entity));
        }
    }
}
