﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Windows.Input;

namespace StratumUi.Wpf.Core.Controls;

public class Thumb : System.Windows.Controls.Primitives.Thumb, IThumb
{
    private TouchDevice? _currentDevice;

    protected override void OnPreviewTouchDown(TouchEventArgs e)
    {
        // Release any previous capture
        ReleaseCurrentDevice();
        // Capture the new touch
        CaptureCurrentDevice(e);
    }

    protected override void OnPreviewTouchUp(TouchEventArgs e)
    {
        ReleaseCurrentDevice();
    }

    protected override void OnLostTouchCapture(TouchEventArgs e)
    {
        // Only re-capture if the reference is not null
        // This way we avoid re-capturing after calling ReleaseCurrentDevice()
        if (_currentDevice != null)
        {
            CaptureCurrentDevice(e);
        }
    }

    private void ReleaseCurrentDevice()
    {
        if (_currentDevice == null) return;
        // Set the reference to null so that we don't re-capture in the OnLostTouchCapture() method
        var temp = _currentDevice;
        _currentDevice = null;
        ReleaseTouchCapture(temp);
    }

    private void CaptureCurrentDevice(TouchEventArgs e)
    {
        var gotTouch = CaptureTouch(e.TouchDevice);
        if (gotTouch) _currentDevice = e.TouchDevice;
    }
}