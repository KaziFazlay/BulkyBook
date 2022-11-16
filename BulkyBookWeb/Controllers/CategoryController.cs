
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        public readonly ICategoryRepository _db;
        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objlist = _db.GetAll();
            return View(objlist);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name cannot match with Display Name");
            }
            if (ModelState.IsValid)
            {
                _db.Add(category);
                _db.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            if (id == null & id == 0)
            {
                return NotFound();
            }
            var obj = _db.GetFirstOrDefault(u=>u.Id==id);
            if(obj==null)
            {
                return NotFound(); 
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name cannot match with Display Name");
            }
            if (ModelState.IsValid)
            {
                _db.Update(category);
                _db.Save();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            if (id == null & id == 0)
            {
                return NotFound();
            }
            var obj = _db.GetFirstOrDefault(u=>u.Id==id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
             _db.Remove(obj);
             _db.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
         
        }
    }
}
