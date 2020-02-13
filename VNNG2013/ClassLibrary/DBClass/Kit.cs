using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Attributes;
using System.Windows.Media;
using System.Windows;

namespace ClassLibrary.DBClass
{
	public abstract class Kit : DBObject
	{
		private Color foreground;
		[DBAttribute]
		public Color Foreground
		{
			get { return this.foreground; }
			set { this.foreground = value; this.ToNotifyChanges("Foreground"); }
		}

		private Color background;
		[DBAttribute]
		public Color Background
		{
			get { return this.background; }
			set { this.background = value; this.ToNotifyChanges("Background"); }
		}

		private Color border;
		[DBAttribute]
		public Color Border
		{
			get { return this.border; }
			set { this.border = value; this.ToNotifyChanges("Border"); }
		}

		private int number;
		[DBAttribute]
		public int Number
		{
			get { return this.number; }
			set { this.number = value; this.ToNotifyChanges("Number"); }
		}


		public Kit() : base() { }

		protected override void EmptyInitialization()
		{
			base.EmptyInitialization();

			this.Foreground = Colors.Red;
			this.Background = Colors.White;
			this.Border = Colors.Black;
			this.Number = 1;
		}

		public override string GetXAMLDataTemplate()
		{
			return
			@"<Border BorderBrush='Black' BorderThickness='2' Background='Lavender' Margin='1' Padding='1'>
				<StackPanel Orientation='Horizontal' >
					<StackPanel>
						<TextBlock>
							<Run FontWeight='Bold' Foreground='Black' Text='Тип: ' />
							<Run FontWeight='Bold' Foreground='Black' >
								<Run.Text>
									<Binding Path='Number'>
										<Binding.Converter>
											<converters:IntToKitTypeConverter  />
										</Binding.Converter>
									</Binding>
								</Run.Text>
							</Run>
						</TextBlock>
						<toolkit:IntegerUpDown Width='180' Value='{Binding Path=Number}' Minimum='0' />
						<TextBlock FontWeight='Bold' Foreground='Black' Text='Цвет номера:' />
						<toolkit:ColorPicker Width='180' SelectedColor='{Binding Path=Foreground}' DisplayColorAndName='True' />
						<TextBlock FontWeight='Bold' Foreground='Black' Text='Цвет фона:' />
						<toolkit:ColorPicker Width='180' SelectedColor='{Binding Path=Background}' DisplayColorAndName='True' />
						<TextBlock FontWeight='Bold' Foreground='Black' Text='Цвет границы:' />
						<toolkit:ColorPicker Width='180' SelectedColor='{Binding Path=Border}' DisplayColorAndName='True' />
					</StackPanel>
					<Grid>
						<Ellipse Height='150' Width='150' StrokeThickness='10' >
							<Ellipse.Fill>
								<Binding Path='Background' Mode='TwoWay'>
									<Binding.Converter>
										<converters:ColorToBrushConverter  />
									</Binding.Converter>
								</Binding>
							</Ellipse.Fill>
							<Ellipse.Stroke>
								<Binding Path='Border'>
									<Binding.Converter>
										<converters:ColorToBrushConverter  />
									</Binding.Converter>
								</Binding>
							</Ellipse.Stroke>
							<Ellipse.Effect>
								<DropShadowEffect BlurRadius='10' ShadowDepth='3' Color='Black' />
							</Ellipse.Effect>
						</Ellipse>
						<TextBlock Text='99' HorizontalAlignment='Center' VerticalAlignment='Center' FontWeight='Bold' FontSize='72' >
							<TextBlock.Foreground>
								<Binding Path='Foreground'>
									<Binding.Converter>
										<converters:ColorToBrushConverter  />
									</Binding.Converter>
								</Binding>
							</TextBlock.Foreground>
						</TextBlock>
					</Grid>
				</StackPanel>
			</Border>";
		}

	}



	[Entity("Форма клуба", false)]
	public class ClubsKit : Kit
	{
		[DBAttribute]
		internal id ClubID;
		private Club club;
		public Club Club
		{
			get { return this.GetDBObjectPropertyValue<Club>("Club"); }
			set { this.SetDBObjectPropertyValue("Club", value); }
		}

		public ClubsKit() : base() { }

		public override string DisplayedText
		{
			get
			{
				return "Это Форма клуба";
			}
		}

		public static ClubsKit Default
		{
			get
			{
				return new ClubsKit()
				{
					Foreground = Colors.Red,
					Background = Colors.White,
					Border = Colors.Black
				};
			}
		}

		public static ClubsKit GKDefault
		{
			get
			{
				return new ClubsKit()
				{
					Foreground = Colors.White,
					Background = Colors.Black,
					Border = Colors.Black
				};
			}
		}
	}



	[Entity("Форма сборной", false)]
	public class NationsKit : Kit
	{
		[DBAttribute]
		internal id NationID;
		private Nation nation;
		public Nation Nation
		{
			get { return this.GetDBObjectPropertyValue<Nation>("Nation"); }
			set { this.SetDBObjectPropertyValue("Nation", value); }
		}

		public NationsKit() : base() { }

		public override string DisplayedText
		{
			get
			{
				return "Это Форма сборной";
			}
		}

		public static NationsKit Default
		{
			get
			{
				return new NationsKit()
				{
					Foreground = Colors.Red,
					Background = Colors.White,
					Border = Colors.Black
				};
			}
		}

		public static NationsKit GKDefault
		{
			get
			{
				return new NationsKit()
				{
					Foreground = Colors.White,
					Background = Colors.Black,
					Border = Colors.Black
				};
			}
		}
	}



}
