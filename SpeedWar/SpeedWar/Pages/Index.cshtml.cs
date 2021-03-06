using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpeedWar.Models;
using SpeedWar.Models.Interfaces;
using SpeedWar.Models.Services;

namespace SpeedWar.Pages
{
    public class IndexModel : PageModel
    {
        public IUserManager _user { get; set; }
        public IndexModel(IUserManager user)
        {
            _user = user;
        }


        [BindProperty]
        public string Username { get; set; }
        public User Player { get; set; }

        public void OnGet()
        {
        }
        /// <summary>
        ///  Find the player from database by username and render the play Index page with username show up at the upleft corner
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPost()
        {
            Player = await _user.GetUserAsync(Username);
            return RedirectToPage("/Play/Index",Player);
        }
    }
}