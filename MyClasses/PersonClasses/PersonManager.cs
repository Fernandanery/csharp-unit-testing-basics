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
    }
}
