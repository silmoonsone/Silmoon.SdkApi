using System;

namespace Silmoon.SdkApi.Warpper.Telegram
{
    /// <summary>
    /// 收到消息时触发的事件参数
    /// </summary>
    public class TelegramMessageReceivedEventArgs : EventArgs
    {
        public TelegramMessageReceivedEventArgs(TelegramMessage message, long updateId)
        {
            Message = message;
            UpdateId = updateId;
        }

        /// <summary>
        /// 收到的消息
        /// </summary>
        public TelegramMessage Message { get; }

        /// <summary>
        /// Update 唯一标识，用于 getUpdates 的 offset
        /// </summary>
        public long UpdateId { get; }

        /// <summary>
        /// 快捷获取 ChatId
        /// </summary>
        public string ChatId => Message?.ChatId;

        /// <summary>
        /// 快捷获取发件人信息
        /// </summary>
        public TelegramUser From => Message?.From;

        /// <summary>
        /// 快捷获取消息文本
        /// </summary>
        public string Text => Message?.Text;
    }
}
