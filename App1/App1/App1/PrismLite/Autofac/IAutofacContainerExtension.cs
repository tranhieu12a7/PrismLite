using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1.PrismLite.Autofac
{
    interface IAutofacContainerExtension
    {
        void Build();
        T Resolve<T>();
        object Resolve(Type type);
        //đăng ký interface
        void RegisterInstance<TInterface, TImplementation>() where TImplementation : TInterface;
        void Register<TInterface, TImplementation>() where TImplementation : TInterface;
    }
}
