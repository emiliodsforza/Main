using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    [CourseMustBeDifferentFromDescription(ErrorMessage = "The Title must be different than the Description")]
    public class CourseForCreationDto //: IValidatableObject
    {
        [Required(ErrorMessage ="You should fill out a title")]
        [MaxLength(100, ErrorMessage = "The Title shouldn't have more than 100 characters")]
        public string Title { get; set; }
        [MaxLength(1500, ErrorMessage ="The Description should not be more than 1500 chahrcters")]
        public string Description { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if(Title == Description)
        //    {
        //        yield return new ValidationResult(
        //            "The provided description should be different from the Title",
        //            new[] { "CourseForCreationDto" });
        //    }
        //}
    }
}
