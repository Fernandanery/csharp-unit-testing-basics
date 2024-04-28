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
        [Owner("FernandaN")]
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

            //You shall not pass!!
            peopleActual = peopleExpected;

            CollectionAssert.AreEqual(peopleExpected, peopleActual);

        }

        [TestMethod]
        [Owner("FernandaN")]
        public void AreCollectionEqualWithCompareTest()
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

            //You shall not pass!!
            peopleActual = peopleExpected;

            //Anotação: Criando um compare onde criamos o proprio modo de validação
            //Para cada item, verifica não o objeto em si, mas sim se ps valores são iguais
            CollectionAssert.AreEqual(peopleExpected, peopleActual, Comparer<Person>.Create((x, y) => 
            x.FirstName == y.FirstName && x.LastName == y.LastName ? 0 : 1));

        }

        [TestMethod]
        [Owner("FernandaN")]
        public void AreCollectionEquivalentTest()
        {

            PersonManager personManager = new PersonManager();
            List<Person> peopleExpected = new List<Person>();
            List<Person> peopleActual = new List<Person>();

            peopleActual = personManager.GetPeople();

            //Anotação: Temos dois elementos iguais porém em posições diferentes
            peopleExpected.Add(peopleActual[1]);
            peopleExpected.Add(peopleActual[2]);
            peopleExpected.Add(peopleActual[0]);

            //Anotação: Quando usamos o AreEquivalent não faz diferença a ordem que passamos os parametros
            CollectionAssert.AreEquivalent(peopleExpected, peopleActual);

        }

        [TestMethod]
        [Owner("FernandaN")]
        public void IsCollectionOfTypeTest()
        {

            PersonManager personManager = new PersonManager();
            List<Person> peopleActual = new List<Person>();

            peopleActual = personManager.GetSupervisor();

            CollectionAssert.AllItemsAreInstancesOfType(peopleActual, typeof(Supervisor));

        }

    }
}
