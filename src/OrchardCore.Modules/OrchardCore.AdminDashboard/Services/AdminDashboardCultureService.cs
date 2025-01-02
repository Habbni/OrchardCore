using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using OrchardCore.ContentLocalization.Models;
using OrchardCore.ContentManagement;

namespace OrchardCore.AdminDashboard.Services;

public class AdminDashboardCultureService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private CultureInfo currentCulture;

    public AdminDashboardCultureService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private CultureInfo getCurrentCulture()
    {
        if (currentCulture == null)
        {
            currentCulture = _httpContextAccessor.HttpContext?.Features.Get<IRequestCultureFeature>()?.RequestCulture?.Culture ?? CultureInfo.CurrentUICulture;
        }
        return currentCulture;
    }

    public bool IsCultureMatching(ContentItem widget)
    {
        var culture = getCurrentCulture();
        var localizationWidget = widget.As<LocalizationPart>();
        return localizationWidget == null || string.IsNullOrEmpty(localizationWidget.Culture) || localizationWidget.Culture == culture.Name;
    }
}
