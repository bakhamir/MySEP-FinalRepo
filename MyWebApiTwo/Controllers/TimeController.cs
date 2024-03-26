using Microsoft.AspNetCore.Mvc;
using Newtonsoft;
namespace MyWebApiTwo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeController : Controller
    {
            [HttpPost("save")]
            public IActionResult SaveCookieData()
            {
              
                DateTime currentTime = DateTime.Now;
                int monthNumber = currentTime.Month;
                int weekNumber = System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(currentTime, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
                DayOfWeek dayOfWeek = currentTime.DayOfWeek;
                string season = GetSeason(currentTime);
                int dayOfYear = currentTime.DayOfYear;

                var cookieData = new
                {
                    CurrentTime = currentTime,
                    MonthNumber = monthNumber,
                    WeekNumber = weekNumber,
                    DayOfWeek = dayOfWeek,
                    Season = season,
                    DayOfYear = dayOfYear
                };

                Response.Cookies.Append("MyCookie", Newtonsoft.Json.JsonConvert.SerializeObject(cookieData));

                return Ok();
            }

            [HttpGet("retrieve")]
            public IActionResult RetrieveCookieData()
            {
                // Получаем данные из куки
                if (Request.Cookies.TryGetValue("MyCookie", out string cookieValue))
                {
                    var cookieData = Newtonsoft.Json.JsonConvert.DeserializeObject(cookieValue);
                    return Ok(cookieData);
                }
                else
                {
                    return NotFound("Cookie not found");
                }
            }

            private string GetSeason(DateTime date)
            {
                int month = date.Month;
                if (month >= 3 && month <= 5)
                    return "Spring";
                else if (month >= 6 && month <= 8)
                    return "Summer";
                else if (month >= 9 && month <= 11)
                    return "Autumn";
                else
                    return "Winter";
            }
        
    }
}
