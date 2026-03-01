using Newtonsoft.Json;
using Silmoon.Extensions;
using Silmoon.SdkApi.Apple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.SdkApi.Apple
{
    public class InAppPurchase
    {
        public static async Task<AppleReceipt> ReceiptVerify(string receipt)
        {
            using HttpClient client = new HttpClient();

            var sandbox = Encoding.UTF8.GetString(Convert.FromBase64String(receipt)).ToLower().Contains("sandbox");

            var url = sandbox ? "https://sandbox.itunes.apple.com/verifyReceipt" : "https://buy.itunes.apple.com/verifyReceipt";

            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Method = HttpMethod.Post;
            requestMessage.RequestUri = new Uri(url);
            var data = "{ \"receipt-data\": \"" + receipt + "\"}";
            requestMessage.Content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);

            string response = await responseMessage.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject<object>(response);
            var appleReceipt = new AppleReceipt();

            appleReceipt.Status = int.Parse((string)obj.status);
            appleReceipt.Bid = obj?.receipt?.bid;
            appleReceipt.Sandbox = sandbox;
            appleReceipt.Bvrs = obj?.receipt?.bvrs;
            appleReceipt.InAppOwnershipType = obj?.receipt?.in_app_ownership_type;
            appleReceipt.IsInIntroOfferPeriod = ((string)obj?.receipt?.is_in_intro_offer_period).ToBool();
            appleReceipt.IsTrialPeriod = ((string)obj?.receipt?.is_trial_period).ToBool();
            appleReceipt.OriginalPurchaseDate = obj?.receipt?.original_purchase_date.ToString();
            appleReceipt.ItemId = obj?.receipt?.item_id;
            appleReceipt.OriginalPurchaseDateMs = obj?.receipt?.original_purchase_date_ms ?? 0;
            appleReceipt.OriginalPurchaseDatePst = obj?.receipt?.original_purchase_date_pst;
            appleReceipt.OriginalTransactionId = obj?.receipt?.original_transaction_id;
            appleReceipt.ProductId = obj?.receipt?.product_id;
            appleReceipt.PurchaseDate = obj?.receipt?.purchase_date;
            appleReceipt.PurchaseDateMs = obj?.receipt?.purchase_date_ms ?? 0;
            appleReceipt.PurchaseDatePst = obj?.receipt?.purchase_date_pst;
            appleReceipt.Quantity = obj?.receipt?.quantity ?? 0;
            appleReceipt.TransactionId = obj?.receipt?.transaction_id;
            appleReceipt.UniqueIdentifier = obj?.receipt?.unique_identifier;
            appleReceipt.UniqueVendorIdentifier = obj?.receipt?.unique_vendor_identifier;
            return appleReceipt;
        }
    }
}
