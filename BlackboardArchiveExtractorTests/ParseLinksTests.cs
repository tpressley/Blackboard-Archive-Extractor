using ArchiveExtractorBusinessCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CS411Crystal.Tests
{
    [TestClass]
    public class ParseLinksTests
    {

        /// <summary>
        /// Constructor Test
        /// </summary>
        [TestMethod]
        public void ParseLinksTest()
        {
            var parser = new ParseLinks("http://www.cs.odu.edu/~gatkins");
            Assert.AreEqual(parser.Uri, "http://www.cs.odu.edu/~gatkins");
            Assert.AreEqual(parser.FinalUri, "");
        }

        /// <summary>
        /// Request Test
        /// </summary>
        [TestMethod]
        public void RequestUrlTest()
        {
            // init and check valid uri
            var parser = new ParseLinks("http://www.cs.odu.edu/~gatkins");
            var isAlive = parser.RequestUrl();
            Assert.AreEqual(isAlive,true);
            // check redirect
            parser = new ParseLinks("http://www.cs.odu.edu/~gatkins/cs532/redirect.php");
            isAlive = parser.RequestUrl();
            Assert.AreEqual(isAlive, false);
            // check infinite redirect error
            parser = new ParseLinks("http://www.cs.odu.edu/~gatkins/cs532/redirect2.php");
            isAlive = parser.RequestUrl();
            Assert.AreEqual(isAlive, false);
        }

        /// <summary>
        /// Test isAbsoluteUri Function. Check relative path, absolute path, and no path reference
        /// Only absolute path should return true.
        /// </summary>
        [TestMethod]
        public void AbsolutePathTest()
        {
            var parser = new ParseLinks("http://www.cs.odu.edu/~gatkins");
            var relativePath = parser.IsAbsoluteUri("./test.html");
            Assert.AreEqual(relativePath,false);

            var absolutePath = parser.IsAbsoluteUri("http://www.cs.odu.edu");
            Assert.AreEqual(absolutePath, true);

            var errorTest = parser.IsAbsoluteUri("");
            Assert.AreEqual(errorTest, false);
        }
    }
}