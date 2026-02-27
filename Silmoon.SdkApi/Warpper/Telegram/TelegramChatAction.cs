namespace Silmoon.SdkApi.Warpper.Telegram
{
    /// <summary>
    /// Telegram 聊天动作状态，用于 sendChatAction 显示对方可见的状态
    /// </summary>
    public enum TelegramChatAction
    {
        /// <summary>正在输入文本</summary>
        Typing,

        /// <summary>正在上传照片</summary>
        UploadPhoto,

        /// <summary>正在录制视频</summary>
        RecordVideo,

        /// <summary>正在上传视频</summary>
        UploadVideo,

        /// <summary>正在录制语音</summary>
        RecordVoice,

        /// <summary>正在上传语音</summary>
        UploadVoice,

        /// <summary>正在上传文件</summary>
        UploadDocument,

        /// <summary>正在选择贴纸</summary>
        ChooseSticker,

        /// <summary>正在查找位置</summary>
        FindLocation,

        /// <summary>正在录制视频笔记</summary>
        RecordVideoNote,

        /// <summary>正在上传视频笔记</summary>
        UploadVideoNote,
    }
}
