﻿namespace Soloplan.WhatsON.GUI.SubjectTreeView
{
  using System;
  using System.Collections.ObjectModel;
  using System.Linq;
  using Soloplan.WhatsON.Serialization;

  public class SubjectTreeViewModel
  {
    private object lockObject = new object();

    private ObservableCollection<SubjectGroupViewModel> subjectGroups;

    public event EventHandler CountChanged;

    public ObservableCollection<SubjectGroupViewModel> SubjectGroups => this.subjectGroups ?? (this.subjectGroups = this.CreateSubjectGroupViewModelCollection());

    private ObservableCollection<SubjectGroupViewModel> CreateSubjectGroupViewModelCollection()
    {
      var subject = new ObservableCollection<SubjectGroupViewModel>();
      subject.CollectionChanged += this.OnSubjectCollectionChanged;

      return subject;
    }

    private void OnSubjectCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add || e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
      {
        this.CountChanged?.Invoke(this, EventArgs.Empty);
      }
    }

    public SubjectGroupViewModel FirstGroup => this.SubjectGroups.FirstOrDefault();

    public bool OneGroup => this.SubjectGroups.Count == 1;

    public void Init(ObservationScheduler scheduler, ApplicationConfiguration configuration)
    {
      scheduler.StatusQueried -= this.SchedulerStatusQueried;
      scheduler.StatusQueried += this.SchedulerStatusQueried;
      this.ParseConfiguration(configuration);
    }

    private void ParseConfiguration(ApplicationConfiguration configuration)
    {
      var grouping = configuration.SubjectsConfiguration.GroupBy(config => config.GetConfigurationByKey(Subject.Category)?.Value);
      foreach (var group in grouping)
      {
        var subjectGroupViewModel = new SubjectGroupViewModel();
        subjectGroupViewModel.Init(group);
        this.SubjectGroups.Add(subjectGroupViewModel);
      }
    }

    private void SchedulerStatusQueried(object sender, Subject e)
    {
      lock (this.lockObject)
      {
        foreach (var subjectGroupViewModel in this.SubjectGroups)
        {
          if (subjectGroupViewModel.Update(e))
          {
            return;
          }
        }
      }
    }
  }
}