using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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

    public virtual DbSet<AppointmentDatum> AppointmentData { get; set; }

    public virtual DbSet<Board> Boards { get; set; }

    public virtual DbSet<Connection> Connections { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = 46.39.232.190; Initial Catalog = ToDoApp;User Id = WeedFieldsLord422; Password = vag!nA228##; Encrypt=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppointmentDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Appointment_Id");

            entity.Property(e => e.Id).ValueGeneratedOnAdd()
                .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.Location).IsUnicode(false);
            entity.Property(e => e.RecurrenceException).IsUnicode(false);
            entity.Property(e => e.RecurrenceId).HasColumnName("RecurrenceID");
            entity.Property(e => e.RecurrenceRule).IsUnicode(false);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.Subject).IsUnicode(false);
        });

        modelBuilder.Entity<Board>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Board_Id");

            entity.ToTable("Board");

            entity.Property(e => e.Name).IsUnicode(false);
        });

        modelBuilder.Entity<Connection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Connection_Id");

            entity.ToTable("Connection");

            entity.HasOne(d => d.BoardNavigation).WithMany(p => p.Connections)
                .HasForeignKey(d => d.Board)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Connection_Board");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.Connections)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Connection_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_Id");

            entity.ToTable("User");

            entity.Property(e => e.Username).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
