// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using StratumUi.Wpf.Core.Automation.Peers;
using StratumUi.Wpf.Core.ValueBoxed;

namespace StratumUi.Wpf.Core.Controls;

/// <summary>
/// The MetroThumbContentControl control can be used for titles or something else and enables basic drag movement functionality.
/// </summary>
public class ThumbContentControl : ContentControl, IThumb
{
    private TouchDevice? _currentDevice;
    private Point _startDragPoint;
    private Point _startDragScreenPoint;
    private Point _oldDragScreenPoint;

    static ThumbContentControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ThumbContentControl), new FrameworkPropertyMetadata(typeof(ThumbContentControl)));
        FocusableProperty.OverrideMetadata(typeof(ThumbContentControl), new FrameworkPropertyMetadata(default(bool)));
        EventManager.RegisterClassHandler(typeof(ThumbContentControl), Mouse.LostMouseCaptureEvent, new MouseEventHandler(OnLostMouseCapture));
    }

    public static readonly RoutedEvent DragStartedEvent
        = EventManager.RegisterRoutedEvent(nameof(DragStarted),
            RoutingStrategy.Bubble,
            typeof(DragStartedEventHandler),
            typeof(ThumbContentControl));

    public static readonly RoutedEvent DragDeltaEvent
        = EventManager.RegisterRoutedEvent(nameof(DragDelta),
            RoutingStrategy.Bubble,
            typeof(DragDeltaEventHandler),
            typeof(ThumbContentControl));

    public static readonly RoutedEvent DragCompletedEvent
        = EventManager.RegisterRoutedEvent(nameof(DragCompleted),
            RoutingStrategy.Bubble,
            typeof(DragCompletedEventHandler),
            typeof(ThumbContentControl));

    /// <summary>
    /// Adds or remove a DragStartedEvent handler
    /// </summary>
    public event DragStartedEventHandler DragStarted
    {
        add => AddHandler(DragStartedEvent, value);
        remove => RemoveHandler(DragStartedEvent, value);
    }

    /// <summary>
    /// Adds or remove a DragDeltaEvent handler
    /// </summary>
    public event DragDeltaEventHandler DragDelta
    {
        add => AddHandler(DragDeltaEvent, value);
        remove => RemoveHandler(DragDeltaEvent, value);
    }

    /// <summary>
    /// Adds or remove a DragCompletedEvent handler
    /// </summary>
    public event DragCompletedEventHandler DragCompleted
    {
        add => AddHandler(DragCompletedEvent, value);
        remove => RemoveHandler(DragCompletedEvent, value);
    }

    private static readonly DependencyPropertyKey IsDraggingPropertyKey
        = DependencyProperty.RegisterReadOnly(nameof(IsDragging),
            typeof(bool),
            typeof(ThumbContentControl),
            new FrameworkPropertyMetadata(BooleanBoxes.FalseBox));

    /// <summary>
    /// DependencyProperty for the IsDragging property.
    /// </summary>
    public static readonly DependencyProperty IsDraggingProperty = IsDraggingPropertyKey.DependencyProperty;

    /// <summary>
    /// Indicates that the left mouse button is pressed and is over the MetroThumbContentControl.
    /// </summary>
    public bool IsDragging
    {
        get => (bool)GetValue(IsDraggingProperty);
        protected set => SetValue(IsDraggingPropertyKey, BooleanBoxes.Box(value));
    }

    public void CancelDragAction()
    {
        if (!IsDragging) return;

        if (IsMouseCaptured) ReleaseMouseCapture();

        ClearValue(IsDraggingPropertyKey);
        var horizontalChange = _oldDragScreenPoint.X - _startDragScreenPoint.X;
        var verticalChange = _oldDragScreenPoint.Y - _startDragScreenPoint.Y;
        RaiseEvent(new ThumbContentControlDragCompletedEventArgs(horizontalChange, verticalChange, true));
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        if (!IsDragging)
        {
            e.Handled = true;
            try
            {
                // focus me
                Focus();
                // now capture the mouse for the drag action
                CaptureMouse();
                // so now we are in dragging mode
                SetValue(IsDraggingPropertyKey, BooleanBoxes.TrueBox);
                // get the mouse points
                _startDragPoint = e.GetPosition(this);
                _oldDragScreenPoint = _startDragScreenPoint = PointToScreen(_startDragPoint);

                RaiseEvent(new ThumbContentControlDragStartedEventArgs(_startDragPoint.X, _startDragPoint.Y));
            }
            catch (Exception exception)
            {
                Trace.TraceError($"{this}: Something went wrong here: {exception} {Environment.NewLine} {exception.StackTrace}");
                CancelDragAction();
            }
        }

        base.OnMouseLeftButtonDown(e);
    }

    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        if (this.IsMouseCaptured && this.IsDragging)
        {
            e.Handled = true;
            // now we are in normal mode
            this.ClearValue(IsDraggingPropertyKey);
            // release the captured mouse
            this.ReleaseMouseCapture();
            // get the current mouse position and call the completed event with the horizontal/vertical change
            Point currentMouseScreenPoint = this.PointToScreen(e.MouseDevice.GetPosition(this));
            var horizontalChange = currentMouseScreenPoint.X - this._startDragScreenPoint.X;
            var verticalChange = currentMouseScreenPoint.Y - this._startDragScreenPoint.Y;
            this.RaiseEvent(new ThumbContentControlDragCompletedEventArgs(horizontalChange, verticalChange, false));
        }

        base.OnMouseLeftButtonUp(e);
    }

    private static void OnLostMouseCapture(object sender, MouseEventArgs e)
    {
        // Cancel the drag action if we lost capture
        ThumbContentControl thumb = (ThumbContentControl)sender;
        if (!ReferenceEquals(Mouse.Captured, thumb))
        {
            thumb.CancelDragAction();
        }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (!this.IsDragging)
        {
            return;
        }

        if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
        {
            Point currentDragPoint = e.GetPosition(this);
            // Get client point and convert it to screen point
            Point currentDragScreenPoint = this.PointToScreen(currentDragPoint);
            if (currentDragScreenPoint != this._oldDragScreenPoint)
            {
                this._oldDragScreenPoint = currentDragScreenPoint;
                e.Handled = true;
                var horizontalChange = currentDragPoint.X - this._startDragPoint.X;
                var verticalChange = currentDragPoint.Y - this._startDragPoint.Y;
                this.RaiseEvent(new DragDeltaEventArgs(horizontalChange, verticalChange) { RoutedEvent = ThumbContentControl.DragDeltaEvent });
            }
        }
        else
        {
            // clear some saved stuff
            if (ReferenceEquals(e.MouseDevice.Captured, this))
            {
                this.ReleaseMouseCapture();
            }

            this.ClearValue(IsDraggingPropertyKey);
            this._startDragPoint.X = 0;
            this._startDragPoint.Y = 0;
        }
    }

    protected override void OnPreviewTouchDown(TouchEventArgs e)
    {
        // Release any previous capture
        this.ReleaseCurrentDevice();
        // Capture the new touch
        this.CaptureCurrentDevice(e);
    }

    protected override void OnPreviewTouchUp(TouchEventArgs e)
    {
        this.ReleaseCurrentDevice();
    }

    protected override void OnLostTouchCapture(TouchEventArgs e)
    {
        // Only re-capture if the reference is not null
        // This way we avoid re-capturing after calling ReleaseCurrentDevice()
        if (this._currentDevice != null)
        {
            this.CaptureCurrentDevice(e);
        }
    }

    private void ReleaseCurrentDevice()
    {
        if (this._currentDevice != null)
        {
            // Set the reference to null so that we don't re-capture in the OnLostTouchCapture() method
            var temp = this._currentDevice;
            this._currentDevice = null;
            this.ReleaseTouchCapture(temp);
        }
    }

    private void CaptureCurrentDevice(TouchEventArgs e)
    {
        bool gotTouch = this.CaptureTouch(e.TouchDevice);
        if (gotTouch)
        {
            this._currentDevice = e.TouchDevice;
        }
    }

    protected override AutomationPeer OnCreateAutomationPeer()
    {
        return new MetroThumbContentControlAutomationPeer(this);
    }
}