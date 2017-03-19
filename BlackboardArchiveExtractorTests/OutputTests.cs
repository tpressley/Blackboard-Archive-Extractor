using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS411Crystal;

namespace CS411Crystal.Tests
{
    [TestClass]
    public class OutputTests
    {
        [TestMethod]
        public void OutputTest()
        {
            Output output = new Output(System.IO.Directory.GetCurrentDirectory() + "\testDir");
            Assert.AreEqual(output.targetDir, System.IO.Directory.GetCurrentDirectory() + "\testDir");
        }

        [TestMethod]
        public void createDirTest()
        {
            //Initialize and test creating directory
            Output output = new Output(System.IO.Directory.GetCurrentDirectory() + "\testDir");
            Assert.AreEqual(output.createDir(), true);
            //Delete directory
            System.IO.Directory.Delete(System.IO.Directory.GetCurrentDirectory() + "\testDir");
            //Check nonexistent directory location returns false
            Assert.AreEqual(output.createDir("\\Doesntexist"), false);
            //Check creating existing directory returns false
            Assert.AreEqual(output.createDir(System.IO.Directory.GetCurrentDirectory()), false);
        }
    }
}
