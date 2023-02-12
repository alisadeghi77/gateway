using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Parbad;
using Parbad.AspNetCore;
using Parbad.Gateway.Mellat;

namespace Gateway.WebApplication.Controller;

[Route("pay")]
public class PayController : ControllerBase
{
    private readonly IOnlinePayment _onlinePayment;

    public PayController(IOnlinePayment onlinePayment)
    {
        _onlinePayment = onlinePayment;
    }


    [HttpPost]
    public async Task<IActionResult> Pay()
    {
        var callbackUrl = "https://www.site.com/foo/bar";

        var result = await _onlinePayment.RequestAsync(invoice =>
        {
            invoice.SetMellatAdditionalData(new MellatGatewayAdditionalDataRequest())
                .SetCallbackUrl(callbackUrl)
                .SetAmount(1000)
                .SetGateway("Mellat")
                .SetTrackingNumber(10045)
                ;
            //
            // invoice.UseAutoIncrementTrackingNumber();
        });

        if (result.IsSucceed)
            return Ok(result.GatewayTransporter.Descriptor.Url);

        return Ok(result);
    }
}