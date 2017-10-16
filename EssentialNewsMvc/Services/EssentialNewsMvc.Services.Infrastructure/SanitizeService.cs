using EssentialNewsMvc.Services.Infrastructure.Contracts;
using Ganss.XSS;

namespace EssentialNewsMvc.Services.Infrastructure
{
    public class SanitizeService : ISanitizeService
    {
        public string Sanitize(string text)
        {
            var sanitizer = new HtmlSanitizer();
            var sanitized = sanitizer.Sanitize(text);
            return sanitized;
        }
    }
}
