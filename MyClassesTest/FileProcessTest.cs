using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System;
using System.Configuration;
using System.IO;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest
    {
        private const string BAD_FILE_NAME = @"C:\BadFileName.txt";
        private string _GoodFileName;
        public TestContext TestContext { get; set; }

        #region Test Initialize e Cleanup

        // Esses testes é orientado para ser executado apenas local, para realizar no servidor deve ser efetuado na camada assembly (dll)
        // Anotações: Quando pedir para rodar o teste FileNameDoesExists o método TestInitialize será executado antes

        [TestInitialize]
        public void TestInitialize()
        {
            if (TestContext.TestName == "FileNameDoesExists")
            {
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    SetGoodFileName();
                    TestContext.WriteLine($"Creating File: {_GoodFileName}");
                    File.AppendAllText(_GoodFileName, "Some text");

                }

            }

        }

        // Anotações: Após a finalização do método TestInitialize inica o processo do TestCleanup

        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.TestName == "FileNameDoesExists")
            {
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine($"Deleting File: {_GoodFileName}");
                    File.Delete(_GoodFileName);

                }

            }

        }

        #endregion


        [TestMethod]
        [Description("Check to see if a file does exist.")]
        [Owner("Fernanda")]
        public void FileNameDoesExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            SetGoodFileName();

            //TestContext.WriteLine($"Creating File: {_GoodFileName}"); //Mostra o step do teste no output
            //File.AppendAllText(_GoodFileName, "Some text");

            TestContext.WriteLine($"Testing File: {_GoodFileName}");
            fromCall = fp.FileExists(_GoodFileName);

            //TestContext.WriteLine($"Deleting File: {_GoodFileName}");
            //File.Delete(_GoodFileName);

            Assert.IsTrue(fromCall);
            //Assert.IsFalse(fromCall);
        }

        public void SetGoodFileName()
        {
            _GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];

            if (_GoodFileName.Contains("[AppPath]"))
            {
                _GoodFileName = _GoodFileName.Replace("[AppPath]", Environment.GetFolderPath
                                                                   (Environment.SpecialFolder.ApplicationData));
            }
        }

        // Anotação: Pode ser utilizado como teste de performace
        [TestMethod]
        [Timeout(3100)]
        public void SimulateTimout()
        {
            System.Threading.Thread.Sleep(3000);
        }

        [TestMethod]
        [Description("Check to see if a file does not exist.")]
        [Owner("Fernanda")]
        public void FileNameDoesNotExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            fromCall = fp.FileExists(BAD_FILE_NAME);
            Assert.IsFalse(fromCall);
        }

        // Teste para excessão
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [Owner("Fernanda")]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException()
        {
            FileProcess fp = new FileProcess();

            fp.FileExists("");

        }


        // Teste para excessão
        [TestMethod]
        [Owner("Fernanda")]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();

            try
            {
                fp.FileExists("");
            }
            catch (Exception)
            {
                // The test was a Sucess
                return;
            }

            Assert.Fail("Fail expected");

        }
    }
}
