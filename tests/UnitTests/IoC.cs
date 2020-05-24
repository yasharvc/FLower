using Core.Interfaces.Repositories;
using InMemoryRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
	class IoC
	{
		IServiceProvider ServiceProvider { get; set; }
		public IoC()
		{
			var collection = new ServiceCollection();
			collection = AddRepository(collection);
			collection = AddServices(collection);
			ServiceProvider = collection.BuildServiceProvider();
		}

		private ServiceCollection AddServices(ServiceCollection collection)
		{
			return collection;
		}

		private ServiceCollection AddRepository(ServiceCollection collection)
		{
			collection.AddSingleton<IStageRepository, StageRepository>();
			return collection;
		}

		public T GetService<T>() => ServiceProvider.GetService<T>();
	}
}
