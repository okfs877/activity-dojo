using System.ComponentModel.DataAnnotations;
 
namespace CBeltExam.Models
{
    public class RSVP
    {
        // public int id { get; set; }
        public int id {get;set;}
        public int UserId { get; set; }
        public ModelUser User { get; set; }
 
        public int ActivityId { get; set; }
        public ModelActivity Activity { get; set; }
    }
}