
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToursansTripArange.Models;


namespace ToursProject.Controllers
{
   
    public class HomeController : Controller
    {
        Database1Entities5 db = new Database1Entities5();
      [Authorize]
        public ActionResult Index()
        {
            return View();
        }
      public ActionResult shownews()
      {
          return View(db.News.ToList());
      }
      public ActionResult addnews()
      {
          return View();

      }
      public ActionResult addnewsconfirm(News n)
      {
          db.News.Add(n);
          db.SaveChanges();
          return RedirectToAction("shownews");

      }
      public ActionResult deletenews(int id)
      {
          News p = db.News.Find(id);
          db.News.Remove(p);
          db.SaveChanges();

          return RedirectToAction("shownews");

      }
      public ActionResult addcontact(Contact c)
      {
          if (ModelState.IsValid)
          {
              db.Contacts.Add(c);
              db.SaveChanges();
              return RedirectToAction("Index");
          }
          return View("Contact");
      }


      public ActionResult deletecontact(int id)
      {
          Contact p = db.Contacts.Find(id);
          db.Contacts.Remove(p);
          db.SaveChanges();

          return RedirectToAction("Contacts");
      }
        [Authorize]
      public ActionResult News()
      {
          return View(db.News.ToList());
      }

      public ActionResult Contacts()
      {
          return View(db.Contacts.ToList());
      }
      public ActionResult TripBooking()
      {
          return View(db.bookings.ToList());
      }
      public ActionResult deletebooking(int id)
      {
          booking p = db.bookings.Find(id);
          db.bookings.Remove(p);
          db.SaveChanges();

          return RedirectToAction("TripBooking");
      }
        [Authorize]
      public ActionResult Addbooking()
      {
          return View();
      }
        public ActionResult UploadFiles()
        {
            // Returns message that successfully uploaded  
            return View();
        }
        [HttpPost]
        public ActionResult UploadFiles(int id)
        {
            String fileName = null;
        
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                            fileName = fname;
                        }
                        else
                        {
                            fname = file.FileName;
                            fileName = fname;
                        }

                        // Get the complete folder path and store the file inside it.  



                        string extensioin = fname.Substring(fname.LastIndexOf(".") + 1);
                        string fileType = null;
                        //set the file type based on File Extension
                        switch (extensioin)
                        {
                            case "doc":
                                fileType = "application/vnd.ms-word";
                                break;
                            case "docx":
                                fileType = "application/vnd.ms-word";
                                break;
                            case "xls":
                                fileType = "application/vnd.ms-excel";
                                break;
                            case "xlsx":
                                fileType = "application/vnd.ms-excel";
                                break;
                            case "jpg":
                                fileType = "image/jpg";
                                break;
                            case "png":
                                fileType = "image/png";
                                break;
                            case "gif":
                                fileType = "image/gif";
                                break;
                            case "pdf":
                                fileType = "application/pdf";
                                break;
                        }
                        if (fileType == "image/jpg" || fileType == "image/png" || fileType == "image/gif")
                        {


                            fname = Path.Combine(Server.MapPath("~/Gallery/"), fname);
                            file.SaveAs(fname);
                            // DB code you can remove it 
                            Gallery gal = new Gallery();
                            gal.Name = fileName;
                            gal.Path = fname;
                          
                            gal.Extension = extensioin;
                            db.Galleries.Add(gal);
                            db.SaveChanges();
                        }
                     
                      

                

                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
    
        [HttpPost]
      public ActionResult Addbookingconfirm(booking b)
      {
          if (ModelState.IsValid)
          {
              db.bookings.Add(b);
              db.SaveChanges();
              return RedirectToAction("TripBooking");
          }
          return View("Addbooking");
      }
        public ActionResult updatebooking(int id)
        {
            booking p = db.bookings.Find(id);
            return View(p);
        }
        public ActionResult updatebookingconfirm(int id)
        {
            booking p = db.bookings.Find(id);
            String name = Request["t1"];
            String ph = Request["t2"];
            String email = Request["t3"];
            String address = Request["t4"];
            String cnic = Request["t5"];
            String noofpeople = Request["t6"];
            String place = Request["t7"];
            String days = Request["t8"];
            String hotels = Request["t9"];
            p.Name = name;
            p.Phonenumber = ph;
            p.Email = email;
            p.Address = address;
            p.Cinc = cnic;
            p.Noofpeoples = noofpeople;
            p.place = place;
            p.Daysfortrip = days;
            p.Hoteltostay = hotels;

       
            db.SaveChanges();

            return RedirectToAction("TripBooking");
        }
        public ActionResult MainPage()
        {
            return View();
        }
        public ActionResult updateplace(int id)
        {
            Place p = db.Places.Find(id);
            return View(p);
        }
        [HttpPost]
        public ActionResult updateplaceconfirm(int id)
        {
            Place p = db.Places.Find(id);
            String placename = Request["t1"];
            String placeloc = Request["t2"];
            String placedesc = Request["t3"];
            String placeimage = Request["t4"];
            p.placename= placename;
            p.placelocation= placeloc;
            p.placedescription = placedesc;
            p.placeimage= placeimage;
            db.SaveChanges();

            return RedirectToAction("showplace");
        }
        public ActionResult showplace()
        {
            return View(db.Places.ToList());
        }
        public ActionResult addplace()
        {
            return View();
        }
        
