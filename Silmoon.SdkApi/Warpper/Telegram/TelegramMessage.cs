using Newtonsoft.Json;
using System;

namespace Silmoon.SdkApi.Warpper.Telegram
{
    /// <summary>
    /// 收到的 Telegram 消息
    /// </summary>
    public class TelegramMessage
    {
        [JsonProperty("message_id")]
        public int MessageId { get; set; }

        [JsonProperty("from")]
        public TelegramUser From { get; set; }

        [JsonProperty("chat")]
        public TelegramChat Chat { get; set; }

        [JsonProperty("date")]
        public int Date { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// 消息发送时间（UTC）
        /// </summary>
        [JsonIgnore]
        public DateTime DateUtc => DateTimeOffset.FromUnixTimeSeconds(Date).UtcDateTime;

        /// <summary>
        /// 快捷获取 chat_id
        /// </summary>
        [JsonIgnore]
        public string ChatId => Chat?.Id.ToString();
    }
}
