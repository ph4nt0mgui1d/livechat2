using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using livechat2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using livechat2.Hubs;
using Microsoft.EntityFrameworkCore;
using livechat2.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;

namespace livechat2.Controllers;
[Authorize]
public class HomeController : Controller
{   private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<HomeController> _logger;
    private readonly IHubContext<ChatHub> _chathub;
    private readonly IActionResult currentUser;
    public HomeController(ILogger<HomeController> logger, IHubContext<ChatHub> chathub, ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _chathub = chathub;
        _dbContext = dbContext;
        _userManager = userManager;
    }
        
    [HttpGet]
    public IActionResult Index()
    {

        return View();
    }
    
    [HttpPost]

    public async Task<IActionResult> AddMsg([Bind($"text,sender,{}")] Message message)
    //public JsonResult AddMsg()
    {
         await _chathub.Clients.All.SendAsync("ReceiveMsg", message.text);
        _dbContext.UserChat.Add(message);
        return View();
    }

    [HttpGet]
    public JsonResult GetUser()
    {
        var currentUser = _userManager.GetUserId(User);
        var users = _dbContext.Users
        .Where(item => item.Id != currentUser);
    
        //var users = _dbContext.Users.ToList();
        return Json(users);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

