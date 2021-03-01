﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressBarTooltipControl.xaml.cs" company="Soloplan GmbH">
//   Copyright (c) Soloplan GmbH. All rights reserved.
//   Licensed under the MIT License. See License-file in the project root for license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Soloplan.WhatsON.GUI.Common.BuildServer
{
  using System.Windows;
  using System.Windows.Controls;

  /// <summary>
  /// Control for project tooltips.
  /// </summary>
  public partial class ProgressBarTooltipControl : UserControl
  {
    /// <summary>
    /// Dependency property for <see cref="CulpritsProp"/>.
    /// </summary>
    public static readonly DependencyProperty ControlOrientationTooltipProperty = DependencyProperty.Register(nameof(ControlOrientation), typeof(Orientation), typeof(ProgressBarTooltipControl), new PropertyMetadata(Orientation.Vertical));

    public static readonly DependencyProperty CompactDisplayTooltipProperty = DependencyProperty.Register(nameof(CompactDisplay), typeof(bool), typeof(ProgressBarTooltipControl), new PropertyMetadata(false, new PropertyChangedCallback(OnCurrentReadingChanged)));

    public ProgressBarTooltipControl()
    {
      this.InitializeComponent();
    }

    private static void OnCurrentReadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is ProgressBarTooltipControl control)
      {
        control.UpdateTexts();
      }
    }

    public Orientation ControlOrientation
    {
      get => (Orientation)this.GetValue(ControlOrientationTooltipProperty);
      set => this.SetValue(ControlOrientationTooltipProperty, value);
    }

    public bool CompactDisplay
    {
      get => (bool)this.GetValue(CompactDisplayTooltipProperty);
      set => this.SetValue(CompactDisplayTooltipProperty, value);
    }

    public void UpdateTexts()
    {
      if (this.CompactDisplay)
      {
        this.CompletionText.Text = string.Empty;
        this.EstimatedRemainingText.Text = " ETA";
        this.PercentSignText.Text = "%/";
      }
      else
      {
        this.CompletionText.Text = "Completion: ";
        this.EstimatedRemainingText.Text = " estimated remaining";
        this.PercentSignText.Text = "% ";
      }
    }
  }
}
