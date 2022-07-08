using Newtonsoft.Json;
using OnlineShoppingStore.DAL;
using OnlineShoppingStore.Models;
using OnlineShoppingStore.Models.Home;
using OnlineShoppingStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShoppingStore.Controllers;
using System.Net.Mail;
using System.Net;
namespace OnlineShoppingStore.Controllers
{
    public class HomeController : Controller
    {
        dbMyOnlineShoppingEntities ctx = new dbMyOnlineShoppingEntities();
        public ActionResult Index(string search,int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search,4, page));
        }
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult CheckoutDetails()
        {
            return View();
        }
        public ActionResult DecreaseQty(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = ctx.Tbl_Product.Find(productId);
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = prevQty - 1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");
        }
        public ActionResult AddToCart(int productId,string url)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = ctx.Tbl_Product.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var count = cart.Count();
                var product = ctx.Tbl_Product.Find(productId);
                for (int i = 0; i < count;i++ )
                {
                    if (cart[i].Product.ProductId == productId)
                    {
                        int prevQty = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cart.Where(x => x.Product.ProductId == productId).SingleOrDefault();
                        if (prd == null)
                        {
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });
                        }
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect(url);
        }
        public ActionResult RemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
        }
        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(ContactFormModel contact)
        {
            var result = false;
            try
            {
               
                var fromMail = new MailAddress("Vidyaponugoti31@gmail.com", "vidya"); // set your email    
                var fromEmailpassword = "vidya3108"; // Set your password     
                var toEmail = new MailAddress("sindukondabala0610@gmail.com");
                var Subject = contact.Subject;
                var smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = true;
                smtp.UseDefaultCredentials = true;
                NetworkCredential NetworkCred = new NetworkCredential(fromMail.Address, fromEmailpassword);
                smtp.Credentials = NetworkCred;

                var Message = new MailMessage(fromMail, toEmail);
                Message.Subject = Subject;

                string userMessage = "";
                userMessage = "<br/>Name :" + contact.FirstName + "" + contact.LastName;
                userMessage = userMessage + "<br/>Email Id: " + contact.Email;

                userMessage = userMessage + "<br/>Message: " + contact.Body;

                string Body = "Hi, <br/><br/> A new enquiry by user. Detail is as follows:<br/><br/> " + userMessage + "<br/><br/>Thanks";

                Message.Body = Body;

                Message.IsBodyHtml = true;

                smtp.Send(Message);
                result = true;
                ViewBag.Message = "Email sent sucessfully.";

            }
            catch(Exception ex)
            {
                result = false;
                ViewBag.Message = "Please Contact Technical Team...";
            }

             return Json(result, JsonRequestBehavior.AllowGet);
        }

        //[AllowAnonymous]
        //public ActionResult UserVerification()
        //{
        //    return View();
        //}

        [AllowAnonymous]
        public ActionResult UserVerification(string id)
        {
            bool Status = false;
            var VerifyMsg = "";
            using (var Context = new dbMyOnlineShoppingEntities())
            {
                Context.Configuration.ValidateOnSaveEnabled = false; // Ignor to password confirmation     
                var IsVerify = Context.Tbl_Registration.Where(u => u.OTP.ToString() == id).FirstOrDefault();

                if (IsVerify != null)
                {
                    IsVerify.EmailVerification = true;
                    Context.SaveChanges();
                    VerifyMsg = "Email Verification completed";
                    ViewBag.Status = true;
                }
                else
                {
                    VerifyMsg = "Invalid Request...Email not verify";
                    ViewBag.Status = false;
                }
            }
            ViewBag.Message = VerifyMsg;
            return View();
        }


        [AllowAnonymous]
        public ActionResult ResetPassword(string userName)
        {
            bool Status = false;
            var VerifyMsg = "";
            using (var Context = new dbMyOnlineShoppingEntities())
            {
                Context.Configuration.ValidateOnSaveEnabled = false; // Ignor to password confirmation     
                var IsVerify = Context.Tbl_Registration.Where(u => u.EmailId== userName).FirstOrDefault();

                if (IsVerify != null)
                {
                    IsVerify.EmailVerification = true;
                    Context.SaveChanges();
                    VerifyMsg = "Email Verification completed";
                    Status = true;
                }
                else
                {
                    VerifyMsg = "Invalid Request...Email not verify";
                    Status = false;
                }
            }
            ViewBag.Message = VerifyMsg;
            ViewBag.Status = Status;
            return View();
        }
    }
}