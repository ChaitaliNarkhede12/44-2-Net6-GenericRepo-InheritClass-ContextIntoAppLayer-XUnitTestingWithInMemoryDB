using AutoMapper;
using Moq;
using TCCS.Application.Services;
using TCCS.Application.ViewModels;
using TCCS.DataAccess.Interfaces;
using TCCS.DataAccess.Models;
using TCCS.DataAccess.Repositories;
using TCCS.UnitTesting.Core;
using TCCS.UnitTesting.Core.MoqData;

namespace TCCS.Application.UnitTesting
{
    public class EmployeeServiceTest : IClassFixture<TCCSDataFixture>
    {
        TCCSDataFixture fixture;
        IMapper mapper;

        public EmployeeServiceTest(TCCSDataFixture fixture)
        {
            this.fixture = fixture;

            AutoMapping autoMapping = new AutoMapping();
            mapper = autoMapping.GetMapper(new EmployeeMapping());

            var employeeModelList = GetEmployeeModelList();
            var employeesList = mapper.Map<IEnumerable<Employee>>(employeeModelList);


            EmployeeMoq employeeMoq = new EmployeeMoq(fixture);

            var mockList = employeeMoq.GetMoqDataList();

            if (mockList.Count == 0)
            {
                employeeMoq.MoqDataList(employeesList);
            }

        }

