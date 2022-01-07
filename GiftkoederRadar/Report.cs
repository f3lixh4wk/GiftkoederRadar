using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;

namespace GiftkoederRadar
{
	public class Report : INotifyPropertyChanged
	{
		public static string InitialPostCode = "Postleitzahl";
		public static string InitialTown = "Ort";
		public static string InitialStreet = "Optional - Straße";
		public static string InitialBaitTitle = "z.B. Hackfleischstück mit Nägeln";
		private static Brush initialForegroundColor = Brushes.LightGray;

		public Report()
		{
			Country = Countries.First();
			PostCode = InitialPostCode;
			Town = InitialTown;
			Street = InitialStreet;
			BaitTitle = InitialBaitTitle;
			PostCodeForegroundColor = initialForegroundColor;
			TownForegroundColor = initialForegroundColor;
			StreetForegroundColor = initialForegroundColor;
			BaitTitleForegroundColor = initialForegroundColor;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				Debug.WriteLine("<<Model>> *** Feure Event *** Informiere View über Änderung der Model-Property " + propertyName);
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public int ReportId
		{
			get { return reportId; }
			set
			{
				if (value.Equals(reportId))
					return;

				reportId = value;
				OnPropertyChanged("ReportId");
			}
		}

		public string Country
		{
			get { return country; }
			set
			{
				if (value.Equals(country))
					return;

				country = value;
				OnPropertyChanged("Country");
			}
		}

		public string PostCode
		{
			get { return postCode; }
			set
			{
				if (value.Equals(postCode))
					return;

				if (value.Equals(""))
				{
					postCode = InitialPostCode;
					PostCodeForegroundColor = initialForegroundColor;
				}
				else
					postCode = value;
				OnPropertyChanged("PostCode");
			}
		}

		public string Town
		{
			get { return town; }
			set
			{
				if (value.Equals(town))
					return;

				if (value.Equals(""))
				{
					town = InitialTown;
					TownForegroundColor = initialForegroundColor;
				}					
				else
					town = value;
				OnPropertyChanged("Town");
			}
		}

		public string Street
		{
			get { return street; }
			set
			{
				if (value.Equals(street))
					return;

				if (value.Equals(""))
				{
					street = InitialStreet;
					StreetForegroundColor = initialForegroundColor;
				}
				else
					street = value;
				OnPropertyChanged("Street");
			}
		}

		public string BaitTitle
		{
			get { return baitTitle; }
			set
			{
				if (value.Equals(baitTitle))
					return;

				if (value.Equals(""))
				{
					baitTitle = InitialBaitTitle;
					BaitTitleForegroundColor = initialForegroundColor;
				}
				else
					baitTitle = value;
				OnPropertyChanged("BaitTitle");
			}
		}

		public string Description
		{
			get { return description; }
			set
			{
				if (value.Equals(description))
					return;

				description = value;
				OnPropertyChanged("Description");
			}
		}

		public Image Image
		{
			get { return image; }
			set
			{
				if (value.Equals(image))
					return;

				image = value;
				OnPropertyChanged("Image");
			}
		}

		public Brush PostCodeForegroundColor
		{
			get { return postCodeForegroundColor; }
			set
			{
				if (value.Equals(postCodeForegroundColor))
					return;

				postCodeForegroundColor = value;
				OnPropertyChanged("PostCodeForegroundColor");
			}
		}

		public Brush TownForegroundColor
		{
			get { return townForegroundColor; }
			set
			{
				if (value.Equals(townForegroundColor))
					return;

				townForegroundColor = value;
				OnPropertyChanged("TownForegroundColor");
			}
		}

		public Brush StreetForegroundColor
		{
			get { return streetForegroundColor; }
			set
			{
				if (value.Equals(streetForegroundColor))
					return;

				streetForegroundColor = value;
				OnPropertyChanged("StreetForegroundColor");
			}
		}

		public Brush BaitTitleForegroundColor
		{
			get { return baitTitleForegroundColor; }
			set
			{
				if (value.Equals(baitTitleForegroundColor))
					return;

				baitTitleForegroundColor = value;
				OnPropertyChanged("BaitTitleForegroundColor");
			}
		}

		private int reportId;
		private string country;
		private string postCode;
		private string town;
		private string street;
		private string baitTitle;
		private string description;
		private Image image;
		private Brush postCodeForegroundColor;
		private Brush townForegroundColor;
		private Brush streetForegroundColor;
		private Brush baitTitleForegroundColor;
	}

	public class Countries : ObservableCollection<string>
	{
		public Countries() : base()
		{
			Add("Deutschland");
			Add("Österreich");
			Add("Schweiz");
		}

		public static string First()
		{
			return "Deutschland";
		}
	}
}
