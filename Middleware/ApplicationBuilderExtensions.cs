using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, string root)
        {
            var path = Path.Combine(root, "node_modules");
            var fileProvider = new PhysicalFileProvider(path);
            var options = new StaticFileOptions();
            options.RequestPath = "/node_modules";
            options.FileProvider = fileProvider;
            app.UseStaticFiles(options);
            return app;
        }

        public static IApplicationBuilder UseScripts(this IApplicationBuilder app, string root)
        {
            var path = Path.Combine(root, "Scripts");
            var fileProvider = new PhysicalFileProvider(path);
            var options = new StaticFileOptions();
            options.RequestPath = "/Scripts";
            options.FileProvider = fileProvider;
            app.UseStaticFiles(options);
            return app;
        }

		public static IApplicationBuilder UseData(this IApplicationBuilder app, string root)
		{
			var path = Path.Combine(root, "Data");
			var fileProvider = new PhysicalFileProvider(path);
			var options = new StaticFileOptions();
			options.RequestPath = "/Data";
			options.FileProvider = fileProvider;
			app.UseStaticFiles(options);
			return app;
		}

	}
}
