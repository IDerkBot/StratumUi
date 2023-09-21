// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Windows.Controls.Primitives;

namespace StratumUi.Wpf.Core.Controls;

public class ThumbContentControlDragCompletedEventArgs : DragCompletedEventArgs
{
    public ThumbContentControlDragCompletedEventArgs(double horizontalOffset, double verticalOffset, bool canceled)
        : base(horizontalOffset, verticalOffset, canceled)
    {
        RoutedEvent = ThumbContentControl.DragCompletedEvent;
    }
}