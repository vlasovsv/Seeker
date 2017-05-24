using Autofac;
using Nancy;
using Nancy.ModelBinding;

using Seeker.Searching;

namespace Seeker.Modules
{
    /// <summary>
    /// Represents a search api module.
    /// </summary>
    public sealed class ApiSearchModule : NancyModule
    {
        #region Private fields

        private readonly LuceneWrapper _lucene;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a search api module.
        /// </summary>
        public ApiSearchModule()
            : base("api/v1/")
        {
            _lucene = AutofacContext.Container.Resolve<LuceneWrapper>();

            Get("/search", parameters =>
            {
                var request = this.BindAndValidate<SearchRequest>();

                var validationResult = this.ModelValidationResult;

                if (!validationResult.IsValid)
                {
                    var negotiator = Negotiate.WithModel(validationResult);
                    if (request.SuppressResponseCodes)
                    {
                        return negotiator.WithStatusCode(HttpStatusCode.OK);
                    }
                    else
                    {
                        return negotiator.WithStatusCode(HttpStatusCode.BadRequest);
                    }
                }

                var result = _lucene.Search(request);

                return Negotiate.WithModel(result).WithFullNegotiation();
            });
        }

        #endregion
    }
}
