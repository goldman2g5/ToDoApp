using System;
using System.Collections.Generic;
using ToDoApp.Server.Models;
using ToDoApp.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Server.Models;

public partial class Connection
{
    public int Id { get; set; }

    public int User { get; set; }

    public int Board { get; set; }

    public bool? IsCreator { get; set; }

    public virtual Board BoardNavigation { get; set; } = null!;

    public virtual User UserNavigation { get; set; } = null!;
}


public static class ConnectionEndpoints
{
	public static void MapConnectionEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Connection", async (ToDoAppContext db) =>
        {
            return await db.Connections.ToListAsync();
        })
        .WithName("GetAllConnections")
        .Produces<List<Connection>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Connection/{id}", async (int Id, ToDoAppContext db) =>
        {
            return await db.Connections.FindAsync(Id)
                is Connection model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetConnectionById")
        .Produces<Connection>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Connection/{id}", async (int Id, Connection connection, ToDoAppContext db) =>
        {
            var foundModel = await db.Connections.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            
            db.Update(connection);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })   
        .WithName("UpdateConnection")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Connection/", async (Connection connection, ToDoAppContext db) =>
        {
            db.Connections.Add(connection);
            await db.SaveChangesAsync();
            return Results.Created($"/Connections/{connection.Id}", connection);
        })
        .WithName("CreateConnection")
        .Produces<Connection>(StatusCodes.Status201Created);


        routes.MapDelete("/api/Connection/{id}", async (int Id, ToDoAppContext db) =>
        {
            if (await db.Connections.FindAsync(Id) is Connection connection)
            {
                db.Connections.Remove(connection);
                await db.SaveChangesAsync();
                return Results.Ok(connection);
            }

            return Results.NotFound();
        })
        .WithName("DeleteConnection")
        .Produces<Connection>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}