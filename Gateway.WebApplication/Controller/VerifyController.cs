using System;
using System.Threading.Tasks;
using Gateway.Services.Extensions;
using Microsoft.AspNetCore.Mvc;
using Parbad;
using Parbad.Gateway.Mellat;

namespace Gateway.WebApplication.Controller;

[Route("Verify")]
public class VerifyController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IOnlinePayment _onlinePayment;

    public VerifyController(IOnlinePayment onlinePayment)
    {
        _onlinePayment = onlinePayment;
    }


    [HttpGet, HttpPost]
    public async Task<IActionResult> Verify(string paymentToken)
    {
        var invoice = await _onlinePayment.FetchAsync();

        // Check if the invoice is new or it's already processed before.
        if (invoice.Status != PaymentFetchResultStatus.ReadyForVerifying)
        {
            // You can also see if the invoice is already verified before.
            var isAlreadyVerified = invoice.IsAlreadyVerified;
            return Content("The payment was not successful.");
        }

        // This is an example of cancelling an invoice when you think that the payment process must be stopped.
        if (invoice.TrackingNumber < 0)
        {
            var cancelResult = await _onlinePayment.CancelAsync(invoice,
                cancellationReason: "Sorry, We have no more products to sell.");

            return View("CancelResult", cancelResult);
        }

        var verifyResult = await _onlinePayment.VerifyAsync(invoice);

        // Note: Save the verifyResult.TransactionCode in your database.

        return View(verifyResult);
    }
}
