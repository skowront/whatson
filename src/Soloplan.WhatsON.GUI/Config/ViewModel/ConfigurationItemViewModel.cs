﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigurationItemViewModel.cs" company="Soloplan GmbH">
//   Copyright (c) Soloplan GmbH. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Soloplan.WhatsON.GUI.Config.ViewModel
{
  /// <summary>
  /// The view model for see <see cref="ConfigurationItem"/>.
  /// </summary>
  public class ConfigurationItemViewModel : ViewModelBase
  {
    /// <summary>
    /// The key.
    /// </summary>
    private string key;

    /// <summary>
    /// The value.s
    /// </summary>
    private string value;

    /// <summary>
    /// Gets the key.
    /// </summary>
    public string Key
    {
      get => this.key;
      private set
      {
        this.key = value;
        this.OnPropertyChanged();
      }
    }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public string Value
    {
      get => this.value;
      set
      {
        this.value = value;
        this.OnPropertyChanged();
      }
    }

    /// <summary>
    /// Gets the original configuration item.
    /// </summary>
    public ConfigurationItem ConfigurationItem { get; private set; }

    /// <summary>
    /// Loads the configuration item view model from the source object.
    /// </summary>
    /// <param name="configurationItemSource">The configuration item source.</param>
    public void Load(ConfigurationItem configurationItemSource)
    {
      this.IsLoaded = false;
      try
      {
        this.ConfigurationItem = configurationItemSource;
        this.Key = configurationItemSource.Key;
        this.Value = configurationItemSource.Value;
      }
      finally
      {
        this.IsLoaded = true;
      }
    }

    /// <summary>
    /// Loads the configuration item view model from the source object.
    /// </summary>
    /// <param name="itemKey">The key.</param>
    public void Load(string itemKey)
    {
      this.IsLoaded = false;
      try
      {
        this.Key = itemKey;
      }
      finally
      {
        this.IsLoaded = true;
      }
    }

    /// <summary>
    /// Applies modifications to source.
    /// </summary>
    /// <param name="newItemCreated">if set to <c>true</c> a new item was created.</param>
    public void ApplyToSource(out bool newItemCreated)
    {
      newItemCreated = false;
      if (this.ConfigurationItem == null)
      {
        this.ConfigurationItem = new ConfigurationItem(this.key);
        newItemCreated = true;
      }

      this.ConfigurationItem.Value = this.value;
    }
  }
}