using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.Globalization;

// Документацию по шаблону элемента "Основная страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=234237

namespace FindTheExcessImage
{
    /// <summary>
    /// Основная страница, которая обеспечивает характеристики, являющимися общими для большинства приложений.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }



        public async Task OnLoaded(bool loadState, bool reloadImages)
        {
            if (!(await ImageProvider.HasConnection(new Word("word", "word"))))
            {
                ShowNoInternetMessage(null);
                return;
            }

            HintButton.SetValue(AutomationProperties.NameProperty, string.Format("{0} ({1})", Strings.textHint, Game.HintsLeft));

            await Game.InitializeFirstTurn();

			if (reloadImages)
				SelectImage_Tapped(null, null);

            Game.PreloadImages();
        }


        private void ShowNoInternetMessage(object sender)
        {
            Window.Current.Content = new NoConnectionPage(this, sender);
        }

        public async void SelectImage_Tapped(object sender, RoutedEventArgs e)
        {
            try
            {
				ProgressBar.Visibility = Visibility.Visible;
				ImagesItemsControl.IsEnabled = false;

				if (sender != null)
	            {
		            var selectedImage = ((Image) sender).DataContext as Picture;
		            await Game.DoTurn(selectedImage);
	            }

				if (Game.CurrentTurnNumber == Game.TurnsCount)
				{
					ShowFinalDialog();
				}
				else
				{
					CurrentTurnRun.Text = (Game.CurrentTurnNumber+1).ToString();
					TotalImagesCountRun.Text = Game.TurnsCount.ToString();

					GuessedImagesProcentsRun.Text = Game.CurrentTurnNumber == 0
						? 0.ToString()
						: ((int)(((double)Game.GuessedImagesCount / Game.CurrentTurnNumber) * 100)).ToString();
					ImagesItemsControl.ItemsSource = Game.CurrentTurn.DisplayedImages;
					if (Game.HintsLeft > 0) 
                        HintButton.IsEnabled = true;
				}
            }
            catch (Exception ex)
            {
                ShowNoInternetMessage(sender);
            }
            finally
            {
                StatisticsManager.IncrementGamesCount();
                ProgressBar.Visibility = Visibility.Collapsed;
                ImagesItemsControl.IsEnabled = true;
            }
        }

        private void ShowFinalDialog()
        {
            ProgressBar.Visibility = Visibility.Visible;
            ImagesItemsControl.IsEnabled = false;

			var guessedImagesProcents = ((double)Game.GuessedImagesCount / Game.TurnsCount) * 100;
            string text;
            if (guessedImagesProcents > 99)
                text = Strings.textYouAreMaster;
            else if (guessedImagesProcents > 94)
                text = Strings.textBrilliantly;
            else if (guessedImagesProcents > 89)
                text = Strings.textExcellent;
            else if (guessedImagesProcents > 85)
                text = Strings.textWellDone;
            else if (guessedImagesProcents > 75)
                text = Strings.textGoodResult;
            else if (guessedImagesProcents > 65)
                text = Strings.textKeepItUp;
            else if (guessedImagesProcents > 50)
                text = Strings.textNormal;
            else
                text = Strings.textPoor;
            
            var dlg = new MessageDialog(
				string.Format(Strings.textStats, (int)guessedImagesProcents, Game.GuessedImagesCount, Game.TurnsCount),
                text);
            dlg.Commands.Add(new UICommand(Strings.textTryAgain, async delegate
                {
                    var mainPage = new MainPage();
                    await mainPage.OnLoaded(false, true);
                    Window.Current.Content = mainPage;
                }));
            dlg.ShowAsync();
        }



        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
	        Game.ImagesCount--;
			ImagesCountTextBlock.Text = Game.ImagesCount.ToString();
			if (Game.ImagesCount <= 3) MinusButton.IsEnabled = false;
            PlusButton.IsEnabled = true;
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
			Game.ImagesCount++;
			ImagesCountTextBlock.Text = Game.ImagesCount.ToString();
			if (Game.ImagesCount >= 12) PlusButton.IsEnabled = false;
            MinusButton.IsEnabled = true;
        }

        private void HintButton_Click(object sender, RoutedEventArgs e)
        {
			HintButton.IsEnabled = false;
	        var word = Game.GetHint();
            if (word == null)
	            return;
			new MessageDialog(string.Format("{0}: \"{1}\"", Strings.textHintText, word.Translated),
                              Strings.textHint).ShowAsync();
            HintButton.SetValue(AutomationProperties.NameProperty, string.Format("{0} ({1})", Strings.textHint,  Game.HintsLeft));
        }

        private void Image_ImageOpened(object sender, RoutedEventArgs e)
        {
			((Picture) ((Image)sender).DataContext).LoadingRingVisibility = Visibility.Collapsed;
        }

	    private void LanguageSelectionItemsControl_OnLoaded(object sender, RoutedEventArgs e)
	    {
			LanguageSelectionItemsControl.ItemsSource = ApplicationLanguages.ManifestLanguages.Select(x => new Language(x)).OrderBy(x => x.LanguageTag);
		}

	    private async void SelectedLanguageButton_OnClick(object sender, RoutedEventArgs e)
	    {
			LanguageSelectionDialog.IsOpen = false;

			var button = sender as Button;
		    if (button == null) 
				return;
		    var language = button.DataContext as Language;
		    if (language == null) 
				return;
			if (language.LanguageTag == ApplicationLanguages.PrimaryLanguageOverride)
			    return;

			ImagesItemsControl.IsEnabled = false;
		    ProgressBar.Visibility = Visibility.Visible;

		    GlobalSettings.PreferredLanguage = language.LanguageTag;
		    ApplicationLanguages.PrimaryLanguageOverride = language.LanguageTag;

		    //await SaveState();
		    var newMainPage = new MainPage();
			await newMainPage.OnLoaded(true, false);
			Window.Current.Content = newMainPage;
	    }

	    private void ChangeLanguageButton_OnClick(object sender, RoutedEventArgs e)
	    {
	        if (BottomAppBar != null) 
                BottomAppBar.IsOpen = false;
	        LanguageSelectionDialog.IsOpen = true;
	    }
    }
}
