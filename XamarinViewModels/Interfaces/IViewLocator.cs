using System;
using Xamarin.Forms;

namespace XamarinViewModels.Interfaces
{
    public interface IViewLocator
    {
        Page CreateAndBindPageFor<TViewModel>(TViewModel viewModel) where TViewModel : ViewModelBase;
        Type FindPageForViewModel(Type viewModelType);
    }
}
