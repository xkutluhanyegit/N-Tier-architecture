using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Applications.DTOs.Request.Auth;
using Domain.Entities;
using FluentValidation;

namespace Applications.Validations.FluentValidation
{
    public class UserValidator:AbstractValidator<RegisterRequest>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone Number is required");
            RuleFor(x => x.PhoneNumber).MinimumLength(10).WithMessage("Phone Number must be at least 10 characters");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not valid");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }    
    }
}