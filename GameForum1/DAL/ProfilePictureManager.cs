using Microsoft.AspNetCore.Identity;

namespace GameForum1.DAL
{
    public class ProfilePictureManager
    {
        private readonly GameForum1Context _context;
        public ProfilePictureManager(GameForum1Context context)
        {
            _context = context;
        }
        public async Task<List<AppFile>> GetProfilePictures()
        {
            var allPictures = await _context.File.ToListAsync();

            return allPictures;
        }

        public async Task<string> SetProfilePicture(string userId)
        {
            var imageSrc = string.Empty;

            var profilePictures = await GetProfilePictures();

            foreach (var picture in profilePictures)
            {
                if (userId == picture.UserId)
                {
                    var imgContent = picture.Content;
                    imageSrc = string.Format("data:{0};base64,{1}", "jpg", Convert.ToBase64String(imgContent));
                }
            }
            if (imageSrc == null || imageSrc == string.Empty)
            {
                imageSrc = "../img/defaultProfile.jpg";
            }
            return imageSrc;
        }
    }
}
