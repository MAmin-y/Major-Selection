using MajorSelection.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MajorSelection.Models.Domain;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using MajorSelection.Models.ViewModel;

namespace MajorSelection.Pages.Majors;
[Authorize]
public class ListModel : PageModel
{
    private readonly IMajorRepository _majorRepository;
    public List<Major> Majors { get; set; }
    public ListModel(IMajorRepository majorRepository)
    {
        _majorRepository = majorRepository;
    }

    public async Task OnGet()
    {
        var notificationJson = (string)TempData["Notification"];
        if (notificationJson != null)
        {
            ViewData["Notification"] = JsonSerializer.Deserialize<MyNotification>(notificationJson);
        }

        Majors = (await _majorRepository.GetAllAsync())?.ToList();
    }

}