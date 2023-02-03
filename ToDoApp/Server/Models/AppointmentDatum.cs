using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Server.Data;

namespace ToDoApp.Server.Models;

public partial class AppointmentDatum
{
    public int Id { get; set; }

    public string? Subject { get; set; }

    public string? Location { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string? Description { get; set; }

    public bool IsAllDay { get; set; }

    public string? RecurrenceRule { get; set; }

    public string? RecurrenceException { get; set; }

    public int? RecurrenceId { get; set; }
}


public static class AppointmentDatumEndpoints
{
	public static void MapAppointmentDatumEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/AppointmentDatum", async (ToDoAppContext db) =>
        {
            return await db.AppointmentData.ToListAsync();
        })
        .WithName("GetAllAppointmentDatums")
        .Produces<List<AppointmentDatum>>(StatusCodes.Status200OK);

        routes.MapGet("/api/AppointmentDatum/{id}", async (int Id, ToDoAppContext db) =>
        {
            return await db.AppointmentData.FindAsync(Id)
                is AppointmentDatum model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetAppointmentDatumById")
        .Produces<AppointmentDatum>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/AppointmentDatum/{id}", async (int Id, AppointmentDatum appointmentDatum, ToDoAppContext db) =>
        {
            var foundModel = await db.AppointmentData.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            db.ChangeTracker.Clear();
            db.Update(appointmentDatum);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })   
        .WithName("UpdateAppointmentDatum")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/AppointmentDatum/", async (AppointmentDatum appointmentDatum, ToDoAppContext db) =>
        {
            db.AppointmentData.Add(appointmentDatum);
            await db.SaveChangesAsync();
            return Results.Created($"/AppointmentDatums/{appointmentDatum.Id}", appointmentDatum);
        })
        .WithName("CreateAppointmentDatum")
        .Produces<AppointmentDatum>(StatusCodes.Status201Created);


        routes.MapDelete("/api/AppointmentDatum/{id}", async (int Id, ToDoAppContext db) =>
        {
            if (await db.AppointmentData.FindAsync(Id) is AppointmentDatum appointmentDatum)
            {
                db.AppointmentData.Remove(appointmentDatum);
                await db.SaveChangesAsync();
                return Results.Ok(appointmentDatum);
            }

            return Results.NotFound();
        })
        .WithName("DeleteAppointmentDatum")
        .Produces<AppointmentDatum>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}