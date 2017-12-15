using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
 
namespace CBeltExam.Models
{
    public class MyDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now.Date;
        }
    }
    public class Activity
    {
        [Required]
        [Display(Name="Title")]
        [MinLength(2)]
        public string Title {get;set;}

        [Required]
        public string Hour {get;set;}

        [Required]
        [Display(Name="Minute")]
        [MinLength(2)]
        [RegularExpression("^[0-5][0-9]")]
        public string Time {get;set;}

        [Required]
        public string AmPm {get;set;}

        [Required]
        [Display(Name="Date")]
        [MyDate(ErrorMessage ="Invalid date")]
        [DataType(DataType.Date)]
        public string Date {get;set;}

        [Required]
        [Display(Name="Duration")]
        [Range(1, 2147483646)]
        public int Duration {get;set;}

        [Required]
        public string DurType {get;set;}

        [Required]
        [MinLength(10)]
        public string Description {get;set;}
    }
    public class ModelActivity{
        public int id {get;set;}
        public string Title {get;set;}
        public DateTime Date {get;set;}
        public int Duration {get;set;}
        public string Coordinator {get;set;}
        public int creatorId {get;set;}
        public string DurType {get;set;}
        public string Description {get;set;}
        public List<RSVP> RSVPs {get;set;}
        public ModelActivity(){
            RSVPs = new List<RSVP>();
        }
    }
}