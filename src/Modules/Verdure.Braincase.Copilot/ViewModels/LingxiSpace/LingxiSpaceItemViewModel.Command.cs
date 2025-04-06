using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Models;
using Verdure.Braincase.Core.Contracts.Services;
using Verdure.Braincase.Core.Models;
using Verdure.Braincase.WinUI.Common.Helpers;
using Windows.Storage;

namespace Verdure.Braincase.Copilot.ViewModels;
public partial class LingxiSpaceItemViewModel : ObservableRecipient
{
    [RelayCommand]
    public async Task LingxiItemClickAsync(object? select)
    {
        if (select is LingxiSpaceItemViewModel image)
        {
            var storageFolder = await KnownFolders.PicturesLibrary
                .CreateFolderAsync("ElectronBot\\data\\ImageFiles", CreationCollisionOption.OpenIfExists);

            var storageFile = await storageFolder
                .CreateFileAsync($"CustomViewPicture-{image.Id}.png", CreationCollisionOption.ReplaceExisting);
            var localSettingsService = Ioc.Default.GetRequiredService<ILocalSettingsService>();

            var botSetting = await localSettingsService.ReadSettingAsync<BotSetting>(Constants.BotSettingKey);
            if (botSetting != null && !string.IsNullOrEmpty(image.ImageData))
            {
                var writeableBitmapImage = await ImageHelper.WriteableBitmapFromBase64StringAsync(image.ImageData);

                if (await ImageHelper.SaveWriteableBitmapImageFileAsync(writeableBitmapImage, storageFile))
                {
                    botSetting.CustomViewPicturePath = storageFile.Path;
                    await localSettingsService.SaveSettingAsync(Constants.BotSettingKey, botSetting);

                    var clockView = new ChangeClockView
                    {
                        ClockViewName = "CustomView"
                    };
                    WeakReferenceMessenger.Default.Send(clockView);
                }
            }
        }
    }
}