        [Fact]
        public async Task GetAllEmployee_ShouldReturnList()
        {
            //Arrange
            var service = new EmployeeService(new EmployeeRepository(fixture.tccsContext), mapper, fixture.tccsContext);

            //Act
            var result = await service.GetAllEmployee();

            //Assert
            Assert.IsAssignableFrom<List<EmployeeModel>>(result);
            Assert.NotNull(result);
            Assert.Equal(9, result.Count());
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnList()
        {
            //Arrange
            int id = 1;
            var service = new EmployeeService(new EmployeeRepository(fixture.tccsContext), mapper, fixture.tccsContext);

            //Act
            var result = await service.GetEmployeeById(id);

            //Assert
            Assert.IsAssignableFrom<EmployeeModel>(result);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetEmployeeByIdUsingPredicate_ShouldReturnList()
        {
            //Arrange
            //Arrange
            int id = 1;
            var service = new EmployeeService(new EmployeeRepository(fixture.tccsContext), mapper, fixture.tccsContext);

            //Act
            var result = await service.GetEmployeeById(x=>x.Id == id);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<EmployeeModel>>(result);
            Assert.NotNull(result);
        }





        [Fact]
        public async Task AddEmployeeAsync_ShouldSaveemployee()
        {
            //Arrange
            var addEmployee = AddEmployee();
            var service = new EmployeeService(new EmployeeRepository(fixture.tccsContext), mapper, fixture.tccsContext);


            //Act
            var result = await service.AddEmployeeAsync(addEmployee);
            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task AddEmployeeAsync_ThrowException()
        {
            //Arrange
            var service = new EmployeeService(new EmployeeRepository(fixture.tccsContext), mapper, fixture.tccsContext);

            //Act
            Task act() => service.AddEmployeeAsync(null);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task AddEmployeeRange_ShouldSaveemployee()
        {
            //Arrange
            var addEmployeeRange = AddEmployeeRange();
            var service = new EmployeeService(new EmployeeRepository(fixture.tccsContext), mapper, fixture.tccsContext);


            //Act
            var res = await service.AddEmployeeRange(addEmployeeRange);
            //Assert
            Assert.Equal(1, res);
        }


        [Fact]
        public async Task AddEmployeeRangeAsync_ShouldSaveemployee()
        {
            //Arrange
            var addEmployeeRange = AddEmployeeRangeAsync();
            var service = new EmployeeService(new EmployeeRepository(fixture.tccsContext), mapper, fixture.tccsContext);

            //Act
            var res = await service.AddEmployeeRangeAsync(addEmployeeRange);
            //Assert
            Assert.Equal(1, res);
        }




        [Fact]
        public async Task UpdateEmployee_ShouldUpdateEmployee()
        {
            //Arrange
            var updateEmployee = UpdateEmployee();
            var service = new EmployeeService(new EmployeeRepository(fixture.tccsContext), mapper, fixture.tccsContext);

            //Act
            var res = await service.UpdateEmployee(updateEmployee);

            //Assert
            Assert.Equal(1, res);
        }

        [Fact]
        public async Task UpdateEmployeeRange_ShouldUpdateEmployee()
        {
            //Arrange
            var updateEmployeeRange = UpdateEmployeeRange();
            var service = new EmployeeService(new EmployeeRepository(fixture.tccsContext), mapper, fixture.tccsContext);

            //Act
            var res = await service.UpdateEmployeeRange(updateEmployeeRange);

            //Assert
            Assert.Equal(1, res);
        }





        [Fact]
        public async Task RemoveEmployee_ShouldRemoveemployeeAsync()
        {
            //Arrange
            var removeEmployee = RemoveEmployee();
            var service = new EmployeeService(new EmployeeRepository(fixture.tccsContext), mapper, fixture.tccsContext);

            //Act
            var res = await service.RemoveEmployee(removeEmployee);

            Assert.Equal(1, res);
        }

        [Fact]
        public async Task RemoveEmployeeById_ShouldRemoveemployeeAsync()
        {
            //Arrange
            int id = 6;
            var service = new EmployeeService(new EmployeeRepository(fixture.tccsContext), mapper, fixture.tccsContext);

            //Act
            var res = await service.RemoveEmployeeById(id);

            Assert.Equal(1, res);
        }

        [Fact]
        public async Task RemoveEmployeeRange_ShouldRemoveemployeeAsync()
        {
            var removeEmployeeRange = RemoveEmployeeRange();
            var service = new EmployeeService(new EmployeeRepository(fixture.tccsContext), mapper, fixture.tccsContext);

            //Act
            var res = await service.RemoveEmployeeRange(removeEmployeeRange);

            Assert.Equal(1, res);
        }






        [Fact]
        public async Task SingleOrDefaultEmployeeAsync_ShouldReturnList()
        {
            int id = 8;
            var service = new EmployeeService(new EmployeeRepository(fixture.tccsContext), mapper, fixture.tccsContext);

            //Act
            var result = await service.SingleOrDefaultEmployeeAsync(x => x.Id == id);

            //Assert
            Assert.Equal(result.Name, "test8");
        }

        [Fact]
        public async Task FirstOrDefaultEmployeeAsync_ShouldReturnList()
        {
            int id = 9;
            var service = new EmployeeService(new EmployeeRepository(fixture.tccsContext), mapper, fixture.tccsContext);

            //Act
            var result = await service.FirstOrDefaultEmployeeAsync(x => x.Id == id);

            //Assert
            Assert.Equal(result.Name, "test9");
        }




        private IEnumerable<EmployeeModel> GetEmployeeModelList()
        {
            List<EmployeeModel> employeeList = new List<EmployeeModel>()
            {
                new EmployeeModel{Id=1,Name="test1",EmailId="test1@gmail.com"},
                new EmployeeModel{Id=2,Name="test2",EmailId="test2@gmail.com"},
                new EmployeeModel{Id=3,Name="test3",EmailId="test3@gmail.com"},
                new EmployeeModel{Id=4,Name="test4",EmailId="test4@gmail.com"},
                new EmployeeModel{Id=5,Name="test5",EmailId="test5@gmail.com"},
                new EmployeeModel{Id=6,Name="test6",EmailId="test6@gmail.com"},
                new EmployeeModel{Id=7,Name="test7",EmailId="test7@gmail.com"},
                new EmployeeModel{Id=8,Name="test8",EmailId="test8@gmail.com"},
                new EmployeeModel{Id=9,Name="test9",EmailId="test9@gmail.com"},
            };

            return employeeList;
        }

        private EmployeeModel AddEmployee()
        {
            EmployeeModel employee = new EmployeeModel()
            {
                Name = "test10",
                EmailId = "test10@gmail.com"
            };

            return employee;
        }

        private List<EmployeeModel> AddEmployeeRange()
        {
            List<EmployeeModel> employeeList = new List<EmployeeModel>()
            {
                new EmployeeModel{Name="test11",EmailId="test11@gmail.com"}
            };

            return employeeList;
        }

        private List<EmployeeModel> AddEmployeeRangeAsync()
        {
            List<EmployeeModel> employeeList = new List<EmployeeModel>()
            {
                new EmployeeModel{Name="test12",EmailId="test12@gmail.com"}
            };

            return employeeList;
        }

        private EmployeeModel UpdateEmployee()
        {
            EmployeeModel employee = new EmployeeModel()
            {
                Id = 3,
                Name = "test3-33",
                EmailId = "test3@gmail.com"
            };

            return employee;
        }

        private List<EmployeeModel> UpdateEmployeeRange()
        {
            List<EmployeeModel> employee = new List<EmployeeModel>()
            {
                new EmployeeModel{
                    Id = 4,
                    Name = "test4-4",
                    EmailId = "test4@gmail.com"
                }
            };

            return employee;
        }

        private EmployeeModel RemoveEmployee()
        {
            EmployeeModel employee = new EmployeeModel()
            {
                Id = 5,
                Name = "test5",
                EmailId = "test5@gmail.com"
            };

            return employee;
        }

        private List<EmployeeModel> RemoveEmployeeRange()
        {
            List<EmployeeModel> employee = new List<EmployeeModel>()
            {
               new EmployeeModel{ Id=7,Name="test7-7",EmailId="test7@gmail.com"}
            };

            return employee;
        }
    }
}