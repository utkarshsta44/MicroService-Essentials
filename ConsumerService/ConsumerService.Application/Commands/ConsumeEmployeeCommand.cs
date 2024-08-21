using MediatR;

namespace ConsumerService.Application.Commands
{
    public record ConsumeEmployeeCommand(string Message) : IRequest;
}
