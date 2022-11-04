using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using EngineerApplication.ContextStructure.Data;
using EngineerApplication.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EngineerApplication.Areas.Identity.Pages.Account
{
  [AllowAnonymous]
  public class ForgotPasswordModel : PageModel
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly EngineerDbContext _db;

    public ForgotPasswordModel(UserManager<IdentityUser> userManager, EngineerDbContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
      [Required]
      [EmailAddress]
      public string Email { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
      if (ModelState.IsValid)
      {
        var user = await _userManager.FindByEmailAsync(Input.Email);
        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        {
          // Don't reveal that the user does not exist or is not confirmed
          return RedirectToPage("./ForgotPasswordConfirmation");
        }

        var emailReceiver = user.Email;

        var email = new Email(new EmailParams
        {
          HostSmtp = "smtp.gmail.com",
          Port = 587,
          EnableSsl = true,
          SenderName = "Administrator Dropshipping Application",
          SenderEmail = "marcinkowalczyk24.7@gmail.com",
          SenderEmailPassword = "vkaqksszjcxkhkym"
        });

        await email.Send(
                  $"E'mail ze zmianą hasła",
                  $"Wysłano z aplikacji Application Dropshipping w celu potwierdzenia podania hasła: {user.PasswordHash}, dnia {DateTime.Now}. W sprawie kontaktu z administratorem prosimy o kontakt mailowy.",
                  emailReceiver);

        return RedirectToPage("./ForgotPasswordConfirmation");
      }

      return Page();
    }
  }
}
