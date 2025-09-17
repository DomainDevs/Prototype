// Application/Clientes/Commands/UpdateClienteCommandHandler.cs
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace Application.Clientes.Commands;

public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, bool>
{
    private readonly IClienteRepository _repository;

    public UpdateClienteCommandHandler(IClienteRepository repository)
    {
        _repository = repository;
    }

    /*
    public async Task<bool> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = new Cliente
        {
            Id = request.Id,
            Nombre = request.Nombre,
            Apellido = request.Apellido,
            Email = request.Email
        };

        var rows = await _repository.UpdateAsync(cliente);
        return rows > 0;
    }*/

    public async Task<bool> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = new Cliente { Id = request.Id };

        // Lista para acumular propiedades que vienen en el request
        var includeProps = new List<Expression<Func<Cliente, object>>>();

        if (!string.IsNullOrEmpty(request.Nombre))
        {
            cliente.Nombre = request.Nombre;
            includeProps.Add(c => c.Nombre);
        }

        if (!string.IsNullOrEmpty(request.Apellido))
        {
            cliente.Apellido = request.Apellido;
            includeProps.Add(c => c.Apellido);
        }

        if (!string.IsNullOrEmpty(request.Email))
        {
            cliente.Email = request.Email;
            includeProps.Add(c => c.Email);
        }

        // Si no vino ninguna propiedad, no hacemos nada
        if (includeProps.Count == 0)
            return false;

        // Aquí combinas todas las props en una sola expresión
        var combined = Combine(includeProps);

        var rows = await _repository.UpdateAsync(cliente, combined);
        return rows > 0;
    }

    private static Expression<Func<Cliente, object>> Combine(
    List<Expression<Func<Cliente, object>>> expressions)
    {
        return c => new
        {
            // Se proyectan todas las propiedades de la lista
            values = expressions.Select(e => e.Compile().Invoke(c)).ToArray()
        };
    }

}