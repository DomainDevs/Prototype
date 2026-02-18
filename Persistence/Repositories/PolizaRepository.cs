using DataToolkit.Library.Common;
using DataToolkit.Library.Sql;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Queries;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories;

//RESPONSABILIDAD: solo ejecuta SQL.
public class PolizaRepository : IPolizaRepository
{
    private readonly ISqlExecutor _sqlExecutor;
    private readonly GetPolizaCompletaQuery _queryBuilder;

    public PolizaRepository(ISqlExecutor sqlExecutor, GetPolizaCompletaQuery queryBuilder)
    {
        _sqlExecutor = sqlExecutor;
        _queryBuilder = queryBuilder;
    }

    public async Task<Polizas?> GetPolizaCompletaAsync(
        int idPv,
        int? codRiesgo = null
        )

    {
        // 1️⃣ Obtener SQL desde el Query Object
        string sql = _queryBuilder.BuildSql(idPv, codRiesgo, true);

        // 2️⃣ Configurar MultiMapRequest
        var request = new MultiMapRequest
        {
            Sql = sql,
            Types = new[] { typeof(Polizas), typeof(PolizaExt), typeof(PolizaRiesgos), typeof(PolizaCoberturas) },
            SplitOn = "Pe_Id_pv,Pr_Cod_Riesgo,Pc_Cod_Cobertura",
            Parameters = new { IdPv = idPv },
            MapFunction = objects =>
            {
                // Aquí haces el mapeo de objetos al modelo final
                var poliza = (Polizas)objects[0];
                var ext = (PolizaExt)objects[1];
                var riesgo = (PolizaRiesgos)objects[2];
                var cobertura = (PolizaCoberturas)objects[3];

                // Por ejemplo, agregar los hijos al padre
                poliza.PolizaExt = ext;
                riesgo.PolizaCoberturas = new List<PolizaCoberturas> { cobertura };
                poliza.PolizaRiesgos = new List<PolizaRiesgos> { riesgo };

                return poliza;
            }
        };

        // 3️⃣ Ejecutar mapeo múltiple
        var polizas = await _sqlExecutor.FromSqlMultiMapAsync<Polizas>(request);

        // 4️⃣ Retornar el primer resultado (solo nos interesa uno)
        return polizas.FirstOrDefault();
    }
}
