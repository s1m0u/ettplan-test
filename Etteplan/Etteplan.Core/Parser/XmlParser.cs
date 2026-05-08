using Etteplan.Core.Models;
using Etteplan.Core.Enums;
using Etteplan.Core.Parser.Abstractions;
using System.Xml;
using System.Xml.Linq;

namespace Etteplan.Core.Parser
{
    public class XmlParser : IXmlParser
    {
        public ResultBase Parse(string xmlContent, string id)
        {
            var doc = XDocument.Parse(xmlContent);

            var res = doc.Root!.Elements("trans-unit")
                .Where(e => e.Attribute("id")?.Value == id)
                .FirstOrDefault();

            if (res != null)
            {
                var target = res.Element("target")?.Value;

                if (target == null)
                {
                    return new ErrorResult("Expected target not found.");
                }

                if (string.IsNullOrEmpty(target))
                {
                    return new ErrorResult("Target is empty");
                }

                return new SuccessResult(id, target);
            }

            return new ErrorResult($"Element with Id {id} not found.");
        }
    }
}
