using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Attributes;
using System.Windows;
using System.ComponentModel;

namespace ClassLibrary.DBClass
{
	[Entity("История соревнования", false)]
	public class HistoryOfCompetition : DBObject
	{
		[DBAttribute]
		internal id CompetitionID;
		private Competition competition;
		[EditingProperty("Соревнование")]
		public Competition Competition
		{
			get { return this.GetDBObjectPropertyValue<Competition>("Competition"); }
			set { this.SetDBObjectPropertyValue("Competition", value); }
		}

		private string year;
		[DBAttribute]
		[EditingProperty("Год")]
		public string Year
		{
			get { return this.year; }
			set { this.year = value; this.ToNotifyChanges("Year"); }
		}

		private string otherName;
		[DBAttribute]
		[EditingProperty("Другое название")]
		public string OtherName
		{
			get { return this.otherName; }
			set { this.otherName = value; this.ToNotifyChanges("OtherName"); }
		}

		[DBAttribute]
		internal id FirstPlaceID;
		private Club firstPlace;
		[EditingProperty("Первое место")]
		public Club FirstPlace
		{
			get { return this.GetDBObjectPropertyValue<Club>("FirstPlace"); }
			set 
			{
				if (value != null)
				{
					if (this.SecondPlaceID == value.ID) this.SecondPlace = null;
					if (this.ThirdPlaceID == value.ID) this.ThirdPlace = null;
				}
				this.SetDBObjectPropertyValue("FirstPlace", value); 
			}
		}

		[DBAttribute]
		internal id SecondPlaceID;
		private Club secondPlace;
		[EditingProperty("Второе место")]
		public Club SecondPlace
		{
			get { return this.GetDBObjectPropertyValue<Club>("SecondPlace"); }
			set
			{
				if (value != null)
				{
					if (this.FirstPlaceID == value.ID) this.FirstPlace = null;
					if (this.ThirdPlaceID == value.ID) this.ThirdPlace = null;
				}
				this.SetDBObjectPropertyValue("SecondPlace", value);
			}
		}

		[DBAttribute]
		internal id ThirdPlaceID;
		private Club thirdPlace;
		[EditingProperty("Третье место")]
		public Club ThirdPlace
		{
			get { return this.GetDBObjectPropertyValue<Club>("SecondPlace"); }
			set
			{
				if (value != null)
				{
					if (this.FirstPlaceID == value.ID) this.FirstPlace = null;
					if (this.SecondPlaceID == value.ID) this.SecondPlace = null;
				}
				this.SetDBObjectPropertyValue("SecondPlace", value);
			}
		}






		public HistoryOfCompetition() : base() { }

		protected override void EmptyInitialization()
		{
			base.EmptyInitialization();
		}

		public override string DisplayedText
		{
			get { return "{История соревнования}"; }
		}

		public override string GetXAMLDataTemplate()
		{
			return
			@"<StackPanel Orientation='Horizontal'>
				<TextBox Grid.Column='0' Width='55 ' Text='{Binding Path=Year}' MaxLength='8' />
				<TextBox Grid.Column='1' Width='200' Text='{Binding Path=OtherName}' />
				<dataeditor:DBObjectSelector Width='200' Property='HistoryOfCompetition.FirstPlace' />
				<dataeditor:DBObjectSelector Width='200' Property='HistoryOfCompetition.SecondPlace' />
				<dataeditor:DBObjectSelector Width='200' Property='HistoryOfCompetition.ThirdPlace' />
			</StackPanel>";
		}
	}
}
