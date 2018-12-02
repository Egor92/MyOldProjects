using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FindTheExcessImage
{
    static class GlobalSettings
    {
        #region Версии слов

        /// <summary>Получает номер локальной версии слов</summary>
        /// <param name="lang">Язык</param>
        /// <returns>Номер версии</returns>
        public static int GetLocalWordsVersion(string lang)
        {
            try
            {
                var data = ApplicationData.Current.RoamingSettings.Values["WordsVersion." + lang];
                return (int)data;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>Задает номер локальной версии слов</summary>
        /// <param name="lang">Язык</param>
        /// <param name="version">Новый номер версии</param>
        public static void SetLocalWordsVersion(string lang, int version)
        {
            ApplicationData.Current.RoamingSettings.Values["WordsVersion." + lang] = version;
        }

		/// <summary> Предпочитаемый язык </summary>
		public static string PreferredLanguage
		{
			get
			{
				if (ApplicationData.Current.RoamingSettings.Values["PreferredLanguage"] == null)
					return null;
				return (string)ApplicationData.Current.RoamingSettings.Values["PreferredLanguage"];
			}
			set { ApplicationData.Current.RoamingSettings.Values["PreferredLanguage"] = value; }
		}
		#endregion

    }
}
