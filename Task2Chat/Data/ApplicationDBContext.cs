using Microsoft.EntityFrameworkCore;
using Task2Chat.Models;

namespace Task2Chat.Data;

public partial class ApplicationDBContext : DbContext
{
    public ApplicationDBContext()
    {
    }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectParticipant> ProjectParticipants { get; set; }

    public virtual DbSet<ProjectTask> ProjectTasks { get; set; }

    public virtual DbSet<TaskAssignment> TaskAssignments { get; set; }

    public virtual DbSet<TaskComment> TaskComments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Username=sa;Password=12345;Database=Task2ChatDB;Port=5432");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatRoom>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("chatrooms_pkey");

            entity.Property(e => e.RoomId).ValueGeneratedNever();

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ChatRooms).HasConstraintName("chatrooms_createdby_fkey");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("messages_pkey");

            entity.HasOne(d => d.Room).WithMany(p => p.Messages).HasConstraintName("messages_roomid_fkey");

            entity.HasOne(d => d.Sender).WithMany(p => p.Messages).HasConstraintName("messages_senderid_fkey");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("projects_pkey");

            entity.Property(e => e.ProjectId).ValueGeneratedNever();

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Projects).HasConstraintName("projects_createdby_fkey");
        });

        modelBuilder.Entity<ProjectParticipant>(entity =>
        {
            entity.HasKey(e => e.ProjectParticipantId).HasName("projectparticipants_pkey");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectParticipants).HasConstraintName("projectparticipants_projectid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ProjectParticipants).HasConstraintName("projectparticipants_userid_fkey");
        });

        modelBuilder.Entity<ProjectTask>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("projecttask_pkey");

            entity.Property(e => e.TaskId).ValueGeneratedNever();

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.ProjectTaskAssignedToNavigations).HasConstraintName("projecttask_assignedto_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProjectTaskCreatedByNavigations).HasConstraintName("projecttask_createdby_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectTasks).HasConstraintName("projecttask_projectid_fkey");
        });

        modelBuilder.Entity<TaskAssignment>(entity =>
        {
            entity.HasKey(e => e.TaskAssignmentid).HasName("taskassignments_pkey");

            entity.HasOne(d => d.ProjectTask).WithMany(p => p.TaskAssignments).HasConstraintName("taskassignments_taskid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.TaskAssigments).HasConstraintName("taskassignments_userid_fkey");
        });

        modelBuilder.Entity<TaskComment>(entity =>
        {
            entity.HasKey(e => e.TaskCommentId).HasName("taskcomments_pkey");

            entity.HasOne(d => d.ProjectTask).WithMany(p => p.TaskComments).HasConstraintName("taskcomments_taskid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.TaskComments).HasConstraintName("taskcomments_userid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
