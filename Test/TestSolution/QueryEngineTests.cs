using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataToolkit.Library.Sql;
using Persistence.QueryEngine;

namespace TestSolution;

public class QueryEngineTests
{
    [Fact]
    public async Task QueryAsync_ReturnsExpectedResults()
    {
        // Arrange
        var mockExecutor = new Mock<ISqlExecutor>();

        var expected = new List<DummyModel>
        {
            new DummyModel { Id = 1, Name = "Test1" },
            new DummyModel { Id = 2, Name = "Test2" }
        };

        mockExecutor
            .Setup(x => x.FromSqlAsync<DummyModel>("SELECT * FROM Dummy", null))
            .ReturnsAsync(expected);

        var queryEngine = new QueryEngine(mockExecutor.Object);

        // Act
        var result = await queryEngine.QueryAsync<DummyModel>("SELECT * FROM Dummy");

        // Assert
        Assert.Equal(expected.Count, result.Count());
        Assert.Equal("Test1", result.First().Name);

        // Verifica que realmente llamó al Executor
        mockExecutor.Verify(x =>
            x.FromSqlAsync<DummyModel>("SELECT * FROM Dummy", null),
            Times.Once);
    }

    private class DummyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}