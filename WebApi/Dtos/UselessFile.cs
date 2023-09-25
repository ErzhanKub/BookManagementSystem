using Application.Users.Commands;

namespace WebApi.Dtos
{
    public static class UselessFile
    {
        public static string Gentleman(string input)
        {
            string result = null!;
            if (input == "erzhan")
                result = "https://github.com/ErzhanKub";
            return result!;
        }

        public static string NeverGiveUp(string input)
        {
            string result = null!;

            if (input == "nevergiveup")
                result = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";

            return result!;
        }
    }
}
