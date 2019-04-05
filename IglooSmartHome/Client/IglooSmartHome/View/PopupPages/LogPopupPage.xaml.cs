using IglooSmartHome.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace IglooSmartHome.View.PopupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogPopupPage : PopupPage
    {
        public LogPopupPage(LogPopupViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}