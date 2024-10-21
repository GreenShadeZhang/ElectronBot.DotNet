using Verdure.ElectronBot.Core.Models;

namespace Verdure.ElectronBot.Core.Contracts.Services;
public interface IEmoticonActionFrameService
{
    Task<bool> SendToUsbDeviceAsync(EmoticonActionFrame data, CancellationToken cancellationToken = default);
    void ClearQueue();
}
