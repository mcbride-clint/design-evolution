using DesignApp.Application.Services;
using DesignApp.Domain.Models;
using DesignApp.Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace DesignApp.Pages.Users
{
    public class UserMaintModel : PageModel
    {
        /*
         * Simulates our current structure in our Application with a basic page.
         * It can see all the UserIds in the Database and Edit the whole List that is sent back.
         * This is not a working example.
         */

        /// <summary>
        /// Used as the Property on UserMainModel to Pass Data to the Front End
        /// </summary>
        public List<User> Users { get; set; }


        /// <summary>
        /// This would be our "Grid Page" but with strong typing on our data instead of a DataTable.  
        /// Runs Sql to get the full list and gives it to the UserMaintModel Properties to Go to the Front End.
        /// </summary>
        /// <returns></returns>
        public void OnGet()
        {
            UserRepository userRepo = new UserRepository();
            UserService service = new UserService(userRepo);
            Users = service.GetAllUsers();
        }

        /// <summary>
        /// Simulates one of our "Save Pages"
        /// Receives All the Data From Front End to Save
        /// </summary>
        /// <param name="row"></param>
        public void OnPost()
        {
            UserRepository userRepo = new UserRepository();
            UserService service = new UserService(userRepo);
            service.SaveAllUsers(Users);
        }


        /*
         * Pros:
         * Everything is right here.
         * No Chance of breaking other pages since everything is Silo'd(sp?)
         * Strongly Typed Objects are easier to work with than DataTables or Arrays from the database
         * 
         * Cons:
         * All the Code is very rigid and stuck to the UI
         * No way to verify the code is working correctly without starting the website and testing every possible input
         */
    }
}