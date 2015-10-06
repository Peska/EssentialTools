using EssentialTools.Models;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EssentialTools.Infrastructure
{
	public class NinjectDependencyResolver : IDependencyResolver
	{
		private IKernel kernel;

		public NinjectDependencyResolver(IKernel kernelParam)
		{
			kernel = kernelParam;

			AddBindings();
		}

		public object GetService(Type serviceType)
		{
			return kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type typeService)
		{
			return kernel.GetAll(typeService);
		}

		private void AddBindings()
		{
			kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().InRequestScope();
			kernel.Bind<IDiscountHelper>().To<DefaulDiscountHelper>().WithConstructorArgument(50M);
			kernel.Bind<IDiscountHelper>().To<FlexibleDiscountHelper>().WhenInjectedInto<LinqValueCalculator>();
        }
	}
}