﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            if (TestContext.TestName.StartsWith("FileNameDoesExists"))
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
            if (TestContext.TestName.StartsWith("FileNameDoesExists"))
            {
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine($"Deleting File: {_GoodFileName}");
                    //File.Delete(_GoodFileName);

                }
            }
        }

        #endregion


        [TestMethod]
        [Description("Check to see if a file does exist.")]
        [Owner("Fernanda")]
        [Priority(0)]
        public void FileNameDoesExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            SetGoodFileName();

            TestContext.WriteLine($"Testing File: {_GoodFileName}");
            fromCall = fp.FileExists(_GoodFileName);

            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        [Owner("FernandaN")]
        [DataSource("MySql.Data.MySqlClient",
            "Server=localhost;DataBase=unit-test;Uid=root;Pwd=123456", "FileProcessTest", DataAccessMethod.Sequential)]
        [Ignore]
        public void FileExistsTestFromDB()
        {
            //Anotãções: Crio a classe que eu quero testar
            FileProcess fp = new FileProcess();
            string fileName;

            //Anotações: Criando os tres campos
            bool expectedValue, causesException, fromCall;
 
            //Anotações: Preencho o fileName que esta no banco de dados
            fileName = TestContext.DataRow["FileName"].ToString();

            //Anotações: Preencho o expectedValue que está no banco de dados para um registro especifico
            expectedValue = Convert.ToBoolean(TestContext.DataRow["ExpectedValue"]);

            //Anotações: A causa da excessão
            causesException = Convert.ToBoolean(TestContext.DataRow["CausesException"]);

            try
            {
                fromCall = fp.FileExists(fileName);
                Assert.AreEqual(expectedValue, fromCall,
                    $"File: {fileName} has failed. METHOD: FileExistsTestFromDB");
            }
            catch (ArgumentException)
            {

                Assert.IsTrue(causesException);
            }
        }

        [TestMethod]
        [Ignore]
        public void FileNameDoesExistsSimpleMessage()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            SetGoodFileName();

            TestContext.WriteLine($"Testing File: {_GoodFileName}");
            fromCall = fp.FileExists(_GoodFileName);

            Assert.IsFalse(fromCall, "File Does NOT Exists");
        }

        [TestMethod]
        [Ignore]

        public void FileNameDoesExistsMessageFormatting()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            SetGoodFileName();

            TestContext.WriteLine($"Testing File: {_GoodFileName}");
            fromCall = fp.FileExists(_GoodFileName);

            // Anotação: Mostra o caminho no qual deu erro
            Assert.IsFalse(fromCall, "File {0} Does NOT Exists.", _GoodFileName);
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

        private const string FILE_NAME = @"FileToDeploy.txt";

        [TestMethod]
        [Owner("Fernanda-teste")]
        [DeploymentItem(FILE_NAME)]
        public void FileNameDoesExistsUsingDeploymentDirectory()
        {
            FileProcess fp = new FileProcess();
            string fileName;
            bool fromCall;

            fileName = $@"{TestContext.DeploymentDirectory}\{FILE_NAME}";
            TestContext.WriteLine($"Checking File: {fileName}");
            fromCall = fp.FileExists(fileName);


            Assert.IsTrue(fromCall);

        }

        // Anotação: Pode ser utilizado como teste de performace
        [TestMethod]
        [Timeout(3100)]
        [Priority(0)]

        public void SimulateTimout()
        {
            System.Threading.Thread.Sleep(3000);
        }

        [TestMethod]
        [Description("Check to see if a file does not exist.")]
        [Owner("Fernanda")]
        [Priority(1)]

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
        [Priority(1)]
        [TestCategory("Exception")]

        public void FileNameNullOrEmpty_ThrowsArgumentNullException()
        {
            FileProcess fp = new FileProcess();

            fp.FileExists("");

        }


        // Teste para excessão
        [TestMethod]
        [Owner("Fernanda")]
        [Priority(1)]

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
