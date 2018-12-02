using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewYear2012
{
	class Question
	{
		private string text;

		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		private string answer;

		public string Answer
		{
			get { return answer; }
			set { answer = value; }
		}

		public Question(string Text, string Answer)
		{
			this.Text = Text;
			this.Answer = Answer;
		}
	}

}
