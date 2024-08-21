using ConsumerService.Domain.Entities;

namespace ConsumerService.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee);
    }
}
