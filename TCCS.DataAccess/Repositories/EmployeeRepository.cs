using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TCCS.DataAccess.Interfaces;
using TCCS.DataAccess.Models;

namespace TCCS.DataAccess.Repositories
{
    public class EmployeeRepository : Repository<Employee,int>, IEmployeeRepository
    {

        public EmployeeRepository(TccsContext context) : base(context)
        {
        }



        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            return await GetAll();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await GetById(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeeById(Expression<Func<Employee, bool>> predicate)
        {
            return await GetById(predicate);
        }

        public async Task<Employee> AddEmployeeAsync(Employee entity)
        {
            return await AddAsync(entity);
        }

        public Employee UpdateEmployee(Employee entity)
        {
            return Update(entity);
        }

        public void RemoveEmployee(Employee entity)
        {
            Remove(entity);
        }

        public async Task RemoveEmployeeById(int id)
        {
            await RemoveById(id);
        }




        //public int SaveEmployeeChanges()
        //{
        //    return SaveChanges();
        //}

        //public async Task<int> SaveEmployeeChangesAsync()
        //{
        //    return await SaveChangesAsync();
        //}




        public async Task<Employee> SingleOrDefaultEmployeeAsync(Expression<Func<Employee, bool>> predicate)
        {
            return await SingleOrDefaultAsync(predicate);
        }

        public async Task<Employee> FirstOrDefaultEmployeeAsync(Expression<Func<Employee, bool>> predicate)
        {
            return await FirstOrDefaultAsync(predicate);
        }




        public void AddEmployeeRange(IEnumerable<Employee> entities)
        {
            AddRange(entities);
        }

        public async Task AddEmployeeRangeAsync(IEnumerable<Employee> entities)
        {
            await AddRangeAsync(entities);
        }

        public void UpdateEmployeeRange(IEnumerable<Employee> entities)
        {
            UpdateRange(entities);
        }

        public void RemoveEmployeeRange(IEnumerable<Employee> entities)
        {
            RemoveRange(entities);
        }
    }
}
