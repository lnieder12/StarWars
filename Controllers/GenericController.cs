using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using StarWars.Model;
using StarWars.Service;

namespace StarWars.Controllers;

public class GenericController<T> : ControllerBase where T : class
{
    protected StarWarsDbContext Context;

    protected Service<T> Service;

    public GenericController(StarWarsDbContext context)
    {
        Context = context;
        Service = new Service<T>(context);
    }

    // GET
    [HttpGet]
    public virtual ActionResult<List<T>> GetAll()
    {
        return Service.GetAll();
    }


    // GET
    [HttpGet("{id}")]
    public virtual ActionResult<T> Get(int id)
    {
        var item = Service.Get(id);
        if(item == null)
        {
            return NotFound();
        }
        return item;
    }


    // PATCH
    [HttpPatch("{id}")]
    public ActionResult<T> Patch(int id, [FromBody] JsonPatchDocument<T> patchDocument)
    {
        var item = Service.Patch(id, patchDocument);
        if(item == null)
        {
            return NotFound();
        }
        return item;
    }


    // POST
    [HttpPost]
    public virtual ActionResult<T> Add(T obj)
    {
        var item = Service.Add(obj);
        if(item == null)
        {
            return NotFound();
        }
        return item; 
    }


    // DELETE
    [HttpDelete("{id}")]
    public ActionResult<bool> Delete(int id)
    {
        if(Service.Delete(id))
            return true;
        return NotFound();
    }

    [HttpGet("page")]
    public ActionResult<List<T>> GetPage(int skip, int pageSize)
    {
        var items = Service.GetPage(skip, pageSize);
        if (items == null)
        {
            return BadRequest();
        }
        return items;
    }
}
