namespace Scrapper.Application.Dtos
{
    public record ReviewEntry(string Date, string User, string Title, string Content, int Rating)
    {
        private readonly IList<Employee> employeesWorkedWith = new List<Employee>(1); 

        public void AddEmployee(Employee e)
        {
            employeesWorkedWith.Add(e);
        }

        public IReadOnlyList<Employee> Employees => employeesWorkedWith.ToList();
    }

    public record Employee(string Name, int Rating);
}
