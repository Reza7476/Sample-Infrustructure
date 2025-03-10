using Sample.Commons.Interfaces;

namespace Sample.Persistence.EF.Interfaces;

public interface IInfrastructureAppService : IScope
{
    string? ConnectionString { get; }
}
