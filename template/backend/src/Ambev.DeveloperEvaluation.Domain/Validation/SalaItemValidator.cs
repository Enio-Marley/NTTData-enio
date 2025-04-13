﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SalaItemValidator : AbstractValidator<SaleItem>
    {
        public SalaItemValidator()
        {
            RuleFor(item => item.ProductId)
                .NotEmpty()
                .WithMessage("Product ID cannot be empty.");

            RuleFor(item => item.ProductName)
                .NotEmpty()
                .WithMessage("Product name cannot be empty.")
                .Length(3, 150)
                .WithMessage("Product name must be between 3 and 150 characters.");

            RuleFor(item => item.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.")
                .LessThan(20)
                .WithMessage("Quantity must be less than 20.");

            RuleFor(item => item.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Unit price must be greater than zero.");
        }
    }
}
