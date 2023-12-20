﻿using System.Collections.ObjectModel;
using System.Runtime.InteropServices.WindowsRuntime;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ElectronBot.Braincase.Contracts.Services;
using ElectronBot.Braincase.Contracts.ViewModels;
using ElectronBot.Braincase.Services;
using HelloWordKeyboard.DotNet;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Verdure.ElectronBot.Core.Models;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

namespace ElectronBot.Braincase.ViewModels;

public partial class Hw75ViewModel : ObservableRecipient, INavigationAware
{
    /// <summary>
    /// eink content
    /// </summary>
    [ObservableProperty]
    UIElement? _element;

    /// <summary>
    /// 时钟选中数据
    /// </summary>
    [ObservableProperty]
    ComboxItemModel? clockComBoxSelect;

    /// <summary>
    /// 表盘列表
    /// </summary>
    [ObservableProperty]
    public ObservableCollection<ComboxItemModel> clockComboxModels;

    private readonly IClockViewProviderFactory _viewProviderFactory;

    private readonly DispatcherTimer _dispatcherTimer = new ();

    private readonly IHw75DynamicDevice _hw75DynamicDevice;

    public Hw75ViewModel(ComboxDataService comboxDataService, IClockViewProviderFactory viewProviderFactory
        , IHw75DynamicDevice hw75DynamicDevice)
    {
        ClockComboxModels = comboxDataService.GetClockViewComboxList();
        _viewProviderFactory = viewProviderFactory;
        _dispatcherTimer.Interval = new TimeSpan(0, 0, 20);

        _dispatcherTimer.Tick += DispatcherTimer_Tick;
        _hw75DynamicDevice = hw75DynamicDevice;
    }

    private async void DispatcherTimer_Tick(object? sender, object e)
    {
        try
        {
            var renderTargetBitmap = new RenderTargetBitmap();

            await renderTargetBitmap.RenderAsync(Element);

            var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();

            using var stream = new InMemoryRandomAccessStream();
            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
            (uint)renderTargetBitmap.PixelWidth,
            (uint)renderTargetBitmap.PixelHeight,
                96,
                96,
                pixelBuffer.ToArray());

            await encoder.FlushAsync();
            stream.Seek(0);

            using var image = SixLabors.ImageSharp.Image.Load<Rgba32>(stream.AsStream());

            image.Mutate(x =>
            {
                x.Resize(128, 296);
                x.Grayscale();
            });

            var byteArray = image.EnCodeImageToBytes();


            _ = _hw75DynamicDevice.SetEInkImage(byteArray, 0, 0, 128, 296, false);
        }
        catch (Exception ex)
        {
        }

    }


    /// <summary>
    /// 表盘切换方法
    /// </summary>
    [RelayCommand]
    private void ClockChanged()
    {
        var clockName = ClockComBoxSelect?.DataKey;

        if (!string.IsNullOrWhiteSpace(clockName))
        {
            var viewProvider = _viewProviderFactory.CreateClockViewProvider(clockName);

            Element = viewProvider.CreateClockView(clockName);
        }
    }

    public void OnNavigatedTo(object parameter)
    {
        var viewProvider = _viewProviderFactory.CreateClockViewProvider("DefautView");

        Element = viewProvider.CreateClockView("DefautView");

        _dispatcherTimer.Start();

        try
        {
            _hw75DynamicDevice.Open();
        }
        catch (Exception ex)
        {
        }

    }
    public void OnNavigatedFrom()
    {
        _dispatcherTimer.Stop();
        _hw75DynamicDevice.Close();
        _hw75DynamicDevice.Dispose();
    }
}
