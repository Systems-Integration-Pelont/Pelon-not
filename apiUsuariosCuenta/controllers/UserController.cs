using apiUsuariosCuenta.authorization.concretes;
using apiUsuariosCuenta.Authorization.interfaces;
using apiUsuariosCuenta.dtos;
using apiUsuariosCuenta.entities;
using apiUsuariosCuenta.services.Interfaces;

namespace apiUsuariosCuenta.controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
  private readonly IUserService _service;
  private readonly IATMInterUserTransactionService _atmInterUserTransactionService;
  private readonly ISelfATMOperationService _selfAtmOperationService;
  private readonly ISelfOperationService _selfOperationService;
  private readonly IInterUserTransactionService _interUserTransactionService;
  private readonly IAuthorizationHandler _authorizationHandler;
  private readonly IUserContext _userContext;

  public UserController(IUserService service, IATMInterUserTransactionService atmInterUserTransactionService, ISelfATMOperationService selfAtmOperationService, ISelfOperationService selfOperationService, IInterUserTransactionService interUserTransactionService, IAuthorizationHandler authorizationHandler, IUserContext userContext)
  {
    _service = service;
    _atmInterUserTransactionService = atmInterUserTransactionService;
    _selfAtmOperationService = selfAtmOperationService;
    _selfOperationService = selfOperationService;
    _interUserTransactionService = interUserTransactionService;
    _authorizationHandler = authorizationHandler;
    _userContext = userContext;
  }
  
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
  {
    var entities = await _service.GetAll();
    return Ok(entities);
  }
  
  [HttpGet("GetTransactionsFromUser")]
  public async Task<ActionResult<object>> GetAllTransactionsFromUser(Guid id, CancellationToken cancellationToken)
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

    
    var selfOperations = await _selfOperationService.GetByUserID(id);
    var selfAtmOperations = await _selfAtmOperationService.GetByUserID(id);
    var atmInterUserTransactions = await _atmInterUserTransactionService.GetByUserID(id);
    var interUserTransactions = await _interUserTransactionService.GetByUserID(id);

    var transactions = new
    {
      SelfOperations = selfOperations.Select(s => new
      {
        s.SelfOperationID,
        s.BankAcountID,
        s.Amount,
        s.DateTime,
      }),
      SelfATMOperations = selfAtmOperations.Select(s => new
      {
        s.SelfATMOperationID,
        s.BankAcountID,
        s.ATMID,
        s.Amount,
        s.DateTime,
      }),
      ATMInterUserTransactions = atmInterUserTransactions.Select(s => new
      {
        s.ATMInterUserTransactionID,
        s.FromBankAccountID,
        s.ToBankAccountID,
        s.ATMID,
        s.Amount,
        s.DateTime,
      }),
      InterUserTransactions = interUserTransactions.Select(s => new
      {
        s.InterUserTransactionID,
        s.FromBankAccountID,
        s.ToBankAccountID,
        s.Amount,
        s.DateTime,
      })
    };

    return Ok(transactions);
  }
  
  [HttpGet]
  [Route("images")]
  public async Task<ActionResult<IEnumerable<ImagesDto>>> GetImages()
  {
    var entities = await _service.GetImages();
    return Ok(entities);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Usuario>> Get(Guid id, CancellationToken cancellationToken)
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
    var entity = await _service.GetById(id);
    if (entity == null)
      return NotFound();
    return Ok(entity);
  }

  [HttpPost]
  public async Task<ActionResult> Post(Usuario entity)
  {
    await _service.Add(entity);
    return CreatedAtAction(nameof(Get), new { id = entity.UserId }, entity);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult> Put(Guid id, Usuario entity, CancellationToken cancellationToken)
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
    if (id != entity.UserId)
      return BadRequest();

    var updated = await _service.Put(entity);
    if (updated == null)
      return NotFound();
    return NoContent();
  }

  [HttpPatch("{id}")]
  public async Task<ActionResult> Patch(Guid id, [FromBody] Usuario entity, CancellationToken cancellationToken)
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