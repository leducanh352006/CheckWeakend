using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CheckWeakend.Models;

namespace CheckWeakend.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index(DateTime? date)
    {
        var model = BuildWeekendViewModel(date);
        return View(model);
    }

    [HttpGet]
    [HttpPost]
    public IActionResult CheckWeekend(DateTime? date)
    {
        var model = BuildWeekendViewModel(date);
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private WeekendCheckViewModel BuildWeekendViewModel(DateTime? targetDate)
    {
        DateTime selectedDate = targetDate?.Date ?? DateTime.Today;
        DayOfWeek dayOfWeek = selectedDate.DayOfWeek;

        bool isWeekend = dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
        string message = isWeekend ? "Today is weekend!" : "today isn't weakend";

        string dayOfWeekVi = dayOfWeek switch
        {
            DayOfWeek.Monday => "Thứ Hai",
            DayOfWeek.Tuesday => "Thứ Ba",
            DayOfWeek.Wednesday => "Thứ Tư",
            DayOfWeek.Thursday => "Thứ Năm",
            DayOfWeek.Friday => "Thứ Sáu",
            DayOfWeek.Saturday => "Thứ Bảy",
            DayOfWeek.Sunday => "Chủ Nhật",
            _ => string.Empty
        };

        // Calculate days until next weekend (Saturday)
        int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)dayOfWeek + 7) % 7;
        if (isWeekend)
        {
            daysUntilSaturday = 0;
        }

        // Generate current week items (Monday to Sunday around selectedDate)
        int delta = dayOfWeek == DayOfWeek.Sunday ? -6 : 1 - (int)dayOfWeek;
        DateTime monday = selectedDate.AddDays(delta);

        var weekDays = new List<WeekDayItemViewModel>();
        for (int i = 0; i < 7; i++)
        {
            DateTime currentDay = monday.AddDays(i);
            bool currentIsWeekend = currentDay.DayOfWeek == DayOfWeek.Saturday || currentDay.DayOfWeek == DayOfWeek.Sunday;
            
            string shortName = currentDay.DayOfWeek switch
            {
                DayOfWeek.Monday => "T2",
                DayOfWeek.Tuesday => "T3",
                DayOfWeek.Wednesday => "T4",
                DayOfWeek.Thursday => "T5",
                DayOfWeek.Friday => "T6",
                DayOfWeek.Saturday => "T7",
                DayOfWeek.Sunday => "CN",
                _ => string.Empty
            };

            weekDays.Add(new WeekDayItemViewModel
            {
                Date = currentDay,
                DayName = shortName,
                DayNumber = currentDay.Day,
                IsWeekend = currentIsWeekend,
                IsToday = currentDay.Date == DateTime.Today,
                IsSelected = currentDay.Date == selectedDate
            });
        }

        return new WeekendCheckViewModel
        {
            SelectedDate = selectedDate,
            IsWeekend = isWeekend,
            Message = message,
            DayOfWeekVietnamese = dayOfWeekVi,
            DaysUntilWeekend = daysUntilSaturday,
            WeekDays = weekDays
        };
    }
}
