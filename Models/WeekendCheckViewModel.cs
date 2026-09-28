using System;
using System.Collections.Generic;

namespace CheckWeakend.Models;

public class WeekDayItemViewModel
{
    public DateTime Date { get; set; }
    public string DayName { get; set; } = string.Empty;
    public int DayNumber { get; set; }
    public bool IsWeekend { get; set; }
    public bool IsToday { get; set; }
    public bool IsSelected { get; set; }
}

public class WeekendCheckViewModel
{
    public DateTime SelectedDate { get; set; } = DateTime.Today;
    public bool IsWeekend { get; set; }
    public string Message { get; set; } = string.Empty;
    public string DayOfWeekVietnamese { get; set; } = string.Empty;
    public int DaysUntilWeekend { get; set; }
    public List<WeekDayItemViewModel> WeekDays { get; set; } = new();
}
