using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Attributes;
using System.Windows;

namespace ClassLibrary.DBClass
{
	[Entity("Футбольная организация", true)]
	[ItemsCollection("Информация")]
	public class FootballOrganization : DBObject
	{
		private string name;
		[DBAttribute]
		[EditingProperty("Название")]
		public string Name
		{
			get { return this.name; }
			set { this.name = value; this.ToNotifyChanges("Name"); }
		}

		[EditingProperty("Нации")]
		public Nation[] Nations
		{
			get { return this.GetBindedArrayPropertyValue<Nation>("Nations"); }
			set { this.SetBindedArrayPropertyValue("Nations", value); }
		}

		private Competition theHiestChampionship;
		[EditingProperty("Высший чемпионат")]
		[Condition(MemberType.Method, "IsTheHiestNationalChampionship")]
		public Competition TheHiestChampionship
		{
			get { return this.GetBindedPropertyValue<Competition>("TheHiestChampionship", null); }
			set { this.SetBindedPropertyValue("TheHiestChampionship", value); }
		}

		private Competition cup;
		[EditingProperty("Кубок", "Информация")]
		[Condition(MemberType.Method, "IsNationalCup")]
		public Competition Cup
		{
			get { return this.GetBindedPropertyValue<Competition>("Cup", "FootballOrganization"); }
			set { this.SetBindedPropertyValue("Cup", value); }
		}

		private Competition superCup;
		[EditingProperty("Супер-кубок", "Информация")]
		[Condition(MemberType.Method, "IsNationalSuperCup")]
		public Competition SuperCup
		{
			get { return this.GetBindedPropertyValue<Competition>("SuperCup", "FootballOrganization"); }
			set { this.SetBindedPropertyValue("SuperCup", value); }
		}
		








		public FootballOrganization() : base() { }

		protected override void EmptyInitialization()
		{
			this.Name = "Новая ФО";
		}

		public override string DisplayedText
		{
			get
			{
				StringBuilder Result = new StringBuilder();
				Result.Append(this.Name);
				Result.Append(" (");
				foreach (Nation nation in this.Nations)
					Result.Append(string.Format("{0}, ", nation.Name));
				if (this.Nations.Length > 0)
					Result.Remove(Result.Length - 2, 2);
				else
					Result.Append('?');
				Result.Append(")");
				return Result.ToString();
			}
		}
	}
}
