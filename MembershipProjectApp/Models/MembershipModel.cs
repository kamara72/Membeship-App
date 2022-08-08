using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipProjectApp.Models
{
    public class MembershipModel
    {
        [Key]
        public int MemberId { get; set; }
        [Required(ErrorMessage = "Required *")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Required *")]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Required *")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Required *")]
        [Display(Name = "Contact/Phone")]
        public string Contact { get; set; }
        [Required(ErrorMessage = "Required *")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required *")]
        public string Occupation { get; set; }
        [Required(ErrorMessage = "Required *")]
        [Display(Name = "Level of Education")]
        public string Education { get; set; }
        [Required(ErrorMessage = "Required *")]
        [Display(Name = "County of Origin")]
        public string CountyOfOrigin { get; set; }
        [Required(ErrorMessage = "Required *")]
        [Display(Name = "County of Residence")]
        public string CountyOfResidence { get; set; }
        [Required(ErrorMessage = "Required *")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Required *")]
        [Display(Name = "Membership Type")]
        public string Membership { get; set; }
        [Required(ErrorMessage = "Required *")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Required *")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Required *")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Required *")]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }
        [Required(ErrorMessage = "Required *")]
        public string Ethnicity { get; set; }
        public string Language { get; set; }
        //public string LanguageTwo { get; set; }
        //public string LanguageThree { get; set; }
        //public string Dialect { get; set; }
        //public string DialectTwo { get; set; }
        //public string OtherDialectandLanguage { get; set; }
        [Display(Name = "Mosque Name")]
        public string Mosque { get; set; }
        [Display(Name = "ID Number")]
        public string IDNumber { get; set; }
        [Display(Name = "Receipt Number")]
        public string ReceiptNumber { get; set; }
        [Required]
        [Display(Name = "Pre Registration")]
        public string PreRegistration { get; set; }
        [Required]
        public string Registration { get; set; }
        [Column(TypeName = "decimal(19,2)")]
        public decimal Payment { get; set; }
        [Column(TypeName = "decimal(19,2)")]
        public decimal Balance { get; set; }
        public string Person { get; set; }
        [Display(Name = "Number of Person")]
        public int NumberOfPerson { get; set; }
        public string RecordedBy { get; set; }
        [Display(Name = "Date Recorded")]
        public DateTime DateRecorded { get; set; }
    }
}
