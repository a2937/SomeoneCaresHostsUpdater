using HostsDownloaderWindows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HostsDownloaderUnitTestProject
{
    [TestClass]
    public class HostsFileRetrieverTests
    {
        private MockRepository mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }



        [TestMethod]
        public void GetWebHostsFile_StateUnderTest_ExpectedBehavior()
        {
            // Act
            var result = HostsFileRetriever.IsWebHostsFileAccessible();

            // Assert
            Assert.IsTrue(result);
        }

        /*
        [TestMethod]
        public void GetLocalHostsFile_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateHostsFileRetriever();

            // Act
            var result = unitUnderTest.GetLocalHostsFile();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void GetHostsFilePath_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateHostsFileRetriever();

            // Act
            var result = unitUnderTest.GetHostsFilePath();

            // Assert
            Assert.Fail();
        }
        */
    }
}
