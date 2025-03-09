using Sample.Commons.Interfaces;

namespace Sample.Persistence.EF.Interfaces;

public interface IInfraastructureAppService : IScope
{
    string? ConnectionString { get; }
}
