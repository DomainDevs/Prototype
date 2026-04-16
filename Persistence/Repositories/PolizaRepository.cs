using DataToolkit.Library.Common;
using DataToolkit.Library.UnitOfWorkLayer;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories;

public class PolizaRepository : IPolizaRepository
{
    private readonly IUnitOfWork _uow;
    private readonly GetPolizaCompletaQuery _queryBuilder;

    public PolizaRepository(IUnitOfWork uow, GetPolizaCompletaQuery queryBuilder)
    {
        _uow = uow;
        _queryBuilder = queryBuilder;
    }

    public async Task<Polizas?> GetPolizaCompletaAsync(int idPv, int? codRiesgo = null)
    {
        // 1️⃣ Construir SQL pasando el Fluent que vive en el UnitOfWork
        // Asumiendo que tu IUnitOfWork expone una propiedad o método para obtener el Fluent
        string sql = _queryBuilder.BuildSql(_uow.Fluent, idPv, codRiesgo);

        // 2️⃣ Configurar MultiMapRequest
        var request = new MultiMapRequest
        {
            Sql = sql,
            Types = new[] { typeof(Polizas), typeof(PolizaExt), typeof(PolizaRiesgos), typeof(PolizaCoberturas) },
            SplitOn = "Pe_Id_pv,Pr_Cod_Riesgo,Pc_Cod_Cobertura",
            Parameters = new { IdPv = idPv, CodRiesgo = codRiesgo },
            MapFunction = objects =>
            {
                var poliza = (Polizas)objects[0];
                poliza.PolizaExt = (PolizaExt)objects[1];

                var riesgo = (PolizaRiesgos)objects[2];
                if (objects[3] is PolizaCoberturas cobertura)
                    riesgo.PolizaCoberturas = new List<PolizaCoberturas> { cobertura };

                poliza.PolizaRiesgos = new List<PolizaRiesgos> { riesgo };
                return poliza;
            }
        };

        // 3️⃣ Ejecución centralizada
        var result = await _uow.Sql.FromSqlMultiMapAsync<Polizas>(request);
        return result.FirstOrDefault();
    }
}