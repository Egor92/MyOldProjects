using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeepMelody
{
	public static class Note
	{
		public const double DO = 261.63;
		public const double DO_diez = 277.18;
		public const double RE_bemol = 277.18;
		public const double RE = 293.66;
		public const double RE_diez = 311.13;
		public const double MI_bemol = 311.13;
		public const double MI = 329.63;
		public const double FA = 349.23;
		public const double FA_diez = 369.99;
		public const double SOL_bemol = 369.99;
		public const double SOL = 392.00;
		public const double SOL_diez = 415.30;
		public const double LA_bemol = 415.30;
		public const double LA = 440.00;
		public const double LA_diez = 466.16;
		public const double SI_bemol = 466.16;
		public const double SI = 493.88;

		public static void Play(double Note, int Oktava, int Duration = 300)
		{
			double StepenOktavy = Math.Pow(2.0, (double) Oktava);
			int PlayedNote = (int)(Note * StepenOktavy);
			Console.Beep(PlayedNote, Duration);
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Note.Play(Note.RE, 1);
			Note.Play(Note.MI, 1);
			Note.Play(Note.FA, 1);
			Note.Play(Note.LA, 1);
			Note.Play(Note.SOL, 1);
			Note.Play(Note.FA, 1);
			Note.Play(Note.MI, 1);
			Note.Play(Note.RE, 1);
			Note.Play(Note.DO_diez, 1);
			Note.Play(Note.RE, 1);
			Note.Play(Note.MI, 1);
			Note.Play(Note.LA, 0);
			Note.Play(Note.FA, 1);
			Note.Play(Note.MI, 1);
			Note.Play(Note.RE, 1);
		}
	}
}
