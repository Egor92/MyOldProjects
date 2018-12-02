using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
using Windows.Storage;
using Windows.System;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Шаблон пустого приложения задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234227

namespace FindTheExcessImage
{
    /// <summary>
    /// Обеспечивает зависящее от конкретного приложения поведение, дополняющее класс Application по умолчанию.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Инициализирует одноэлементный объект приложения.  Это первая выполняемая строка разрабатываемого
        /// кода; поэтому она является логическим эквивалентом main() или WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

			if (GlobalSettings.PreferredLanguage != null)
				ApplicationLanguages.PrimaryLanguageOverride = GlobalSettings.PreferredLanguage;
        }

        /// <summary>
        /// Вызывается при обычном запуске приложения пользователем.  Будут использоваться другие точки входа,
        /// если приложение запускается для открытия конкретного файла, отображения
        /// результатов поиска и т. д.
        /// </summary>
        /// <param name="args">Сведения о запросе и обработке запуска.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Не повторяйте инициализацию приложения, если в окне уже имеется содержимое,
            // только обеспечьте активность окна
            if (rootFrame == null)
            {
                // Создание фрейма, который станет контекстом навигации, и переход к первой странице
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Загрузить состояние за ранее приостановленного приложения
                }

                // Размещение фрейма в текущем окне
                Window.Current.Content = rootFrame;
            }

            bool loadState = (args.PreviousExecutionState == ApplicationExecutionState.Terminated);
            ExtendedSplash extendedSplash = new ExtendedSplash(args.SplashScreen, loadState);
            Window.Current.Content = extendedSplash;

			Window.Current.Activated += delegate { Unsubscribe(); Subscribe(); };
			// Обеспечение активности текущего окна
            Window.Current.Activate();
        }

        private void Subscribe()
        {
            var currentPane = SettingsPane.GetForCurrentView();
            currentPane.CommandsRequested += currentPane_CommandsRequested;
        }


        private void Unsubscribe()
        {
            var currentPane = SettingsPane.GetForCurrentView();
            currentPane.CommandsRequested -= currentPane_CommandsRequested;
        }

        void currentPane_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {

            var appCommands = args.Request.ApplicationCommands;

            var command = new SettingsCommand("Privacy Policy", "Privacy Policy",
                                              async delegate
                                                  {
                                                      await Launcher.LaunchUriAsync(
                                                          new Uri("http://trumpsoftware.heliohost.org/privacy.html"));
                                                  });
            appCommands.Add(command);
        }

        /// <summary>
        /// Вызывается при приостановке выполнения приложения. Состояние приложения сохраняется
        /// без учета информации о том, будет ли оно завершено или возобновлено с неизменным
        /// содержимым памяти.
        /// </summary>
        /// <param name="sender">Источник запроса приостановки.</param>
        /// <param name="e">Сведения о запросе приостановки.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Сохранить состояние приложения и остановить все фоновые операции
            if (Window.Current.Content is MainPage)
            {
                //await (Window.Current.Content as MainPage).SaveState();
            }

	        StatisticsManager.SaveStatisticInLocalSettings();

            Unsubscribe();
            deferral.Complete();
        }

    }
}
