// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvertBooleanConverter.cs" company="Soloplan GmbH">
//   Copyright (c) Soloplan GmbH. All rights reserved.
//   Licensed under the MIT License. See License-file in the project root for license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Soloplan.WhatsON.Jenkins.GUI.Converters
{
    using Soloplan.WhatsON.GUI.Common.BuildServer;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

  public class BuildControlToVisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
            dynamic input;

            int x = 0;
            x = 2;
      try
            {
                input = (JenkinsStatusViewModel)value;
            }
      catch(Exception ex)
            {
                return false;
            }
      if (input == null)
      {
        return Visibility.Hidden;
      }
      else
      {
        if (input.Culprits.Count <= 0 && input.CommittedToThisBuild.Count <= 0)
        {
          return Visibility.Hidden;
        }
        else
        {
          return Visibility.Visible;
        }
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return Negate(value);
    }

    private static object Negate(object value)
    {
      if (value is bool boolean)
      {
        return !boolean;
      }

      return false;
    }
  }
}