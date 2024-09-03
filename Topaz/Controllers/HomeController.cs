using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Topaz.Models;

namespace Topaz.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationContext _context;

    public HomeController(ApplicationContext context)
    {
        _context = context;

        if (!_context.Companies.Any())
        {
            Company oko = new Company { Name = "oko" };
            Company dsoft = new Company { Name = "dsoft" };
            Company dno = new Company { Name = "dno" };
            Company kison = new Company { Name = "kison" };

            User u1 = new User { Name = "Tom", Age = 19, Company = oko };
            User u2 = new User { Name = "Bob", Age = 22, Company = dsoft };
            User u3 = new User { Name = "Alex", Age = 20, Company = oko };
            User u4 = new User { Name = "Tony", Age = 18, Company = dsoft };
            User u5 = new User { Name = "Kan", Age = 25, Company = oko };
            User u6 = new User { Name = "Tyn", Age = 27, Company = oko };
            User u7 = new User { Name = "Alice", Age = 34, Company = dno };
            User u8 = new User { Name = "Li", Age = 37, Company = kison };
            User u9 = new User { Name = "Den", Age = 28, Company = dno };
            User u10 = new User { Name = "Hin", Age = 30, Company = kison };

            _context.Companies.AddRange(oko, dsoft, dno, kison);
            _context.Users.AddRange(u1, u2, u3, u4, u5, u6, u7, u8, u9, u10);
            _context.SaveChanges();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Index(SortOrder sortOrder = SortOrder.NameAsc)
    {
        IQueryable<User> users = _context.Users.Include(x => x.Company);

        ViewData["NameSort"] = sortOrder == SortOrder.NameAsc ? SortOrder.NameDesc
            : SortOrder.NameAsc;
        ViewData["AgeSort"] = sortOrder == SortOrder.AgeAsc ? SortOrder.AgeDesc
            : SortOrder.AgeAsc;
        ViewData["CompanySort"] = sortOrder == SortOrder.CompanyAsc ? SortOrder.CompanyDesc
            : SortOrder.CompanyAsc;

        users = sortOrder switch
        {
            SortOrder.NameDesc => users.OrderByDescending(x => x.Name),
            SortOrder.AgeAsc => users.OrderBy(x => x.Age),
            SortOrder.AgeDesc => users.OrderByDescending(x => x.Age),
            SortOrder.CompanyAsc => users.OrderBy(x => x.Company!.Name),
            SortOrder.CompanyDesc => users.OrderByDescending(x => x.Company!.Name),
            _ => users.OrderBy(x => x.Name),
        };

        return View(await users.AsNoTracking().ToListAsync());
    }
}