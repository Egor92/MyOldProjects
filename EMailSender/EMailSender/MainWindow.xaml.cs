using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Mail;

namespace EMailSender
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

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			int Count = 101;
			for (int I = 0; I < Count; I++)
			{
				MailMessage message = new MailMessage();
				message.From = new MailAddress("novikov.ea@mail.ru");
				message.Subject = "Привет";
				message.Body = "Программа EMailSender успешно отправила Сообщение №" + (I+1).ToString();
				message.To.Add("sollinka@mail.ru");

				SmtpClient client = new SmtpClient("smpt.mail.ru", 25);
				client.Credentials = new NetworkCredential("novikov.ea@mail.ru", "pupsik20082003");
				client.EnableSsl = true;

				client.Send(message);
			}
		}
	}
}
