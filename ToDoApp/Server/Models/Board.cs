using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ToDoApp.Server.Data;

namespace ToDoApp.Server.Models;

public partial class Board
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<AppointmentDatum> AppointmentData { get; } = new List<AppointmentDatum>();

    public virtual ICollection<Connection> Connections { get; } = new List<Connection>();
}


public static class BoardEndpoints
{
	public static void MapBoardEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Board", async (ToDoAppContext db) =>
        {
            return await db.Boards.ToListAsync();
        })
        .WithName("GetAllBoards")
        .Produces<List<Board>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Board/{id}", async (int Id, ToDoAppContext db) =>
        {
            return await db.Boards.FindAsync(Id)
                is Board model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetBoardById")
        .Produces<Board>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Board/{id}", async (int Id, Board board, ToDoAppContext db) =>
        {
            var foundModel = await db.Boards.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            
            db.Update(board);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })   
        .WithName("UpdateBoard")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Board/", async (Board board, ToDoAppContext db) =>
        {
            db.Boards.Add(board);
            await db.SaveChangesAsync();
            return Results.Created($"/Boards/{board.Id}", board);
        })
        .WithName("CreateBoard")
        .Produces<Board>(StatusCodes.Status201Created);


        routes.MapDelete("/api/Board/{id}", async (int Id, ToDoAppContext db) =>
        {
            if (await db.Boards.FindAsync(Id) is Board board)
            {
                //ПОЧЕМУ НЕ РАБОТАЕТ????? ? БЛЯДЛЬ
                //foreach (var appointment in board.AppointmentData)
                //{
                //    Console.WriteLine("bebra");
                //    db.AppointmentData.Remove(appointment);
                //}
                //foreach (var connection in board.Connections)
                //{
                //    db.Connections.Remove(connection);
                //}
                db.Boards.Remove(board);
                await db.SaveChangesAsync();
                return Results.Ok(board);
            }

            return Results.NotFound();
        })
        .WithName("DeleteBoard")
        .Produces<Board>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}