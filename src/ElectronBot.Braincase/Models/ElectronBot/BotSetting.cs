﻿namespace Models;
public class BotSetting
{
    public string CustomClockTitle
    {
        get; set;
    } = "你好世界☺️";

    public int CustomClockTitleFontSize
    {
        get; set;
    } = 16;


    public string ChatGPTSessionKey
    {
        get; set;
    } = "";

    public string TuringAppkey
    {
        get; set;
    } = string.Empty;

    public string TuringUserId
    {
        get; set;
    } = string.Empty;

    public string OpenAIBaseUrl
    {
        get; set;
    } = "https://api.openai-proxy.com/";

    public string SparkDeskAppId
    {
        get; set;
    } = string.Empty;

    public string SparkDeskAPISecret
    {
        get; set;
    } = string.Empty;

    public string SparkDeskAPIKey
    {
        get; set;
    } = string.Empty;

    /// <summary>
    /// 手势识别回复文本
    /// </summary>
    public string AnswerText
    {
        get; set;
    } = "你想做什么,你需要帮忙吗,我能帮你做些什么,你需要帮助吗";

    public string CustomViewPicturePath
    {
        get;
        set;
    } = string.Empty;

    public string CustomHw75ImagePath
    {
        get;
        set;
    } = "ms-appx:///Assets/Images/Hw75CustomViewDefault.png";

    public float GaussianBlurValue
    {
        get;
        set;
    } = 4.0f;

    public bool CustomViewContentIsVisibility
    {
        get;
        set;
    } = true;

    public bool IsHelloEnabled
    {
        get;
        set;
    } = true;


    public string ChatGPTVersion
    {
        get; set;
    } = "gpt-3.5-turbo";
}
