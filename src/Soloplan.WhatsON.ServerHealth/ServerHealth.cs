﻿namespace Soloplan.WhatsON.ServerHealth
{
  using System;
  using System.Net.NetworkInformation;
  using System.Text;
  using Soloplan.WhatsON.ServerBase;

  [SubjectType("Server Health Check", Description = "Ping a server and return the state depending on the reply.")]
  public class ServerHealth : ServerSubject
  {
    public ServerHealth(string name, string serverAdress)
      : base(name, serverAdress)
    {
    }

    protected override void ExecuteQuery(params string[] args)
    {
      var ping = new Ping();
      var options = new PingOptions();

      // Create a buffer of 32 bytes of data to be transmitted.
      var buffer = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

      try
      {
        var reply = ping.Send(this.Address, 120, buffer, options);

        var state = ObservationState.Unknown;
        if (reply?.Status == IPStatus.Success)
        {
          state = ObservationState.Success;
        }
        else if (reply != null)
        {
          state = ObservationState.Failure;
        }

        var newStatus = new Status(state) { Name = $"Pinging {this.Address} ({reply?.RoundtripTime}ms)", Time = DateTime.Now };
        this.CurrentStatus = newStatus;
      }
      catch (PingException ex)
      {
        var newStatus = new Status(ObservationState.Failure) { Name = $"{this.Address}: {ex.Message}", Time = DateTime.Now, Detail = ex.InnerException?.Message };
        this.CurrentStatus = newStatus;
      }
    }
  }
}