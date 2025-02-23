namespace Sample.Commons.UnitOfWork;

public interface IUnitOfWork
{
    Task Begin();
    Task Commit();
    Task Complete();
    Task RoleBack();
}
