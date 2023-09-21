using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using ControlzEx;
using JetBrains.Annotations;
using StratumUi.Wpf.Core.ValueBoxed;

namespace StratumUi.Wpf.Core.Controls;

/// <summary>
/// A slider control with the ability to select a range between two values.
/// </summary>
[DefaultEvent("RangeSelectionChanged")]
[TemplatePart(Name = "PART_Container", Type = typeof(FrameworkElement))]
[TemplatePart(Name = "PART_RangeSliderContainer", Type = typeof(StackPanel))]
[TemplatePart(Name = "PART_LeftEdge", Type = typeof(RepeatButton))]
[TemplatePart(Name = "PART_LeftThumb", Type = typeof(System.Windows.Controls.Primitives.Thumb))]
[TemplatePart(Name = "PART_MiddleThumb", Type = typeof(System.Windows.Controls.Primitives.Thumb))]
[TemplatePart(Name = "PART_RightThumb", Type = typeof(System.Windows.Controls.Primitives.Thumb))]
[TemplatePart(Name = "PART_RightEdge", Type = typeof(RepeatButton))]
public class RangeSlider2 : RangeBase
{
    #region Routed UI commands

    public static readonly RoutedUICommand MoveBack
        = new(nameof(MoveBack), nameof(MoveBack), typeof(RangeSlider2),
            new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.B, ModifierKeys.Control) }));

    public static readonly RoutedUICommand MoveForward
        = new(nameof(MoveForward), nameof(MoveForward), typeof(RangeSlider2),
            new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F, ModifierKeys.Control) }));

    public static readonly RoutedUICommand MoveAllForward
        = new(nameof(MoveAllForward), nameof(MoveAllForward), typeof(RangeSlider2),
            new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F, ModifierKeys.Alt) }));

    public static readonly RoutedUICommand MoveAllBack
        = new(nameof(MoveAllBack), nameof(MoveAllBack), typeof(RangeSlider2),
            new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.B, ModifierKeys.Alt) }));

    #endregion

    #region Routed events

    /// <summary>Identifies the <see cref="RangeSelectionChanged"/> routed event.</summary>
    public static readonly RoutedEvent RangeSelectionChangedEvent
        = EventManager.RegisterRoutedEvent(nameof(RangeSelectionChanged),
            RoutingStrategy.Bubble,
            typeof(RangeSelectionChangedEventHandler<double>),
            typeof(RangeSlider2));

    public event RangeSelectionChangedEventHandler<double> RangeSelectionChanged
    {
        add => AddHandler(RangeSelectionChangedEvent, value);
        remove => RemoveHandler(RangeSelectionChangedEvent, value);
    }

    /// <summary>Identifies the <see cref="LowerValueChanged"/> routed event.</summary>
    public static readonly RoutedEvent LowerValueChangedEvent
        = EventManager.RegisterRoutedEvent(nameof(LowerValueChanged),
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<double>),
            typeof(RangeSlider2));

    public event RoutedPropertyChangedEventHandler<double> LowerValueChanged
    {
        add => AddHandler(LowerValueChangedEvent, value);
        remove => RemoveHandler(LowerValueChangedEvent, value);
    }

    /// <summary>Identifies the <see cref="UpperValueChanged"/> routed event.</summary>
    public static readonly RoutedEvent UpperValueChangedEvent
        = EventManager.RegisterRoutedEvent(nameof(UpperValueChanged),
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<double>),
            typeof(RangeSlider2));

    public event RoutedPropertyChangedEventHandler<double> UpperValueChanged
    {
        add => AddHandler(UpperValueChangedEvent, value);
        remove => RemoveHandler(UpperValueChangedEvent, value);
    }

    /// <summary>Identifies the <see cref="LowerThumbDragStarted"/> routed event.</summary>
    public static readonly RoutedEvent LowerThumbDragStartedEvent
        = EventManager.RegisterRoutedEvent(nameof(LowerThumbDragStarted),
            RoutingStrategy.Bubble,
            typeof(DragStartedEventHandler),
            typeof(RangeSlider2));

    public event DragStartedEventHandler LowerThumbDragStarted
    {
        add => AddHandler(LowerThumbDragStartedEvent, value);
        remove => RemoveHandler(LowerThumbDragStartedEvent, value);
    }

    /// <summary>Identifies the <see cref="LowerThumbDragCompleted"/> routed event.</summary>
    public static readonly RoutedEvent LowerThumbDragCompletedEvent
        = EventManager.RegisterRoutedEvent(nameof(LowerThumbDragCompleted),
            RoutingStrategy.Bubble,
            typeof(DragCompletedEventHandler),
            typeof(RangeSlider2));

    public event DragCompletedEventHandler LowerThumbDragCompleted
    {
        add => AddHandler(LowerThumbDragCompletedEvent, value);
        remove => RemoveHandler(LowerThumbDragCompletedEvent, value);
    }

    /// <summary>Identifies the <see cref="UpperThumbDragStarted"/> routed event.</summary>
    public static readonly RoutedEvent UpperThumbDragStartedEvent
        = EventManager.RegisterRoutedEvent(nameof(UpperThumbDragStarted),
            RoutingStrategy.Bubble,
            typeof(DragStartedEventHandler),
            typeof(RangeSlider2));

    public event DragStartedEventHandler UpperThumbDragStarted
    {
        add => AddHandler(UpperThumbDragStartedEvent, value);
        remove => RemoveHandler(UpperThumbDragStartedEvent, value);
    }

    /// <summary>Identifies the <see cref="UpperThumbDragCompleted"/> routed event.</summary>
    public static readonly RoutedEvent UpperThumbDragCompletedEvent
        = EventManager.RegisterRoutedEvent(nameof(UpperThumbDragCompleted),
            RoutingStrategy.Bubble,
            typeof(DragCompletedEventHandler),
            typeof(RangeSlider2));

    public event DragCompletedEventHandler UpperThumbDragCompleted
    {
        add => AddHandler(UpperThumbDragCompletedEvent, value);
        remove => RemoveHandler(UpperThumbDragCompletedEvent, value);
    }

    /// <summary>Identifies the <see cref="CentralThumbDragStarted"/> routed event.</summary>
    public static readonly RoutedEvent CentralThumbDragStartedEvent
        = EventManager.RegisterRoutedEvent(nameof(CentralThumbDragStarted),
            RoutingStrategy.Bubble,
            typeof(DragStartedEventHandler),
            typeof(RangeSlider2));

    public event DragStartedEventHandler CentralThumbDragStarted
    {
        add => AddHandler(CentralThumbDragStartedEvent, value);
        remove => RemoveHandler(CentralThumbDragStartedEvent, value);
    }

    /// <summary>Identifies the <see cref="CentralThumbDragCompleted"/> routed event.</summary>
    public static readonly RoutedEvent CentralThumbDragCompletedEvent
        = EventManager.RegisterRoutedEvent(nameof(CentralThumbDragCompleted),
            RoutingStrategy.Bubble,
            typeof(DragCompletedEventHandler),
            typeof(RangeSlider2));

    public event DragCompletedEventHandler CentralThumbDragCompleted
    {
        add => AddHandler(CentralThumbDragCompletedEvent, value);
        remove => RemoveHandler(CentralThumbDragCompletedEvent, value);
    }

    /// <summary>Identifies the <see cref="LowerThumbDragDelta"/> routed event.</summary>
    public static readonly RoutedEvent LowerThumbDragDeltaEvent
        = EventManager.RegisterRoutedEvent(nameof(LowerThumbDragDelta),
            RoutingStrategy.Bubble,
            typeof(DragDeltaEventHandler),
            typeof(RangeSlider2));

    public event DragDeltaEventHandler LowerThumbDragDelta
    {
        add => AddHandler(LowerThumbDragDeltaEvent, value);
        remove => RemoveHandler(LowerThumbDragDeltaEvent, value);
    }

    /// <summary>Identifies the <see cref="UpperThumbDragDelta"/> routed event.</summary>
    public static readonly RoutedEvent UpperThumbDragDeltaEvent
        = EventManager.RegisterRoutedEvent(nameof(UpperThumbDragDelta),
            RoutingStrategy.Bubble,
            typeof(DragDeltaEventHandler),
            typeof(RangeSlider2));

    public event DragDeltaEventHandler UpperThumbDragDelta
    {
        add => AddHandler(UpperThumbDragDeltaEvent, value);
        remove => RemoveHandler(UpperThumbDragDeltaEvent, value);
    }

    /// <summary>Identifies the <see cref="CentralThumbDragDelta"/> routed event.</summary>
    public static readonly RoutedEvent CentralThumbDragDeltaEvent
        = EventManager.RegisterRoutedEvent(nameof(CentralThumbDragDelta),
            RoutingStrategy.Bubble,
            typeof(DragDeltaEventHandler),
            typeof(RangeSlider2));

    public event DragDeltaEventHandler CentralThumbDragDelta
    {
        add => AddHandler(CentralThumbDragDeltaEvent, value);
        remove => RemoveHandler(CentralThumbDragDeltaEvent, value);
    }

    #endregion

    #region Dependency properties

    /// <summary>Identifies the <see cref="UpperValue"/> dependency property.</summary>
    public static readonly DependencyProperty UpperValueProperty
        = DependencyProperty.Register(nameof(UpperValue),
            typeof(double),
            typeof(RangeSlider2),
            new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender, RangesChanged, CoerceUpperValue));

    /// <summary>
    /// Get/sets the end of the range selection.
    /// </summary>
    [Bindable(true)]
    [Category("Common")]
    public double UpperValue
    {
        get => (double)GetValue(UpperValueProperty);
        set => SetValue(UpperValueProperty, value);
    }

    [MustUseReturnValue]
    internal static object CoerceUpperValue(DependencyObject d, object? baseValue)
    {
        if (d is not RangeSlider2 rangeSlider2 || baseValue is not double value) return baseValue ?? 0D;
        if (value > rangeSlider2.Maximum || rangeSlider2.LowerValue + rangeSlider2.MinRange > rangeSlider2.Maximum)
            return rangeSlider2.Maximum;

        if (value < rangeSlider2.LowerValue + rangeSlider2.MinRange)
            return rangeSlider2.LowerValue + rangeSlider2.MinRange;

        return baseValue;
    }

    /// <summary>Identifies the <see cref="LowerValue"/> dependency property.</summary>
    public static readonly DependencyProperty LowerValueProperty
        = DependencyProperty.Register(nameof(LowerValue),
            typeof(double),
            typeof(RangeSlider2),
            new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender, RangesChanged, CoerceLowerValue));

    /// <summary>
    /// Get/sets the beginning of the range selection.
    /// </summary>
    [Bindable(true)]
    [Category("Common")]
    public double LowerValue
    {
        get => (double)GetValue(LowerValueProperty);
        set => SetValue(LowerValueProperty, value);
    }

    //Lower/Upper values property changed callback
    private static void RangesChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        if (dependencyObject is not RangeSlider2 rangeSlider2) return;
        if (rangeSlider2._internalUpdate)
            return;

        rangeSlider2.CoerceLowerUpperValues();
    }

    private void CoerceLowerUpperValues()
    {
        CoerceValue(LowerValueProperty);
        CoerceValue(UpperValueProperty);
        RaiseValueChangedEvents(this);
        _oldLower = LowerValue;
        _oldUpper = UpperValue;
        ReCalculateSize();
    }

    [MustUseReturnValue]
    internal static object CoerceLowerValue(DependencyObject d, object? baseValue)
    {
        if (d is not RangeSlider2 rangeSlider2 || baseValue is not double value) return baseValue ?? 0D;
        if (value < rangeSlider2.Minimum || rangeSlider2.UpperValue - rangeSlider2.MinRange < rangeSlider2.Minimum)
            return rangeSlider2.Minimum;

        if (value > rangeSlider2.UpperValue - rangeSlider2.MinRange)
            return rangeSlider2.UpperValue - rangeSlider2.MinRange;

        return baseValue;
    }

    /// <summary>Identifies the <see cref="MinRange"/> dependency property.</summary>
    public static readonly DependencyProperty MinRangeProperty
        = DependencyProperty.Register(nameof(MinRange),
            typeof(double),
            typeof(RangeSlider2),
            new FrameworkPropertyMetadata(0d, MinRangeChanged, CoerceMinRange), IsValidMinRange);

    /// <summary>
    /// Get/sets the minimum range that can be selected.
    /// </summary>
    [Bindable(true)]
    [Category("Common")]
    public double MinRange
    {
        get => (double)GetValue(MinRangeProperty);
        set => SetValue(MinRangeProperty, value);
    }

    private static void MinRangeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var value = (double)e.NewValue;
        if (value < 0) value = 0;

        var slider = (RangeSlider2)dependencyObject;
        dependencyObject.CoerceValue(MinRangeProperty);
        slider._internalUpdate = true;
        slider.UpperValue = Math.Max(slider.UpperValue, slider.LowerValue + value);
        slider.UpperValue = Math.Min(slider.UpperValue, slider.Maximum);
        slider._internalUpdate = false;

        slider.CoerceValue(UpperValueProperty);

        RaiseValueChangedEvents(dependencyObject);

        slider._oldLower = slider.LowerValue;
        slider._oldUpper = slider.UpperValue;

        slider.ReCalculateSize();
    }

    [MustUseReturnValue]
    private static object CoerceMinRange(DependencyObject d, object? baseValue)
    {
        if (d is not RangeSlider2 rangeSlider2 || baseValue is not double value) return baseValue ?? 0D;
        if (rangeSlider2.LowerValue + value > rangeSlider2.Maximum)
            return rangeSlider2.Maximum - rangeSlider2.LowerValue;

        return baseValue;
    }

    /// <summary>Identifies the <see cref="MinRangeWidth"/> dependency property.</summary>
    public static readonly DependencyProperty MinRangeWidthProperty
        = DependencyProperty.Register(nameof(MinRangeWidth),
            typeof(double),
            typeof(RangeSlider2),
            new FrameworkPropertyMetadata(0d, MinRangeWidthChanged, CoerceMinRangeWidth), IsValidMinRange);

    /// <summary>
    /// Get/sets the minimal distance between two thumbs.
    /// </summary>
    [Bindable(true)]
    [Category("Common")]
    public double MinRangeWidth
    {
        get => (double)GetValue(MinRangeWidthProperty);
        set => SetValue(MinRangeWidthProperty, value);
    }

    private static void MinRangeWidthChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        (sender as RangeSlider2)?.ReCalculateSize();
    }

    [MustUseReturnValue]
    private static object CoerceMinRangeWidth(DependencyObject d, object? baseValue)
    {
        if (d is not RangeSlider2 slider2 || baseValue is not double value) return baseValue ?? 0D;
        if (slider2._leftThumb == null || slider2._rightThumb == null) return baseValue;
        double width;
        if (slider2.Orientation == Orientation.Horizontal)
            width = slider2.ActualWidth - slider2._leftThumb.ActualWidth - slider2._rightThumb.ActualWidth;
        else
            width = slider2.ActualHeight - slider2._leftThumb.ActualHeight - slider2._rightThumb.ActualHeight;

        return value > width / 2 ? width / 2 : value;
    }

    private static bool IsValidMinRange(object? value)
    {
        return value is double doubleValue && IsValidDouble(doubleValue) && doubleValue >= 0d;
    }

    /// <summary>Identifies the <see cref="MoveWholeRange"/> dependency property.</summary>
    public static readonly DependencyProperty MoveWholeRangeProperty
        = DependencyProperty.Register(nameof(MoveWholeRange),
            typeof(bool),
            typeof(RangeSlider2),
            new PropertyMetadata(BooleanBoxes.FalseBox));

    /// <summary>
    /// Get/sets whether whole range will be moved when press on right/left/central part of control
    /// </summary>
    [Bindable(true)]
    [Category("Behavior")]
    public bool MoveWholeRange
    {
        get => (bool)GetValue(MoveWholeRangeProperty);
        set => SetValue(MoveWholeRangeProperty, BooleanBoxes.Box(value));
    }

    /// <summary>Identifies the <see cref="ExtendedMode"/> dependency property.</summary>
    public static readonly DependencyProperty ExtendedModeProperty
        = DependencyProperty.Register(nameof(ExtendedMode),
            typeof(bool),
            typeof(RangeSlider2),
            new PropertyMetadata(BooleanBoxes.FalseBox));

    /// <summary>
    /// Get/sets whether possibility to make manipulations inside range with left/right mouse buttons + control button
    /// </summary>
    [Bindable(true)]
    [Category("Behavior")]
    public bool ExtendedMode
    {
        get => (bool)GetValue(ExtendedModeProperty);
        set => SetValue(ExtendedModeProperty, BooleanBoxes.Box(value));
    }

    /// <summary>Identifies the <see cref="IsSnapToTickEnabled"/> dependency property.</summary>
    public static readonly DependencyProperty IsSnapToTickEnabledProperty
        = DependencyProperty.Register(nameof(IsSnapToTickEnabled),
            typeof(bool),
            typeof(RangeSlider2),
            new PropertyMetadata(BooleanBoxes.FalseBox));

    /// <summary>
    /// When 'true', RangeSlider2 will automatically move the Thumb (and/or change current value) to the closest TickMark.
    /// </summary>
    [Bindable(true)]
    [Category("Appearance")]
    public bool IsSnapToTickEnabled
    {
        get => (bool)GetValue(IsSnapToTickEnabledProperty);
        set => SetValue(IsSnapToTickEnabledProperty, BooleanBoxes.Box(value));
    }

    /// <summary>Identifies the <see cref="Orientation"/> dependency property.</summary>
    public static readonly DependencyProperty OrientationProperty
        = DependencyProperty.Register(nameof(Orientation),
            typeof(Orientation),
            typeof(RangeSlider2),
            new FrameworkPropertyMetadata(Orientation.Horizontal));

    /// <summary>
    /// Gets or sets the orientation of the <see cref="T:MahApps.Metro.Controls.RangeSlider2" />.
    /// </summary>
    [Bindable(true)]
    [Category("Common")]
    public Orientation Orientation
    {
        get => (Orientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>Identifies the <see cref="TickPlacement"/> dependency property.</summary>
    public static readonly DependencyProperty TickPlacementProperty
        = DependencyProperty.Register(nameof(TickPlacement),
            typeof(TickPlacement),
            typeof(RangeSlider2),
            new FrameworkPropertyMetadata(TickPlacement.None));

    /// <summary>
    /// Gets or sets the position of tick marks with respect to the <see cref="T:System.Windows.Controls.Primitives.Track" /> of the <see cref="T:MahApps.Metro.Controls.RangeSlider2" />.
    /// </summary>
    /// <returns>
    /// A <see cref="P:MahApps.Metro.Controls.RangeSlider2.TickPlacement" /> value that defines how to position the tick marks in a <see cref="T:MahApps.Metro.Controls.RangeSlider2" /> with respect to the slider bar. The default is <see cref="F:System.Windows.Controls.Primitives.TickPlacement.None" />.
    /// </returns>
    [Bindable(true)]
    [Category("Appearance")]
    public TickPlacement TickPlacement
    {
        get => (TickPlacement)GetValue(TickPlacementProperty);
        set => SetValue(TickPlacementProperty, value);
    }

    /// <summary>Identifies the <see cref="TickFrequency"/> dependency property.</summary>
    public static readonly DependencyProperty TickFrequencyProperty
        = DependencyProperty.Register(nameof(TickFrequency),
            typeof(double),
            typeof(RangeSlider2),
            new FrameworkPropertyMetadata(1d), IsValidDoubleValue);

    /// <summary>
    /// Gets or sets the interval between tick marks.
    /// </summary>
    /// <returns>
    /// The distance between tick marks. The default is (1.0).
    /// </returns>
    [Bindable(true)]
    [Category("Appearance")]
    public double TickFrequency
    {
        get => (double)GetValue(TickFrequencyProperty);
        set => SetValue(TickFrequencyProperty, value);
    }

    /// <summary>Identifies the <see cref="Ticks"/> dependency property.</summary>
    public static readonly DependencyProperty TicksProperty
        = DependencyProperty.Register(nameof(Ticks),
            typeof(DoubleCollection),
            typeof(RangeSlider2),
            new FrameworkPropertyMetadata(default(DoubleCollection)));

    /// <summary>
    /// Gets or sets the positions of the tick marks to display for a <see cref="T:MahApps.Metro.Controls.RangeSlider2" />. </summary>
    /// <returns>
    /// A set of tick marks to display for a <see cref="T:MahApps.Metro.Controls.RangeSlider2" />. The default is <see langword="null" />.
    /// </returns>
    [Bindable(true)]
    [Category("Appearance")]
    public DoubleCollection? Ticks
    {
        get => (DoubleCollection?)GetValue(TicksProperty);
        set => SetValue(TicksProperty, value);
    }

    /// <summary>Identifies the <see cref="IsMoveToPointEnabled"/> dependency property.</summary>
    public static readonly DependencyProperty IsMoveToPointEnabledProperty
        = DependencyProperty.Register(nameof(IsMoveToPointEnabled),
            typeof(bool),
            typeof(RangeSlider2),
            new PropertyMetadata(BooleanBoxes.FalseBox));

    /// <summary>
    /// Get or sets IsMoveToPoint feature which will enable/disable moving to exact point inside control when user clicked on it
    /// Gets or sets a value that indicates whether the two <see cref="P:System.Windows.Controls.Primitives.Track.Thumb" /> of a <see cref="T:MahApps.Metro.Controls.RangeSlider2" /> moves immediately to the location of the mouse click that occurs while the mouse pointer pauses on the <see cref="T:MahApps.Metro.Controls.RangeSlider2" /> tracks.
    /// </summary>
    [Bindable(true)]
    [Category("Behavior")]
    public bool IsMoveToPointEnabled
    {
        get => (bool)GetValue(IsMoveToPointEnabledProperty);
        set => SetValue(IsMoveToPointEnabledProperty, BooleanBoxes.Box(value));
    }

    /// <summary>Identifies the <see cref="AutoToolTipPlacement"/> dependency property.</summary>
    public static readonly DependencyProperty AutoToolTipPlacementProperty
        = DependencyProperty.Register(nameof(AutoToolTipPlacement),
            typeof(AutoToolTipPlacement),
            typeof(RangeSlider2),
            new FrameworkPropertyMetadata(AutoToolTipPlacement.None));

    /// <summary>
    /// Gets or sets whether a tooltip that contains the current value of the <see cref="T:MahApps.Metro.Controls.RangeSlider2" /> displays when the <see cref="P:System.Windows.Controls.Primitives.Track.Thumb" /> is pressed. If a tooltip is displayed, this property also specifies the placement of the tooltip.
    /// </summary>
    /// <returns>
    /// One of the <see cref="T:System.Windows.Controls.Primitives.AutoToolTipPlacement" /> values that determines where to display the tooltip with respect to the <see cref="P:System.Windows.Controls.Primitives.Track.Thumb" /> of the <see cref="T:MahApps.Metro.Controls.RangeSlider2" />, or that specifies to not show a tooltip. The default is <see cref="F:System.Windows.Controls.Primitives.AutoToolTipPlacement.None" />, which specifies that a tooltip is not displayed.
    /// </returns>
    [Bindable(true)]
    [Category("Behavior")]
    public AutoToolTipPlacement AutoToolTipPlacement
    {
        get => (AutoToolTipPlacement)GetValue(AutoToolTipPlacementProperty);
        set => SetValue(AutoToolTipPlacementProperty, value);
    }

    /// <summary>Identifies the <see cref="AutoToolTipPrecision"/> dependency property.</summary>
    public static readonly DependencyProperty AutoToolTipPrecisionProperty
        = DependencyProperty.Register(nameof(AutoToolTipPrecision),
            typeof(int),
            typeof(RangeSlider2),
            new FrameworkPropertyMetadata(0), IsValidPrecision);

    /// <summary>
    /// Gets or sets the number of digits that are displayed to the right side of the decimal point for the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Value" /> of the <see cref="T:MahApps.Metro.Controls.RangeSlider2" /> in a tooltip.
    /// </summary>
    /// <returns>
    /// The precision of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Value" /> that displays in the tooltip, specified as the number of digits that appear to the right of the decimal point. The default is zero (0).
    /// </returns>
    [Bindable(true)]
    [Category("Appearance")]
    public int AutoToolTipPrecision
    {
        get => (int)GetValue(AutoToolTipPrecisionProperty);
        set => SetValue(AutoToolTipPrecisionProperty, value);
    }

    /// <summary>Identifies the <see cref="AutoToolTipLowerValueTemplate"/> dependency property.</summary>
    public static readonly DependencyProperty AutoToolTipLowerValueTemplateProperty
        = DependencyProperty.Register(nameof(AutoToolTipLowerValueTemplate),
            typeof(DataTemplate),
            typeof(RangeSlider2),
            new FrameworkPropertyMetadata(null));

    /// <summary>
    /// Gets or sets a template for the auto tooltip to show the lower value.
    /// </summary>
    [Bindable(true)]
    [Category("Behavior")]
    public DataTemplate? AutoToolTipLowerValueTemplate
    {
        get => (DataTemplate?)GetValue(AutoToolTipLowerValueTemplateProperty);
        set => SetValue(AutoToolTipLowerValueTemplateProperty, value);
    }

    /// <summary>Identifies the <see cref="AutoToolTipUpperValueTemplate"/> dependency property.</summary>
    public static readonly DependencyProperty AutoToolTipUpperValueTemplateProperty
        = DependencyProperty.Register(nameof(AutoToolTipUpperValueTemplate),
            typeof(DataTemplate),
            typeof(RangeSlider2),
            new FrameworkPropertyMetadata(null));

    /// <summary>
    /// Gets or sets a template for the auto tooltip to show the upper value.
    /// </summary>
    [Bindable(true)]
    [Category("Behavior")]
    public DataTemplate? AutoToolTipUpperValueTemplate
    {
        get => (DataTemplate?)GetValue(AutoToolTipUpperValueTemplateProperty);
        set => SetValue(AutoToolTipUpperValueTemplateProperty, value);
    }

    /// <summary>Identifies the <see cref="AutoToolTipRangeValuesTemplate"/> dependency property.</summary>
    public static readonly DependencyProperty AutoToolTipRangeValuesTemplateProperty
        = DependencyProperty.Register(nameof(AutoToolTipRangeValuesTemplate),
            typeof(DataTemplate),
            typeof(RangeSlider2),
            new FrameworkPropertyMetadata(null));

    /// <summary>
    /// Gets or sets a template for the auto tooltip to show the center value.
    /// </summary>
    [Bindable(true)]
    [Category("Behavior")]
    public DataTemplate? AutoToolTipRangeValuesTemplate
    {
        get => (DataTemplate?)GetValue(AutoToolTipRangeValuesTemplateProperty);
        set => SetValue(AutoToolTipRangeValuesTemplateProperty, value);
    }

    /// <summary>Identifies the <see cref="Interval"/> dependency property.</summary>
    public static readonly DependencyProperty IntervalProperty
        = DependencyProperty.Register(nameof(Interval),
            typeof(int),
            typeof(RangeSlider2),
            new FrameworkPropertyMetadata(100, IntervalChangedCallback), IsValidPrecision);

    /// <summary>
    /// Get/sets value how fast thumbs will move when user press on left/right/central with left mouse button (IsMoveToPoint must be set to FALSE)
    /// </summary>
    [Bindable(true)]
    [Category("Behavior")]
    public int Interval
    {
        get => (int)GetValue(IntervalProperty);
        set => SetValue(IntervalProperty, value);
    }

    /// <summary>Identifies the <see cref="IsSelectionRangeEnabled"/> dependency property.</summary>
    public static readonly DependencyProperty IsSelectionRangeEnabledProperty
        = DependencyProperty.Register(nameof(IsSelectionRangeEnabled),
            typeof(bool),
            typeof(RangeSlider2),
            new FrameworkPropertyMetadata(BooleanBoxes.FalseBox));

    /// <summary>
    /// Gets or sets a value that indicates whether the <see cref="T:MahApps.Metro.Controls.RangeSlider2" /> displays a selection range along the <see cref="T:MahApps.Metro.Controls.RangeSlider2" />.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if a selection range is displayed; otherwise, <see langword="false" />. The default is <see langword="false" />.
    /// </returns>
    [Bindable(true)]
    [Category("Appearance")]
    public bool IsSelectionRangeEnabled
    {
        get => (bool)GetValue(IsSelectionRangeEnabledProperty);
        set => SetValue(IsSelectionRangeEnabledProperty, BooleanBoxes.Box(value));
    }

    /// <summary>Identifies the <see cref="SelectionStart"/> dependency property.</summary>
    public static readonly DependencyProperty SelectionStartProperty
        = DependencyProperty.Register(nameof(SelectionStart),
            typeof(double),
            typeof(RangeSlider2),
            new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectionStartChanged, CoerceSelectionStart),
            IsValidDoubleValue);

    /// <summary>
    /// Gets or sets the smallest value of a specified selection for a <see cref="T:MahApps.Metro.Controls.RangeSlider2" />.
    /// </summary>
    /// <returns>
    /// The largest value of a selected range of values of a <see cref="T:MahApps.Metro.Controls.RangeSlider2" />. The default is zero (0.0).
    /// </returns>
    [Bindable(true)]
    [Category("Appearance")]
    public double SelectionStart
    {
        get => (double)GetValue(SelectionStartProperty);
        set => SetValue(SelectionStartProperty, value);
    }

    private static void OnSelectionStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as RangeSlider2)?.CoerceValue(SelectionEndProperty);
    }

    [MustUseReturnValue]
    private static object CoerceSelectionStart(DependencyObject d, object? baseValue)
    {
        if (d is not RangeSlider2 rangeSlider2 || baseValue is not double value) return baseValue ?? 0D;
        var minimum = rangeSlider2.Minimum;
        var maximum = rangeSlider2.Maximum;

        if (value < minimum)
            return minimum;

        if (value > maximum)
            return maximum;

        return baseValue;
    }

    /// <summary>Identifies the <see cref="SelectionEnd"/> dependency property.</summary>
    public static readonly DependencyProperty SelectionEndProperty
        = DependencyProperty.Register(nameof(SelectionEnd),
            typeof(double),
            typeof(RangeSlider2),
            new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectionEndChanged, CoerceSelectionEnd),
            IsValidDoubleValue);

    /// <summary>
    /// Gets or sets the largest value of a specified selection for a <see cref="T:MahApps.Metro.Controls.RangeSlider2" />.
    /// </summary>
    /// <returns>
    /// The largest value of a selected range of values of a <see cref="T:MahApps.Metro.Controls.RangeSlider2" />. The default is zero (0.0).
    /// </returns>
    [Bindable(true)]
    [Category("Appearance")]
    public double SelectionEnd
    {
        get => (double)GetValue(SelectionEndProperty);
        set => SetValue(SelectionEndProperty, value);
    }

    private static void OnSelectionEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as RangeSlider2)?.CoerceValue(SelectionStartProperty);
    }

    [MustUseReturnValue]
    private static object CoerceSelectionEnd(DependencyObject d, object? baseValue)
    {
        if (d is not RangeSlider2 rangeSlider2 || baseValue is not double value) return baseValue ?? 0D;
        var selectionStart = rangeSlider2.SelectionStart;
        var maximum = rangeSlider2.Maximum;

        if (value < selectionStart)
            return selectionStart;

        if (value > maximum)
            return maximum;

        return baseValue;
    }

    protected double MovableRange => Maximum - Minimum - MinRange;

    #endregion

    #region Variables

    private const double Epsilon = 0.00000153;

    private bool _internalUpdate;
    private System.Windows.Controls.Primitives.Thumb? _centerThumb;
    private System.Windows.Controls.Primitives.Thumb? _leftThumb;
    private System.Windows.Controls.Primitives.Thumb? _rightThumb;
    private RepeatButton? _leftButton;
    private RepeatButton? _rightButton;
    private StackPanel _visualElementsContainer = null!;
    private FrameworkElement _container = null!;
    private double _movableWidth;
    private readonly DispatcherTimer _timer;
    private uint _tickCount;
    private double _currentPoint;
    private bool _isInsideRange;
    private bool _centerThumbBlocked;
    private Direction _direction;
    private ButtonType _bType;
    private Point _position;
    private Point _basePoint;
    private double _currenValue;
    private double _density;
    private ToolTip? _autoToolTip;
    private double _oldLower;
    private double _oldUpper;
    private bool _isMoved;
    private bool _roundToPrecision;
    private int _precision;
    private readonly PropertyChangeNotifier _actualWidthPropertyChangeNotifier;
    private readonly PropertyChangeNotifier _actualHeightPropertyChangeNotifier;
    private readonly PropertyChangeNotifier _visibilityPropertyChangeNotifier;

    #endregion

    public RangeSlider2()
    {
        CommandBindings.Add(new CommandBinding(MoveBack, (_, _) => MoveSelection(true)));
        CommandBindings.Add(new CommandBinding(MoveForward, (_, _) => MoveSelection(false)));
        CommandBindings.Add(new CommandBinding(MoveAllForward, (_, _) => ResetSelection(false)));
        CommandBindings.Add(new CommandBinding(MoveAllBack, (_, _) => ResetSelection(true)));

        _actualWidthPropertyChangeNotifier = new PropertyChangeNotifier(this, ActualWidthProperty);
        _actualWidthPropertyChangeNotifier.ValueChanged += (_, _) => ReCalculateSize();
        _actualHeightPropertyChangeNotifier = new PropertyChangeNotifier(this, ActualHeightProperty);
        _actualHeightPropertyChangeNotifier.ValueChanged += (_, _) => ReCalculateSize();
        _visibilityPropertyChangeNotifier = new PropertyChangeNotifier(this, VisibilityProperty);
        _visibilityPropertyChangeNotifier.ValueChanged += (_, _) =>
        {
            if (IsVisible)
            {
                ReCalculateSize();
            }
        };

        _timer = new DispatcherTimer();
        _timer.Tick += MoveToNextValue;
        _timer.Interval = TimeSpan.FromMilliseconds(Interval);
    }

    static RangeSlider2()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(RangeSlider2), new FrameworkPropertyMetadata(typeof(RangeSlider2)));
        MinimumProperty.OverrideMetadata(typeof(RangeSlider2), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsMeasure, MinPropertyChangedCallback, CoerceMinimum));
        MaximumProperty.OverrideMetadata(typeof(RangeSlider2), new FrameworkPropertyMetadata(100d, FrameworkPropertyMetadataOptions.AffectsMeasure, MaxPropertyChangedCallback, CoerceMaximum));
    }

    /// <summary>
    /// Responds to a change in the value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Minimum"/> property.
    /// </summary>
    /// <param name="oldMinimum">The old value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Minimum"/> property.</param><param name="newMinimum">The new value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Minimum"/> property.</param>
    protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
    {
        CoerceValue(SelectionStartProperty);
        ReCalculateSize();
    }

    /// <summary>
    /// Responds to a change in the value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Maximum"/> property.
    /// </summary>
    /// <param name="oldMaximum">The old value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Maximum"/> property.</param><param name="newMaximum">The new value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Maximum"/> property.</param>
    protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
    {
        CoerceValue(SelectionStartProperty);
        CoerceValue(SelectionEndProperty);
        ReCalculateSize();
    }

    private static void MoveThumb(FrameworkElement x, FrameworkElement y, double change, Orientation orientation)
    {
        if (orientation == Orientation.Horizontal)
        {
            MoveThumbHorizontal(x, y, change);
        }
        else if (orientation == Orientation.Vertical)
        {
            MoveThumbVertical(x, y, change);
        }
    }

    private static void MoveThumb(FrameworkElement x, FrameworkElement y, double change, Orientation orientation, out Direction direction)
    {
        direction = Direction.Increase;
        if (orientation == Orientation.Horizontal)
        {
            direction = change < 0 ? Direction.Decrease : Direction.Increase;
            MoveThumbHorizontal(x, y, change);
        }
        else if (orientation == Orientation.Vertical)
        {
            direction = change < 0 ? Direction.Increase : Direction.Decrease;
            MoveThumbVertical(x, y, change);
        }
    }

    private static void MoveThumbHorizontal(FrameworkElement x, FrameworkElement y, double horizontalChange)
    {
        if (!double.IsNaN(x.Width) && !double.IsNaN(y.Width))
        {
            if (horizontalChange < 0) //slider went left
            {
                var change = GetChangeKeepPositive(x.Width, horizontalChange);
                if (x.Name == "PART_MiddleThumb")
                {
                    if (x.Width > x.MinWidth)
                    {
                        if (x.Width + change < x.MinWidth)
                        {
                            var dif = x.Width - x.MinWidth;
                            x.Width = x.MinWidth;
                            y.Width += dif;
                        }
                        else
                        {
                            x.Width += change;
                            y.Width -= change;
                        }
                    }
                }
                else
                {
                    x.Width += change;
                    y.Width -= change;
                }
            }
            else if (horizontalChange > 0) //slider went right if(horizontal change == 0 do nothing)
            {
                var change = -GetChangeKeepPositive(y.Width, -horizontalChange);
                if (y.Name == "PART_MiddleThumb")
                {
                    if (y.Width > y.MinWidth)
                    {
                        if (y.Width - change < y.MinWidth)
                        {
                            var dif = y.Width - y.MinWidth;
                            y.Width = y.MinWidth;
                            x.Width += dif;
                        }
                        else
                        {
                            x.Width += change;
                            y.Width -= change;
                        }
                    }
                }
                else
                {
                    x.Width += change;
                    y.Width -= change;
                }
            }
        }
    }

    private static void MoveThumbVertical(FrameworkElement x, FrameworkElement y, double verticalChange)
    {
        if (!double.IsNaN(x.Height) && !double.IsNaN(y.Height))
        {
            if (verticalChange < 0) //slider went up
            {
                var change = -GetChangeKeepPositive(y.Height, verticalChange); //get positive number
                if (y.Name == "PART_MiddleThumb")
                {
                    if (y.Height > y.MinHeight)
                    {
                        if (y.Height - change < y.MinHeight)
                        {
                            var dif = y.Height - y.MinHeight;
                            y.Height = y.MinHeight;
                            x.Height += dif;
                        }
                        else
                        {
                            x.Height += change;
                            y.Height -= change;
                        }
                    }
                }
                else
                {
                    x.Height += change;
                    y.Height -= change;
                }
            }
            else if (verticalChange > 0) //slider went down if(horizontal change == 0 do nothing)
            {
                var change = GetChangeKeepPositive(x.Height, -verticalChange); //get negative number
                if (x.Name == "PART_MiddleThumb")
                {
                    if (x.Height > y.MinHeight)
                    {
                        if (x.Height + change < x.MinHeight)
                        {
                            var dif = x.Height - x.MinHeight;
                            x.Height = x.MinHeight;
                            y.Height += dif;
                        }
                        else
                        {
                            x.Height += change;
                            y.Height -= change;
                        }
                    }
                }
                else
                {
                    x.Height += change;
                    y.Height -= change;
                }
            }
        }
    }

    //Recalculation of Control Height or Width
    private void ReCalculateSize()
    {
        if (_leftButton == null || _rightButton == null || _centerThumb == null || _rightThumb == null || _leftThumb == null) return;
        if (Orientation == Orientation.Horizontal)
        {
            _movableWidth = Math.Max(ActualWidth - _rightThumb.ActualWidth - _leftThumb.ActualWidth - MinRangeWidth, 1);
            if (MovableRange <= 0)
            {
                _leftButton.Width = double.NaN;
                _rightButton.Width = double.NaN;
            }
            else
            {
                _leftButton.Width = Math.Max(_movableWidth * (LowerValue - Minimum) / MovableRange, 0);
                _rightButton.Width = Math.Max(_movableWidth * (Maximum - UpperValue) / MovableRange, 0);
            }

            if (IsValidDouble(_rightButton.Width) && IsValidDouble(_leftButton.Width))
                _centerThumb.Width = Math.Max(ActualWidth - (_leftButton.Width + _rightButton.Width + _rightThumb.ActualWidth + _leftThumb.ActualWidth), 0);
            else
                _centerThumb.Width = Math.Max(ActualWidth - (_rightThumb.ActualWidth + _leftThumb.ActualWidth), 0);
        }
        else if (Orientation == Orientation.Vertical)
        {
            _movableWidth = Math.Max(ActualHeight - _rightThumb.ActualHeight - _leftThumb.ActualHeight - MinRangeWidth, 1);
            if (MovableRange <= 0)
            {
                _leftButton.Height = double.NaN;
                _rightButton.Height = double.NaN;
            }
            else
            {
                _leftButton.Height = Math.Max(_movableWidth * (LowerValue - Minimum) / MovableRange, 0);
                _rightButton.Height = Math.Max(_movableWidth * (Maximum - UpperValue) / MovableRange, 0);
            }

            if (IsValidDouble(_rightButton.Height) && IsValidDouble(_leftButton.Height))
                _centerThumb.Height = Math.Max(ActualHeight - (_leftButton.Height + _rightButton.Height + _rightThumb.ActualHeight + _leftThumb.ActualHeight), 0);
            else
                _centerThumb.Height = Math.Max(ActualHeight - (_rightThumb.ActualHeight + _leftThumb.ActualHeight), 0);
        }

        _density = _movableWidth / MovableRange;
    }

    //Method calculates new values when IsSnapToTickEnabled = FALSE
    private void ReCalculateRangeSelected(bool reCalculateLowerValue, bool reCalculateUpperValue, Direction direction)
    {
        _internalUpdate = true; //set flag to signal that the properties are being set by the object itself
        if (direction == Direction.Increase)
        {
            if (reCalculateUpperValue)
            {
                _oldUpper = UpperValue;
                var width = Orientation == Orientation.Horizontal ? _rightButton!.Width : _rightButton!.Height;
                //Check first if button width is not Double.NaN
                if (IsValidDouble(width))
                {
                    // Make sure to get exactly rangestop if thumb is at the end
                    var upper = Equals(width, 0.0) ? Maximum : Math.Min(Maximum, Maximum - MovableRange * width / _movableWidth);
                    UpperValue = _isMoved ? upper : _roundToPrecision ? Math.Round(upper, _precision) : upper;
                }
            }

            if (reCalculateLowerValue)
            {
                _oldLower = LowerValue;
                var width = Orientation == Orientation.Horizontal ? _leftButton!.Width : _leftButton!.Height;
                //Check first if button width is not Double.NaN
                if (IsValidDouble(width))
                {
                    // Make sure to get exactly rangestart if thumb is at the start
                    var lower = Equals(width, 0.0) ? Minimum : Math.Max(Minimum, Minimum + MovableRange * width / _movableWidth);
                    LowerValue = _isMoved ? lower : _roundToPrecision ? Math.Round(lower, _precision) : lower;
                }
            }
        }
        else
        {
            if (reCalculateLowerValue)
            {
                _oldLower = LowerValue;
                var width = Orientation == Orientation.Horizontal ? _leftButton!.Width : _leftButton!.Height;
                //Check first if button width is not Double.NaN
                if (IsValidDouble(width))
                {
                    // Make sure to get exactly rangestart if thumb is at the start
                    var lower = Equals(width, 0.0) ? Minimum : Math.Max(Minimum, Minimum + MovableRange * width / _movableWidth);
                    LowerValue = _isMoved ? lower : _roundToPrecision ? Math.Round(lower, _precision) : lower;
                }
            }

            if (reCalculateUpperValue)
            {
                _oldUpper = UpperValue;
                var width = Orientation == Orientation.Horizontal ? _rightButton!.Width : _rightButton!.Height;
                //Check first if button width is not Double.NaN
                if (IsValidDouble(width))
                {
                    // Make sure to get exactly rangestop if thumb is at the end
                    var upper = Equals(width, 0.0) ? Maximum : Math.Min(Maximum, Maximum - MovableRange * width / _movableWidth);
                    UpperValue = _isMoved ? upper : _roundToPrecision ? Math.Round(upper, _precision) : upper;
                }
            }
        }

        _roundToPrecision = false;
        _internalUpdate = false; //set flag to signal that the properties are being set by the object itself

        RaiseValueChangedEvents(this, reCalculateLowerValue, reCalculateUpperValue);
    }

    //Method used for checking and setting correct values when IsSnapToTickEnable = TRUE (When thumb moving separately)
    private void ReCalculateRangeSelected(bool reCalculateLowerValue, bool reCalculateUpperValue, double value, Direction direction)
    {
        _internalUpdate = true; //set flag to signal that the properties are being set by the object itself
        var tickFrequency = TickFrequency.ToString(CultureInfo.InvariantCulture);
        if (reCalculateLowerValue)
        {
            _oldLower = LowerValue;
            double lower = 0;
            if (IsSnapToTickEnabled) lower = direction == Direction.Increase ? Math.Min(UpperValue - MinRange, value) : Math.Max(Minimum, value);

            if (!tickFrequency.ToLower().Contains("e+") && tickFrequency.Contains("."))
            {
                //decimal part is for cutting value exactly on that number of digits, which has TickFrequency to have correct values
                var decimalPart = tickFrequency.Split('.');
                LowerValue = Math.Round(lower, decimalPart[1].Length, MidpointRounding.AwayFromZero);
            }
            else
                LowerValue = lower;
        }

        if (reCalculateUpperValue)
        {
            _oldUpper = UpperValue;
            double upper = 0;
            if (IsSnapToTickEnabled) upper = direction == Direction.Increase ? Math.Min(value, Maximum) : Math.Max(LowerValue + MinRange, value);

            if (!tickFrequency.ToLower().Contains("e+") && tickFrequency.Contains('.'))
            {
                var decimalPart = tickFrequency.Split('.');
                UpperValue = Math.Round(upper, decimalPart[1].Length, MidpointRounding.AwayFromZero);
            }
            else
                UpperValue = upper;
        }

        _internalUpdate = false; //set flag to signal that the properties are being set by the object itself

        RaiseValueChangedEvents(this, reCalculateLowerValue, reCalculateUpperValue);
    }

    //Method used for checking and setting correct values when IsSnapToTickEnable = TRUE (When thumb moving together)
    private void ReCalculateRangeSelected(double newLower, double newUpper, Direction direction)
    {
        _internalUpdate = true; //set flag to signal that the properties are being set by the object itself
        _oldLower = LowerValue;
        _oldUpper = UpperValue;

        if (IsSnapToTickEnabled)
        {
            double lower;
            double upper;

            if (direction == Direction.Increase)
            {
                lower = Math.Min(newLower, Maximum - (UpperValue - LowerValue));
                upper = Math.Min(newUpper, Maximum);
            }
            else
            {
                lower = Math.Max(newLower, Minimum);
                upper = Math.Max(Minimum + (UpperValue - LowerValue), newUpper);
            }

            var tickFrequency = TickFrequency.ToString(CultureInfo.InvariantCulture);
            if (!tickFrequency.ToLower().Contains("e+") && tickFrequency.Contains('.'))
            {
                //decimal part is for cutting value exactly on that number of digits, which has TickFrequency to have correct values
                var decimalPart = tickFrequency.Split('.');
                //used when whole range decreasing to have correct updated values (lower first, upper - second)
                if (direction == Direction.Decrease)
                {
                    LowerValue = Math.Round(lower, decimalPart[1].Length, MidpointRounding.AwayFromZero);
                    UpperValue = Math.Round(upper, decimalPart[1].Length, MidpointRounding.AwayFromZero);
                }
                //used when whole range increasing to have correct updated values (upper first, lower - second)
                else
                {
                    UpperValue = Math.Round(upper, decimalPart[1].Length, MidpointRounding.AwayFromZero);
                    LowerValue = Math.Round(lower, decimalPart[1].Length, MidpointRounding.AwayFromZero);
                }
            }
            else
            {
                //used when whole range decreasing to have correct updated values (lower first, upper - second)
                if (direction == Direction.Decrease)
                {
                    LowerValue = lower;
                    UpperValue = upper;
                }
                //used when whole range increasing to have correct updated values (upper first, lower - second)
                else
                {
                    UpperValue = upper;
                    LowerValue = lower;
                }
            }
        }

        _internalUpdate = false; //set flag to signal that the properties are being set by the object itself

        RaiseValueChangedEvents(this);
    }

    public void MoveSelection(bool isLeft)
    {
        var widthChange = SmallChange * (UpperValue - LowerValue) * _movableWidth / MovableRange;

        widthChange = isLeft ? -widthChange : widthChange;
        MoveThumb(_leftButton!, _rightButton!, widthChange, Orientation, out _direction);
        ReCalculateRangeSelected(true, true, _direction);
        CoerceLowerUpperValues();
    }

    public void ResetSelection(bool isStart)
    {
        var widthChange = Maximum - Minimum;
        widthChange = isStart ? -widthChange : widthChange;

        MoveThumb(_leftButton!, _rightButton!, widthChange, Orientation, out _direction);
        ReCalculateRangeSelected(true, true, _direction);
        CoerceLowerUpperValues();
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _container = GetTemplateChild("PART_Container") as FrameworkElement ?? throw new MissingRequiredTemplatePartException(this, "PART_Container");
        _visualElementsContainer = GetTemplateChild("PART_RangeSliderContainer") as StackPanel ?? throw new MissingRequiredTemplatePartException(this, "PART_RangeSliderContainer");
        _centerThumb = GetTemplateChild("PART_MiddleThumb") as System.Windows.Controls.Primitives.Thumb ?? throw new MissingRequiredTemplatePartException(this, "PART_MiddleThumb");
        _leftButton = GetTemplateChild("PART_LeftEdge") as RepeatButton ?? throw new MissingRequiredTemplatePartException(this, "PART_LeftEdge");
        _rightButton = GetTemplateChild("PART_RightEdge") as RepeatButton ?? throw new MissingRequiredTemplatePartException(this, "PART_RightEdge");
        _leftThumb = GetTemplateChild("PART_LeftThumb") as System.Windows.Controls.Primitives.Thumb ?? throw new MissingRequiredTemplatePartException(this, "PART_LeftThumb");
        _rightThumb = GetTemplateChild("PART_RightThumb") as System.Windows.Controls.Primitives.Thumb ?? throw new MissingRequiredTemplatePartException(this, "PART_RightThumb");

        InitializeVisualElementsContainer();
        ReCalculateSize();
    }

    //adds visual element to the container
    private void InitializeVisualElementsContainer()
    {
        _leftThumb!.DragCompleted -= LeftThumbDragComplete;
        _rightThumb!.DragCompleted -= RightThumbDragComplete;
        _leftThumb.DragStarted -= LeftThumbDragStart;
        _rightThumb.DragStarted -= RightThumbDragStart;
        _centerThumb!.DragStarted -= CenterThumbDragStarted;
        _centerThumb.DragCompleted -= CenterThumbDragCompleted;

        //handle the drag delta events
        _centerThumb.DragDelta -= CenterThumbDragDelta;
        _leftThumb.DragDelta -= LeftThumbDragDelta;
        _rightThumb.DragDelta -= RightThumbDragDelta;

        _visualElementsContainer.PreviewMouseDown -= VisualElementsContainerPreviewMouseDown;
        _visualElementsContainer.PreviewMouseUp -= VisualElementsContainerPreviewMouseUp;
        _visualElementsContainer.MouseLeave -= VisualElementsContainerMouseLeave;
        _visualElementsContainer.MouseDown -= VisualElementsContainerMouseDown;

        _leftThumb.DragCompleted += LeftThumbDragComplete;
        _rightThumb.DragCompleted += RightThumbDragComplete;
        _leftThumb.DragStarted += LeftThumbDragStart;
        _rightThumb.DragStarted += RightThumbDragStart;
        _centerThumb.DragStarted += CenterThumbDragStarted;
        _centerThumb.DragCompleted += CenterThumbDragCompleted;

        //handle the drag delta events
        _centerThumb.DragDelta += CenterThumbDragDelta;
        _leftThumb.DragDelta += LeftThumbDragDelta;
        _rightThumb.DragDelta += RightThumbDragDelta;

        _visualElementsContainer.PreviewMouseDown += VisualElementsContainerPreviewMouseDown;
        _visualElementsContainer.PreviewMouseUp += VisualElementsContainerPreviewMouseUp;
        _visualElementsContainer.MouseLeave += VisualElementsContainerMouseLeave;
        _visualElementsContainer.MouseDown += VisualElementsContainerMouseDown;
    }

    //Handler for preview mouse button down for the whole StackPanel container
    private void VisualElementsContainerPreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (_leftButton == null || _rightButton == null || _leftThumb == null || _rightThumb == null) return;
        var position = Mouse.GetPosition(_visualElementsContainer);
        if (Orientation == Orientation.Horizontal)
        {
            if (position.X < _leftButton.ActualWidth)
                LeftButtonMouseDown();
            else if (position.X > ActualWidth - _rightButton.ActualWidth)
                RightButtonMouseDown();
            else if (position.X > _leftButton.ActualWidth + _leftThumb.ActualWidth &&
                     position.X < ActualWidth - (_rightButton.ActualWidth + _rightThumb.ActualWidth)) 
                CentralThumbMouseDown();
        }
        else
        {
            if (position.Y > ActualHeight - _leftButton.ActualHeight)
                LeftButtonMouseDown();
            else if (position.Y < _rightButton.ActualHeight)
                RightButtonMouseDown();
            else if (position.Y > _rightButton.ActualHeight + _rightButton.ActualHeight &&
                     position.Y < ActualHeight - (_leftButton.ActualHeight + _leftThumb.ActualHeight)) 
                CentralThumbMouseDown();
        }
    }

    private void VisualElementsContainerMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.MiddleButton == MouseButtonState.Pressed) MoveWholeRange = MoveWholeRange != true;
    }

    #region Mouse events

    private void VisualElementsContainerMouseLeave(object sender, MouseEventArgs e)
    {
        _tickCount = 0;
        _timer.Stop();
    }

    private void VisualElementsContainerPreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        _tickCount = 0;
        _timer.Stop();
        _centerThumbBlocked = false;
    }

    private void LeftButtonMouseDown()
    {
        if (_leftButton == null || _rightButton == null || _leftThumb == null || _rightThumb == null || _centerThumb == null) return;
        if (Mouse.LeftButton != MouseButtonState.Pressed) return;
        var p = Mouse.GetPosition(_visualElementsContainer);
        var change = Orientation == Orientation.Horizontal
            ? _leftButton.ActualWidth - p.X + _leftThumb.ActualWidth / 2
            : -(_leftButton.ActualHeight - (ActualHeight - (p.Y + _leftThumb.ActualHeight / 2)));
        if (!IsSnapToTickEnabled)
        {
            switch (IsMoveToPointEnabled)
            {
                case true when !MoveWholeRange:
                    MoveThumb(_leftButton, _centerThumb, -change, Orientation, out _direction);
                    ReCalculateRangeSelected(true, false, _direction);
                    CoerceLowerUpperValues();
                    break;
                case true when MoveWholeRange:
                    MoveThumb(_leftButton, _rightButton, -change, Orientation, out _direction);
                    ReCalculateRangeSelected(true, true, _direction);
                    CoerceLowerUpperValues();
                    break;
            }
        }
        else
        {
            switch (IsMoveToPointEnabled)
            {
                case true when !MoveWholeRange:
                    JumpToNextTick(Direction.Decrease, ButtonType.BottomLeft, -change, LowerValue, true);
                    break;
                case true when MoveWholeRange:
                    JumpToNextTick(Direction.Decrease, ButtonType.Both, -change, LowerValue, true);
                    break;
            }
        }

        if (IsMoveToPointEnabled) return;
        _position = Mouse.GetPosition(_visualElementsContainer);
        _bType = MoveWholeRange ? ButtonType.Both : ButtonType.BottomLeft;
        _currentPoint = Orientation == Orientation.Horizontal ? _position.X : _position.Y;
        _currenValue = LowerValue;
        _isInsideRange = false;
        _direction = Direction.Decrease;
        _timer.Start();
    }

    private void RightButtonMouseDown()
    {
        if (_leftButton == null || _rightButton == null || _leftThumb == null || _rightThumb == null || _centerThumb == null) return;
        if (Mouse.LeftButton != MouseButtonState.Pressed) return;
        var p = Mouse.GetPosition(_visualElementsContainer);
        var change = Orientation == Orientation.Horizontal
            ? _rightButton.ActualWidth - (ActualWidth - (p.X + _rightThumb.ActualWidth / 2))
            : -(_rightButton.ActualHeight - (p.Y - _rightThumb.ActualHeight / 2));
        if (!IsSnapToTickEnabled)
        {
            switch (IsMoveToPointEnabled)
            {
                case true when !MoveWholeRange:
                    MoveThumb(_centerThumb, _rightButton, change, Orientation, out _direction);
                    ReCalculateRangeSelected(false, true, _direction);
                    CoerceLowerUpperValues();
                    break;
                case true when MoveWholeRange:
                    MoveThumb(_leftButton, _rightButton, change, Orientation, out _direction);
                    ReCalculateRangeSelected(true, true, _direction);
                    CoerceLowerUpperValues();
                    break;
            }
        }
        else
        {
            switch (IsMoveToPointEnabled)
            {
                case true when !MoveWholeRange:
                    JumpToNextTick(Direction.Increase, ButtonType.TopRight, change, UpperValue, true);
                    break;
                case true when MoveWholeRange:
                    JumpToNextTick(Direction.Increase, ButtonType.Both, change, UpperValue, true);
                    break;
            }
        }

        if (IsMoveToPointEnabled) return;
        _position = Mouse.GetPosition(_visualElementsContainer);
        _bType = MoveWholeRange ? ButtonType.Both : ButtonType.TopRight;
        _currentPoint = Orientation == Orientation.Horizontal ? _position.X : _position.Y;
        _currenValue = UpperValue;
        _direction = Direction.Increase;
        _isInsideRange = false;
        _timer.Start();
    }

    private void CentralThumbMouseDown()
    {
        if (_leftButton == null || _rightButton == null || _leftThumb == null || _rightThumb == null || _centerThumb == null) return;
        if (!ExtendedMode) return;
        if (Mouse.LeftButton == MouseButtonState.Pressed && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
        {
            _centerThumbBlocked = true;
            var p = Mouse.GetPosition(_visualElementsContainer);
            var change = Orientation == Orientation.Horizontal
                ? p.X + _leftThumb.ActualWidth / 2 - (_leftButton.ActualWidth + _leftThumb.ActualWidth)
                : -(ActualHeight - (p.Y + _leftThumb.ActualHeight / 2 + _leftButton.ActualHeight));
            if (!IsSnapToTickEnabled)
                switch (IsMoveToPointEnabled)
                {
                    case true when !MoveWholeRange:
                        MoveThumb(_leftButton, _centerThumb, change, Orientation, out _direction);
                        ReCalculateRangeSelected(true, false, _direction);
                        CoerceLowerUpperValues();
                        break;
                    case true when MoveWholeRange:
                        MoveThumb(_leftButton, _rightButton, change, Orientation, out _direction);
                        ReCalculateRangeSelected(true, true, _direction);
                        CoerceLowerUpperValues();
                        break;
                }
            else
                switch (IsMoveToPointEnabled)
                {
                    case true when !MoveWholeRange:
                        JumpToNextTick(Direction.Increase, ButtonType.BottomLeft, change, LowerValue, true);
                        break;
                    case true when MoveWholeRange:
                        JumpToNextTick(Direction.Increase, ButtonType.Both, change, LowerValue, true);
                        break;
                }

            if (IsMoveToPointEnabled) return;
            _position = Mouse.GetPosition(_visualElementsContainer);
            _bType = MoveWholeRange ? ButtonType.Both : ButtonType.BottomLeft;
            _currentPoint = Orientation == Orientation.Horizontal ? _position.X : _position.Y;
            _currenValue = LowerValue;
            _direction = Direction.Increase;
            _isInsideRange = true;
            _timer.Start();
        }
        else if (Mouse.RightButton == MouseButtonState.Pressed && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
        {
            _centerThumbBlocked = true;
            var p = Mouse.GetPosition(_visualElementsContainer);
            var change = Orientation == Orientation.Horizontal
                ? ActualWidth - (p.X + _rightThumb.ActualWidth / 2 + _rightButton.ActualWidth)
                : -(p.Y + _rightThumb.ActualHeight / 2 - (_rightButton.ActualHeight + _rightThumb.ActualHeight));
            if (!IsSnapToTickEnabled)
                switch (IsMoveToPointEnabled)
                {
                    case true when !MoveWholeRange:
                        MoveThumb(_centerThumb, _rightButton, -change, Orientation, out _direction);
                        ReCalculateRangeSelected(false, true, _direction);
                        CoerceLowerUpperValues();
                        break;
                    case true when MoveWholeRange:
                        MoveThumb(_leftButton, _rightButton, -change, Orientation, out _direction);
                        ReCalculateRangeSelected(true, true, _direction);
                        CoerceLowerUpperValues();
                        break;
                }
            else
                switch (IsMoveToPointEnabled)
                {
                    case true when !MoveWholeRange:
                        JumpToNextTick(Direction.Decrease, ButtonType.TopRight, -change, UpperValue, true);
                        break;
                    case true when MoveWholeRange:
                        JumpToNextTick(Direction.Decrease, ButtonType.Both, -change, UpperValue, true);
                        break;
                }

            if (IsMoveToPointEnabled) return;
            _position = Mouse.GetPosition(_visualElementsContainer);
            _bType = MoveWholeRange ? ButtonType.Both : ButtonType.TopRight;
            _currentPoint = Orientation == Orientation.Horizontal ? _position.X : _position.Y;
            _currenValue = UpperValue;
            _direction = Direction.Decrease;
            _isInsideRange = true;
            _timer.Start();
        }
    }

    #endregion

    #region Thumb Drag event handlers

    private void LeftThumbDragStart(object sender, DragStartedEventArgs e)
    {
        _isMoved = true;
        if (AutoToolTipPlacement != AutoToolTipPlacement.None)
        {
            _autoToolTip ??= new ToolTip { Placement = PlacementMode.Custom, CustomPopupPlacementCallback = PopupPlacementCallback };

            _autoToolTip.SetValue(ContentControl.ContentTemplateProperty, AutoToolTipLowerValueTemplate);
            _autoToolTip.Content = GetToolTipNumber(LowerValue);
            _autoToolTip.PlacementTarget = _leftThumb;
            _autoToolTip.IsOpen = true;
        }

        _basePoint = Mouse.GetPosition(_container);
        e.RoutedEvent = LowerThumbDragStartedEvent;
        RaiseEvent(e);
    }

    private void LeftThumbDragDelta(object sender, DragDeltaEventArgs e)
    {
        if (_leftButton == null || _rightButton == null || _leftThumb == null || _rightThumb == null || _centerThumb == null) return;
        var change = Orientation == Orientation.Horizontal ? e.HorizontalChange : e.VerticalChange;
        if (!IsSnapToTickEnabled)
        {
            MoveThumb(_leftButton, _centerThumb, change, Orientation, out _direction);
            ReCalculateRangeSelected(true, false, _direction);
            CoerceLowerUpperValues();
        }
        else
        {
            Direction localDirection;
            var currentPoint = Mouse.GetPosition(_container);
            if (Orientation == Orientation.Horizontal)
            {
                if (currentPoint.X >= 0 && currentPoint.X < _container.ActualWidth - (_rightButton.ActualWidth + _rightThumb.ActualWidth + _centerThumb.MinWidth))
                {
                    localDirection = currentPoint.X > _basePoint.X ? Direction.Increase : Direction.Decrease;
                    JumpToNextTick(localDirection, ButtonType.BottomLeft, change, LowerValue, false);
                }
            }
            else
            {
                if (currentPoint.Y <= _container.ActualHeight && currentPoint.Y > _rightButton.ActualHeight + _rightThumb.ActualHeight + _centerThumb.MinHeight)
                {
                    localDirection = currentPoint.Y < _basePoint.Y ? Direction.Increase : Direction.Decrease;
                    JumpToNextTick(localDirection, ButtonType.BottomLeft, -change, LowerValue, false);
                }
            }
        }

        _basePoint = Mouse.GetPosition(_container);
        if (AutoToolTipPlacement != AutoToolTipPlacement.None
            && _autoToolTip is not null)
        {
            _autoToolTip.Content = GetToolTipNumber(LowerValue);
            RelocateAutoToolTip();
        }

        e.RoutedEvent = LowerThumbDragDeltaEvent;
        RaiseEvent(e);
    }

    private void LeftThumbDragComplete(object sender, DragCompletedEventArgs e)
    {
        if (_autoToolTip != null)
        {
            _autoToolTip.IsOpen = false;
            _autoToolTip = null;
        }

        e.RoutedEvent = LowerThumbDragCompletedEvent;
        RaiseEvent(e);
    }

    private void RightThumbDragStart(object sender, DragStartedEventArgs e)
    {
        _isMoved = true;
        if (AutoToolTipPlacement != AutoToolTipPlacement.None)
        {
            _autoToolTip ??= new ToolTip { Placement = PlacementMode.Custom, CustomPopupPlacementCallback = PopupPlacementCallback };

            _autoToolTip.SetValue(ContentControl.ContentTemplateProperty, AutoToolTipUpperValueTemplate);
            _autoToolTip.Content = GetToolTipNumber(UpperValue);
            _autoToolTip.PlacementTarget = _rightThumb;
            _autoToolTip.IsOpen = true;
        }

        _basePoint = Mouse.GetPosition(_container);
        e.RoutedEvent = UpperThumbDragStartedEvent;
        RaiseEvent(e);
    }

    private void RightThumbDragDelta(object sender, DragDeltaEventArgs e)
    {
        if (_leftButton == null || _rightButton == null || _leftThumb == null || _rightThumb == null || _centerThumb == null) return;
        var change = Orientation == Orientation.Horizontal ? e.HorizontalChange : e.VerticalChange;
        if (!IsSnapToTickEnabled)
        {
            MoveThumb(_centerThumb, _rightButton, change, Orientation, out _direction);
            ReCalculateRangeSelected(false, true, _direction);
            CoerceLowerUpperValues();
        }
        else
        {
            Direction localDirection;
            var currentPoint = Mouse.GetPosition(_container);
            if (Orientation == Orientation.Horizontal)
            {
                if (currentPoint.X < _container.ActualWidth && currentPoint.X > _leftButton.ActualWidth + _leftThumb.ActualWidth + _centerThumb.MinWidth)
                {
                    localDirection = currentPoint.X > _basePoint.X ? Direction.Increase : Direction.Decrease;
                    JumpToNextTick(localDirection, ButtonType.TopRight, change, UpperValue, false);
                }
            }
            else
            {
                if (currentPoint.Y >= 0 && currentPoint.Y < _container.ActualHeight - (_leftButton.ActualHeight + _leftThumb.ActualHeight + _centerThumb.MinHeight))
                {
                    localDirection = currentPoint.Y < _basePoint.Y ? Direction.Increase : Direction.Decrease;
                    JumpToNextTick(localDirection, ButtonType.TopRight, -change, UpperValue, false);
                }
            }

            _basePoint = Mouse.GetPosition(_container);
        }

        if (AutoToolTipPlacement != AutoToolTipPlacement.None
            && _autoToolTip is not null)
        {
            _autoToolTip.Content = GetToolTipNumber(UpperValue);
            RelocateAutoToolTip();
        }

        e.RoutedEvent = UpperThumbDragDeltaEvent;
        RaiseEvent(e);
    }

    private void RightThumbDragComplete(object sender, DragCompletedEventArgs e)
    {
        if (_autoToolTip != null)
        {
            _autoToolTip.IsOpen = false;
            _autoToolTip = null;
        }

        e.RoutedEvent = UpperThumbDragCompletedEvent;
        RaiseEvent(e);
    }

    private void CenterThumbDragStarted(object sender, DragStartedEventArgs e)
    {
        _isMoved = true;
        if (AutoToolTipPlacement != AutoToolTipPlacement.None)
        {
            _autoToolTip ??= new ToolTip { Placement = PlacementMode.Custom, CustomPopupPlacementCallback = PopupPlacementCallback };

            var autoToolTipRangeValuesTemplate = AutoToolTipRangeValuesTemplate;
            _autoToolTip.SetValue(ContentControl.ContentTemplateProperty, autoToolTipRangeValuesTemplate);
            if (autoToolTipRangeValuesTemplate != null)
                _autoToolTip.Content = new RangeSliderAutoTooltipValues(this);
            else
                _autoToolTip.Content = GetToolTipNumber(LowerValue) + " - " + GetToolTipNumber(UpperValue);

            _autoToolTip.PlacementTarget = _centerThumb;
            _autoToolTip.IsOpen = true;
        }

        _basePoint = Mouse.GetPosition(_container);
        e.RoutedEvent = CentralThumbDragStartedEvent;
        RaiseEvent(e);
    }

    private void CenterThumbDragDelta(object sender, DragDeltaEventArgs e)
    {
        if (_leftButton == null || _rightButton == null || _leftThumb == null || _rightThumb == null || _centerThumb == null) return;
        if (!_centerThumbBlocked)
        {
            var change = Orientation == Orientation.Horizontal ? e.HorizontalChange : e.VerticalChange;
            if (!IsSnapToTickEnabled)
            {
                MoveThumb(_leftButton, _rightButton, change, Orientation, out _direction);
                ReCalculateRangeSelected(true, true, _direction);
                CoerceLowerUpperValues();
            }
            else
            {
                Direction localDirection;
                var currentPoint = Mouse.GetPosition(_container);
                if (Orientation == Orientation.Horizontal)
                {
                    if (currentPoint.X >= 0 && currentPoint.X < _container.ActualWidth)
                    {
                        localDirection = currentPoint.X > _basePoint.X ? Direction.Increase : Direction.Decrease;
                        JumpToNextTick(localDirection, ButtonType.Both, change, localDirection == Direction.Increase ? UpperValue : LowerValue, false);
                    }
                }
                else
                {
                    if (currentPoint.Y >= 0 && currentPoint.Y < _container.ActualHeight)
                    {
                        localDirection = currentPoint.Y < _basePoint.Y ? Direction.Increase : Direction.Decrease;
                        JumpToNextTick(localDirection, ButtonType.Both, -change, localDirection == Direction.Increase ? UpperValue : LowerValue, false);
                    }
                }
            }

            _basePoint = Mouse.GetPosition(_container);
            if (AutoToolTipPlacement != AutoToolTipPlacement.None && _autoToolTip is not null)
            {
                if (_autoToolTip.ContentTemplate != null)
                    (_autoToolTip.Content as RangeSliderAutoTooltipValues)?.UpdateValues(this);
                else
                    _autoToolTip.Content = GetToolTipNumber(LowerValue) + " - " + GetToolTipNumber(UpperValue);

                RelocateAutoToolTip();
            }
        }

        e.RoutedEvent = CentralThumbDragDeltaEvent;
        RaiseEvent(e);
    }

    private void CenterThumbDragCompleted(object sender, DragCompletedEventArgs e)
    {
        if (_autoToolTip != null)
        {
            _autoToolTip.IsOpen = false;
            _autoToolTip = null;
        }

        e.RoutedEvent = CentralThumbDragCompletedEvent;
        RaiseEvent(e);
    }

    #endregion

    #region Helper methods

    private static double GetChangeKeepPositive(double width, double increment) => Math.Max(width + increment, 0) - width;

    //Method updates end point, which is needed to correctly compare current position on the thumb with
    //current width of button
    private double UpdateEndPoint(ButtonType type, Direction dir)
    {
        if (_leftButton == null || _rightButton == null || _leftThumb == null || _rightThumb == null || _centerThumb == null) return 0;
        double d = 0;
        switch (dir)
        {
            //if we increase value 
            case Direction.Increase when type == ButtonType.BottomLeft || (type == ButtonType.Both && _isInsideRange):
                d = Orientation == Orientation.Horizontal
                    ? _leftButton.ActualWidth + _leftThumb.ActualWidth
                    : ActualHeight - (_leftButton.ActualHeight + _leftThumb.ActualHeight);
                break;
            case Direction.Increase:
            {
                if (type == ButtonType.TopRight || (type == ButtonType.Both && !_isInsideRange))
                    d = Orientation == Orientation.Horizontal ? ActualWidth - _rightButton.ActualWidth : _rightButton.ActualHeight;

                break;
            }
            case Direction.Decrease when type == ButtonType.BottomLeft || (type == ButtonType.Both && !_isInsideRange):
                d = Orientation == Orientation.Horizontal ? _leftButton.ActualWidth : ActualHeight - _leftButton.ActualHeight;
                break;
            case Direction.Decrease:
            {
                if (type == ButtonType.TopRight || (type == ButtonType.Both && _isInsideRange))
                    d = Orientation == Orientation.Horizontal
                        ? ActualWidth - _rightButton.ActualWidth - _rightThumb.ActualWidth
                        : _rightButton.ActualHeight + _rightThumb.ActualHeight;

                break;
            }
        }

        return d;
    }

    private bool GetResult(double currentPoint, double endPoint, Direction direction)
    {
        if (direction == Direction.Increase)
            return Orientation == Orientation.Horizontal && currentPoint > endPoint || Orientation == Orientation.Vertical && currentPoint < endPoint;

        return Orientation == Orientation.Horizontal && currentPoint < endPoint || Orientation == Orientation.Vertical && currentPoint > endPoint;
    }

    //This is timer event, which starts when IsMoveToPoint = false
    //Supports IsSnapToTick option
    private void MoveToNextValue(object? sender, EventArgs e)
    {
        if (_leftButton == null || _rightButton == null || _leftThumb == null || _rightThumb == null || _centerThumb == null) return;
        //Get updated position of cursor
        _position = Mouse.GetPosition(_visualElementsContainer);
        _currentPoint = Orientation == Orientation.Horizontal ? _position.X : _position.Y;
        var endpoint = UpdateEndPoint(_bType, _direction);
        var result = GetResult(_currentPoint, endpoint, _direction);
        double widthChange;
        if (!IsSnapToTickEnabled)
        {
            widthChange = SmallChange;
            if (_tickCount > 5) widthChange = LargeChange;

            _roundToPrecision = true;
            if (!widthChange.ToString(CultureInfo.InvariantCulture).ToLower().Contains('e') &&
                widthChange.ToString(CultureInfo.InvariantCulture).Contains('.'))
            {
                var array = widthChange.ToString(CultureInfo.InvariantCulture).Split('.');
                _precision = array[1].Length;
            }
            else
                _precision = 0;

            //Change value sign according to Horizontal or Vertical orientation
            widthChange = Orientation == Orientation.Horizontal ? widthChange : -widthChange;
            //Change value sign one more time according to Increase or Decrease direction
            widthChange = _direction == Direction.Increase ? widthChange : -widthChange;
            if (result)
            {
                switch (_bType)
                {
                    case ButtonType.BottomLeft:
                        MoveThumb(_leftButton, _centerThumb, widthChange * _density, Orientation, out _direction);
                        ReCalculateRangeSelected(true, false, _direction);
                        CoerceLowerUpperValues();
                        break;
                    case ButtonType.TopRight:
                        MoveThumb(_centerThumb, _rightButton, widthChange * _density, Orientation, out _direction);
                        ReCalculateRangeSelected(false, true, _direction);
                        CoerceLowerUpperValues();
                        break;
                    case ButtonType.Both:
                        MoveThumb(_leftButton, _rightButton, widthChange * _density, Orientation, out _direction);
                        ReCalculateRangeSelected(true, true, _direction);
                        CoerceLowerUpperValues();
                        break;
                }
            }
        }
        else
        {
            //Get the difference between current and next value
            widthChange = CalculateNextTick(_direction, _currenValue, 0, true);
            var value = widthChange;
            //Change value sign according to Horizontal or Vertical orientation
            widthChange = Orientation == Orientation.Horizontal ? widthChange : -widthChange;
            switch (_direction)
            {
                case Direction.Increase:
                {
                    if (result)
                        switch (_bType)
                        {
                            case ButtonType.BottomLeft:
                                MoveThumb(_leftButton, _centerThumb, widthChange * _density, Orientation);
                                ReCalculateRangeSelected(true, false, LowerValue + value, _direction);
                                CoerceLowerUpperValues();
                                break;
                            case ButtonType.TopRight:
                                MoveThumb(_centerThumb, _rightButton, widthChange * _density, Orientation);
                                ReCalculateRangeSelected(false, true, UpperValue + value, _direction);
                                CoerceLowerUpperValues();
                                break;
                            case ButtonType.Both:
                                MoveThumb(_leftButton, _rightButton, widthChange * _density, Orientation);
                                ReCalculateRangeSelected(LowerValue + value, UpperValue + value, _direction);
                                CoerceLowerUpperValues();
                                break;
                        }

                    break;
                }
                case Direction.Decrease:
                {
                    if (result)
                        switch (_bType)
                        {
                            case ButtonType.BottomLeft:
                                MoveThumb(_leftButton, _centerThumb, -widthChange * _density, Orientation);
                                ReCalculateRangeSelected(true, false, LowerValue - value, _direction);
                                CoerceLowerUpperValues();
                                break;
                            case ButtonType.TopRight:
                                MoveThumb(_centerThumb, _rightButton, -widthChange * _density, Orientation);
                                ReCalculateRangeSelected(false, true, UpperValue - value, _direction);
                                CoerceLowerUpperValues();
                                break;
                            case ButtonType.Both:
                                MoveThumb(_leftButton, _rightButton, -widthChange * _density, Orientation);
                                ReCalculateRangeSelected(LowerValue - value, UpperValue - value, _direction);
                                CoerceLowerUpperValues();
                                break;
                        }

                    break;
                }
            }
        }

        _tickCount++;
    }

    //Helper method to handle snapToTick scenario and decrease amount of code
    private void SnapToTickHandle(ButtonType type, Direction direction, double difference)
    {
        if (_leftButton == null || _rightButton == null || _leftThumb == null || _rightThumb == null || _centerThumb == null) return;
        var value = difference;
        //change sign of "difference" variable because Horizontal and Vertical orientations has are different directions
        difference = Orientation == Orientation.Horizontal ? difference : -difference;
        if (direction == Direction.Increase)
            switch (type)
            {
                case ButtonType.TopRight:
                    if (UpperValue < Maximum)
                    {
                        MoveThumb(_centerThumb, _rightButton, difference * _density, Orientation);
                        ReCalculateRangeSelected(false, true, UpperValue + value, direction);
                        CoerceLowerUpperValues();
                    }

                    break;
                case ButtonType.BottomLeft:
                    if (LowerValue < UpperValue - MinRange)
                    {
                        MoveThumb(_leftButton, _centerThumb, difference * _density, Orientation);
                        ReCalculateRangeSelected(true, false, LowerValue + value, direction);
                        CoerceLowerUpperValues();
                    }

                    break;
                case ButtonType.Both:
                    if (UpperValue < Maximum)
                    {
                        MoveThumb(_leftButton, _rightButton, difference * _density, Orientation);
                        ReCalculateRangeSelected(LowerValue + value, UpperValue + value, direction);
                        CoerceLowerUpperValues();
                    }

                    break;
            }
        else
            switch (type)
            {
                case ButtonType.TopRight:
                    if (UpperValue > LowerValue + MinRange)
                    {
                        MoveThumb(_centerThumb, _rightButton, -difference * _density, Orientation);
                        ReCalculateRangeSelected(false, true, UpperValue - value, direction);
                        CoerceLowerUpperValues();
                    }

                    break;
                case ButtonType.BottomLeft:
                    if (LowerValue > Minimum)
                    {
                        MoveThumb(_leftButton, _centerThumb, -difference * _density, Orientation);
                        ReCalculateRangeSelected(true, false, LowerValue - value, direction);
                        CoerceLowerUpperValues();
                    }

                    break;
                case ButtonType.Both:
                    if (LowerValue > Minimum)
                    {
                        MoveThumb(_leftButton, _rightButton, -difference * _density, Orientation);
                        ReCalculateRangeSelected(LowerValue - value, UpperValue - value, direction);
                        CoerceLowerUpperValues();
                    }

                    break;
            }
    }

    //Calculating next value for Tick
    private double CalculateNextTick(Direction direction, double checkingValue, double distance, bool moveDirectlyToNextTick)
    {
        var checkingValuePos = checkingValue - Minimum;
        if (!IsMoveToPointEnabled)
        {
            //Check if current value is exactly Tick value or it situated between Ticks
            var checkingValueChanged = checkingValuePos; // + distance; // <-- introduced by @drayde with #2006 but it breaks the left thumb movement #2880
            var x = checkingValueChanged / TickFrequency;
            if (!IsDoubleCloseToInt(x))
            {
                distance = TickFrequency * (int)x;
                if (direction == Direction.Increase) distance += TickFrequency;

                distance -= Math.Abs(checkingValuePos);
                _currenValue = 0;
                return Math.Abs(distance);
            }
        }

        //If we need move directly to next tick without calculating the difference between ticks
        //Use when MoveToPoint disabled
        if (moveDirectlyToNextTick)
            distance = TickFrequency;
        //If current value == tick (Value is divisible)
        else
        {
            //current value in units (exactly in the place under cursor)
            var currentValue = checkingValuePos + (distance / _density);
            var x = currentValue / TickFrequency;
            if (direction == Direction.Increase)
            {
                var nextValue = x.ToString(CultureInfo.InvariantCulture).ToLower().Contains("e+")
                    ? x * TickFrequency + TickFrequency
                    : (int)x * TickFrequency + TickFrequency;
                distance = nextValue - Math.Abs(checkingValuePos);
            }
            else
            {
                var previousValue = x.ToString(CultureInfo.InvariantCulture).ToLower().Contains("e+")
                    ? x * TickFrequency
                    : (int)x * TickFrequency;
                distance = Math.Abs(checkingValuePos) - previousValue;
            }
        }

        //return absolute value without sign not to depend on it if value is negative 
        //(could cause bugs in calculations if return not absolute value)
        return Math.Abs(distance);
    }

    //Move thumb to next calculated Tick and update corresponding value
    private void JumpToNextTick(Direction direction, ButtonType type, double distance, double checkingValue, bool jumpDirectlyToTick)
    {
        //find the difference between current value and next value
        var difference = CalculateNextTick(direction, checkingValue, distance, false);
        var p = Mouse.GetPosition(_visualElementsContainer);
        var pos = Orientation == Orientation.Horizontal ? p.X : p.Y;
        var widthHeight = Orientation == Orientation.Horizontal ? ActualWidth : ActualHeight;
        var tickIntervalInPixels = direction == Direction.Increase
            ? TickFrequency * _density
            : -TickFrequency * _density;

        if (jumpDirectlyToTick)
            SnapToTickHandle(type, direction, difference);
        else if (direction == Direction.Increase)
        {
            if (!IsDoubleCloseToInt(checkingValue / TickFrequency))
            {
                if (distance > difference * _density / 2 || distance >= widthHeight - pos || distance >= pos) SnapToTickHandle(type, direction, difference);
            }
            else if (distance > tickIntervalInPixels / 2 || distance >= widthHeight - pos || distance >= pos) SnapToTickHandle(type, direction, difference);
        }
        else if (!IsDoubleCloseToInt(checkingValue / TickFrequency))
        {
            if (distance <= -(difference * _density) / 2 || UpperValue - LowerValue < difference) SnapToTickHandle(type, direction, difference);
        }
        else if (distance < tickIntervalInPixels / 2 || UpperValue - LowerValue < difference) SnapToTickHandle(type, direction, difference);
    }

    //Change AutotoolTip position to move sync with Thumb
    private void RelocateAutoToolTip()
    {
        if (_autoToolTip is null)
            return;

        var offset = _autoToolTip.HorizontalOffset;
        _autoToolTip.HorizontalOffset = offset + 0.001;
        _autoToolTip.HorizontalOffset = offset;
    }

    //CHeck if two doubles approximately equals
    private bool ApproximatelyEquals(double value1, double value2) => Math.Abs(value1 - value2) <= Epsilon;

    private bool IsDoubleCloseToInt(double val) => ApproximatelyEquals(Math.Abs(val - Math.Round(val)), 0);

    internal string GetToolTipNumber(double value)
    {
        var numberFormatInfo = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
        numberFormatInfo.NumberDecimalDigits = AutoToolTipPrecision;
        return value.ToString("N", numberFormatInfo);
    }

    //CustomPopupPlacement callback for placing AutoToolTip int TopLeft or BottomRight position
    private CustomPopupPlacement[] PopupPlacementCallback(Size popupSize, Size targetSize, Point offset)
    {
        switch (AutoToolTipPlacement)
        {
            case AutoToolTipPlacement.TopLeft:
                if (Orientation == Orientation.Horizontal)
                {
                    // Place popup at top of thumb
                    return new[] { new CustomPopupPlacement(new Point((targetSize.Width - popupSize.Width) * 0.5, -popupSize.Height), PopupPrimaryAxis.Horizontal) };
                }

                // Place popup at left of thumb 
                return new[] { new CustomPopupPlacement(new Point(-popupSize.Width, (targetSize.Height - popupSize.Height) * 0.5), PopupPrimaryAxis.Vertical) };

            case AutoToolTipPlacement.BottomRight:
                if (Orientation == Orientation.Horizontal)
                {
                    // Place popup at bottom of thumb 
                    return new[] { new CustomPopupPlacement(new Point((targetSize.Width - popupSize.Width) * 0.5, targetSize.Height), PopupPrimaryAxis.Horizontal) };
                }

                // Place popup at right of thumb 
                return new[] { new CustomPopupPlacement(new Point(targetSize.Width, (targetSize.Height - popupSize.Height) * 0.5), PopupPrimaryAxis.Vertical) };

            default:
                return new CustomPopupPlacement[] { };
        }
    }

    #endregion

    #region Validation methods

    private static bool IsValidDoubleValue(object? value) => value is double doubleValue && IsValidDouble(doubleValue);

    private static bool IsValidDouble(double? d) => d is not null && !double.IsNaN(d.Value) && !double.IsInfinity(d.Value);

    private static bool IsValidPrecision(object? value) => value is int intValue && intValue >= 0;

    #endregion

    #region Coerce callbacks

    [MustUseReturnValue]
    private static object CoerceMinimum(DependencyObject d, object? baseValue)
    {
        if (d is not RangeSlider2 rangeSlider2 || baseValue is not double value) return baseValue ?? 0D;
        return value > rangeSlider2.Maximum ? rangeSlider2.Maximum : baseValue;
    }

    [MustUseReturnValue]
    private static object CoerceMaximum(DependencyObject d, object? baseValue)
    {
        if (d is not RangeSlider2 rangeSlider2 || baseValue is not double value) return baseValue ?? 0D;
        return value < rangeSlider2.Minimum ? rangeSlider2.Minimum : baseValue;
    }

    #endregion

    #region PropertyChanged CallBacks

    private static void MaxPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
    {
        dependencyObject.CoerceValue(MaximumProperty);
        dependencyObject.CoerceValue(MinimumProperty);
        dependencyObject.CoerceValue(UpperValueProperty);
    }

    private static void MinPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
    {
        dependencyObject.CoerceValue(MinimumProperty);
        dependencyObject.CoerceValue(MaximumProperty);
        dependencyObject.CoerceValue(LowerValueProperty);
    }

    private static void IntervalChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var rs = (RangeSlider2)dependencyObject;
        rs._timer.Interval = TimeSpan.FromMilliseconds((int)e.NewValue);
    }

    //Raises all value changes events
    private static void RaiseValueChangedEvents(DependencyObject dependencyObject, bool lowerValueReCalculated = true, bool upperValueReCalculated = true)
    {
        var slider = (RangeSlider2)dependencyObject;
        var lowerValueEquals = Equals(slider._oldLower, slider.LowerValue);
        var upperValueEquals = Equals(slider._oldUpper, slider.UpperValue);

        if ((lowerValueReCalculated || upperValueReCalculated) && (!lowerValueEquals || !upperValueEquals))
            slider.RaiseEvent(new RangeSelectionChangedEventArgs<double>(slider._oldLower, slider.LowerValue, slider._oldUpper, slider.UpperValue, RangeSelectionChangedEvent));

        if (lowerValueReCalculated && !lowerValueEquals) 
            slider.RaiseEvent(new RoutedPropertyChangedEventArgs<double>(slider._oldLower, slider.LowerValue, LowerValueChangedEvent));

        if (upperValueReCalculated && !upperValueEquals) 
            slider.RaiseEvent(new RoutedPropertyChangedEventArgs<double>(slider._oldUpper, slider.UpperValue, UpperValueChangedEvent));
    }

    #endregion

    //enum for understanding which repeat button (left, right or both) is changing its width or height
    private enum ButtonType
    {
        BottomLeft,
        TopRight,
        Both
    }

    //enum for understanding current thumb moving direction 
    private enum Direction
    {
        Increase,
        Decrease
    }
}