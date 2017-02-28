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
        [TestMethod()]
        public void ParseLinksTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void requestUrlTest()
        {
            ParseLinks parser = new ParseLinks("http://www.cs.odu.edu/~gatkins");
            Assert.Fail();
        }
    }
}