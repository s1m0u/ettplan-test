using Etteplan.Core.Enums;

namespace Etteplan.Core.Models
{
    public abstract record ResultBase(
            Status Status,
            DateTime Timestamp
        );
}
