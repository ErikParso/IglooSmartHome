using IglooSmartHome.IglooEventArgs;
using IglooSmartHome.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IglooSmartHome.View.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailView : MasterDetailPage
    {
        private readonly MasterDetailViewModel _viewModel;

        public MasterDetailView(MasterDetailViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        private async void MasterDetailPage_Appearing(object sender, System.EventArgs e)
            => await _viewModel.Initialize();
    }
}