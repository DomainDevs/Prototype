// PvHeaderDeleteCommand.cs
using MediatR;
namespace Application.Features.Poliza.Commands;

public record PvHeaderDeleteCommand(int CodSuc, int CodRamo, long NroPol, int NroEndoso) : IRequest<bool>;

