using System;
using System.Threading.Tasks;

namespace XamarinViewModels.Interfaces
{
    public interface IViewModelLifeCycle
    {
        /// <summary>
        /// Called exactly once, before the viewmodel enters the navigation stack
        /// </summary>
        Task BeforeFirstShown();

        /// <summary>
        /// Called exactly once, when the viewmodel leaves the navigation stack
        /// </summary>
        Task AfterDismissed();
    }
}
