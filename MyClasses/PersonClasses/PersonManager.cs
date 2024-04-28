using System;
using System.Collections.Generic;

namespace MyClasses.PersonClasses
{
    public class PersonManager
    {
        public Person CreatePerson(string first, string last, bool isSupervisor)
        {
            Person ret = null;

            if (!string.IsNullOrEmpty(first))
            {
                if (isSupervisor)
                {
                    ret = new Supervisor();
                }
                else
                    ret = new Employee();

                ret.FirstName = first;
                ret.LastName = last;

            }

            return ret;

        }

        public List<Person> GetPeople()
        {
            List<Person> people = new List<Person>();

            people.Add(new Person()
            {
                FirstName = "Fernanda",
                LastName = "Nery"
            });

            people.Add(new Person()
            {
                FirstName = "Laura",
                LastName = "Antonia"

            });

            people.Add(new Person()
            {
                FirstName = "Thiago",
                LastName = "Jose"

            });

            return people;

        }
    }
}
