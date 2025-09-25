// DeletePvHeaderCommand.cs
using MediatR;
namespace Application.Features.Poliza.Commands.Delete;

public record DeletePvHeaderCommand(int CodSuc, int CodRamo, long NroPol, int NroEndoso) : IRequest<bool>;
