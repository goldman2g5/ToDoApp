using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Server.Models;

namespace ToDoApp.Server.Data;

public partial class ToDoAppContext : DbContext
{
    public ToDoAppContext()
    {
    }

    public ToDoAppContext(DbContextOptions<ToDoAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppointmentData> AppointmentData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source = 46.39.232.190; Initial Catalog = ToDoApp;User Id = WeedFieldsLord422; Password = vag!nA228##; Encrypt=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppointmentData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Appointment_Id");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.Location).IsUnicode(false);
            entity.Property(e => e.RecurrenceException).IsUnicode(false);
            entity.Property(e => e.RecurrenceId).HasColumnName("RecurrenceID");
            entity.Property(e => e.RecurrnceRule).IsUnicode(false);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.Subject).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
