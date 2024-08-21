using ConsumerService.Application.Commands;
using ConsumerService.Application.Interfaces;
using ConsumerService.Domain.Entities;
using MediatR;
using System.Text.Json;

namespace ConsumerService.Application.Handlers
{
    public class ConsumeEmployeeCommandHandler : IRequestHandler<ConsumeEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public ConsumeEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<Unit> Handle(ConsumeEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = JsonSerializer.Deserialize<Employee>(request.Message);
            await _employeeRepository.AddAsync(employee);
            return Unit.Value;
        }
    }
}
