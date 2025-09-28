// PvHeaderUpdateCommand.cs
using MediatR;
namespace Application.Features.Poliza.Commands;

public record PvHeaderUpdateCommand(int CodSuc, int CodRamo, long NroPol, int NroEndoso, int IdPv, int? CodGrupoEndo, int? CodTipoEndo, string TxtDescription, DateTime? FechaVencimiento, bool? Esdigital, DateTime? FechaCreacion, byte? Esprueba, short? Eserror, decimal? Prima, decimal? SumaAseg) : IRequest<int>;

