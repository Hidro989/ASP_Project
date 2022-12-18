using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ShareEbook_v1.Models;

public class Table : ViewComponent
{
    public IViewComponentResult Invoke(List<Post> posts){
        return View(posts);
    }
}