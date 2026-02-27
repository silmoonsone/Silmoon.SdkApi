using Silmoon.Extension;
using Silmoon.SdkApi.Warpper.Telegram;

var configFilePath = AppContext.BaseDirectory + "config.local.json";
if (!File.Exists(configFilePath)) configFilePath = AppContext.BaseDirectory + "config.json";
var jsonConfig = JsonHelperV2.LoadJsonFromFile(configFilePath);

string? BotKey = jsonConfig.Value<string>("telegram_bot_key");

TelegramBot bot = new TelegramBot(BotKey);

string chatId = string.Empty;
bot.OnMessage += async (s, e) =>
{
    chatId = e.ChatId;
    Console.WriteLine($"Received message {e.ChatId} from {e.Message.From.Username}: {e.Message.Text}");
    await bot.SendChatActionAsync(e.Message.ChatId, TelegramChatAction.Typing);
    await Task.Delay(1000);
    var replyResult = await bot.SendMessageAsync(e.Message.ChatId, "Echo: " + e.Message.Text);
    Console.WriteLine($"Sent reply to {e.ChatId}, result: {replyResult.ToJsonString()}");
    bot.StopPolling();
};

await bot.StartPollingAsync();

Console.WriteLine("Bot started. Press Enter to exit.");
Console.ReadLine();