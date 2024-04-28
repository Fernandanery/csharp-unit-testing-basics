using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses.PersonClasses;
using System;

namespace MyClassesTest
{
    [TestClass]
    public class PersonManagerTest
    {
        [TestMethod]
        public void CreatePerson_OfTypeEmployeeTest()
        {
            PersonManager personManagaer = new PersonManager();
            Person person;

            person = personManagaer.CreatePerson("Fernanda", "Nery", false);

            Assert.IsInstanceOfType(person, typeof(Employee));

        }
    }
}
