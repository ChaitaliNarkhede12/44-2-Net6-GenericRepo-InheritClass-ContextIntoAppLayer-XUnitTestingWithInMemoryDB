using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCCS.UnitTesting.Core.MoqData;
using TCCS.UnitTesting.Core;
using TCCS.DataAccess.Models;
using TCCS.DataAccess.Repositories;

namespace TCCS.DataAccess.UnitTesting
{
    public class RepositoryTest : IClassFixture<TCCSDataFixture>
    {
        TCCSDataFixture fixture;

        public RepositoryTest(TCCSDataFixture fixture)
        {
            this.fixture = fixture;

            var employeeList = GetEmployeeList();
            var employeeMoq = new EmployeeMoq(fixture);

            var mockList = employeeMoq.GetMoqDataList();
            if (mockList.Count == 0)
            {
                employeeMoq.MoqDataList(employeeList);
            }

        }


        [Fact]
        public async Task GetAll_ShouldReturn()
        {
            var repo = new Repository<Employee, int>(fixture.tccsContext);

            var res = await repo.GetAll();

            Assert.Equal(10, res.Count());
        }

        [Fact]
        public async Task GetById_ShouldReturn()
        {
            var repo = new Repository<Employee, int>(fixture.tccsContext);

            var res = await repo.GetById(1);

            Assert.NotNull(res);
        }

        [Fact]
        public async Task GetByIdPredicate_ShouldReturn()
        {
            var repo = new Repository<Employee, int>(fixture.tccsContext);

            var res = await repo.GetById(x => x.Id == 1);

            Assert.Equal(1, res.Count());
        }




        [Fact]
        public async Task AddAsync_ShouldReturn()
        {
            var empployee = AddEmployee();

            var repo = new Repository<Employee, int>(fixture.tccsContext);

            var emp = await repo.AddAsync(empployee);
            var res = await repo.SaveChangesAsync();

            Assert.NotNull(emp);
            Assert.Equal(1, res);
        }

        [Fact]
        public async Task AddRange_ShouldReturn()
        {
            var empployees = AddEmployeeRange();

            var repo = new Repository<Employee, int>(fixture.tccsContext);

            repo.AddRange(empployees);
            var res = await repo.SaveChangesAsync();

            Assert.Equal(1, res);

        }

        [Fact]
        public async Task AddRangeAsync_ShouldReturn()
        {
            var empployees = AddEmployeeRangeAsync();

            var repo = new Repository<Employee, int>(fixture.tccsContext);

            await repo.AddRangeAsync(empployees);
            var res = await repo.SaveChangesAsync();

            Assert.Equal(3, res);

        }





        [Fact]
        public async Task Update_ShouldReturn()
        {
            var employee = UpdateEmployee();

            var repo = new Repository<Employee, int>(fixture.tccsContext);

            var emp = repo.Update(employee);
            var res = await repo.SaveChangesAsync();

            Assert.Equal(1, res);
        }

        [Fact]
        public async Task UpdateRange_ShouldReturn()
        {
            var employee = UpdateEmployeeRange();

            var repo = new Repository<Employee, int>(fixture.tccsContext);

            repo.UpdateRange(employee);
            var res = await repo.SaveChangesAsync();

            Assert.Equal(1, res);
        }





        [Fact]
        public async Task Remove_ShouldReturn()
        {
            var emp = RemoveEmployee();

            var repo = new Repository<Employee, int>(fixture.tccsContext);
            repo.Remove(emp);
        }

        [Fact]
        public async Task RemoveById_ShouldReturn()
        {
            var repo = new Repository<Employee, int>(fixture.tccsContext);
            repo.RemoveById(6);
        }

        [Fact]
        public async Task RemoveRange_ShouldRemove()
        {
            //Arrange
            var empployeeList = RemoveEmployeeRange();

            var repo = new Repository<Employee, int>(fixture.tccsContext);
            //Act
            repo.RemoveRange(empployeeList);
            var res = await repo.SaveChangesAsync();

            Assert.Equal(1, res);
        }




        [Fact]
        public async Task SingleOrDefaultAsync_ShouldReturnList()
        {
            //Arrange
            var repo = new Repository<Employee, int>(fixture.tccsContext);

            //Act
            var result = await repo.SingleOrDefaultAsync(x => x.Id == 8);

            //Assert
            Assert.Equal(result.Name, "test8");
        }

        [Fact]
        public async Task FirstOrDefaultAsync_ShouldReturnList()
        {
            //Arrange
            var repo = new Repository<Employee, int>(fixture.tccsContext);

            //Act
            var result = await repo.FirstOrDefaultAsync(x => x.Id == 9);

            //Assert
            Assert.Equal(result.Name, "test9");
        }



        private IEnumerable<Employee> GetEmployeeList()
        {
            List<Employee> employeeList = new List<Employee>()
            {
                new Employee{Id=1,Name="test1",EmailId="test1@gmail.com"},
                new Employee{Id=2,Name="test2",EmailId="test2@gmail.com"},
                new Employee{Id=3,Name="test3",EmailId="test3@gmail.com"},
                new Employee{Id=4,Name="test4",EmailId="test4@gmail.com"},
                new Employee{Id=5,Name="test5",EmailId="test5@gmail.com"},
                new Employee{Id=6,Name="test6",EmailId="test6@gmail.com"},
                new Employee{Id=7,Name="test7",EmailId="test7@gmail.com"},
                new Employee{Id=8,Name="test8",EmailId="test8@gmail.com"},
                new Employee{Id=9,Name="test9",EmailId="test9@gmail.com"},
            };

            return employeeList;
        }

        private Employee AddEmployee()
        {
            Employee employee = new Employee()
            {
                Name = "test10",
                EmailId = "test10@gmail.com"
            };

            return employee;
        }

        private List<Employee> AddEmployeeRange()
        {
            List<Employee> employeeList = new List<Employee>()
            {
                new Employee{Name="test11",EmailId="test11@gmail.com"}
            };

            return employeeList;
        }

        private List<Employee> AddEmployeeRangeAsync()
        {
            List<Employee> employeeList = new List<Employee>()
            {
                new Employee{Name="test12",EmailId="test12@gmail.com"}
            };

            return employeeList;
        }

        private Employee UpdateEmployee()
        {
            Employee employee = new Employee()
            {
                Id = 3,
                Name = "test3-33",
                EmailId = "test3@gmail.com"
            };

            return employee;
        }

        private List<Employee> UpdateEmployeeRange()
        {
            List<Employee> employee = new List<Employee>()
            {
                new Employee{
                    Id = 4,
                    Name = "test4-4",
                    EmailId = "test4@gmail.com"
                }
            };

            return employee;
        }

        private Employee RemoveEmployee()
        {
            Employee employee = new Employee()
            {
                Id = 5,
                Name = "test5",
                EmailId = "test5@gmail.com"
            };

            return employee;
        }

        private List<Employee> RemoveEmployeeRange()
        {
            List<Employee> employee = new List<Employee>()
            {
               new Employee{ Id=7,Name="test7-7",EmailId="test7@gmail.com"}
            };

            return employee;
        }
    }
}
