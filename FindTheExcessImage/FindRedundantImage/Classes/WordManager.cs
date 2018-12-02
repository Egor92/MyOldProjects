using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Globalization;
using Windows.Storage;
using PortableClassLibrary;

namespace FindTheExcessImage
{
    static class WordsManager
    {
        private static readonly List<Word> Words = new List<Word>();

        public static IEnumerable<Word> GetRandomWords(int count)
        {
            return Words.Mix().Take(count);
        }

        public static async Task LoadWords()
        {
            string lang =
                ApplicationLanguages.Languages.Select(x => x.Substring(0, 2))
                    .First(x => ApplicationLanguages.ManifestLanguages.Contains(x));

            bool isSuccess = false;
            try
            {
                int serverVersion = await GetServerWordsVersion();
                if (serverVersion > GlobalSettings.GetLocalWordsVersion(lang)) // Если на сервере новее
                {
                    string words = await new HttpClient().GetStringAsync(SpecialPaths.GetWordsFilePath(lang));
                    LoadWordsFromText(words);
                    await SaveWordsToFile(lang, words);
                    GlobalSettings.SetLocalWordsVersion(lang, serverVersion);
                    isSuccess = true;
                }
            }
            catch
            {
            }

            if (!isSuccess)
            {
                string words = await GetLocalWords(lang);

                LoadWordsFromText(words);
            }

        }

        private static void LoadWordsFromText(string text)
        {
            foreach (var word in text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                Words.Add(new Word(word.Split(';')[0], word.Split(';')[1]));
            }
        }

        private static async Task SaveWordsToFile(string lang, string text)
        {
            var file =
                await
                    ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(string.Format("{0}.words.txt", lang),
                        CreationCollisionOption.ReplaceExisting);
            byte[] buffer = new UTF8Encoding().GetBytes(text);
            await file.WriteAsync(buffer, 0, buffer.Length);
            file.Flush();
            file.Dispose();
        }

        private static async Task<int> GetServerWordsVersion()
        {
            try
            {
                return int.Parse(await new HttpClient().GetStringAsync(SpecialPaths.VersionFilePath));
            }
            catch
            {
                return 0;
            }
        }


        private static async Task<string> GetLocalWords(string lang)
        {
            bool isSuccess = true;
            try
            {
                return
                    await
                        FileIO.ReadTextAsync(
                            await ApplicationData.Current.LocalFolder.GetFileAsync(string.Format("{0}.words.txt", lang)));
            }
            catch
            {
                isSuccess = false;
            }

            if (!isSuccess)
            {
                return await FileIO.ReadTextAsync(await StorageFile.GetFileFromPathAsync(
                    Path.Combine(Package.Current.InstalledLocation.Path,
                        @"Resources\" + lang + @"\" + lang + ".words.txt")));
            }

            return "";
        }

    }
}
