namespace GameForum1.DAL
{
    public class Data
    {

        public static async Task<string> GenerateNickNameAsync()
        {
            var value = Random.Shared.Next(10);
            string nickName = string.Empty;

            var colors = "Yellow,Blue,Orange,Green,Red,Black,Purple,Grey,White,Pink".Split(',');
            nickName = colors[value];

            var dishes = "Spaghetti,Meatball,Pie,Potato,Maccaroni,Pizza,Broccoli,Ketchup,Cupcake,LemonChicken".Split(',');
            nickName += dishes[value];

            nickName += (value + 1) + (value + 1) + (value + 1) + (value + 1);

            return nickName;
        }
    }
}
