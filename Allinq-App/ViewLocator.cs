﻿using System;
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
            var pageType = FindPageForViewModel(viewModel.GetType());

            var page = (Page)Activator.CreateInstance(pageType);

            page.BindingContext = viewModel;

            return page;
        }

        protected virtual Type FindPageForViewModel(Type viewModelType)
        {
            var retval = myMappings.FirstOrDefault(m => m.Key == viewModelType);
            var retval1 = myMappings.FirstOrDefault();

            return retval.Value;
             
            var pageTypeName = viewModelType
                .AssemblyQualifiedName
                .Replace("ViewModel", "View");

            var pageType = Type.GetType(pageTypeName);
            if (pageType == null)
                throw new ArgumentException(pageTypeName + " type does not exist");

            return pageType;
        }

        public void AddMapping<TViewModel, TView>()
            where TViewModel : ViewModelBase where TView : ContentPage
        {
            myMappings.Add(typeof(TViewModel), typeof(TView));
        } 
    }
}
