using MyShop.Models;
using MyShopCommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MyShop.ViewModels
{
	public class ShippingAddressViewModel : NotifyModel
	{
		public INavigation Navigation { get; set; }
		public ShippingAddressViewModel(INavigation navigation)
		{
			Navigation = navigation;
			PopulateCountry();
			PopulateState();
		}

		private void PopulateState()
		{
			States = new List<State>();
			States.Add(new State { Id = 1, Name = "Test State" });
		}

		private void PopulateCountry()
		{
			Countries = new List<Country>();
			Countries.Add(new Country { Id = 1, Name = "USA" });
		}

		public Command SaveShippingAddress
		{
			get
			{
				return new Command(async () =>
				{
					if (CheckValidations())
					{
						ShippingAddress addresess = new ShippingAddress();
						addresess.FullName = FullName;
						addresess.PinCode = PinCode.ToString();
						addresess.StateId = SelectedState.Id;
						addresess.StreetNo = StreetNo;
						addresess.CityName = CityName;
						addresess.MobileNumber = MobileNumber;
						addresess.HouseNo = HouseNo;
						addresess.Landmark = Landmark;
						addresess.Area = Area;
						addresess.CountryId = SelectedCountry.Id;
						addresess.UserId = GlobalVariables.user_id;

						var response = await new GlobalFunctions().SaveShippingAddress(addresess);
						if (response.IsValid)
						{
							
							// success
							await Application.Current.MainPage.DisplayAlert("Message", "Address Saved Successfully", "OK");
						await	Navigation.PopAsync();
						}
						else
						{
							await Application.Current.MainPage.DisplayAlert("Message", "Address Failed to save", "OK");
						}
					}
				});
			}
		}
		private bool CheckValidations()
		{
			if (string.IsNullOrWhiteSpace(FullName))
			{
				Application.Current.MainPage.DisplayAlert("Message", "Please enter FullName", "Cancel");
				return false;
			}
			else if (PinCode <= 0)
			{
				Application.Current.MainPage.DisplayAlert("Message", "Please enter PinCode", "Cancel");
				return false;
			}
			else if (SelectedState == null)
			{
				Application.Current.MainPage.DisplayAlert("Message", "Please select state", "Cancel");
				return false;
			}

			else if (string.IsNullOrWhiteSpace(StreetNo))
			{
				Application.Current.MainPage.DisplayAlert("Message", "Please enter StreetNo", "Cancel");
				return false;
			}
			else if (string.IsNullOrEmpty(CityName))
			{
				Application.Current.MainPage.DisplayAlert("Message", "Please enter city", "Cancel");
				return false;
			}
			else if (SelectedCountry == null)
			{
				Application.Current.MainPage.DisplayAlert("Message", "Please enter StreetNo", "Cancel");
				return false;
			}

			else
			{
				return true;
			}
		}

		private string _FullName;

		public string FullName
		{
			get { return _FullName; }
			set { _FullName = value; OnPropertyChanged(); }
		}
		private string _mobileNumber;

		public string MobileNumber
		{
			get { return _mobileNumber; }
			set { _mobileNumber = value; OnPropertyChanged(); }
		}
		private int _pinCode;

		public int PinCode
		{
			get { return _pinCode; }
			set { _pinCode = value; OnPropertyChanged(); }
		}

		private string _houseNo;

		public string HouseNo
		{
			get { return _houseNo; }
			set { _houseNo = value; OnPropertyChanged(); }
		}
		private string _streetNo;

		public string StreetNo
		{
			get { return _streetNo; }
			set { _streetNo = value; OnPropertyChanged(); }
		}
		private string _area;

		public string Area
		{
			get { return _area; }
			set { _area = value; OnPropertyChanged(); }
		}
		private string _landmark;

		public string Landmark
		{
			get { return _landmark; }
			set { _landmark = value; OnPropertyChanged(); }
		}
		private List<State> _states;
		public List<State> States
		{
			get { return _states; }
			set { _states = value; OnPropertyChanged(); }
		}
		private List<City> _cities;
		public List<City> Cities
		{
			get { return _cities; }
			set { _cities = value; OnPropertyChanged(); }
		}
		private List<Country> _countries;
		public List<Country> Countries
		{
			get { return _countries; }
			set { _countries = value; OnPropertyChanged(); }
		}

		private State _selectedstate;

		public State SelectedState
		{
			get { return _selectedstate; }
			set { _selectedstate = value; OnPropertyChanged(); }
		}

		private Country _selectedCountry;

		public Country SelectedCountry
		{
			get { return _selectedCountry; }
			set { _selectedCountry = value; OnPropertyChanged(); }
		}
		private string _cityName;

		public string CityName
		{
			get { return _cityName; }
			set { _cityName = value; OnPropertyChanged(); }
		}
	}
}
