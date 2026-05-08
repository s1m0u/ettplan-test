using Etteplan.Core.Enums;

namespace Etteplan.Core.Models
{
    public record ErrorResult(
            string Message
        )
        : ResultBase(Status.Failure, DateTime.UtcNow);
}