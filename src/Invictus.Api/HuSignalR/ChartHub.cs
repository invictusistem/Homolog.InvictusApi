using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Invictus.Api.HuSignalR
{
    public class ChartHub : Hub
    {
        //public async Task BroadcastChartData(List<ChartModel> data)
        //{
        //    await Clients.All.SendAsync("broadcastchartdata", data);
        //}
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }

        public async Task BroadcastChartData(List<ChartModel> data, string connectionId)
        {

            await Clients.Client(connectionId).SendAsync("broadcastchartdata", data);
        }


        public async Task BroadcastToConnection(string data, string connectionId)
        {
            await Clients.Client(connectionId).SendAsync("broadcasttoclient", data);
        }

        public async Task BroadcastToUser(string data, string userId)
        {
            await Clients.User(userId).SendAsync("broadcasttouser", data);
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task BroadcastToGroup(string groupName)
        {
            await Clients.Group(groupName).SendAsync("broadcasttogroup", $"{Context.ConnectionId} has joined the group {groupName}.");
        }
    }

    public class ChartModel
    {
        public List<int> Data { get; set; }
        public string Label { get; set; }

        public ChartModel()
        {
            Data = new List<int>();
        }
    }

    public class TimerManager
    {
        private Timer _timer;
        private AutoResetEvent _autoResetEvent;
        private Action _action;

        public DateTime TimerStarted { get; }

        public TimerManager(Action action)
        {
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, _autoResetEvent, 1000, 2000);
            TimerStarted = DateTime.Now;
        }

        public void Execute(object stateInfo)
        {
            _action();

            if ((DateTime.Now - TimerStarted).Seconds > 60)
            {
                _timer.Dispose();
            }
        }
    }
}
