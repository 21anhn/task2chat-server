using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task2Chat.Core.Models;

public partial class Message
{
    [Key]
    public int MessageId { get; set; }

    public Guid? Roomid { get; set; }

    public Guid? SenderId { get; set; }

    public string MessageText { get; set; }

    [StringLength(50)]
    public string MessageType { get; set; }

    [Precision(8, 0)]
    public decimal? CreatedAt { get; set; }

    [ForeignKey("roomid")]
    [InverseProperty("messages")]
    public virtual ChatRoom Room { get; set; }

    [ForeignKey("senderid")]
    [InverseProperty("messages")]
    public virtual User Sender { get; set; }
}
