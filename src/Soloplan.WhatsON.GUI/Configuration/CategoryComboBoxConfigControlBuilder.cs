﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryComboBoxConfigControlBuilder.cs" company="Soloplan GmbH">
//   Copyright (c) Soloplan GmbH. All rights reserved.
//   Licensed under the MIT License. See License-file in the project root for license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Soloplan.WhatsON.GUI.Configuration
{
  using System.Windows;
  using System.Windows.Controls;
  using System.Windows.Data;
  using Soloplan.WhatsON.Configuration;
  using Soloplan.WhatsON.GUI.Configuration.ViewModel;
  using Soloplan.WhatsON.Model;

  /// <summary>
  /// The control builder for editable combo box with provided suggested values.
  /// </summary>
  /// <seealso cref="Soloplan.WhatsON.GUI.Configuration.IConfigControlBuilder" />
  public class CategoryComboBoxConfigControlBuilder : ComboBoxConfigControlBuilder
  {
    /// <summary>
    /// Gets the supported configuration items key.
    /// </summary>
    public override string SupportedConfigurationItemsKey => Connector.Category;

    /// <summary>
    /// Creates a new control and returns it.
    /// </summary>
    /// <param name="configItem">The configuration item of the connector.</param>
    /// <param name="configItemAttribute">The configuration item attribute.</param>
    /// <returns>
    /// Returns the <see cref="FrameworkElement" /> for the <see cref="configItem" />.
    /// </returns>
    public override FrameworkElement GetControlInternal(IConfigurationItem configItem, ConfigurationItemAttribute configItemAttribute)
    {
      var comboBox = (ComboBox)base.GetControlInternal(configItem, configItemAttribute);
      comboBox.IsEditable = true;
      var suggestedValueBinding = new Binding();
      suggestedValueBinding.Source = GlobalConfigDataViewModel.Instance;
      suggestedValueBinding.Path = new PropertyPath(nameof(GlobalConfigDataViewModel.UsedCategories));
      suggestedValueBinding.Mode = BindingMode.OneWay;
      suggestedValueBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
      BindingOperations.SetBinding(comboBox, ItemsControl.ItemsSourceProperty, suggestedValueBinding);
      return comboBox;
    }
  }
}