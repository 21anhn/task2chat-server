using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task2Chat.Models;

public partial class TaskComment
{
    [Key]
    public int TaskCommentId { get; set; }

    public Guid? TaskId { get; set; }

    public Guid? UserId { get; set; }

    public string CommentText { get; set; }

    [Precision(8, 0)]
    public decimal? CreatedAt { get; set; }

    [ForeignKey("taskid")]
    [InverseProperty("taskcomments")]
    public virtual ProjectTask ProjectTask { get; set; }

    [ForeignKey("userid")]
    [InverseProperty("taskcomments")]
    public virtual User User { get; set; }
}
