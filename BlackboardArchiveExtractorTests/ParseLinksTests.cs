using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS411Crystal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchiveExtractorBusinessCode;

namespace CS411Crystal.Tests
{
    [TestClass()]
    public class ParseLinksTests
    {

        /// <summary>
        /// Constructor Test
        /// </summary>
        [TestMethod()]
        public void ParseLinksTest()
        {
            ParseLinks parser = new ParseLinks("http://www.cs.odu.edu/~gatkins");
            Assert.AreEqual(parser.uri, "http://www.cs.odu.edu/~gatkins");
            Assert.AreEqual(parser.finalUri, "");
        }

        /// <summary>
        /// Request Test
        /// </summary>
        [TestMethod()]
        public void requestUrlTest()
        {
            // init and check valid uri
            ParseLinks parser = new ParseLinks("http://www.cs.odu.edu/~gatkins");
            bool isAlive = parser.requestUrl();
            Assert.AreEqual(isAlive,true);
            // check redirect
            parser = new ParseLinks("http://www.cs.odu.edu/~gatkins/cs532/redirect.php");
            isAlive = parser.requestUrl();
            Assert.AreEqual(isAlive, false);
            // check infinite redirect error
            parser = new ParseLinks("http://www.cs.odu.edu/~gatkins/cs532/redirect2.php");
            isAlive = parser.requestUrl();
            Assert.AreEqual(isAlive, false);
        }

        /// <summary>
        /// Test isAbsoluteUri Function. Check relative path, absolute path, and no path reference
        /// Only absolute path should return true.
        /// </summary>
        [TestMethod()]
        public void AbsolutePathTest()
        {
            ParseLinks parser = new ParseLinks("http://www.cs.odu.edu/~gatkins");
            bool relativePath = parser.isAbsoluteUri("./test.html");
            Assert.AreEqual(relativePath,false);

            bool absolutePath = parser.isAbsoluteUri("http://www.cs.odu.edu");
            Assert.AreEqual(absolutePath, true);

            bool errorTest = parser.isAbsoluteUri("");
            Assert.AreEqual(errorTest, false);
        }
    }
}