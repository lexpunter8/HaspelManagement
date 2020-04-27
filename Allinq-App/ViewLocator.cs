using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using XamarinViewModels.Interfaces;

namespace XamarinViewModels
{
    public class ViewLocator : IViewLocator
    {
        private Dictionary<Type, Type> myMappings = new Dictionary<Type, Type>();
        public Page CreateAndBindPageFor<TViewModel>(TViewModel viewModel) where TViewModel : ViewModelBase
        {
            try
            {

                var pageType = FindPageForViewModel(viewModel.GetType());

                var page = (Page)Activator.CreateInstance(pageType);

                page.BindingContext = viewModel;

                return page;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public virtual Type FindPageForViewModel(Type viewModelType)
        {
            var retval = myMappings.FirstOrDefault(m => m.Key == viewModelType);
            var retval1 = myMappings.FirstOrDefault();

            return retval.Value;
        }

        public void AddMapping<TViewModel, TView>()
            where TViewModel : ViewModelBase where TView : Page
        {
            myMappings.Add(typeof(TViewModel), typeof(TView));
        } 
    }
}
