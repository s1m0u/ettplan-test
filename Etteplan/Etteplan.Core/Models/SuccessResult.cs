using Etteplan.Core.Enums;

namespace Etteplan.Core.Models
{
    public record SuccessResult(
            string Id,
            string Value
        ) 
        : ResultBase(Status.Success, DateTime.UtcNow);
}
