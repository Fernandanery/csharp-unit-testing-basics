using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses.PersonClasses;
using System;
using System.Collections.Generic;

namespace MyClassesTest
{
    [TestClass]
    public class CollectionAssertClassTest
    {
        [TestMethod]
        public void AreCollectionEqualFailsBecauseNoCompareTest()
        {
            PersonManager personManager = new PersonManager();

            List<Person> peopleExpected = new List<Person>();
            List<Person> peopleActual = new List<Person>();

            peopleExpected.Add(new Person()
            {
                FirstName = "Fernanda",
                LastName = "Nery"
            });

            peopleExpected.Add(new Person() 
            {
                FirstName = "Laura",
                LastName = "Antonia"

            });

            peopleExpected.Add(new Person()
            {
                FirstName = "Thiago",
                LastName = "Jose"
            
            });

            peopleActual = peopleExpected;

            CollectionAssert.AreEqual(peopleExpected, peopleActual);

        }

    }
}
