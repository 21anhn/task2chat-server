using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task2Chat.Models;

[Index("email", Name = "users_email_key", IsUnique = true)]
public partial class User
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(255)]
    public string FullName { get; set; }

    [Required]
    [StringLength(255)]
    public string Email { get; set; }

    [StringLength(15)]
    public string PhoneNumber { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string Address { get; set; }

    public bool? IsActive { get; set; }

    [StringLength(10)]
    public string Gender { get; set; }

    public string ProfilePictureUrl { get; set; }

    [StringLength(15)]
    public string LastLoginAt { get; set; }

    [Precision(8, 0)]
    public decimal? CreatedAt { get; set; }

    [Precision(8, 0)]
    public decimal? UpdatedAt { get; set; }

    [InverseProperty("createdbyNavigation")]
    public virtual ICollection<ChatRoom> ChatRooms { get; set; } = new List<ChatRoom>();

    [InverseProperty("sender")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    [InverseProperty("user")]
    public virtual ICollection<ProjectParticipant> ProjectParticipants { get; set; } = new List<ProjectParticipant>();

    [InverseProperty("createdbyNavigation")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("assignedtoNavigation")]
    public virtual ICollection<ProjectTask> ProjectTaskAssignedToNavigations { get; set; } = new List<ProjectTask>();

    [InverseProperty("createdbyNavigation")]
    public virtual ICollection<ProjectTask> ProjectTaskCreatedByNavigations { get; set; } = new List<ProjectTask>();

    [InverseProperty("user")]
    public virtual ICollection<TaskAssignment> TaskAssigments { get; set; } = new List<TaskAssignment>();

    [InverseProperty("user")]
    public virtual ICollection<TaskComment> TaskComments { get; set; } = new List<TaskComment>();
}
