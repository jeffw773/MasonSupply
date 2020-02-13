using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mason_Supply.Models;
using Mason_Supply.Data;

namespace Mason_Supply.Controllers
{
    public class HomeController : Controller
    {
        IOrderRepository orderRepo;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IOrderRepository o)
        {
            _logger = logger;
            orderRepo = o;
        }

        public IActionResult Index()
        {
            return View(orderRepo.Orders);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public ViewResult ShapeInput()
        {
            ShapeInputView SIV = new ShapeInputView();

            //New order gets added to Repo upon visitation....Which may be bad......Especially when you just leave.
            orderRepo.AddOrder(SIV.Order); //BUG found: Even if you go back to the index without entering any shape info it will create a blank order
            return View("ShapeInput", SIV);
        }


        //[HttpPost]
        //public ActionResult Index(string[] dynamicField)
        //{
        //    ViewBag.Data = string.Join(",", dynamicField ?? new string[] { });
        //    return View();
        //}



        [HttpPost]
        public ViewResult ShapeInput(ShapeInputView siv)
        {
            //If siv order doesn't exist in orderRepo then add it
            //for(int i = 0; i < orderRepo.Orders.Count; i++)
            //{
            //    if(siv.Order.OrderID == orderRepo.Orders[i].OrderID)
            //    {
            //        //DO NOTHING IF IT EXISTS ALREADY
            //    }
            //    else if(i == orderRepo.Orders.Count - 1 && siv.OrderID != orderRepo.Orders[i].OrderID)
            //    {
            //        orderRepo.AddOrder(siv.Order); //add the fresh order to the orderRepo
            //    }
            //}

            siv.Order = orderRepo.GetOrderByID(siv.OrderID);
            //siv.Order.AddOrderShape(siv.Shape);
            orderRepo.AddShape(siv.Order, siv.Shape);
            
            return View("ShapeInput", siv);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
