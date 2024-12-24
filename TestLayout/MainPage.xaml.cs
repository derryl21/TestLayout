using System.Diagnostics;

namespace TestLayout
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private DataTemplate? _activeTemplate;
        bool _isMapView = true;

        public MainPage()
        {
            InitializeComponent();
            SizeChanged += MainPage_SizeChanged;
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object? sender, EventArgs e)
        {
            //VisualStateManager.GoToState(this, "DesktopState");
            UpdateLayout();
        }

        private void UpdateLayout()
        {

            if (!_isMapView)
                return;

            bool isLandscape = Width > Height;
            bool isTablet = DeviceInfo.Idiom == DeviceIdiom.Tablet;

            DataTemplate? template = isTablet 
                ? (DataTemplate)Resources["DesktopTemplate"] 
                : isLandscape ? (DataTemplate)Resources["DesktopTemplate"] 
                : (DataTemplate)Resources["PhoneTemplate"];
            //this.ControlTemplate = template;

            if (template is not null && template != _activeTemplate) 
            {
                MainContentPresenter.Content = (View)template.CreateContent();
                _activeTemplate = template;
                Debug.WriteLine("Changed Template");
            }
        }

        private void MainPage_SizeChanged(object? sender, EventArgs e)
        {
            UpdateLayout();

            //if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            //{
            //    VisualStateManager.GoToState(this, "PhoneState");
            //    return;
            //}

            //VisualStateManager.GoToState(this, "DesktopState");
            ////switch (DeviceInfo.Idiom)
            //{
            //    case DeviceIdiom.Phone :
            //        VisualStateManager.GoToState(this, "PhoneState");
            //        break;
            //    default:
            //        VisualStateManager.GoToState(this, "DesktopState");
            //        break;
            //}
        }

        private void mapButton_Clicked(object sender, EventArgs e)
        {
            _isMapView = true;
            UpdateLayout();
        }

        private void cardButton_Clicked(object sender, EventArgs e)
        {
            _isMapView = false;
            DataTemplate? template = (DataTemplate)Resources["CardViewTemplate"];
            //this.ControlTemplate = template;

            if (template is not null && template != _activeTemplate)
            {
                MainContentPresenter.Content = (View)template.CreateContent();
                _activeTemplate = template;
                Debug.WriteLine("Changed Template");
            }
        }
    }

}
