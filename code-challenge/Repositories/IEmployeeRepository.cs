using challenge.Models;
using System;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetById(String id);
        Employee GetById(String id, bool includeCompensation);
        Employee Add(Employee employee);
        Employee Update(Employee employee);
        Employee Remove(Employee employee);
        Task SaveAsync();
    }
}