using Microsoft.Extensions.Hosting;
using Silmoon.Extensions.Hosting.Interfaces;
using Silmoon.Extensions;
using Silmoon.SdkApi.Warpper.Telegram;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HostTesting.Services
{

    public class TelegramBotService : IHostedService
    {
        TelegramBot? bot = null;
        ISilmoonConfigureService SilmoonConfigureService { get; set; }
        public TelegramBotService(ISilmoonConfigureService silmoonConfigureService)
        {
            SilmoonConfigureService = silmoonConfigureService;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            bot = new TelegramBot(SilmoonConfigureService.ConfigJson.Value<string>("telegram_bot_key"));
            bot.OnMessage += Bot_OnMessage;
            _ = bot.StartPollingAsync(cancellationToken: cancellationToken);
            return Task.CompletedTask;
        }

        private async void Bot_OnMessage(object? sender, TelegramMessageReceivedEventArgs e)
        {
            Console.WriteLine($"Received message {e.ChatId} from {e.Message.From.Username}: {e.Message.Text}");
            await bot!.SendChatActionAsync(e.Message.ChatId, TelegramChatAction.Typing);
            await Task.Delay(1000);
            var replyResult = await bot.SendMessageAsync(e.Message.ChatId, "Echo: " + e.Message.Text);
            Console.WriteLine($"Sent echo reply to {e.ChatId}, result: {replyResult.ToJsonString()}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            bot!.StopPolling();
            return Task.CompletedTask;
        }
    }
}
