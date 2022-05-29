using miniTC.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace miniTC.View.UserControls
{
    /// <summary>
    /// Interaction logic for PanelTC.xaml
    /// </summary>
    public partial class PanelTC : UserControl
    {
        #region Constructor
        public PanelTC()
        {
            InitializeComponent();
        }
        #endregion

        #region Dependencies

        public static readonly DependencyProperty TBTextProperty = DependencyProperty.Register(
            "TBText",
            typeof(string),
            typeof(PanelTC),
            new FrameworkPropertyMetadata(null)
            );

        public static readonly DependencyProperty CBItemsSourceProperty = DependencyProperty.Register(
            "CBItemsSource",
            typeof(ObservableCollection<string>),
            typeof(PanelTC),
            new FrameworkPropertyMetadata(null)
            );

        public static readonly DependencyProperty CBSelectedItemProperty = DependencyProperty.Register(
            "CBSelectedItem",
            typeof(string),
            typeof(PanelTC),
            new FrameworkPropertyMetadata(null)
            );

        public static readonly DependencyProperty LBItemsSourceProperty = DependencyProperty.Register(
            "LBItemsSource",
            typeof(ObservableCollection<IElement>),
            typeof(PanelTC),
            new FrameworkPropertyMetadata(null)
            );

        public static readonly DependencyProperty LBSelectedItemProperty = DependencyProperty.Register(
            "LBSelectedItem",
            typeof(IElement),
            typeof(PanelTC),
            new FrameworkPropertyMetadata(null)
            );

        #endregion

        #region Properties

        public string TBText
        {
            get { return (string)GetValue(TBTextProperty); }
            set { SetValue(TBTextProperty, value); }
        }

        public ObservableCollection<string> CBItemsSource
        {
            get { return (ObservableCollection<string>)GetValue(CBItemsSourceProperty); }
            set { SetValue(CBItemsSourceProperty, value); }
        }

        public string CBSelectedItem
        {
            get { return (string)GetValue(CBSelectedItemProperty); }
            set { SetValue(CBSelectedItemProperty, value); }
        }

        public ObservableCollection<IElement> LBItemsSource
        {
            get { return (ObservableCollection<IElement>)GetValue(LBItemsSourceProperty); }
            set { SetValue(LBItemsSourceProperty, value); }
        }

        public IElement LBSelectedItem
        {
            get { return (IElement)GetValue(LBSelectedItemProperty); }
            set { SetValue(LBSelectedItemProperty, value); }
        }

        #endregion

        #region Events

        public static readonly RoutedEvent DriveChangedEvent = EventManager.RegisterRoutedEvent(
            "DriveSelected",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(PanelTC)
            );

        public event RoutedEventHandler DriveChanged
        {
            add { AddHandler(DriveChangedEvent, value); }
            remove { RemoveHandler(DriveChangedEvent, value); }
        }

        void RaiseDriveChanged(object sender, SelectionChangedEventArgs e)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(DriveChangedEvent);
            RaiseEvent(newEventArgs);
        }

        public static readonly RoutedEvent ElementChangedEvent = EventManager.RegisterRoutedEvent(
            "ElementSelected",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(PanelTC)
            );

        public event RoutedEventHandler ElementChanged
        {
            add { AddHandler(ElementChangedEvent, value); }
            remove { RemoveHandler(ElementChangedEvent, value); }
        }

        void RaiseElementChanged(object sender, SelectionChangedEventArgs e)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ElementChangedEvent);
            RaiseEvent(newEventArgs);
        }

        #endregion
    }
}
