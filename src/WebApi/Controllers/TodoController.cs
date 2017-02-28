using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository) {
            this._todoRepository = todoRepository;
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll() {
            return _todoRepository.GetAll();
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(string id) {
            var item = _todoRepository.Find(id);
            if(item == null) {
                return NotFound();
            }

            return new ObjectResult(item);
        }
        
        [HttpPost]
        [Route("Create")]
        public IActionResult Create(TodoItem item) {

            if (item == null) {
                return BadRequest();
            }

            _todoRepository.Add(item);

            return RedirectToAction("GetById", "ToDo", new {id = item.Key});
        }
    }


}