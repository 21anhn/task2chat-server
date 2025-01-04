using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task2Chat.Models;

[Table("projecttask")]
public partial class ProjectTask
{
    [Key]
    public Guid TaskId { get; set; }

    [Required]
    [StringLength(255)]
    public string Title { get; set; }

    public string Description { get; set; }

    [StringLength(50)]
    public string Status { get; set; }

    [StringLength(50)]
    public string Priority { get; set; }

    [StringLength(15)]
    public string DueDate { get; set; }

    public Guid? ProjectId { get; set; }

    public Guid? AssignedTo { get; set; }

    public Guid? CreatedBy { get; set; }

    [Precision(8, 0)]
    public decimal? CreatedAt { get; set; }

    [Precision(8, 0)]
    public decimal? UpdatedAt { get; set; }

    [ForeignKey("assignedto")]
    [InverseProperty("projecttaskassignedtoNavigations")]
    public virtual User AssignedToNavigation { get; set; }

    [ForeignKey("createdby")]
    [InverseProperty("projecttaskcreatedbyNavigations")]
    public virtual User CreatedByNavigation { get; set; }

    [ForeignKey("projectid")]
    [InverseProperty("projecttasks")]
    public virtual Project Project { get; set; }

    [InverseProperty("task")]
    public virtual ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();

    [InverseProperty("task")]
    public virtual ICollection<TaskComment> TaskComments { get; set; } = new List<TaskComment>();
}
