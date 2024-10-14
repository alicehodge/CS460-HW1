using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CS460_HW1.Models;

namespace CS460_HW1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Submit(TeamForm form)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", form);
        }

        var names = form.Names.Split('\n').Select(n => n.Trim()).ToList();
        var sortedTeams = SortNamesIntoTeams(names, form.TeamSize);
        return View("Teams", sortedTeams);
    }

    private List<Team> SortNamesIntoTeams(List<string> names, int teamSize)
    {
        var teams = new List<Team>();
        var shuffledNames = names.OrderBy(n => System.Guid.NewGuid()).ToList();

        for (int i = 0; i < shuffledNames.Count; i += teamSize)
        {
            teams.Add(new Team
            {
                TeamNumber = teams.Count + 1,
                Members = shuffledNames.Skip(i).Take(teamSize).ToList()
            });
        }

        return teams;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
