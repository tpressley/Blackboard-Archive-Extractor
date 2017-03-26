using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS411Crystal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Request Tests
        /// </summary>
        [TestMethod()]
        public void RequestUrlTest()
        {
            // C:\Users\Grant\Downloads\imsmanifest.xml
            // init and check valid uri
            // alive page - 200
            ParseLinks parser = new ParseLinks("http://www.cs.odu.edu/~gatkins");
            bool isAlive = parser.RequestUrl();
            Assert.AreEqual("http://www.cs.odu.edu/~gatkins/",parser.finalUri);
            Assert.AreEqual(true, isAlive);
            // check 404 page - 404
            parser = new ParseLinks("http://www.cs.odu.edu/~gatkins/blahblah404definitely");
            isAlive = parser.RequestUrl();
            Assert.AreEqual("", parser.finalUri);
            Assert.AreEqual(false, isAlive);
            // check redirect to an alive page - 200
            parser = new ParseLinks("http://www.cs.odu.edu/~gatkins/cs532/redirect.php");
            isAlive = parser.RequestUrl();
            Assert.AreEqual("http://www.cs.odu.edu/~mln/teaching/cs532-s17/test/pdfs.html", parser.finalUri);
            Assert.AreEqual(true, isAlive);
            // check infinite redirect error - 404
            parser = new ParseLinks("http://www.cs.odu.edu/~gatkins/cs532/redirect2.php");
            isAlive = parser.RequestUrl();
            Assert.AreEqual("", parser.finalUri);
            Assert.AreEqual(false, isAlive);
        }

        /// <summary>
        /// Test isAbsoluteUri Function. Check relative path, absolute path, and no path reference
        /// Only absolute path should return true.
        /// </summary>
        [TestMethod()]
        public void AbsolutePathTest()
        {
            ParseLinks parser = new ParseLinks("./test.html");
            bool relativePath = parser.IsAbsoluteUri();
            Assert.AreEqual(relativePath,false);
            
            parser.uri = "http://www.cs.odu.edu";
            bool absolutePath = parser.IsAbsoluteUri();
            Assert.AreEqual(absolutePath, true);
            
            parser.uri = "";
            bool errorTest = parser.IsAbsoluteUri();
            Assert.AreEqual(errorTest, false);
        }
    }
}