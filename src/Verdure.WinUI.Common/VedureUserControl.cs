using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Verdure.WinUI.Common;
public class VedureUserControl<TViewModel> : UserControl
    where TViewModel : class
{
    /// <summary>
    /// Dependency property for <see cref="ViewModel"/>.
    /// </summary>
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register(nameof(ViewModel), typeof(TViewModel), typeof(VedureUserControl<TViewModel>), new PropertyMetadata(default, new PropertyChangedCallback((dp, args) =>
            {
                var instance = dp as VedureUserControl<TViewModel>;
                instance?.OnViewModelChanged(args);
            })));

    public TViewModel ViewModel
    {
        get => (TViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public IServiceProvider ServiceProvider => Ioc.Default.GetRequiredService<IServiceProvider>();

    protected virtual void OnViewModelChanged(DependencyPropertyChangedEventArgs e)
    {

    }
}