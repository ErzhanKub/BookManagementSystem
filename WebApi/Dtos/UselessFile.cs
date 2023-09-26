namespace WebApi.Dtos
{
    public static class UselessFile
    {
        public static string Gentleman(string input)
        {
            string output = null!;
            if (input == "erzhan")
                output = "https://github.com/ErzhanKub";
            return output!;
        }

        public static string NeverGiveUp(string input)
        {
            string output = null!;

            if (input == "nevergiveup")
                output = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";

            return output!;
        }
    }
}
