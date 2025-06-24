using EnglishWeb.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace EnglishWeb.Controllers
{
    public class HistoryController : Controller
    {
        dbEnglishDataContext db = new dbEnglishDataContext();

        // GET: History
        public ActionResult Index()
        {
            // Kiểm tra nếu chưa đăng nhập
            if (Session["User"] == null)
            {
                TempData["Message"] = "⚠️ Please login to view your flashcard history.";
                return RedirectToAction("Login", "User");
            }

            // Lấy thông tin người dùng từ session
            var user = (User)Session["User"];
            int userId = user.UserId;

            // Truy vấn danh sách các từ đã thêm
            var history = db.UserVocabularyHistories
                            .Where(h => h.UserId == userId)
                            .OrderByDescending(h => h.NextReview)
                            .ToList();

            return View(history);
        }
    }
}
