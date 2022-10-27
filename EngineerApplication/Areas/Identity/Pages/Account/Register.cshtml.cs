using System.ComponentModel.DataAnnotations;
using EngineerApplication.Entities;
using EngineerApplication.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EngineerApplication.Areas.Identity.Pages.Account
{
  [Authorize]
  public class RegisterModel : PageModel
  {
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<RegisterModel> _logger;
    private readonly IEmailSender _emailSender;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RegisterModel(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        ILogger<RegisterModel> logger,
        IEmailSender emailSender,
        RoleManager<IdentityRole> roleManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _logger = logger;
      _emailSender = emailSender;
      _roleManager = roleManager;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }

    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    public class InputModel
    {
      [Required]
      [EmailAddress]
      [Display(Name = "Email")]
      public string Email { get; set; }

      [Required]
      [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Password")]
      public string Password { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = "Confirm password")]
      [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
      public string ConfirmPassword { get; set; }

      public string Name { get; set; }
      public string StreetAddress { get; set; }
      public string City { get; set; }
      public string State { get; set; }
      public string PostalCode { get; set; }

      public string PhoneNumber { get; set; }

    }

    public async Task OnGetAsync(string? returnUrl = null)
    {
      ReturnUrl = returnUrl;
      ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
      returnUrl ??= Url.Content("~/");
      ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
      if (ModelState.IsValid)
      {
        var user = new ApplicationUser
        {
          UserName = Input.Email,
          Email = Input.Email,
          Name = Input.Name,
          City = Input.City,
          StreetAddress = Input.StreetAddress,
          State = Input.State,
          PostalCode = Input.PostalCode,
          PhoneNumber = Input.PhoneNumber
        };
        var result = await _userManager.CreateAsync(user, Input.Password);
        if (result.Succeeded)
        {
          string role = Request.Form["rdUserRole"].ToString();

          if (role == UsefulConsts.Admin)
          {
            await _userManager.AddToRoleAsync(user, UsefulConsts.Admin);
            user.RoleName = UsefulConsts.Admin;
          }
          else
          {
            if (role == UsefulConsts.Customer)
            {
              await _userManager.AddToRoleAsync(user, UsefulConsts.Customer);
              user.RoleName = UsefulConsts.Customer;
            }
          }

          _logger.LogInformation("User created a new account with password.");

          if (_userManager.Options.SignIn.RequireConfirmedAccount)
          {
            var emailReceiver = user.Email;

            var email = new Email(new EmailParams
            {
              HostSmtp = "smtp.gmail.com",
              Port = 587,
              EnableSsl = true,
              SenderName = "Administrator",
              SenderEmail = "marcinkowalczyk24.7@gmail.com",
              SenderEmailPassword = "vkaqksszjcxkhkym"
            });

            await email.Send(
                      $"E'mail z potwierdzeniem założenia konta dla użytkownika {user.Name} z rolą {user.RoleName}",
                      $"Wysłano z aplikacji Application Dropshipping w celu potwierdzenia założenia konta dla użytkownika {user.Name}, dnia {DateTime.UtcNow}",
                      emailReceiver);

            return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
          }
          else
          {
            var emailReceiver = user.Email;

            var email = new Email(new EmailParams
            {
              HostSmtp = "smtp.gmail.com",
              Port = 587,
              EnableSsl = true,
              SenderName = "Administrator",
              SenderEmail = "marcinkowalczyk24.7@gmail.com",
              SenderEmailPassword = "vkaqksszjcxkhkym"
            });

            await email.Send(
                      $"E'mail z potwierdzeniem założenia konta dla użytkownika {user.Name} z rolą {user.RoleName}",
                      $"Wysłano z aplikacji Application Dropshipping w celu potwierdzenia założenia konta dla użytkownika {user.Name}, dnia {DateTime.UtcNow}",
                      emailReceiver);
            return LocalRedirect(returnUrl);
          }
        }
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }

      // If we got this far, something failed, redisplay form
      return Page();
    }
  }
}
