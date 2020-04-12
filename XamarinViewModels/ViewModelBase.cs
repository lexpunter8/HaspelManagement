using System.Threading.Tasks;
using XamarinViewModels.Interfaces;

namespace XamarinViewModels
{
    public abstract class ViewModelBase : PropertyChangedBase, IViewModelLifeCycle
    {
        public virtual Task BeforeFirstShown()
        {
            return Task.CompletedTask;
        }

        public virtual Task AfterDismissed()
        {
            return Task.CompletedTask;
        }
    }
}
