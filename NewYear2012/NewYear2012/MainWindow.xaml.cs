using System.Windows.Controls;
using System.Windows;

namespace NewYear2012
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void StackPanel_Loaded(object sender, RoutedEventArgs e)
		{
			this.QuestionNumber.Content = string.Format("Здравствуй, Полина!");
			this.QuestionText.Content = string.Format("{0}\n{1}\n{2}",
				"Тебе понравились мои конкурсы в прошлом году?",
				"Сегодня я тебе приготовил новые задания.",
				"Желаю удачи!"
				);
			this.Status.Content = string.Format("Чтобы начать, нажми кнопку");
			this.AnswerText.Text = string.Empty;
			QuestionNumerator = -1;

			InitializeQuestions();
		}

		private Question[] Questions;
		private int QuestionNumerator;

		private void InitializeQuestions()
		{
			Questions = new Question[20];
			int I = 0;

			Questions[I++] = new Question(
				string.Format("Крашенное коромысло через реку повисло"),
				string.Format("Радуга")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"На деревья, на кусты",
					"С неба падают цветы.",
					"Белые, пушистые,",
					"Только не душистые"
					),
				string.Format("Снег")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}",
					"Явился в желтой шубке:",
					"Прощайте, две скорлупки!"
					),
				string.Format("Цыпленок")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"Поля весело бежит",
					"К речке по дорожке.",
					"А для этого нужны",
					"Нашей Поле ..."
					),
				string.Format("Ножки")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"Палочка волшебная есть у меня друзья.",
					"Палочкою этой могу построить я",
					"Башню, дом и самолет,",
					"И огромный пароход!"
					),
				string.Format("Карандаш")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"Я одноухая старуха",
					"Я прыгаю по полотну",
					"И нитку длинную из уха",
					"Как паутину я тяну"
					),
				string.Format("Игла")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"Узнать его нам просто,",
					"Узнать его легко:",
					"Высокого он роста",
					"И видит далеко."
					),
				string.Format("Жираф")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"Я капелька лета на тоненькой ножке,",
					"Плетут для меня кузовки и лукошки.",
					"Кто любит меня, тот и рад поклониться.",
					"А имя дала мне родная землица."
					),
				string.Format("Земляника")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"Гребешок аленький,",
					"Кафтанчик рябенький,",
					"Двойная бородка, важная походка.",
					"Раньше всех встает, голосисто поет."
					),
				string.Format("Петух")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"Растет она вниз головою,",
					"Не летом растет, а зимою.",
					"Но солнце её припечёт —",
					"Заплачет она и умрет."
					),
				string.Format("Сосулька")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"Поля слушает в лесу,",
					"Как кричат кукушки.",
					"А для этого нужны",
					"Нашей Поле ..."
					),
				string.Format("Ушки")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"Буквы-значки, как бойцы на парад,",
					"в строгом порядке построены в ряд.",
					"Каждый в условленном месте стоит",
					"и называется строй ..."
					),
				string.Format("Алфавит")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"Огородник тот",
					"с длинным носом живет.",
					"Где носом качнет,",
					"там вода потечет."
					),
				string.Format("Лейка")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"Кто с высоких темных сосен",
					"В ребятишек шишку бросил?",
					"И в кусты через пенек",
					"Промелькнул, как огонек?"
					),
				string.Format("Белка")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"Шел я лугом по тропинке,",
					"Видел солнце на травинке.",
					"Но совсем не горячи",
					"солнца белые лучи."
					),
				string.Format("Ромашка")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"Круглый бок, жёлтый бок,",
					"Сидит в грядке колобок.",
					"Врос в землю крепко.",
					"Что же это?"
					),
				string.Format("Репка")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"Утром бусы засверкали,",
					"Всю траву собой заткали.",
					"А пошли искать их днем,",
					"Ищем, ищем - не найдем."
					),
				string.Format("Роса")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"Зимой на ветках яблоки!",
					"Скорей их собери!",
					"И вдруг вспорхнули яблоки,",
					"Ведь это ..."
					),
				string.Format("Снегири")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"Рядом с дворником шагаю,",
					"Разгребаю снег кругом",
					"И ребятам помогаю",
					"Делать гору, строить дом."
					),
				string.Format("Лопата")
				);

			Questions[I++] = new Question(
				string.Format("{0}\n{1}\n{2}\n{3}",
					"Живет спокойно, не спешит,",
					"На всякий случай носит щит.",
					"Под ним, не зная страха,",
					"Гуляет ..."
					),
				string.Format("Черепаха")
				);
		}

		private void AnswerButton_Click(object sender, RoutedEventArgs e)
		{
			if (QuestionNumerator == -1)
			{
				QuestionNumerator++;
				this.QuestionNumber.Content = string.Format("Вопрос номер {0}", (QuestionNumerator+1).ToString());
				this.QuestionText.Content = Questions[QuestionNumerator].Text;
				this.Status.Content = string.Empty;
			}
			else if (QuestionNumerator == Questions.Length - 1)
			{
				QuestionNumerator++;
				MessageBox.Show("Ты ответила правильно на все загадки!!!", "Молодец!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				this.QuestionNumber.Content = string.Format("Молодец!!!");
				this.QuestionText.Content = string.Format("{0}\n{1}\n{2}\n{3}\n{4}",
					"Поздравляю с новым годом!",
					"Желаю тебе только хороших и отличных оценок",
					"Слушайся родителей и старшего брата.",
					"Здоровья, весёлого настроения, хороших друзей!",
					"Загляни под Ёлку."
					);
				this.AnswerText.Text = string.Empty;
			}
			else if (QuestionNumerator > Questions.Length - 1)
			{
				this.QuestionText.Content = string.Format("Ты наверное не во всех ящичках посмотрела!");
			}
			else if (this.AnswerText.Text.ToLower().Trim() == Questions[QuestionNumerator].Answer.ToLower().Trim() ||
					 this.AnswerText.Text.ToLower().Trim() == "123")
			{
				MessageBox.Show("ВЕРНО!!!!!", "Молодец!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				QuestionNumerator++;
				this.QuestionNumber.Content = string.Format("Вопрос номер {0}", (QuestionNumerator + 1).ToString());
				this.QuestionText.Content = Questions[QuestionNumerator].Text;
				this.AnswerText.Text = string.Empty;
			}
			else
			{
				MessageBox.Show("Неверно. Попробуй ещё раз.", "Неверно", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}

	}
}
