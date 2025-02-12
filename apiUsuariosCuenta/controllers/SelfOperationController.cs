using apiUsuariosCuenta.authorization.concretes;
using apiUsuariosCuenta.Authorization.interfaces;
using apiUsuariosCuenta.entities;
using apiUsuariosCuenta.services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IAuthorizationHandler = apiUsuariosCuenta.Authorization.interfaces.IAuthorizationHandler;

namespace apiUsuariosCuenta.controllers;

[ApiController]
[Route("api/[controller]")]
public class SelfOperationController : ControllerBase
{
  private readonly ISelfOperationService _service;
  private readonly IAuthorizationHandler _authorizationHandler;
  private readonly IUserContext _userContext;
  
  public SelfOperationController(ISelfOperationService service, IUserContext userContext, IAuthorizationHandler authorizationHandler)
  {
    _service = service;
    _userContext = userContext;
    _authorizationHandler = authorizationHandler;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<SelfOperation>>> GetAll()
  {
    var entities = await _service.GetAll();
    return Ok(entities);
  }
  
  [HttpGet("getFromUser")]
  public async Task<ActionResult<IEnumerable<SelfOperation>>> GetFromUser(Guid id, CancellationToken cancellationToken)
  {
    string? jwt = _userContext.Jwt;
    
    if (jwt == null)
    {
      return Unauthorized("You are not authenticated");
    }

    bool hasPermitions = await _authorizationHandler.HandleAsync(id, new Permitions(Resource.Users, Operation.Read), cancellationToken);

    if (!hasPermitions)
    {
      return Unauthorized("You are not allowed to read this transaction");
    }
    var transactions = await _service.GetByUserID(id);
    return Ok(transactions);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<SelfOperation>> Get(Guid id)
  {
    var entity = await _service.GetById(id);
    if (entity == null)
      return NotFound();
    return Ok(entity);
  }
  
  [HttpPost]
  public async Task<ActionResult> Post(SelfOperation entity, CancellationToken cancellationToken)
  {
    string? jwt = _userContext.Jwt;
    
    if (jwt == null)
    {
      return Unauthorized("You are not authenticated");
    }

    bool hasPermitions = await _authorizationHandler.HandleAsync(entity.BankAcountID ?? Guid.Empty, new Permitions(Resource.Users, Operation.Read), cancellationToken);

    if (!hasPermitions)
    {
      return Unauthorized("You are not allowed to read this transaction");
    }
    
    await _service.Add(entity);
    return CreatedAtAction(nameof(Get), new { id = entity.SelfOperationID }, entity);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult> Put(Guid id, SelfOperation entity)
  {
    if (id != entity.SelfOperationID)
      return BadRequest();

    var updated = await _service.Put(entity);
    if (updated == null)
      return NotFound();
    return NoContent();
  }

  [HttpPatch("{id}")]
  public async Task<ActionResult> Patch(Guid id, [FromBody] SelfOperation entity)
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