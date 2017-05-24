using FluentValidation;

using Seeker.Searching;

namespace Seeker.Validation
{
    /// <summary>
    /// Represents a search query validator.
    /// </summary>
    public class SearchRequestValidator : AbstractValidator<SearchRequest>
    {
        #region Constructors

        /// <summary>
        /// Creates a query validator.
        /// </summary>
        public SearchRequestValidator()
        {
            RuleFor(x => x.Limit).InclusiveBetween(1, 300);
            RuleFor(x => x.Offset).GreaterThanOrEqualTo(0).WithMessage("\"Offset\" should be greater or equal zero");
        }

        #endregion
    }
}
