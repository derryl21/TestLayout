namespace TestLayout
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

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
            bool isLandscape = Width > Height;

            var template = isLandscape ? (DataTemplate)Resources["DesktopTemplate"] 
                : (DataTemplate)Resources["PhoneTemplate"];
            //this.ControlTemplate = template;

            if (template is not null) 
            {
                MainContentPresenter.Content = (View)template.CreateContent();
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

    }

}
