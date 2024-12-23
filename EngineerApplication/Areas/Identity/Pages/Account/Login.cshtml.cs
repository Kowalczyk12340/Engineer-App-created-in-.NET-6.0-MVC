﻿using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace EngineerApplication.Areas.Identity.Pages.Account
{
  [AllowAnonymous]
  public class LoginModel : PageModel
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<LoginModel> _logger;
    private readonly IEmailSender _emailSender;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserClaimsPrincipalFactory<IdentityUser> _userPrincipal;
    private readonly IAuthenticationSchemeProvider _authProvider;
    private readonly IUserConfirmation<IdentityUser> _userConfirmation;
    private readonly IOptions<IdentityOptions> _options;
    private readonly ILogger<SignInManager<IdentityUser>> _loggerV;

    public LoginModel(SignInManager<IdentityUser> signInManager,
        ILogger<LoginModel> logger,
        UserManager<IdentityUser> userManager,
        IEmailSender emailSender,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<IdentityUser> userPrincipal,
        IAuthenticationSchemeProvider authProvider,
        IUserConfirmation<IdentityUser> userConfirmation,
        IOptions<IdentityOptions> options,
        ILogger<SignInManager<IdentityUser>> loggerV)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _emailSender = emailSender;
      _logger = logger;
      _contextAccessor = contextAccessor;
      _authProvider = authProvider;
      _userPrincipal = userPrincipal;
      _userConfirmation = userConfirmation;
      _options = options;
      _loggerV = loggerV;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    public string ReturnUrl { get; set; }

    [TempData]
    public string ErrorMessage { get; set; }

    public class InputModel
    {
      [Required]
      [EmailAddress]
      public string Email { get; set; }

      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      [Display(Name = "Remember me?")]
      public bool RememberMe { get; set; }
    }

    public async Task OnGetAsync(string? returnUrl = null)
    {
      if (!string.IsNullOrEmpty(ErrorMessage))
      {
        ModelState.AddModelError(string.Empty, ErrorMessage);
      }

      returnUrl ??= Url.Content("~/");

      // Clear the existing external cookie to ensure a clean login process
      await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

      ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

      ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
      returnUrl ??= Url.Content("~/");

      if (ModelState.IsValid)
      {
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
        if (result.Succeeded)
        {
          _logger.LogInformation("User logged in.");
          return LocalRedirect(returnUrl);
        }
        if (result.RequiresTwoFactor)
        {
          return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
        }
        if (result.IsLockedOut)
        {
          _logger.LogWarning("User account locked out.");
          return RedirectToPage("./Lockout");
        }
        else
        {
          ModelState.AddModelError(string.Empty, "Invalid login attempt.");
          return Page();
        }
      }

      // If we got this far, something failed, redisplay form
      return Page();
    }

    public async Task<IActionResult> OnPostSendVerificationEmailAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      var user = await _userManager.FindByEmailAsync(Input.Email);
      if (user == null)
      {
        ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
      }

      var userId = await _userManager.GetUserIdAsync(user);
      var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
      var callbackUrl = Url.Page(
          "/Account/ConfirmEmail",
          pageHandler: null,
          values: new { userId, code },
          protocol: Request.Scheme);
      await _emailSender.SendEmailAsync(
          Input.Email,
          "Confirm your email",
          $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

      ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
      return Page();
    }
  }
}
