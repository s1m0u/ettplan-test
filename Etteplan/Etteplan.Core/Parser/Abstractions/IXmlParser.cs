using Etteplan.Core.Models;

namespace Etteplan.Core.Parser.Abstractions
{
    public interface IXmlParser
    {
        ResultBase Parse(string xmlContent, string id);
    }
}
