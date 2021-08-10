using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeYourNotes_webApi.Models.Validators
{
    public class PageQueryValidator : AbstractValidator<PageQuery>
    {
        private readonly int[] allowedRecordSizes = new[] { 5, 10, 15, 20, 25, 50, 100 };
        public PageQueryValidator()
        {
            RuleFor(x => x.RecordsPackageNumber)
                .GreaterThanOrEqualTo(1);
            RuleFor(x => x.RecordsPackageSize)
                .Custom((value, context) =>
                {
                    if (!allowedRecordSizes.Contains(value))
                    {
                        context.AddFailure("RecordsPackageSize", $"Must be in [{string.Join(",", allowedRecordSizes)}]");
                    }
                });
            RuleFor(x => x.PageId)
                .GreaterThanOrEqualTo(1);
        }
    }
}
