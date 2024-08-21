using ConsumerService.Application.Interfaces;
using ConsumerService.Domain.Entities;

namespace ConsumerService.Infrastructure.Persistence
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ConsumerDbContext _context;

        public EmployeeRepository(ConsumerDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }
    }
}
