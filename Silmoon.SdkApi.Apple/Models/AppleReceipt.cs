using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.SdkApi.Apple.Models
{
    public class AppleReceipt
    {
        [DisplayName("状态码")]
        public int Status { get; set; }
        [DisplayName("原始交易时间")]
        public string OriginalPurchaseDatePst { get; set; }
        [DisplayName("交易时间")]
        public long PurchaseDateMs { get; set; }
        [DisplayName("唯一标识")]
        public string UniqueIdentifier { get; set; }
        [DisplayName("原始交易ID")]
        public string OriginalTransactionId { get; set; }
        [DisplayName("版本号")]
        public string Bvrs { get; set; }
        [DisplayName("交易ID")]
        public string TransactionId { get; set; }
        [DisplayName("数量")]
        public int Quantity { get; set; }
        [DisplayName("购买类型")]
        public string InAppOwnershipType { get; set; }
        [DisplayName("唯一商户标识")]
        public string UniqueVendorIdentifier { get; set; }
        [DisplayName("商品ID")]
        public string ItemId { get; set; }
        [DisplayName("原始交易时间")]
        public string OriginalPurchaseDate { get; set; }
        [DisplayName("是否在试用期")]
        public bool IsInIntroOfferPeriod { get; set; }
        [DisplayName("产品ID")]
        public string ProductId { get; set; }
        [DisplayName("交易时间")]
        public string PurchaseDate { get; set; }
        [DisplayName("是否试用期")]
        public bool IsTrialPeriod { get; set; }
        [DisplayName("交易时间")]
        public string PurchaseDatePst { get; set; }
        [DisplayName("商户ID")]
        public string Bid { get; set; }
        [DisplayName("原始交易时间")]
        public long OriginalPurchaseDateMs { get; set; }

        public bool Sandbox { get; set; }

    }
}
