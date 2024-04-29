using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses.PersonClasses;
using System;
using System.Collections.Generic;

namespace MyClassesTest
{
    [TestClass]
    public class PersonManagerTest
    {
        [TestMethod]
        [Owner("FernandaN")]
        public void CreatePerson_OfTypeEmployeeTest()
        {
            PersonManager personManagaer = new PersonManager();
            Person person;

            person = personManagaer.CreatePerson("Fernanda", "Nery", false);

            Assert.IsInstanceOfType(person, typeof(Employee));

        }

        [TestMethod]
        [Owner("FernandaN")]
        public void DoEmployExistTest()
        {
            Supervisor supervisor = new Supervisor();

            supervisor.Employees = new List<Employee>();
            supervisor.Employees.Add(new Employee()
            {
                FirstName = "Fernanda",
                LastName = "Nery"
            });

            Assert.IsTrue(supervisor.Employees.Count > 0);

        }
    }
}
