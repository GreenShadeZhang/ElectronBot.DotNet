﻿using Verdure.WinUI.Common.Models;

namespace Verdure.WinUI.Common;
public interface IElectronBotPlayer
{
    Task PlayVideoByPathAsync(string path, List<ElectronBotAction>? actions = null);
    Task PlayVideoByNameIdAsync(string nameId);
    Task PlayAudioByTextAsync(string text);
    Task PlayAudioByPathAsync(string path);
    Task PlayAudioAsync(Stream stream);
    Task PlayImageAsync(string path);
    Task PlayImageAsync(Stream stream);
    Task PlayImageAsync(byte[] bytes);
}
