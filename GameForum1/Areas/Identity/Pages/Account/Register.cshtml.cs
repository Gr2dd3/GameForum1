// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using GameForum1.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace GameForum1.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly GameForum1Context _context;    //new
        private readonly IWebHostEnvironment _webHostEnvironment;  //new
        private readonly SignInManager<GameForum1User> _signInManager;
        private readonly UserManager<GameForum1User> _userManager;
        private readonly IUserStore<GameForum1User> _userStore;
        private readonly IUserEmailStore<GameForum1User> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            GameForum1Context context,               //new
            IWebHostEnvironment webHostEnvironment, // new
            UserManager<GameForum1User> userManager,
            IUserStore<GameForum1User> userStore,
            SignInManager<GameForum1User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _webHostEnvironment = webHostEnvironment;  //new
            _context = context;                        //new
            GeneratedName = "";
        }

        [BindProperty]
        public string GeneratedName { get; set; }

        public GameForum1User MyUser { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// 
        public async Task OnPostGenerateAsync()
        {
            GeneratedName = await DAL.Data.GenerateNickNameAsync();
        }
        public class InputModel
        {
            [BindProperty]
            public FileViewModel FileUpload { get; set; }

            [Required]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Username")]
            public string NickName { get; set; }

            [Required]
            [Display(Name = "Birthyear")]
            public int BirthYear { get; set; }
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            /// 
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                //var user = CreateUser();

                var user = new GameForum1User
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Email = Input.Email,
                    NickName = Input.NickName,
                    BirthYear = Input.BirthYear,
                };

                await _userStore.SetUserNameAsync(user, Input.NickName, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (Input.FileUpload != null)
                    {

                        if (Input.FileUpload.FormFile.Length > 0)  //Upload file to folder
                        {
                            using (var stream = new FileStream(Path.Combine(_webHostEnvironment.WebRootPath, "uploadfiles", Input.FileUpload.FormFile.FileName), FileMode.Create))
                            {
                                await Input.FileUpload.FormFile.CopyToAsync(stream);
                            }
                        }

                        using (var memoryStream = new MemoryStream())
                        {
                            await Input.FileUpload.FormFile.CopyToAsync(memoryStream);

                            //Upload if less that 2 MB
                            if (memoryStream.Length < 2097152)
                            {



                                var file = new AppFile()
                                {
                                    UserId = user.Id,
                                    FileName = Input.FileUpload.FormFile.FileName,
                                    Content = memoryStream.ToArray()
                                };
                                _context.File.Add(file);

                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                ModelState.AddModelError("File", "File can't be larger than 2MB.");
                            }
                        }


                    }



                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
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

        private GameForum1User CreateUser()
        {
            try
            {
                return Activator.CreateInstance<GameForum1User>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(GameForum1User)}'. " +
                    $"Ensure that '{nameof(GameForum1User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<GameForum1User> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<GameForum1User>)_userStore;
        }
    }
}
