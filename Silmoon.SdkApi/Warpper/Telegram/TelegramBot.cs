using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Silmoon.Extension;
using Silmoon.Extension.Http;
using Silmoon.Models;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Silmoon.SdkApi.Warpper.Telegram
{
    /// <summary>
    /// Telegram Bot API 封装，支持事件驱动接收消息和发送消息
    /// </summary>
    public class TelegramBot
    {
        private const string ApiBaseUrl = "https://api.telegram.org/bot";
        private static readonly HttpClient HttpClient = new HttpClient();
        private readonly object _pollingLock = new object();
        private CancellationTokenSource _pollingCts;
        private Task _pollingTask;

        public TelegramBot(string botKey)
        {
            BotKey = botKey ?? throw new ArgumentNullException(nameof(botKey));
        }

        /// <summary>
        /// Bot Token（由 @BotFather 获取）
        /// </summary>
        public string BotKey { get; set; }

        /// <summary>
        /// 收到新消息时触发（仅文本消息）
        /// </summary>
        public event EventHandler<TelegramMessageReceivedEventArgs> OnMessage;
        //public event Func<object, TelegramMessageReceivedEventArgs, Task> OnMessage;

        private string ApiUrl => $"{ApiBaseUrl}{BotKey}";

        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="chatId">聊天 ID（用户、群组或频道）</param>
        /// <param name="text">消息文本</param>
        /// <param name="parseMode">解析模式，如 "HTML" 或 "Markdown"</param>
        /// <param name="disableNotification">是否静默发送</param>
        public async Task<StateSet<bool>> SendMessageAsync(string chatId, string text, string parseMode = null, bool disableNotification = false)
        {
            if (string.IsNullOrEmpty(chatId)) return false.ToStateSet("chatId 不能为空");
            if (string.IsNullOrEmpty(text)) return false.ToStateSet("text 不能为空");

            var data = new
            {
                chat_id = chatId,
                text = text,
                parse_mode = parseMode,
                disable_notification = disableNotification
            };

            var result = await JsonRequest.PostAsync<JObject, object>($"{ApiUrl}/sendMessage", data, null, new JsonRequestSetting { JsonSerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore } });

            if (result.IsSuccess && result.IsSuccessStatusCode && result.Result != null)
            {
                var ok = result.Result["ok"]?.Value<bool>() ?? false;
                return ok ? true.ToStateSet() : false.ToStateSet(result.Result["description"]?.Value<string>() ?? "未知错误");
            }
            return false.ToStateSet(result.Exception?.Message ?? result.Response ?? "请求失败");
        }

        /// <summary>
        /// 回复指定消息
        /// </summary>
        /// <param name="message">要回复的消息</param>
        /// <param name="text">回复文本</param>
        /// <param name="parseMode">解析模式</param>
        public async Task<StateSet<bool>> ReplyToAsync(TelegramMessage message, string text, string parseMode = null)
        {
            if (message == null) return false.ToStateSet("message 不能为空");
            return await SendReplyAsync(message.ChatId, message.MessageId, text, parseMode);
        }

        /// <summary>
        /// 回复指定消息（通过 chatId 和 messageId）
        /// </summary>
        public async Task<StateSet<bool>> SendReplyAsync(string chatId, int replyToMessageId, string text, string parseMode = null)
        {
            if (string.IsNullOrEmpty(chatId)) return false.ToStateSet("chatId 不能为空");
            if (string.IsNullOrEmpty(text)) return false.ToStateSet("text 不能为空");

            var data = new
            {
                chat_id = chatId,
                text = text,
                reply_to_message_id = replyToMessageId,
                parse_mode = parseMode
            };

            var result = await JsonRequest.PostAsync<JObject, object>($"{ApiUrl}/sendMessage", data, null, new JsonRequestSetting { JsonSerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore } });

            if (result.IsSuccess && result.IsSuccessStatusCode && result.Result != null)
            {
                var ok = result.Result["ok"]?.Value<bool>() ?? false;
                return ok ? true.ToStateSet() : false.ToStateSet(result.Result["description"]?.Value<string>() ?? "未知错误");
            }
            return false.ToStateSet(result.Exception?.Message ?? result.Response ?? "请求失败");
        }

        /// <summary>
        /// 设置聊天动作状态（对方将看到对应效果，如"正在输入..."）
        /// </summary>
        /// <param name="chatId">聊天 ID</param>
        /// <param name="action">动作类型</param>
        /// <param name="messageThreadId">可选，论坛主题 ID（仅超级群组和开启主题的私聊）</param>
        /// <remarks>效果持续约 5 秒，Bot 发送消息后会自动清除。处理耗时操作时可每 5 秒重复调用。</remarks>
        public async Task<StateSet<bool>> SendChatActionAsync(string chatId, TelegramChatAction action, int? messageThreadId = null)
        {
            if (string.IsNullOrEmpty(chatId)) return false.ToStateSet("chatId 不能为空");

            var apiAction = GetChatActionString(action);
            var data = new
            {
                chat_id = chatId,
                action = apiAction,
                message_thread_id = messageThreadId
            };

            var result = await JsonRequest.PostAsync<JObject, object>($"{ApiUrl}/sendChatAction", data, null, new JsonRequestSetting { JsonSerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore } });

            if (result.IsSuccess && result.IsSuccessStatusCode && result.Result != null)
            {
                var ok = result.Result["ok"]?.Value<bool>() ?? false;
                return ok ? true.ToStateSet() : false.ToStateSet(result.Result["description"]?.Value<string>() ?? "未知错误");
            }
            return false.ToStateSet(result.Exception?.Message ?? result.Response ?? "请求失败");
        }

        /// <summary>
        /// 设置聊天动作状态（使用字符串，可传 Telegram API 支持的任何 action 值）
        /// </summary>
        public async Task<StateSet<bool>> SendChatActionAsync(string chatId, string action, int? messageThreadId = null)
        {
            if (string.IsNullOrEmpty(chatId)) return false.ToStateSet("chatId 不能为空");
            if (string.IsNullOrEmpty(action)) return false.ToStateSet("action 不能为空");

            var data = new
            {
                chat_id = chatId,
                action = action,
                message_thread_id = messageThreadId
            };

            var result = await JsonRequest.PostAsync<JObject, object>($"{ApiUrl}/sendChatAction", data, null, new JsonRequestSetting { JsonSerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore } });

            if (result.IsSuccess && result.IsSuccessStatusCode && result.Result != null)
            {
                var ok = result.Result["ok"]?.Value<bool>() ?? false;
                return ok ? true.ToStateSet() : false.ToStateSet(result.Result["description"]?.Value<string>() ?? "未知错误");
            }
            return false.ToStateSet(result.Exception?.Message ?? result.Response ?? "请求失败");
        }

        private static string GetChatActionString(TelegramChatAction action)
        {
            switch (action)
            {
                case TelegramChatAction.Typing: return "typing";
                case TelegramChatAction.UploadPhoto: return "upload_photo";
                case TelegramChatAction.RecordVideo: return "record_video";
                case TelegramChatAction.UploadVideo: return "upload_video";
                case TelegramChatAction.RecordVoice: return "record_voice";
                case TelegramChatAction.UploadVoice: return "upload_voice";
                case TelegramChatAction.UploadDocument: return "upload_document";
                case TelegramChatAction.ChooseSticker: return "choose_sticker";
                case TelegramChatAction.FindLocation: return "find_location";
                case TelegramChatAction.RecordVideoNote: return "record_video_note";
                case TelegramChatAction.UploadVideoNote: return "upload_video_note";
                default: return "typing";
            }
        }

        /// <summary>
        /// 是否正在轮询中
        /// </summary>
        public bool IsPolling => _pollingTask != null && !_pollingTask.IsCompleted;

        /// <summary>
        /// 开始长轮询接收更新（非阻塞，立即返回 Task）
        /// </summary>
        /// <param name="timeoutSeconds">每次 getUpdates 的超时秒数（建议 30-60）</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>轮询任务。await 此 Task 会等待轮询持续运行直至 StopPolling；重复调用时返回已在运行的任务，不会新建</returns>
        public Task StartPollingAsync(int timeoutSeconds = 30, CancellationToken cancellationToken = default)
        {
            lock (_pollingLock)
            {
                if (_pollingTask != null && !_pollingTask.IsCompleted)
                    return _pollingTask;

                _pollingCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                _pollingTask = PollingLoopAsync(timeoutSeconds, _pollingCts.Token);
                return _pollingTask;
            }
        }

        /// <summary>
        /// 尝试开始轮询
        /// </summary>
        /// <returns>true 表示成功启动，false 表示已在运行</returns>
        public bool TryStartPolling(int timeoutSeconds = 30, CancellationToken cancellationToken = default)
        {
            lock (_pollingLock)
            {
                if (_pollingTask != null && !_pollingTask.IsCompleted)
                    return false;

                _pollingCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                _pollingTask = PollingLoopAsync(timeoutSeconds, _pollingCts.Token);
                return true;
            }
        }

        /// <summary>
        /// 停止轮询
        /// </summary>
        /// <returns>true 表示成功停止，false 表示未曾调用过 StartPolling 或轮询已结束</returns>
        public bool StopPolling()
        {
            lock (_pollingLock)
            {
                if (_pollingCts == null || _pollingTask == null || _pollingTask.IsCompleted)
                    return false;

                _pollingCts.Cancel();
                return true;
            }
        }

        /// <summary>
        /// 等待轮询任务结束（用于主程序保持运行）
        /// </summary>
        public Task WaitForPollingAsync() => _pollingTask ?? Task.CompletedTask;

        private async Task PollingLoopAsync(int timeoutSeconds, CancellationToken cancellationToken)
        {
            long offset = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var url = $"{ApiUrl}/getUpdates?offset={offset}&timeout={timeoutSeconds}";
                    using (var response = await HttpClient.GetAsync(url, cancellationToken))
                    {
                        response.EnsureSuccessStatusCode();
                        var responseBody = await response.Content.ReadAsStringAsync();
                        var json = JObject.Parse(responseBody);

                        if (json["ok"]?.Value<bool>() != true)
                            continue;

                        var updates = json["result"] as JArray;
                        if (updates == null || updates.Count == 0)
                            continue;

                        foreach (var updateToken in updates)
                        {
                            try
                            {
                                var updateId = updateToken["update_id"]?.Value<long>() ?? 0;
                                offset = updateId + 1;

                                var message = updateToken["message"] ?? updateToken["edited_message"];
                                if (message == null)
                                    continue;

                                var text = message["text"]?.Value<string>();
                                if (string.IsNullOrEmpty(text))
                                    continue;

                                var telegramMessage = message.ToObject<TelegramMessage>();
                                if (telegramMessage == null)
                                    continue;

                                var args = new TelegramMessageReceivedEventArgs(telegramMessage, updateId);
                                OnMessage?.Invoke(this, args);
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"处理 Update 异常: {ex.Message}");
                            }
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"getUpdates 异常: {ex.Message}");
                    await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
                }
            }
        }
    }
}
