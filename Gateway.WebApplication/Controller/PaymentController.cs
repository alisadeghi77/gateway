using System;
using System.Threading.Tasks;
using Gateway.Services.Extensions;
using Microsoft.AspNetCore.Mvc;
using Parbad;
using Parbad.Gateway.Mellat;

namespace Gateway.WebApplication.Controller;

[Route("Payment")]
public class PaymentController : ControllerBase
{
    private readonly IOnlinePayment _onlinePayment;

    public PaymentController(IOnlinePayment onlinePayment)
    {
        _onlinePayment = onlinePayment;
    }


    [HttpPost]
    public async Task<IActionResult> Pay()
    {
        var callbackUrl = "https://localhost:5001/Verify";

        var result = await _onlinePayment.RequestAsync(invoice =>
        {
            invoice.SetMellatAdditionalData(new MellatGatewayAdditionalDataRequest())
                .SetCallbackUrl(callbackUrl)
                .SetAmount(1000)
                .SetGateway("Mellat")
                .SetTrackingNumber(new Random().Next(50000));
        });

        if (result.IsSucceed)
            return Ok(result.GatewayTransporter.Descriptor.GetUrl());

        return Ok(result);
    }
}
