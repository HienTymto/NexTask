using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Workloopz.Data;

public partial class NexTasksContext : DbContext
{
    public NexTasksContext()
    {
    }

    public NexTasksContext(DbContextOptions<NexTasksContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BusinessUnit> BusinessUnits { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Link> Links { get; set; }

    public virtual DbSet<Priorite> Priorites { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProject> UserProjects { get; set; }

    public virtual DbSet<UserTask> UserTasks { get; set; }

    public virtual DbSet<WorkLog> WorkLogs { get; set; }

    public virtual DbSet<WorkLogType> WorkLogTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=NexTasks;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BusinessUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_BusinessUnits_1");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Contents).HasColumnType("text");
            entity.Property(e => e.TaskId).HasColumnName("TaskID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Task).WithMany(p => p.Comments)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Tasks");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Users");
        });

        modelBuilder.Entity<Link>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Link__3214EC2755E6EC8E");

            entity.ToTable("Link");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.SourceTask).WithMany(p => p.LinkSourceTasks)
                .HasForeignKey(d => d.SourceTaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Link_Tasks");

            entity.HasOne(d => d.TargetTask).WithMany(p => p.LinkTargetTasks)
                .HasForeignKey(d => d.TargetTaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Link_Tasks1");
        });

        modelBuilder.Entity<Priorite>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Projects__3214EC0731BCF4CE");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.OwnerNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.Owner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_Users");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.Property(e => e.ActualEnd).HasColumnType("datetime");
            entity.Property(e => e.ActualStart).HasColumnType("datetime");
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.PriorityId)
                .HasDefaultValue(1)
                .HasColumnName("PriorityID");
            entity.Property(e => e.Progress).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.ScheduledEnd).HasColumnType("datetime");
            entity.Property(e => e.ScheduledTime).HasColumnType("datetime");
            entity.Property(e => e.StatusId)
                .HasDefaultValue(1)
                .HasColumnName("StatusID");
            entity.Property(e => e.Tittle).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.Updated).HasColumnType("datetime");

            entity.HasOne(d => d.OwnerNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.Owner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_Users");

            entity.HasOne(d => d.Priority).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_Priorites");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_Tasks_Projects");

            entity.HasOne(d => d.Status).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_Status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.Bu)
                .HasMaxLength(10)
                .HasColumnName("BU");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.JobTitle).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.RandomKey)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Tittle).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(20);

            entity.HasOne(d => d.BuNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Bu)
                .HasConstraintName("FK_Users_BusinessUnits");
        });

        modelBuilder.Entity<UserProject>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserProject");

            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Project).WithMany()
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_UserProject_Projects");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserProject_Users");
        });

        modelBuilder.Entity<UserTask>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.TaskId).HasColumnName("TaskID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Task).WithMany()
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK_UserTasks_Tasks");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserTasks_Users");
        });

        modelBuilder.Entity<WorkLog>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.TaskId).HasColumnName("TaskID");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.OwnerNavigation).WithMany()
                .HasForeignKey(d => d.Owner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkLogs_Users");

            entity.HasOne(d => d.Task).WithMany()
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkLogs_Tasks");

            entity.HasOne(d => d.Type).WithMany()
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_WorkLogs_WorkLogTypes");
        });

        modelBuilder.Entity<WorkLogType>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
