using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ToDoApp.Server.Data;

namespace ToDoApp.Server.Models;

public partial class AppointmentData
{
    public int Id { get; set; }

    public string? Subject { get; set; }

    public string? Location { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string? Description { get; set; }

    public bool IsAllDay { get; set; }

    public string? RecurrnceRule { get; set; }

    public string? RecurrenceException { get; set; }

    public int? RecurrenceId { get; set; }
}


public static class AppointmentDataEndpoints
{
	public static void MapAppointmentDataEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/AppointmentData", async (ToDoAppContext db) =>
        {
            return await db.AppointmentData.ToListAsync();
        })
        .WithName("GetAllAppointmentDatas")
        .Produces<List<AppointmentData>>(StatusCodes.Status200OK);

        routes.MapGet("/api/AppointmentData/{id}", async (int Id, ToDoAppContext db) =>
        {
            return await db.AppointmentData.FindAsync(Id)
                is AppointmentData model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetAppointmentDataById")
        .Produces<AppointmentData>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/AppointmentData/{id}", async (int Id, AppointmentData appointmentData, ToDoAppContext db) =>
        {
            var foundModel = await db.AppointmentData.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            
            db.Update(appointmentData);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })   
        .WithName("UpdateAppointmentData")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/AppointmentData/", async (AppointmentData appointmentData, ToDoAppContext db) =>
        {
            db.AppointmentData.Add(appointmentData);
            await db.SaveChangesAsync();
            return Results.Created($"/AppointmentDatas/{appointmentData.Id}", appointmentData);
        })
        .WithName("CreateAppointmentData")
        .Produces<AppointmentData>(StatusCodes.Status201Created);


        routes.MapDelete("/api/AppointmentData/{id}", async (int Id, ToDoAppContext db) =>
        {
            if (await db.AppointmentData.FindAsync(Id) is AppointmentData appointmentData)
            {
                db.AppointmentData.Remove(appointmentData);
                await db.SaveChangesAsync();
                return Results.Ok(appointmentData);
            }

            return Results.NotFound();
        })
        .WithName("DeleteAppointmentData")
        .Produces<AppointmentData>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}