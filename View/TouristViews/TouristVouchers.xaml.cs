using BookingApp.Model.MutualModels;
using BookingApp.Repository.MutualRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.TouristViews
{
    /// <summary>
    /// Interaction logic for TouristVouchers.xaml
    /// </summary>
    public partial class TouristVouchers : UserControl
    {
        private ObservableCollection<TourVoucher> _vouchers;
        public ObservableCollection<TourVoucher> Vouchers
        {
            get { return _vouchers; }
            set
            {
                if (_vouchers != value)
                {
                    _vouchers = value;
                    OnPropertyChanged();
                }
            }
        }

        public TourVoucherRepository tourVoucherRepository { get; set; }
        public ObservableCollection<TourVoucher> vouchers { get; set; }
        public int userId {  get; set; }
        public TouristVouchers(int userId)
        {
            InitializeComponent();
            DataContext = this;
            this.userId = userId;
            vouchers = new ObservableCollection<TourVoucher>();
            tourVoucherRepository = new TourVoucherRepository();
            Update();
        }
       
        public void Update()
        {
            foreach(TourVoucher voucher in tourVoucherRepository.GetAll())
            {
                if (voucher.TouristId == userId)
                {
                    vouchers.Add(voucher);
                }
            }
            Vouchers = new ObservableCollection<TourVoucher>(vouchers);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
