using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Test.Work.BooksCatalogApi.HttpCache
{
    /// <summary>
	/// Расширения для <see cref="HttpContext"/>
	/// </summary>
	public static class HttpContextExtensions
    {
        private const string NoCache = "no-cache";
        private const string NoCacheMaxAge = "no-cache,max-age=";
        private const string NoStore = "no-store";
        private const string NoStoreNoCache = "no-store,no-cache";
        private const string PublicMaxAge = "public,max-age=";
        private const string PrivateMaxAge = "private,max-age=";

        /// <summary>
        /// Добавляет заголовки Cache-Control и Pragma
        /// </summary>
        /// <param name="context">HTTP контекст</param>
        /// <param name="cacheProfile">Профиль</param>
        /// <returns>Контекст</returns>
        public static HttpContext ApplyCacheProfile(this HttpContext context, CacheProfile cacheProfile)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            if (cacheProfile is null)
                throw new ArgumentNullException(nameof(cacheProfile));

            var headers = context.Response.Headers;

            if (!string.IsNullOrEmpty(cacheProfile.VaryByHeader))
                headers[HeaderNames.Vary] = cacheProfile.VaryByHeader;

            if (cacheProfile.NoStore == true)
            {
                // Cache-control: no-store, no-cache - норм
                if (cacheProfile.Location == ResponseCacheLocation.None)
                {
                    headers[HeaderNames.CacheControl] = NoStoreNoCache;
                    headers[HeaderNames.Pragma] = NoCache;
                }
                else
                {
                    headers[HeaderNames.CacheControl] = NoStore;
                }
            }
            else
            {
                string cacheControlValue;
                var duration = cacheProfile.Duration.GetValueOrDefault().ToString(CultureInfo.InvariantCulture);
                switch (cacheProfile.Location)
                {
                    case ResponseCacheLocation.Any:
                        cacheControlValue = PublicMaxAge + duration;
                        break;
                    case ResponseCacheLocation.Client:
                        cacheControlValue = PrivateMaxAge + duration;
                        break;
                    case ResponseCacheLocation.None:
                        cacheControlValue = NoCacheMaxAge + duration;
                        headers[HeaderNames.Pragma] = NoCache;
                        break;
                    default:
                        var exception = new NotImplementedException(FormattableString.Invariant($"Неизвестный {nameof(ResponseCacheLocation)}: {cacheProfile.Location}"));
                        Debug.Fail(exception.ToString());
                        throw exception;
                }

                headers[HeaderNames.CacheControl] = cacheControlValue;
            }

            return context;
        }
    }
}
