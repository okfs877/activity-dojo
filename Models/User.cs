using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
 
namespace CBeltExam.Models
{
    public class RegisterUser
    {
        [Required]
        [Display(Name="First Name")]
        [MinLength(2)]
        [RegularExpression("^[a-zA-Z]+$")]
        public string FirstName {get;set;}

        [Required]
        [Display(Name="Last Name")]
        [MinLength(2)]
        [RegularExpression("^[a-zA-Z]+$")]
        public string LastName {get;set;}

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage="Invalid Email")]
        [Display(Name="Email")]
        public string Email {get;set;}

        [Required]
        [Display(Name="Password")]
        [MinLength(8)]
        [RegularExpression(@"^((?=[^a-z]*[a-z])(?=.*\W)(?=.*\d).+)$", ErrorMessage ="Password must contain a number a letter and a special character")]
        [DataType(DataType.Password)]
        public string Password {get;set;}

        [Required]
        [Display(Name="Confirm")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Confirm {get;set;}
    }
    public class ModelUser{
        public int id {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string Email {get;set;}
        public string Password {get;set;}
        public List<RSVP> RSVPs {get;set;}
        public ModelUser(){
            RSVPs = new List<RSVP>();
        }
    }
    public class LoginUser
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name="Email")]
        [EmailAddress(ErrorMessage="Invalid Email")]
        public string LogEmail {get;set;}

        [Required]
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        public string LogPassword{get;set;}
    }
}