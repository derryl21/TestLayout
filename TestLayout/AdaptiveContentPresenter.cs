using System.Diagnostics;

namespace TestLayout;

public class AdaptiveContentPresenter : ContentPresenter
{

    public static readonly BindableProperty PhoneLayoutProperty = BindableProperty.Create(nameof(PhoneLayoutProperty), typeof(DataTemplate), typeof(AdaptiveContentPresenter));

    public static readonly BindableProperty DesktopLayoutProperty = BindableProperty.Create(nameof(DesktopLayoutTemplate), typeof(DataTemplate), typeof(AdaptiveContentPresenter));

    public static readonly BindableProperty DefaultLayoutProperty = BindableProperty.Create(nameof(DefaultLayoutTemplate), typeof(DataTemplate), typeof(AdaptiveContentPresenter));

    public static readonly BindableProperty UseDefaultLayoutOnlyProperty = BindableProperty.Create(nameof(UseDefaultLayoutOnly), typeof(bool), typeof(AdaptiveContentPresenter), false, propertyChanged: (bind, oldVal, newVal) =>
    {
        var adaptiveContentPresenter = (AdaptiveContentPresenter)bind;
        adaptiveContentPresenter.UpdateTemplate();

    });

    private DataTemplate _activeTemplate = new DataTemplate();

    public DataTemplate PhoneLayoutTemplate
    {
        get => GetValue(PhoneLayoutProperty) is DataTemplate dataTemplate ? dataTemplate : new();
        set => SetValue(PhoneLayoutProperty, value);
    }

    public DataTemplate DesktopLayoutTemplate
    {
        get => GetValue(DesktopLayoutProperty) is DataTemplate dataTemplate ? dataTemplate : new();
        set => SetValue(DesktopLayoutProperty, value);
    }

    public DataTemplate DefaultLayoutTemplate
    {
        get => GetValue(DefaultLayoutProperty) is DataTemplate dataTemplate ? dataTemplate : new();
        set => SetValue(DefaultLayoutProperty, value);
    }

    public bool UseDefaultLayoutOnly
    {
        get => GetValue(UseDefaultLayoutOnlyProperty) is bool useDefaultLayoutOnly && useDefaultLayoutOnly;
        set => SetValue(UseDefaultLayoutOnlyProperty, value);
    }

    public AdaptiveContentPresenter()
    {
        SizeChanged -= AdaptiveContentPresenter_SizeChanged;
        SizeChanged += AdaptiveContentPresenter_SizeChanged;
    }

    private void AdaptiveContentPresenter_SizeChanged(object? sender, EventArgs e)
    {
        UpdateTemplate();
    }

    private void UpdateTemplate()
    {
        if (UseDefaultLayoutOnly)
        {
            UpdateTemplate(DefaultLayoutTemplate);
            return;
        }
        
        //Adapt layout based on the orientation
        bool isLandscape = Width > Height;
        bool isTablet = DeviceInfo.Idiom == DeviceIdiom.Tablet;

        DataTemplate? template = isTablet
            ? DesktopLayoutTemplate
            : isLandscape ? DesktopLayoutTemplate
            : PhoneLayoutTemplate;

        UpdateTemplate(template);
    }

    private void UpdateTemplate(DataTemplate? template)
    {
        if (template is not null && template != _activeTemplate)
        {
            this.Content = (View)template.CreateContent();
            _activeTemplate = template;
            Debug.WriteLine("Changed Template");
        }
    }
}
