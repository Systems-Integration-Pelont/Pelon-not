using apiUsuariosCuenta.entities;
using apiUsuariosCuenta.services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace apiUsuariosCuenta.controllers;

[ApiController]
[Route("api/[controller]")]

public class RolControler : ControllerBase
{
  private readonly IRolService _service;

  public RolControler(IRolService service)
  {
    _service = service;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Rol>>> GetAll()
  {
    var entities = await _service.GetAll();
    return Ok(entities);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Rol>> Get(Guid id)
  {
    var entity = await _service.GetById(id);
    if (entity == null)
      return NotFound();
    return Ok(entity);
  }

  [HttpPost]
  public async Task<ActionResult> Post(Rol entity)
  {
    await _service.Add(entity);
    return CreatedAtAction(nameof(Get), new { id = entity.RolId }, entity);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult> Put(Guid id, Rol entity)
  {
    if (id != entity.RolId)
      return BadRequest();

    var updated = await _service.Put(entity);
    if (updated == null)
      return NotFound();
    return NoContent();
  }

  [HttpPatch("{id}")]
  public async Task<ActionResult> Patch(Guid id, [FromBody] Rol entity)
  {
    var updated = await _service.Patch(id, entity);
    if (updated == null)
      return NotFound();
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> Delete(Guid id)
  {
    var deleted = await _service.Delete(id);
    if (!deleted)
      return NotFound();
    return NoContent();
  }
}