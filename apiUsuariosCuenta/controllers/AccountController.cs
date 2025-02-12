using apiUsuariosCuenta.entities;
using apiUsuariosCuenta.services.Interfaces;

namespace apiUsuariosCuenta.controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
  private readonly IAccountService _service;
  
  public AccountController(IAccountService service)
  {
    _service = service;
  }
  
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Cuenta>>> GetAll()
  {
    var entities = await _service.GetAll();
    return Ok(entities);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Cuenta>> Get(Guid id)
  {
    var entity = await _service.GetById(id);
    if (entity == null)
      return NotFound();
    return Ok(entity);
  }

  [HttpPost]
  public async Task<ActionResult> Post(Cuenta entity)
  {
    await _service.Add(entity);
    return CreatedAtAction(nameof(Get), new { id = entity.AccountId }, entity);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult> Put(Guid id, Cuenta entity)
  {
    if (id != entity.AccountId)
      return BadRequest();

    var updated = await _service.Put(entity);
    if (updated == null)
      return NotFound();
    return NoContent();
  }

  [HttpPatch("{id}")]
  public async Task<ActionResult> Patch(Guid id, [FromBody] Cuenta entity)
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