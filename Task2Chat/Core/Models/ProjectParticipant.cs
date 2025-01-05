using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task2Chat.Core.Models;

public partial class ProjectParticipant
{
    [Key]
    public int ProjectParticipantId { get; set; }

    public Guid? ProjectId { get; set; }

    public Guid? UserId { get; set; }

    [Precision(8, 0)]
    public decimal? JoinedAt { get; set; }

    [StringLength(50)]
    public string Role { get; set; }

    [ForeignKey("projectid")]
    [InverseProperty("projectparticipants")]
    public virtual Project Project { get; set; }

    [ForeignKey("userid")]
    [InverseProperty("projectparticipants")]
    public virtual User User { get; set; }
}
