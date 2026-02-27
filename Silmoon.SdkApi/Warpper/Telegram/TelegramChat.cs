using Newtonsoft.Json;

namespace Silmoon.SdkApi.Warpper.Telegram
{
    /// <summary>
    /// Telegram 聊天/会话信息
    /// </summary>
    public class TelegramChat
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}
