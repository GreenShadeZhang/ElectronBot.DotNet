﻿// Copyright (c) Rodel. All rights reserved.

namespace ElectronBot.Copilot.Controls;

/// <summary>
/// 预设头像.
/// </summary>
public sealed partial class PresetAvatar : UserControl
{
    /// <summary>
    /// <see cref="PresetId"/> 的依赖属性.
    /// </summary>
    public static readonly DependencyProperty PresetIdProperty =
        DependencyProperty.Register(nameof(PresetId), typeof(string), typeof(PresetAvatar), new PropertyMetadata(default, new PropertyChangedCallback(OnPresetIdChanged)));

    /// <summary>
    /// <see cref="DefaultSymbol"/> 的依赖属性.
    /// </summary>
    public static readonly DependencyProperty DefaultSymbolProperty =
        DependencyProperty.Register(nameof(DefaultSymbol), typeof(FluentIcons.Common.Symbol), typeof(PresetAvatar), new PropertyMetadata(FluentIcons.Common.Symbol.Bot));

    /// <summary>
    /// Initializes a new instance of the <see cref="PresetAvatar"/> class.
    /// </summary>
    public PresetAvatar()
    {
        InitializeComponent();
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    /// <summary>
    /// 是否为聊天预设.
    /// </summary>
    public bool IsChatPreset { get; set; } = true;

    /// <summary>
    /// 预设 ID.
    /// </summary>
    public string PresetId
    {
        get => (string)GetValue(PresetIdProperty);
        set => SetValue(PresetIdProperty, value);
    }

    /// <summary>
    /// 默认图标.
    /// </summary>
    public FluentIcons.Common.Symbol DefaultSymbol
    {
        get => (FluentIcons.Common.Symbol)GetValue(DefaultSymbolProperty);
        set => SetValue(DefaultSymbolProperty, value);
    }

    private static void OnPresetIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var instance = d as PresetAvatar;
        instance?.CheckAvatarAsync();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        CheckAvatarAsync();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
    }

    private void OnPresetAvatarUpdateRequested(object sender, string e)
    {
        if (PresetId == e)
        {
            CheckAvatarAsync();
        }
    }

    private void CheckAvatarAsync()
    {
    }
}
