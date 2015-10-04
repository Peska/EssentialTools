﻿using EssentialTools.Models;
using Ninject;
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
			kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
		}
	}
}