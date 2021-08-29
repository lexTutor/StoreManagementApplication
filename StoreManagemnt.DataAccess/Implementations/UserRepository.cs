using StoreManagement.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace StoreManagemnt.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly FileInfo fileInfo;
        private const string File_Path = "../Users.txt";

        public UserRepository()
        {
            fileInfo = new FileInfo(File_Path);
        }

        public async Task<bool> Add(User user)
        {
            try
            {
                if (!fileInfo.Exists)
                {
                    StreamWriter sw = fileInfo.CreateText();
                    await sw.DisposeAsync();
                }

                using (StreamWriter writer = fileInfo.AppendText())
                {
                    await writer.WriteLineAsync(
                         string.Format("{0},{1},{2},{3},{4}",
                         user.Id, user.Email, user.FirstName, user.LastName, user.Password));

                    await writer.DisposeAsync();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<User> Login(string email, string password)
        {
            try
            {
                if (!fileInfo.Exists)
                {
                    throw new ArgumentNullException(nameof(fileInfo));
                }

                using (StreamReader reader = fileInfo.OpenText())
                {
                    var data = await reader.ReadToEndAsync();
                    string[] users = data.Split(Environment.NewLine);
                    foreach (var user in users)
                    {
                        var userData = user.Split(',');
                        if (userData[1] == email && userData[4] == password)
                        {
                            return new User
                            {
                                Id = userData[0],
                                Email = userData[1],
                                FirstName = userData[2],
                                LastName = userData[3],
                                Password = userData[4]
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            throw new UnauthorizedAccessException("Invalid credentials");
        }
    }
}
