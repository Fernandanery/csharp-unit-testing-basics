using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;

namespace MyClassesTest
{
    [TestClass]
    public class AssertClassTest
    {
        #region
        [TestMethod]
        [Owner("FernandaN")]
        // Anotações: Metodo de teste que compara as strings, se tiver algo de diferente entra as duas retorna erro
        public void AreEqualTest()
        {
            string str1 = "Fernanda";
            string srt2 = "Fernanda";

            Assert.AreEqual(str1, srt2);
        }

        [TestMethod]
        [Owner("FernandaN")]
        [ExpectedException(typeof(AssertFailedException))]
        // Anotações: Espera a excessão da falha
        public void AreEqualCaseSensitiveTest()
        {
            string str1 = "Fernanda";
            string srt2 = "fernanda";

            Assert.AreEqual(str1, srt2, false);
        }

        [TestMethod]
        [Owner("FernandaN")]
        //Anotações: Metodo para mostrar a diferença das strings
        public void AreNotEqualTest()
        {
            string str1 = "Fernanda";
            string srt2 = "Gabriel";

            Assert.AreNotEqual(str1, srt2);
        }
        #endregion

        #region
        [TestMethod]
        public void AreSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = x;

            Assert.AreSame(x, y);
        }
        
        [TestMethod]
        public void AreNotSame()
        {
            FileProcess x = new FileProcess();
            FileProcess y = new FileProcess();

            Assert.AreNotSame(x, y);
        }


        #endregion

    }
}
