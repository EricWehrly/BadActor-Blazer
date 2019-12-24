using System;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Components;

namespace BadActor.Services
{
	public class ModalService
	{
		public event Action<string, RenderFragment> OnShow;
		public event Action OnClose;

		public void Show(string title, Type contentType)
		{
			Console.WriteLine("Showing " + contentType.BaseType);
			/*
			if (contentType.BaseType != typeof(BlazorComponent))
			{
				throw new ArgumentException($"{contentType.FullName} must be a Blazor Component");
			}
			*/

			var content = new RenderFragment(x => { x.OpenComponent(1, contentType); x.CloseComponent(); });
			OnShow?.Invoke(title, content);
		}

		public void Close()
		{
			OnClose?.Invoke();
		}
	}
}
