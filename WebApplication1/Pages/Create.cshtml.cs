using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Pages
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;
        private ILogger<CreateModel> log;
        public CreateModel(AppDbContext db, ILogger<CreateModel> log)
        {
            _db = db;
            this.log = log;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public Customer Customer {get; set;}
        public void OnGet() { }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _db.Customers.Add(Customer);
            await _db.SaveChangesAsync();
            var msg = $"Customer {Customer.Name} added!";
            Message = msg;
            log.LogCritical(msg);
            return RedirectToPage("/Index");
        }
    }
}