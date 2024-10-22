using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Test.Work.BooksCatalogApi.BLL.Models;

namespace Test.Work.BooksCatalogApi.Options
{
    /// <summary>
	/// Опции приложения
	/// </summary>
	public class GlobalOptions
    {
        public GlobalOptions() => CacheProfiles = new CacheProfileOptions();

        [Required]
        public CacheProfileOptions CacheProfiles { get; }

        public KestrelServerOptions Kestrel { get; set; } = default!;

        [Required]
        public ApplicationOptions Application { get; set; } = default!;

        [Required]
        public CompressionOptions Compression { get; set; } = default!;

        [Required]
        public ForwardedHeadersOptions ForwardedHeaders { get; set; } = default!;

        [Required]
        public AuthenticationTokenOptions Token { get; set; } = default!;
    }
}
