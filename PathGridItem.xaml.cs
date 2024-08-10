using System.Windows;
using System.Windows.Controls;

namespace FSSY_v3;

public partial class PathGridItem : UserControl
{
    public PathGridItem()
    {
        InitializeComponent();
        DataContext = this;
    }

    public int PathNumber
    {
        get { return (int)GetValue(PathNumberProperty); }
        set { SetValue(PathNumberProperty, value); }
    }

    public static readonly DependencyProperty PathNumberProperty =
        DependencyProperty.Register("PathNumber", typeof(int), typeof(PathGridItem), new PropertyMetadata(0));

    public string PathText
    {
        get { return (string)GetValue(PathTextProperty); }
        set { SetValue(PathTextProperty, value); }
    }

    public static readonly DependencyProperty PathTextProperty =
        DependencyProperty.Register("PathText", typeof(string), typeof(PathGridItem), new PropertyMetadata(string.Empty));

}