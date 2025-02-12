using apiUsuariosCuenta.contexts;
using apiUsuariosCuenta.dtos;
using apiUsuariosCuenta.entities;
using Microsoft.EntityFrameworkCore;

namespace apiUsuariosCuenta.repositories;


public class UserRepository: IRepository<Usuario>
{
  private readonly BankContexts _context;

  public UserRepository(BankContexts context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Usuario>> GetAll()
  {
    return await _context.Usuarios.ToListAsync();
  }

  public async Task<Usuario?> GetById(Guid id)
  {
    return await _context.Usuarios.FindAsync(id);
  }

  public async Task<bool> Delete(Guid id)
  {
    var u = await _context.Usuarios.FindAsync(id);
    if (u != null)
    {
      _context.Usuarios.Remove(u);
      await _context.SaveChangesAsync();
      return true;
    }
    return false;
  }

  public async Task<Usuario> Add(Usuario entity)
  {
    await _context.Usuarios.AddAsync(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<Usuario> Update(Usuario entity)
  {
    _context.Usuarios.Update(entity);
    await _context.SaveChangesAsync();
    return entity;
  }
  
  public async Task<IEnumerable<ImagesDto>> getUsersImages()
  {
    var users = await _context.Usuarios.ToListAsync();
    var images = new List<ImagesDto>();
    
    foreach (var u in users)
    {
      var imageDto = new ImagesDto();
      string basePath = "/home/deidamia/PycharmProjects/FaceRecognition/CheckedImages/";
      string picturePath = "/home/deidamia/PycharmProjects/FaceRecognition/imagesToCheck/" + u.UserId;
        
      string imagePath = Directory.EnumerateFiles(basePath, u.UserId + ".*")
        .FirstOrDefault(f => f.EndsWith(".png") || f.EndsWith(".jpg"));
        
      if (imagePath != null)
      {
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        string base64String = Convert.ToBase64String(imageBytes);
            
        imageDto.Picture = base64String;
        imageDto.PicturePath = picturePath;
        imageDto.UserID = u.UserId;
            
        images.Add(imageDto);
      }
    }
    
    return images;
  }

}