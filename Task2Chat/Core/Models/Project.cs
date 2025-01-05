using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task2Chat.Core.Models;

public partial class Project
{
    [Key]
    public Guid ProjectId { get; set; }

    [Required]
    [StringLength(255)]
    public string ProjectName { get; set; }

    public string Description { get; set; }

    public Guid? CreatedBy { get; set; }

    [Precision(8, 0)]
    public decimal? CreatedAt { get; set; }

    [Precision(8, 0)]
    public decimal? UpdatedAt { get; set; }

    [ForeignKey("createdby")]
    [InverseProperty("projects")]
    public virtual User CreatedByNavigation { get; set; }

    [InverseProperty("project")]
    public virtual ICollection<ProjectParticipant> ProjectParticipants { get; set; } = new List<ProjectParticipant>();

    [InverseProperty("project")]
    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
}
