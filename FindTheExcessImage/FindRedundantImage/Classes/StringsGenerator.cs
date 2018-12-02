using Windows.ApplicationModel.Resources;

namespace FindTheExcessImage 
{
    public static class Strings 
    {
        private static readonly ResourceLoader Loader = new ResourceLoader();

    	///<summary>Resource string: Find the excess image</summary>
		public static string AppName { get { return Loader.GetString("AppName"); } } 
    	///<summary>Resource string: Find the excess image</summary>
		public static string AppTitleText { get { return Loader.GetString("AppTitle/Text"); } } 
    	///<summary>Resource string: Hint</summary>
		public static string buttonHint { get { return Loader.GetString("buttonHint/[using:Windows/UI/Xaml/Automation]AutomationProperties/Name"); } } 
    	///<summary>Resource string: Language</summary>
		public static string buttonLanguage { get { return Loader.GetString("buttonLanguage/[using:Windows/UI/Xaml/Automation]AutomationProperties/Name"); } } 
    	///<summary>Resource string: Less</summary>
		public static string buttonLess { get { return Loader.GetString("buttonLess/[using:Windows/UI/Xaml/Automation]AutomationProperties/Name"); } } 
    	///<summary>Resource string: More</summary>
		public static string buttonMore { get { return Loader.GetString("buttonMore/[using:Windows/UI/Xaml/Automation]AutomationProperties/Name"); } } 
    	///<summary>Resource string: Select language</summary>
		public static string dialogSelectLanguageTitle { get { return Loader.GetString("dialogSelectLanguage/Title"); } } 
    	///<summary>Resource string: Brilliantly!</summary>
		public static string textBrilliantly { get { return Loader.GetString("textBrilliantly"); } } 
    	///<summary>Resource string: The application uses the internet. At this moment connection is absent.</summary>
		public static string textDescriptionNoInternetText { get { return Loader.GetString("textDescriptionNoInternet/Text"); } } 
    	///<summary>Resource string: Excellent!</summary>
		public static string textExcellent { get { return Loader.GetString("textExcellent"); } } 
    	///<summary>Resource string: A good result</summary>
		public static string textGoodResult { get { return Loader.GetString("textGoodResult"); } } 
    	///<summary>Resource string: Hint</summary>
		public static string textHint { get { return Loader.GetString("textHint"); } } 
    	///<summary>Resource string: All of images except the one related by the following word</summary>
		public static string textHintText { get { return Loader.GetString("textHintText"); } } 
    	///<summary>Resource string: Keep it up!</summary>
		public static string textKeepItUp { get { return Loader.GetString("textKeepItUp"); } } 
    	///<summary>Resource string: No internet connection</summary>
		public static string textNoInternetConnectionText { get { return Loader.GetString("textNoInternetConnection/Text"); } } 
    	///<summary>Resource string: OK!</summary>
		public static string textNormal { get { return Loader.GetString("textNormal"); } } 
    	///<summary>Resource string: of</summary>
		public static string textOfText { get { return Loader.GetString("textOf/Text"); } } 
    	///<summary>Resource string:  pcs.</summary>
		public static string textPcsText { get { return Loader.GetString("textPcs/Text"); } } 
    	///<summary>Resource string: Be careful! Try again.</summary>
		public static string textPoor { get { return Loader.GetString("textPoor"); } } 
    	///<summary>Resource string: Right</summary>
		public static string textRightText { get { return Loader.GetString("textRight/Text"); } } 
    	///<summary>Resource string: Guessed {0}% ({1} of {2})</summary>
		public static string textStats { get { return Loader.GetString("textStats"); } } 
    	///<summary>Resource string: Try again</summary>
		public static string textTryAgain { get { return Loader.GetString("textTryAgain"); } } 
    	///<summary>Resource string: Try again</summary>
		public static string textTryAgainText { get { return Loader.GetString("textTryAgain/Text"); } } 
    	///<summary>Resource string: Well done!</summary>
		public static string textWellDone { get { return Loader.GetString("textWellDone"); } } 
    	///<summary>Resource string: You're the master!</summary>
		public static string textYouAreMaster { get { return Loader.GetString("textYouAreMaster"); } } 
    }
}