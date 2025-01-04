using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task2Chat.Models;

public partial class TaskAssignment
{
    [Key]
    public int TaskAssignmentid { get; set; }

    public Guid? TaskId { get; set; }

    public Guid? UserId { get; set; }

    public decimal? AssignedAt { get; set; }

    [StringLength(50)]
    public string Role { get; set; }

    [ForeignKey("taskid")]
    [InverseProperty("taskassignments")]
    public virtual ProjectTask ProjectTask { get; set; }

    [ForeignKey("userid")]
    [InverseProperty("taskassignments")]
    public virtual User User { get; set; }
}
