namespace UnitTests
{
	public abstract class BaseTest
	{
		private IoC IoC { get; set; } = new IoC();
		public T GetService<T>() => IoC.GetService<T>();
	}
}