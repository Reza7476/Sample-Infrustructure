using Sample.Commons.Interfaces;

namespace Sample.Commons.UnitOfWork;

public interface IUnitOfWork : IScope
{
    Task Begin();
    Task Commit();
    Task Complete();
    Task RoleBack();
}
