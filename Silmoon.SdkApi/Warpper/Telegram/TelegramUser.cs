using Newtonsoft.Json;

namespace Silmoon.SdkApi.Warpper.Telegram
{
    /// <summary>
    /// Telegram 用户信息
    /// </summary>
    public class TelegramUser
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("is_bot")]
        public bool IsBot { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// 获取显示名称，优先 Username，其次 FirstName + LastName
        /// </summary>
        [JsonIgnore]
        public string DisplayName => !string.IsNullOrEmpty(Username) ? $"@{Username}" : $"{FirstName} {LastName}".Trim();
    }
}
