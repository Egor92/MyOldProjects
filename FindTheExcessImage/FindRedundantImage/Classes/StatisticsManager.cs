using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FindTheExcessImage
{
	/// <summary>
	/// Класс, позволяющий управлять информацией о статистике игры
	/// </summary>
	static class StatisticsManager
	{
		private static readonly ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;

		/// <summary>
		/// Счётчик количества сыгранных игр
		/// </summary>
		private static int _count;

		/// <summary>
		/// Нижний предел количества игр для отсылки статистики на сервер
		/// </summary>
		private const int LowSendingLimit = 5;

		static StatisticsManager()
		{
			var count = LocalSettings.Values["Count"];
			if (count != null)
				_count = (int) count;
		}

		/// <summary> Сбросить счётчик </summary>
		public static void ResetGamesCount()
		{
			_count = 0;
		}

		/// <summary> Увеличить счётчик </summary>
		public static void IncrementGamesCount()
		{
			_count++;
		}

		/// <summary> Отправить статистику на сервер </summary>
		public static async void SendStatisticsToServer()
		{
			if (_count < LowSendingLimit)
				return;

			try
			{
				var client = new HttpClient();

				var postArguments = new Dictionary<string, string>
					{
						{"Count", _count.ToString()}
					};
				await client.PostAsync("http://trumpsoftware.heliohost.org/findexcessimage/savestats.php",
					                 new FormUrlEncodedContent(postArguments));
			}
			finally
			{
				ResetGamesCount();
			}
		}

		/// <summary> Сохраняет статистику на локальном диске </summary>
		public static void SaveStatisticInLocalSettings()
		{
			LocalSettings.Values["Count"] = _count;
		}

	}
}
