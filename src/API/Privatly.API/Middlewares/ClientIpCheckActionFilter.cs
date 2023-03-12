using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Privatly.API.Middlewares;

public class ClientIpCheckActionFilter : ActionFilterAttribute
{
    private readonly ILogger _logger;
    private readonly string _safeList;

    public ClientIpCheckActionFilter(string safeList, ILogger logger)
    {
        _safeList = safeList;
        _logger = logger;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var remoteIp = context.HttpContext.Connection.RemoteIpAddress;
        
        _logger.LogDebug("Remote IpAddress: {RemoteIp}", remoteIp);
        var ip = _safeList.Split(';');

        if (remoteIp.IsIPv4MappedToIPv6)
        {
            remoteIp = remoteIp.MapToIPv4();
        }

        var badIp = !ip.Select(IPAddress.Parse).Contains(remoteIp);

        if (badIp)
        {
            _logger.LogWarning("Forbidden Request from IP: {RemoteIp}", remoteIp);
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            return;
        }

        base.OnActionExecuting(context);
    }
}