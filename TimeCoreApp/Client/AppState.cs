using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCoreApp.Shared;

namespace TimeCoreApp.Client
{
    public class AppState
    {
        public event Action OnChange;

        public List<TimeEntry> TimeEntries;
        public TimeEntry CurrentTimeEntry;

        public AppState()
        {
            TimeEntries = new List<TimeEntry>();
        }


        public void AddOrUpdateTime(TimeEntry Time)
        {
            Console.WriteLine($"----------AddOrUpdateTime:---------");
            foreach (var t in TimeEntries)
            {
                Console.WriteLine($"TimeList entry: {System.Text.Json.JsonSerializer.Serialize(t)}");
            }

            var existingTime = TimeEntries.FirstOrDefault(x => x.SystemId == Time.SystemId);
            Console.WriteLine($"Old time: {System.Text.Json.JsonSerializer.Serialize(existingTime)}");
            Console.WriteLine($"New time: {System.Text.Json.JsonSerializer.Serialize(Time)}");
            if (existingTime != null)
            {
                Console.WriteLine($"AddOrUpdateTime: Existing entry found, updating entry:");
                Console.WriteLine($"Old time: {System.Text.Json.JsonSerializer.Serialize(existingTime)}");
                Console.WriteLine($"New time: {System.Text.Json.JsonSerializer.Serialize(Time)}");
                existingTime = new TimeEntry()
                {
                    SystemId = Time.SystemId,
                    Title = string.IsNullOrWhiteSpace(Time.Title) ? "(no description)" :Time.Title,
                    StartTime = Time.StartTime,
                    EndTime = Time.EndTime
                };
            }
            else
            {
                Console.WriteLine($"AddOrUpdateTime: No existing entry found, adding new entry:");
                Console.WriteLine($"New time: {System.Text.Json.JsonSerializer.Serialize(Time)}");
                TimeEntries.Add(new TimeEntry() { 
                    SystemId = Time.SystemId,
                    Title = string.IsNullOrWhiteSpace(Time.Title) ? "(no description)" : Time.Title,
                    StartTime = Time.StartTime,
                    EndTime = Time.EndTime
                });
            }
            Console.WriteLine($"AppState has changed. Invoke OnChange event.");
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
