 using Moq;
using TCCS.DataAccess.Interfaces;
using TCCS.DataAccess.Models;
using TCCS.DataAccess.Repositories;
using TCCS.UnitTesting.Core;
using TCCS.UnitTesting.Core.MoqData;

namespace TCCS.DataAccess.UnitTesting
{
    public class EmployeeRespositoryTest : IClassFixture<TCCSDataFixture>
    {
        TCCSDataFixture fixture;

        public EmployeeRespositoryTest(TCCSDataFixture fixture)
        {
            this.fixture = fixture;

            var employeeList = GetEmployeeList();
            var employeeMoq = new EmployeeMoq(fixture);

            var mockList = employeeMoq.GetMoqDataList();
            if(mockList.Count == 0)
            {
                employeeMoq.MoqDataList(employeeList);
            }
            
        }

        [Fact]
        public async Task GetAllEmployee_ShouldReturnList()
        {
            var repository = new EmployeeRepository(fixture.tccsContext);

            //Act
            var result = await repository.GetAll();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Employee>>(result);
        }


        [Fact]
        public async Task GetEmployeeById_ShouldReturnList()
        {
            //Arrange

            int id = 1;
            var repository = new EmployeeRepository(fixture.tccsContext);

            //Act
            var result = await repository.GetEmployeeById(id);

            //Assert
            Assert.IsAssignableFrom<Employee>(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetEmployeeByIdByPredicate_ShouldReturnList()
        {
            //Arrange
            int id = 2;
            var repository = new EmployeeRepository(fixture.tccsContext);

            //Act
            var result = await repository.GetEmployeeById(x => x.Id == id);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Employee>>(result);
        }





        [Fact]
        public async Task AddEmployeeAsync_ShouldSaveemployee()
        {
            //Arrange
            var addemployee = AddEmployee();

            var repository = new EmployeeRepository(fixture.tccsContext);

            //Act
            var result = await repository.AddEmployeeAsync(addemployee);
            var res = await repository.SaveChangesAsync();
            //Assert
            Assert.IsAssignableFrom<Employee>(result);
            Assert.Equal(addemployee.Name, result.Name);
        }

        [Fact]
        public async Task AddEmployeeAsync_ThrowException()
        {
            //Arrange
            var repository = new EmployeeRepository(fixture.tccsContext);

            //Act
            Task act() => repository.AddEmployeeAsync(null);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task AddEmployeeRange_ShouldSaveemployee()
        {
            //Arrange
            var addEmployeeRange = AddEmployeeRange();

            var repository = new EmployeeRepository(fixture.tccsContext);

            //Act
            repository.AddEmployeeRange(addEmployeeRange);
            var res = await repository.SaveChangesAsync();
            //Assert
            Assert.Equal(1, res);
        }


        [Fact]
        public async Task AddEmployeeRangeAsync_ShouldSaveemployee()
        {
            //Arrange
            var addEmployeeRange = AddEmployeeRangeAsync();

            var repository = new EmployeeRepository(fixture.tccsContext);

            //Act
            await repository.AddEmployeeRangeAsync(addEmployeeRange);
            var res = await repository.SaveChangesAsync();
            //Assert
            Assert.Equal(1, res);
        }





        [Fact]
        public async Task UpdateEmployee_ShouldUpdateEmployee()
        {
            //Arrange
            var updateEmployee = UpdateEmployee();

            var repository = new EmployeeRepository(fixture.tccsContext);

            //Act
            var result = repository.UpdateEmployee(updateEmployee);
            var res = await repository.SaveChangesAsync();

            //Assert
            Assert.IsAssignableFrom<Employee>(result);
            Assert.Equal(updateEmployee.Name, result.Name);
        }

        [Fact]
        public void UpdateEmployee_ShouldThrowException()
        {
            //Arrange
            var repository = new EmployeeRepository(fixture.tccsContext);

            //Act
            Employee act() => repository.UpdateEmployee(null);

            //Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public async Task UpdateEmployeeRange_ShouldUpdateEmployee()
        {
            //Arrange
            var updateEmployee = UpdateEmployeeRange();

            var repository = new EmployeeRepository(fixture.tccsContext);

            //Act
            repository.UpdateEmployeeRange(updateEmployee);
            var res = await repository.SaveChangesAsync();

            //Assert
            Assert.Equal(1, res);
        }





        [Fact]
        public async Task RemoveEmployee_ShouldRemoveemployeeAsync()
        {
            //Arrange
            var removeEmployee = RemoveEmployee();

            var repository = new EmployeeRepository(fixture.tccsContext);

            //Act
            repository.RemoveEmployee(removeEmployee);
            var res = await repository.SaveChangesAsync();

            Assert.Equal(1, res);
        }

        [Fact]
        public async Task RemoveEmployeeById_ShouldRemoveemployeeAsync()
        {
            //Arrange
            int id = 6;

            var repository = new EmployeeRepository(fixture.tccsContext);
            //Act
            await repository.RemoveEmployeeById(id);
            var res = await repository.SaveChangesAsync();

            Assert.Equal(1, res);
        }

        [Fact]
        public async Task RemoveEmployeeRange_ShouldRemoveemployeeAsync()
        {
            //Arrange
            var empployeeList = RemoveEmployeeRange();

            var repository = new EmployeeRepository(fixture.tccsContext);
            //Act
            repository.RemoveEmployeeRange(empployeeList);
            var res = await repository.SaveChangesAsync();

            Assert.Equal(1, res);
        }




        [Fact]
        public async Task SingleOrDefaultEmployeeAsync_ShouldReturnList()
        {
            //Arrange
            var repository = new EmployeeRepository(fixture.tccsContext);

            //Act
            var result = await repository.SingleOrDefaultEmployeeAsync(x => x.Id == 8);

            //Assert
            Assert.Equal(result.Name, "test8");
        }

        [Fact]
        public async Task FirstOrDefaultEmployeeAsync_ShouldReturnList()
        {
            //Arrange
            var repository = new EmployeeRepository(fixture.tccsContext);

            //Act
            var result = await repository.FirstOrDefaultEmployeeAsync(x => x.Id == 9);

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