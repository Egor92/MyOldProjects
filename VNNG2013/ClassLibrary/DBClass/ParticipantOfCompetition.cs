using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Attributes;
using System.Windows;

namespace ClassLibrary.DBClass
{
	public abstract class ParticipantOfCompetition : DBObject { }



	[Entity(typeof(Club), typeof(Competition))]
	public class CompetitionClub : ParticipantOfCompetition
	{
		[DBAttribute]
		internal id ClubID;
		private Club club;
		public Club Club
		{
			get { return this.GetDBObjectPropertyValue<Club>("Club"); }
			set { this.SetDBObjectPropertyValue("Club", value); }
		}

		[DBAttribute]
		internal id CompetitionID;
		private Competition competition;
		public Competition Competition
		{
			get { return this.GetDBObjectPropertyValue<Competition>("Competition"); }
			set { this.SetDBObjectPropertyValue("Competition", value); }
		}

		public CompetitionClub() : base() { }

		public override string DisplayedText
		{
			get { return string.Format("[Клуб={0},Соревнование={1}]", this.Club.ShortName, this.Competition.Name); }
		}

	}

}
