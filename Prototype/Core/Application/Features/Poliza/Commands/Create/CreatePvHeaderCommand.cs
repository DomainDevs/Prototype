// CreatePvHeaderCommand.cs
using MediatR;
namespace Application.Features.Poliza.Commands.Create;

public record CreatePvHeaderCommand(int IdPv, int CodSuc, int CodRamo, long NroPol, int NroEndoso, int? CodGrupoEndo, int? CodTipoEndo, string TxtDescription, DateTime? FechaVencimiento, bool? Esdigital, DateTime? FechaCreacion, byte? Esprueba, short? Eserror, decimal? Prima, decimal? SumaAseg) : IRequest<int>;
