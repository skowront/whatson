﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JenkinsProjectViewModel.cs" company="Soloplan GmbH">
//   Copyright (c) Soloplan GmbH. All rights reserved.
//   Licensed under the MIT License. See License-file in the project root for license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Soloplan.WhatsON.Jenkins.GUI
{
  using Soloplan.WhatsON.GUI.Common.BuildServer;
  using Soloplan.WhatsON.GUI.Common.ConnectorTreeView;
  using Soloplan.WhatsON.Model;

  public class JenkinsProjectViewModel : ConnectorViewModel
  {
    public JenkinsProjectViewModel(Connector connector)
      : base(connector)
    {
      this.CurrentStatus.OpenBuildPage.CanExecuteExternal += (s, e) => e.Cancel = !this.CurrentStatus.Building;
      this.Url = JenkinsApi.UrlHelper.ProjectUrl(connector);
    }

    /// <summary>
    /// Implements function that decides if a tooltip should be visible.
    /// </summary>
    /// <returns>Base returns always true.</returns>
    public override bool IsTooltipVisible()
    {
      if (this.CurrentStatus is JenkinsStatusViewModel status)
      {
        return (status.Culprits.Count == 0 && status.CommittedToThisBuild.Count == 0 && status.State != ObservationState.Running) ? false : true;
      }

      return true;
    }

    protected override BuildStatusViewModel GetStatusViewModel()
    {
      return new JenkinsStatusViewModel(this);
    }
  }
}
