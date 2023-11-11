using Business.Repositories.Messages;
using Business.Repositories.Service;
using Business.Utilities.File;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Model;
using DataAccess.Repositories.Contract;
using Entities.DTO;
using Entities.Models;

namespace Business.Repositories.Manager
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _context;
        private readonly IFileService _fileService;

        public UserManager(IUserRepository context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        
        public async Task Add(RegisterAuthDto registerDto)
        {
            string fileName = _fileService.FileSaveToServer(registerDto.Image, "./Content/Img/");
            //string fileName = _fileService.FileSaveToFtp(registerDto.Image);
            //byte[] fileByteArray = _fileService.FileConvertByteArrayToDatabase(registerDto.Image);

            var user = CreateUser(registerDto,fileName);

            await _context.Add(user);
        }

        private User CreateUser(RegisterAuthDto registerDto,string fileName)
        {
            User user = new User();
            user.Id = 0;
            user.Email = registerDto.Email;
            user.Name = registerDto.Name;
            user.Password = registerDto.Password;
            user.ImageUrl = fileName;
            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            var result = await _context.Get(p => p.Email == email);
            return result;
        }

        public async Task<IResult> Update(User user)
        {
            await _context.Update(user);
            return new SuccessResult(UserMessages.UpdatedUser);
        }

        public async Task<IResult> Delete(User user)
        {
            await _context.Delete(user);
            return new SuccessResult(UserMessages.DeletedUser);
        }

        public async Task<IDataResult<List<User>>> GetList()
        {
            return new SuccessDataResult<List<User>>(await _context.GetAll());
        }

        public async Task<IDataResult<User>> GetById(int id)
        {
            return new SuccessDataResult<User>(await _context.Get(p => p.Id == id));
        }

        public async Task<User> GetByIdForAuth(int id)
        {
            return await _context.Get(p => p.Id == id);
        }

        public async Task<List<OperationClaim>> GetUserOperationClaims(int userId)
        {
            return await _context.GetUserOperatinonClaims(userId);
        }
    }
}
