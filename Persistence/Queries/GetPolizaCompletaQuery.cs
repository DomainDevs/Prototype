using DataToolkit.Library.Fluent;

namespace Persistence.Queries;

// RESPONSABILIDAD: Solo construye la cadena SQL.
// Al no tener dependencias en el constructor, se puede registrar como Singleton.
public class GetPolizaCompletaQuery
{
    public GetPolizaCompletaQuery() { }

    public string BuildSql(
        IFluentQuery query, // <--- La herramienta se pasa por parámetro
        int? idPv,
        int? codRiesgo = null,
        bool? soloActivas = null)
    {
        return query
            .Select(
                "p.*",
                "pe.Id_pv AS Pe_Id_pv", "pe.InfoAdicional", "pe.Observaciones",
                "pe.ClausulasEspeciales", "pe.DatosTecnicos",
                "pr.Cod_Riesgo AS Pr_Cod_Riesgo", "pr.TipoRiesgo",
                "pr.Descripcion AS Pr_Descripcion", "pr.SumaAsegurada AS Pr_SumaAsegurada", "pr.PrimaRiesgo",
                "pc.Cod_Cobertura AS Pc_Cod_Cobertura", "pc.Cobertura", "pc.MontoAsegurado", "pc.Deducible", "pc.PrimaCobertura"
            )
            .From("Polizas p")
            .LeftJoin("Poliza_Ext pe", "p.Id_pv = pe.Id_pv")
            .LeftJoin("PolizaRiesgos pr", "p.Id_pv = pr.Id_pv")
            .LeftJoin("PolizaCoberturas pc", "pr.Id_pv = pc.Id_pv AND pr.Cod_Riesgo = pc.Cod_Riesgo")
            .WhereIf(idPv.HasValue, "p.Id_pv = @IdPv")
            .WhereIf(codRiesgo.HasValue, "pr.Cod_Riesgo = @CodRiesgo")
            .WhereIf(soloActivas == true, "p.Estado = 'ACTIVA'")
            .ToSql();
    }
}