        public ActionResult addplaceconfirm(Place p)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    db.Places.Add(p);
                    db.SaveChanges();
                    return RedirectToAction("showplace");
                }
            }
            catch(Exception ex)
            {
                return Json("Error occurred. Error details: " + ex.Message);
            }


            return Json("Code Run Throgh Jason");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Loginconfirm(signup s)
        {
            
            
            return RedirectToAction("index");
        }

       
        public ActionResult signupform()
        {
            return View();
        }
      
        public ActionResult AdminMainPage()
        {

            return View();
        }
        [Authorize]
        public ActionResult gallery()
        {
            return View(db.Places.ToList());
        }
        public ActionResult signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult signupconfirm(signup s)
        {
            db.signups.Add(s);
            db.SaveChanges();
            return RedirectToAction("index");

        }
        public ActionResult showpassenger()
        {

             
            return View(db.Passengers.ToList());
        }
        public ActionResult addpassenger()
        {
            return View();
        }
        
        
        public ActionResult addpassengerconfirm(Passenger p)
        {
            if (ModelState.IsValid)
            {
                db.Passengers.Add(p);
                db.SaveChanges();
                return RedirectToAction("showpassenger");
            }
            return View("addpassenger");
        }

        public ActionResult updatepassenger(int id)
        {
            Passenger p = db.Passengers.Find(id);
            return View(p);
        }
        [HttpPost]
        public ActionResult updatepassengerconfirm(int id)
        {
            Passenger p = db.Passengers.Find(id);
            String name = Request["t1"];
            String email = Request["t2"];
            String ph = Request["t3"];
            String add = Request["t4"];
            p.phonenumber = ph;
            p.address = add;
            p.email = email;
            p.name = name;
            db.SaveChanges();

            return RedirectToAction("showpassenger");

        }
       
        public ActionResult deleteplace(int id)
        {
            Place p = db.Places.Find(id);
            db.Places.Remove(p);
            db.SaveChanges();

            return RedirectToAction("showplace");
        }
        public ActionResult deletepassenger(int id)
        {
            Passenger p = db.Passengers.Find(id);
            db.Passengers.Remove(p);
            db.SaveChanges();

            return RedirectToAction("showpassenger");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewData["page"] = "About";
            placeabout p = new placeabout();
            p.places = new places();
            p.places.name = "Tours And Trip Of Pakistan";

            p.places.info = "Cold Weather , Mountains, Hotels, Roads, Beautifull Places Golden chance in a very less amount of money . Move to the world with our website";
            p.Muree = new Muree();

            p.Muree.name = "Muree";
            p.Muree.info = "Cold Weather , Mountains, Hotels, Roads, Beautifull Places Golden chance in a very less amount of money . Move to the world with our website";
            p.islamabad = new islamabad();
            p.islamabad.name = "Islamabad";
            p.islamabad.info = "Cold Weather , Mountains, Hotels, Roads, Beautifull Places Golden chance in a very less amount of money . Move to the world with our website";
            p.rawalpindi = new rawalpindi();
            p.rawalpindi.name = "rawalpindi";
            p.rawalpindi.info = "Cold Weather , Mountains, Hotels, Roads, Beautifull Places Golden chance in a very less amount of money . Move to the world with our website";
            p.nathiagali = new nathiaGali();
            p.nathiagali.name = "NathiaGali";
            p.nathiagali.info = "Cold Weather , Mountains, Hotels, Roads, Beautifull Places Golden chance in a very less amount of money . Move to the world with our website";



            Place place = new Place();
            place.placename = "Muree";
            place.placedescription = "Cold Weather , Mountains, Hotels, Roads, Beautifull Places Golden chance in a very less amount of money . Move to the world with our website";
            place.placelocation = "Pakistan";
            place.placeimage = "~/images/6.jpg";
            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}