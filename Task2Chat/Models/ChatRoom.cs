using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task2Chat.Models;

public partial class ChatRoom
{
    [Key]
    public Guid RoomId { get; set; }

    [StringLength(255)]
    public string RoomName { get; set; }

    public Guid? CreatedBy { get; set; }

    [Precision(8, 0)]
    public decimal? CreatedAt { get; set; }

    [ForeignKey("createdby")]
    [InverseProperty("chatrooms")]
    public virtual User CreatedByNavigation { get; set; }

    [InverseProperty("room")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
