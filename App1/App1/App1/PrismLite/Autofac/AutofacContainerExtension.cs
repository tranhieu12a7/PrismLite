﻿using App1.PrismLite.Navigations;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.PrismLite.Autofac
{
    public class AutofacContainerExtension : IAutofacContainerExtension
    {
        public static AutofacContainerExtension Instance { get; } = new AutofacContainerExtension();

        protected IContainer Container { get; set; }

        protected ContainerBuilder _containerBuilder;

        public AutofacContainerExtension()
        {
            _containerBuilder = new ContainerBuilder();
        }
        public bool Built { get; private set; }

        public void Build()
        {
            if (Built == false)
            {
                Container = _containerBuilder.Build();
                Built = true;
            }
        }

        public  T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return Container.Resolve(type);
        }

        public void RegisterInstance<TInterface, TImplementation>(TImplementation instance)
           where TImplementation : class, TInterface
        {
            _containerBuilder.RegisterInstance(instance).As<TInterface>();
        }

        public void RegisterInstance<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _containerBuilder.RegisterType<TImplementation>().As<TInterface>().SingleInstance();
        }

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _containerBuilder.RegisterType<TImplementation>().As<TInterface>().InstancePerLifetimeScope();
        }
        public void RegisterForNavigation<TView, TViewModel>(string Namepage=null) where TViewModel : MVVM.ViewModelBase where TView:Page
        {
            if (string.IsNullOrEmpty(Namepage))
                Namepage = typeof(TView).Name;
            _containerBuilder.RegisterType<TViewModel>();
            PageNavigationRegistry.Register(Namepage, typeof(TView), typeof(TViewModel));
        }

        public void Register<T>() where T : class
        {
            _containerBuilder.RegisterType<T>()
                .InstancePerLifetimeScope();
        }


        public void RegisterViewModels()
        {
            _containerBuilder.RegisterAssemblyTypes(GetType().Assembly)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .AsSelf()
                .InstancePerDependency();
        }
    }
}
