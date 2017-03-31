using System.Collections.Generic;

using Xamarin.Forms;

namespace RabbitTest2
{
	public partial class App : Application
	{
		public static bool UseMockDataStore = true;
		public static string BackendUrl = "https://localhost:5000";

		public static IDictionary<string, string> LoginParameters => null;

		public App()
		{
			InitializeComponent();

			DependencyService.Register<RabbitKommunikator>();

			SetMainPage();
		}

		public static void SetMainPage()
		{
			if (!UseMockDataStore && !Settings.IsLoggedIn)
			{
				Current.MainPage = new NavigationPage(new LoginPage())
				{
					BarBackgroundColor = (Color)Current.Resources["Primary"],
					BarTextColor = Color.White
				};
			}
			else
			{
				GoToMainPage();
			}
		}

		public static void GoToMainPage()
		{
			Current.MainPage = new TabbedPage
			{
				Children = {
					new NavigationPage(new ItemsPage())
					{
						Title = "Browse"
					},
					new NavigationPage(new AboutPage())
					{
						Title = "About"
					},
				}
			};
		}
	}
}
