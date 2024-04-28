using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;

namespace MyClassesTest
{
    [TestClass]
    public class StringAssertClassTest
    {
        [TestMethod]
        [Owner("FernandaN")]
        public void ContainsTest()
        {
            string str1 = "Fernanda Nery";
            string str2 = "Nery";

            StringAssert.Contains(str1, str2);
        }

        [TestMethod]
        [Owner("FernandaN")]
        public void StartsWithTest()
        {
            string str1 = "Todos Caixa Alta";
            string str2 = "Todos Caixa";

            StringAssert.StartsWith(str1, str2);
        }


        [TestMethod]
        [Owner("FernandaN")]
        public void IsAllLowerCaseTest()
        {
            Regex reg = new Regex(@"^([^A-Z])+$");
            //Anotação: procura pelo menos um item que tenha caixa baixa
            //Se não tiver, precisa dar erro

            StringAssert.Matches("todos caixa", reg);
        }

        [TestMethod]
        [Owner("FernandaN")]
        public void IsNotAllLowerCaseTest()
        {
            Regex reg = new Regex(@"^([^A-Z])+$");
            //Anotação: procura pelo menos um item que tenha caixa alta
            //Se não tiver, precisa dar erro

            StringAssert.DoesNotMatch("Todos caixa", reg);
        }


    }
}
