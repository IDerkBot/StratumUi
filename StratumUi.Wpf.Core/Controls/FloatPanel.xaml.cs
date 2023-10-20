using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StratumUi.Wpf.Core.Controls.Enums;

namespace StratumUi.Wpf.Core.Controls;

public partial class FloatPanel : UserControl
{
    #region Properties

    #region Header - object

    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header), typeof(object), typeof(FloatPanel), new PropertyMetadata(default(object)));

    public object Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    #endregion Header

    #region Content - object

    public new static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
        nameof(Content), typeof(object), typeof(FloatPanel), new PropertyMetadata(default(object)));

    public new object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    #endregion Content

    #region HeaderAlignment - HorizontalAlignment

    public static readonly DependencyProperty HeaderAlignmentProperty = DependencyProperty.Register(
        nameof(HeaderAlignment), typeof(HorizontalAlignment), typeof(FloatPanel), new PropertyMetadata(HorizontalAlignment.Left));

    public HorizontalAlignment HeaderAlignment
    {
        get => (HorizontalAlignment)GetValue(HeaderAlignmentProperty);
        set => SetValue(HeaderAlignmentProperty, value);
    }

    #endregion HeaderAlignment

    #region Position - FloatPosition

    public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
        nameof(Position), typeof(FloatPosition), typeof(FloatPanel), new PropertyMetadata(default(FloatPosition)));

    public FloatPosition Position
    {
        get => (FloatPosition)GetValue(PositionProperty);
        set => SetValue(PositionProperty, value);
    }

    #endregion Position

    #region ContentWidth - double

    public static readonly DependencyProperty ContentWidthProperty = DependencyProperty.Register(
        nameof(ContentWidth), typeof(double), typeof(FloatPanel), new PropertyMetadata(1000.0));

    public double ContentWidth
    {
        get => (double)GetValue(ContentWidthProperty);
        set => SetValue(ContentWidthProperty, value);
    }

    #endregion ContentWidth

    #region ContentHeight - double

    public static readonly DependencyProperty ContentHeightProperty = DependencyProperty.Register(
        nameof(ContentHeight), typeof(double), typeof(FloatPanel), new PropertyMetadata(1000.0));

    public double ContentHeight
    {
        get => (double)GetValue(ContentHeightProperty);
        set => SetValue(ContentHeightProperty, value);
    }

    #endregion ContentHeight

    #region OffsetTop - double

    public static readonly DependencyProperty OffsetTopProperty = DependencyProperty.Register(
        nameof(OffsetTop), typeof(double), typeof(FloatPanel), new PropertyMetadata(default(double)));

    public double OffsetTop
    {
        get => (double)GetValue(OffsetTopProperty);
        set => SetValue(OffsetTopProperty, value);
    }

    #endregion OffsetTop

    #region OffsetBottom - double

    public static readonly DependencyProperty OffsetBottomProperty = DependencyProperty.Register(
        nameof(OffsetBottom), typeof(double), typeof(FloatPanel), new PropertyMetadata(default(double)));

    public double OffsetBottom
    {
        get => (double)GetValue(OffsetBottomProperty);
        set => SetValue(OffsetBottomProperty, value);
    }

    #endregion OffsetBottom

    #region OffsetLeft - double

    public static readonly DependencyProperty OffsetLeftProperty = DependencyProperty.Register(
        nameof(OffsetLeft), typeof(double), typeof(FloatPanel), new PropertyMetadata(default(double)));

    public double OffsetLeft
    {
        get => (double)GetValue(OffsetLeftProperty);
        set => SetValue(OffsetLeftProperty, value);
    }

    #endregion OffsetLeft

    #region OffsetRight - double

    public static readonly DependencyProperty OffsetRightProperty = DependencyProperty.Register(
        nameof(OffsetRight), typeof(double), typeof(FloatPanel), new PropertyMetadata(default(double)));

    public double OffsetRight
    {
        get => (double)GetValue(OffsetRightProperty);
        set => SetValue(OffsetRightProperty, value);
    }

    #endregion OffsetRight

    #endregion Properties

    private const int InsideMarginValue = 1;

    public FloatPanel() => InitializeComponent();

    private void FloatPanel_OnLoaded(object sender, RoutedEventArgs e)
    {
        switch (Position)
        {
            case FloatPosition.TopLeft:
            {
                #region Alignment

                VerticalAlignment = VerticalAlignment.Top;
                HorizontalAlignment = HorizontalAlignment.Left;

                HeaderBorder.HorizontalAlignment = HeaderAlignment;

                #endregion Alignment

                #region Grid

                Grid.SetRow(HeaderBorder, 1);
                Grid.SetRow(ContentBorder, 0);

                #endregion Grid

                #region Corner radius

                HeaderBorder.CornerRadius = new CornerRadius(0, 0, 6, 6);
                ContentBorder.CornerRadius = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => new CornerRadius(0, 0, 6, 0),
                    HorizontalAlignment.Right => new CornerRadius(0, 0, 0, 6),
                    _ => ContentBorder.CornerRadius
                };

                #endregion Corner radius

                #region Margin

                HeaderBorder.Margin = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => new Thickness(0, -1, 10, 0),
                    HorizontalAlignment.Right => new Thickness(10, -1, 0, 0),
                    _ => HeaderBorder.Margin
                };
                Margin = new Thickness(OffsetLeft, -ContentBorder.ActualHeight - InsideMarginValue + OffsetTop, OffsetRight, OffsetBottom);

                #endregion Margin

                break;
            }
            case FloatPosition.BottomLeft:
            {
                #region Alignment

                VerticalAlignment = VerticalAlignment.Bottom;
                HorizontalAlignment = HorizontalAlignment.Left;
                
                HeaderBorder.HorizontalAlignment = HeaderAlignment;

                #endregion Alignment

                #region Grid

                Grid.SetRow(HeaderBorder, 0);
                Grid.SetRow(ContentBorder, 1);

                #endregion Grid

                #region Corner radius

                ContentBorder.CornerRadius = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => new CornerRadius(0, 6, 0, 0),
                    HorizontalAlignment.Right => new CornerRadius(6, 0, 0, 0),
                    _ => ContentBorder.CornerRadius
                };

                #endregion Corner radius

                #region Margin

                HeaderBorder.Margin = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => new Thickness(0, 0, 10, -1),
                    HorizontalAlignment.Right => new Thickness(10, 0, 0, -1),
                    _ => HeaderBorder.Margin
                };
                Margin = new Thickness(OffsetLeft, OffsetTop, OffsetRight, -ContentBorder.ActualHeight - InsideMarginValue + OffsetBottom);

                #endregion Margin

                break;
            }
            case FloatPosition.TopRight:
            {
                #region Alignment

                VerticalAlignment = VerticalAlignment.Top;
                HorizontalAlignment = HorizontalAlignment.Right;
                
                HeaderBorder.HorizontalAlignment = HeaderAlignment;

                #endregion Alignment

                #region Grid

                Grid.SetRow(HeaderBorder, 1);
                Grid.SetRow(ContentBorder, 0);

                #endregion Grid

                #region Corner radius

                HeaderBorder.CornerRadius = new CornerRadius(0, 0, 6, 6);
                ContentBorder.CornerRadius = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => new CornerRadius(0, 0, 6, 0),
                    HorizontalAlignment.Right => new CornerRadius(0, 0, 0, 6),
                    _ => ContentBorder.CornerRadius
                };

                #endregion Corner radius

                #region Margin

                HeaderBorder.Margin = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => new Thickness(0, -1, 10, 0),
                    HorizontalAlignment.Right => new Thickness(10, -1, 0, 0),
                    _ => HeaderBorder.Margin
                };
                Margin = new Thickness(OffsetLeft, -ContentBorder.ActualHeight - InsideMarginValue + OffsetTop, OffsetRight, OffsetBottom);

                #endregion Margin

                break;
            }
            case FloatPosition.BottomRight:
            {
                #region Alignment

                VerticalAlignment = VerticalAlignment.Bottom;
                HorizontalAlignment = HorizontalAlignment.Right;
                
                HeaderBorder.HorizontalAlignment = HeaderAlignment;

                #endregion Alignment

                #region Grid

                Grid.SetRow(HeaderBorder, 0);
                Grid.SetRow(ContentBorder, 1);

                #endregion Grid

                #region Corner radius

                ContentBorder.CornerRadius = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => new CornerRadius(0, 6, 0, 0),
                    HorizontalAlignment.Right => new CornerRadius(6, 0, 0, 0),
                    _ => ContentBorder.CornerRadius
                };

                #endregion Corner radius

                #region Margin

                HeaderBorder.Margin = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => new Thickness(0, -1, 10, 0),
                    HorizontalAlignment.Right => new Thickness(10, -1, 0, 0),
                    _ => HeaderBorder.Margin
                };
                Margin = new Thickness(OffsetLeft, OffsetTop, OffsetRight, -ContentBorder.ActualHeight - InsideMarginValue + OffsetBottom);

                #endregion Margin

                break;
            }


            case FloatPosition.LeftTop:
            {
                #region Alignment

                VerticalAlignment = VerticalAlignment.Top;
                HorizontalAlignment = HorizontalAlignment.Left;

                HeaderBorder.VerticalAlignment = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => VerticalAlignment.Top,
                    HorizontalAlignment.Right => VerticalAlignment.Bottom,
                    _ => VerticalAlignment.Stretch
                };

                #endregion Alignment

                #region Grid

                Grid.SetRow(HeaderBorder, 0);
                Grid.SetRow(ContentBorder, 0);
                Grid.SetColumn(HeaderBorder, 1);
                Grid.SetColumn(ContentBorder, 0);

                #endregion Grid

                #region Corner radius

                HeaderBorder.CornerRadius = new CornerRadius(0, 6, 6, 0);
                ContentBorder.CornerRadius = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => new CornerRadius(0, 0, 6, 0),
                    HorizontalAlignment.Right => new CornerRadius(0, 6, 0, 0),
                    _ => ContentBorder.CornerRadius
                };

                #endregion Corner radius

                #region Margin

                HeaderBorder.Margin = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => new Thickness(-1, 0, 0, 10),
                    HorizontalAlignment.Right => new Thickness(-1, 10, 0, 0),
                    _ => HeaderBorder.Margin
                };
                ContentBorder.Margin = new Thickness(-ContentBorder.ActualWidth - InsideMarginValue, 0, 0, 0);
                Margin = new Thickness(OffsetLeft, OffsetTop, OffsetRight, OffsetBottom);

                #endregion Margin
                
                RotateTransform.Angle = 90;

                break;
            }
            case FloatPosition.LeftBottom:
            {
                #region Alignment

                VerticalAlignment = VerticalAlignment.Bottom;
                HorizontalAlignment = HorizontalAlignment.Left;
                
                HeaderBorder.VerticalAlignment = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => VerticalAlignment.Top,
                    HorizontalAlignment.Right => VerticalAlignment.Bottom,
                    _ => VerticalAlignment.Stretch
                };

                #endregion Alignment

                #region Grid

                Grid.SetRow(HeaderBorder, 0);
                Grid.SetRow(ContentBorder, 0);
                Grid.SetColumn(HeaderBorder, 1);
                Grid.SetColumn(ContentBorder, 0);

                #endregion Grid

                #region Corner radius

                HeaderBorder.CornerRadius = new CornerRadius(0, 6, 6, 0);
                ContentBorder.CornerRadius = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => new CornerRadius(0, 0, 6, 0),
                    HorizontalAlignment.Right => new CornerRadius(0, 6, 0, 0),
                    _ => ContentBorder.CornerRadius
                };

                #endregion Corner radius

                #region Margin

                HeaderBorder.Margin = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => new Thickness(-1, 0, 0, 10),
                    HorizontalAlignment.Right => new Thickness(-1, 10, 0, 0),
                    _ => HeaderBorder.Margin
                };
                ContentBorder.Margin = new Thickness(-ContentBorder.ActualWidth - InsideMarginValue, 0, 0, 0);
                Margin = new Thickness(OffsetLeft, OffsetTop, OffsetRight, OffsetBottom);

                #endregion Margin

                RotateTransform.Angle = 90;

                break;
            }
            case FloatPosition.RightTop:
            {
                #region Alignment

                VerticalAlignment = VerticalAlignment.Top;
                HorizontalAlignment = HorizontalAlignment.Right;
                
                HeaderBorder.VerticalAlignment = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => VerticalAlignment.Top,
                    HorizontalAlignment.Right => VerticalAlignment.Bottom,
                    _ => VerticalAlignment.Stretch
                };

                #endregion Alignment

                #region Grid

                Grid.SetRow(HeaderBorder, 0);
                Grid.SetRow(ContentBorder, 0);
                Grid.SetColumn(HeaderBorder, 0);
                Grid.SetColumn(ContentBorder, 1);

                #endregion Grid

                #region Corner radius

                HeaderBorder.CornerRadius = new CornerRadius(6, 0, 0, 6);
                ContentBorder.CornerRadius = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => new CornerRadius(0, 0, 0, 6),
                    HorizontalAlignment.Right => new CornerRadius(6, 0, 0, 0),
                    _ => ContentBorder.CornerRadius
                };

                #endregion Corner radius

                #region Margin

                HeaderBorder.Margin = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => new Thickness(0, 0, -1, 10),
                    HorizontalAlignment.Right => new Thickness(0, 10, -1, 0),
                    _ => HeaderBorder.Margin
                };
                ContentBorder.Margin = new Thickness(0, 0, -ContentBorder.ActualWidth - InsideMarginValue, 0);
                Margin = new Thickness(OffsetLeft, OffsetTop, OffsetRight, OffsetBottom);

                #endregion Margin

                RotateTransform.Angle = -90;

                break;
            }
            case FloatPosition.RightBottom:
            {
                #region Alignment

                VerticalAlignment = VerticalAlignment.Bottom;
                HorizontalAlignment = HorizontalAlignment.Right;
                
                HeaderBorder.VerticalAlignment = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => VerticalAlignment.Top,
                    HorizontalAlignment.Right => VerticalAlignment.Bottom,
                    _ => VerticalAlignment.Stretch
                };

                #endregion Alignment

                #region Grid

                Grid.SetRow(HeaderBorder, 0);
                Grid.SetRow(ContentBorder, 0);
                Grid.SetColumn(HeaderBorder, 0);
                Grid.SetColumn(ContentBorder, 1);

                #endregion Grid

                #region Corner radius

                HeaderBorder.CornerRadius = new CornerRadius(6, 0, 0, 6);
                ContentBorder.CornerRadius = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => new CornerRadius(0, 0, 0, 6),
                    HorizontalAlignment.Right => new CornerRadius(6, 0, 0, 0),
                    _ => ContentBorder.CornerRadius
                };

                #endregion Corner radius

                #region Margin

                HeaderBorder.Margin = HeaderAlignment switch
                {
                    HorizontalAlignment.Left => new Thickness(0, 0, -1, 10),
                    HorizontalAlignment.Right => new Thickness(0, 10, -1, 0),
                    _ => HeaderBorder.Margin
                };
                ContentBorder.Margin = new Thickness(0, 0, -ContentBorder.ActualWidth - InsideMarginValue, 0);
                Margin = new Thickness(OffsetLeft, OffsetTop, OffsetRight, OffsetBottom);

                #endregion Margin

                RotateTransform.Angle = -90;

                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void HeaderBorder_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        Margin = new Thickness(OffsetLeft, OffsetTop, OffsetRight, OffsetBottom);
        ContentBorder.Margin = new Thickness(0, 0, 0, 0);
    }

    private void FloatPanel_OnMouseLeave(object sender, MouseEventArgs e)
    {
        switch (Position)
        {
            case FloatPosition.TopLeft:
                Margin = new Thickness(OffsetLeft, -ContentBorder.ActualHeight - InsideMarginValue + OffsetTop, OffsetRight, OffsetBottom);
                break;
            case FloatPosition.BottomLeft:
                Margin = new Thickness(OffsetLeft, OffsetTop, OffsetRight, -ContentBorder.ActualHeight - InsideMarginValue + OffsetBottom);
                break;
            case FloatPosition.TopRight:
                Margin = new Thickness(OffsetLeft, -ContentBorder.ActualHeight - InsideMarginValue + OffsetTop, OffsetRight, OffsetBottom);
                break;
            case FloatPosition.BottomRight:
                Margin = new Thickness(OffsetLeft, OffsetTop, OffsetRight, -ContentBorder.ActualHeight - InsideMarginValue + OffsetBottom);
                break;
            
            case FloatPosition.LeftTop:
                ContentBorder.Margin = new Thickness(-ContentBorder.ActualWidth - InsideMarginValue, 0, 0, 0);
                Margin = new Thickness(OffsetLeft, OffsetTop, OffsetRight, OffsetBottom);
                break;
            case FloatPosition.LeftBottom:
                ContentBorder.Margin = new Thickness(-ContentBorder.ActualWidth - InsideMarginValue, 0, 0, 0);
                Margin = new Thickness(OffsetLeft, OffsetTop, OffsetRight, OffsetBottom);
                break;
            case FloatPosition.RightTop:
                ContentBorder.Margin = new Thickness(0, 0, -ContentBorder.ActualWidth - InsideMarginValue, 0);
                Margin = new Thickness(OffsetLeft, OffsetTop, OffsetRight, OffsetBottom);
                break;
            case FloatPosition.RightBottom:
                Margin = new Thickness(OffsetLeft, OffsetTop, -ContentBorder.ActualWidth - InsideMarginValue + OffsetRight, OffsetBottom);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}