using Flow.Core.Areas.Returns;

namespace FlowTutorials.Application.Common.Seeds;

public interface IDbExceptionHandler
{
    Flow<T> Handle<T>(Exception ex);
}