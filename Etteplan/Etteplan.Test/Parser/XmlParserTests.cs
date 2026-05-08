using Etteplan.Core.Enums;
using Etteplan.Core.Models;
using Etteplan.Core.Parser;
using Etteplan.Core.Parser.Abstractions;

namespace Etteplan.Test.Parser
{
    [TestClass]
    public sealed class XmlParserTests
    {
        private IXmlParser? _parser;

        [TestInitialize]
        public void Setup()
        {
            _parser = new XmlParser();
        }

        [TestMethod]
        public void ValidXml_ExistingId_SucessResult()
        {
            // Arrange
            var xmlContent = @"<trans-unit id=""42014"" restype=""string"">
				                <source>Filen hittades inte.</source>
				                <target>File not found.</target>
				                    <alt-trans>
					                    <target>Filen saknas.</target>
					                    <note>Alternative translation</note>
				                    </alt-trans>
			                   </trans-unit>
			                   <trans-unit id=""42015"" restype=""string"">
				                   <source>Loading...</source>
				                   <target>Laddar...</target>
				                   <note>Indicates a loading state</note>
			                   </trans-unit>";

            var idToParse = "42014";
            
            // Act
            var result = _parser!.Parse(xmlContent, idToParse);

            // Assert
            Assert.IsInstanceOfType<SuccessResult>(result);
            
            var successResult = (SuccessResult)result;

            Assert.AreEqual(Status.Success, successResult.Status);
            Assert.AreEqual(idToParse, successResult.Id);
            Assert.AreEqual("File not found.", successResult.Value);
        }

        [TestMethod]
        public void ValidXml_NonExistingId_ErrorResult()
        {
            // Arrange
            var xmlContent = @"<trans-unit id=""42015"" restype=""string"">
				                   <source>Loading...</source>
				                   <target>Laddar...</target>
				                   <note>Indicates a loading state</note>
			                   </trans-unit>
			                   <trans-unit id=""42016"" restype=""string"">
				                   <source>Connection lost.</source>
				                   <target>Anslutningen bröts.</target>
				                   <note>Displayed when network connection is lost</note>
			                   </trans-unit>";

            var idToParse = "42014";

            // Act
            var result = _parser!.Parse(xmlContent, idToParse);

            // Assert
            Assert.IsInstanceOfType<ErrorResult>(result);

            var errorResult = (ErrorResult)result;

            Assert.AreEqual(Status.Failure, errorResult.Status);
            Assert.AreEqual("Element with Id 42014 not found.", errorResult.Message);
        }

        [TestMethod]
        public void NonValidXml_ErrorResult()
        {
            // Arrange
            var xmlContent = @"<trans-unit id=""42014"" restype=""string"">
				                <source>Filen hittades inte.</source>
				                    <alt-trans>
					                    <target>Filen saknas.</target>
					                    <note>Alternative translation</note>
				                    </alt-trans>
			                   </trans-unit>
			                   <trans-unit id=""42015"" restype=""string"">
				                   <source>Loading...</source>
				                   <note>Indicates a loading state</note>
			                   </trans-unit>";

            var idToParse = "42014";

            // Act
            var result = _parser!.Parse(xmlContent, idToParse);

            // Assert
            Assert.IsInstanceOfType<ErrorResult>(result);

            var errorResult = (ErrorResult)result;

            Assert.AreEqual(Status.Failure, errorResult.Status);
            Assert.AreEqual("Expected tag target not found.", errorResult.Message);
        }
    }
}
