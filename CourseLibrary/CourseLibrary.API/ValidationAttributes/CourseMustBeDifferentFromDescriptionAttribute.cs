using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CourseLibrary.API.Models;

namespace CourseLibrary.API.ValidationAttributes
{
    public class CourseMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var course = (CourseForCreationDto)validationContext.ObjectInstance;

            if(course.Title == course.Description)
            {
                return new ValidationResult(ErrorMessage, new[] { nameof(CourseForCreationDto) });
            }

            return ValidationResult.Success;
        }
    }
}
