using System;
using System.Linq;
using Parbad;

namespace Gateway.Services.Extensions;

public static class GatewayExtension
{
    public static string GetUrl(this GatewayTransporterDescriptor descriptor)
    {
        var queryParams = string.Join("&", descriptor.Form.Select(s => $"{s.Key}={s.Value}"));
        return new Uri($"{descriptor.Url}?{queryParams}").ToString();
    }
}
