using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Server.Data;

namespace ToDoApp.Server.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public virtual ICollection<Connection> Connections { get; } = new List<Connection>();
}


public static class UserEndpoints
{
	public static void MapUserEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/User", async (ToDoAppContext db) =>
        {
            return await db.Users.ToListAsync();
        })
        .WithName("GetAllUsers")
        .Produces<List<User>>(StatusCodes.Status200OK);

        routes.MapGet("/api/User/{id}", async (int Id, ToDoAppContext db) =>
        {
            return await db.Users.FindAsync(Id)
                is User model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetUserById")
        .Produces<User>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/User/{id}", async (int Id, User user, ToDoAppContext db) =>
        {
            var foundModel = await db.Users.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            
            db.Update(user);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })   
        .WithName("UpdateUser")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/User/", async (User user, ToDoAppContext db) =>
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Results.Created($"/Users/{user.Id}", user);
        })
        .WithName("CreateUser")
        .Produces<User>(StatusCodes.Status201Created);


        routes.MapDelete("/api/User/{id}", async (int Id, ToDoAppContext db) =>
        {
            if (await db.Users.FindAsync(Id) is User user)
            {
                db.Users.Remove(user);
                await db.SaveChangesAsync();
                return Results.Ok(user);
            }

            return Results.NotFound();
        })
        .WithName("DeleteUser")
        .Produces<User>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}