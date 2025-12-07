using Blog.Config;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        //right-click in Controllers folder -> Add -> Controller -> MVC Controller - Empty -> Name: PostController -> Add
        
        public IActionResult Index() // right-click in Index() -> Add View -> Leave everything as default -> Add
        {
            return View();
        }

        //get post by id
        public IActionResult GetById(int id) { 
            return View();
        }

        public IActionResult CreatePost(PostClass post)
        {
            return View();
        }
    }
}
